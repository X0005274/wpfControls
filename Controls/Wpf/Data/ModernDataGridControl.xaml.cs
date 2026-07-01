using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using com.example.Controls.Wpf.Display;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// Data grid with a styled header row, alternating row colors, and row
    /// hover / selection highlighting.
    /// </summary>
    public partial class ModernDataGridControl : UserControl
    {
        private static readonly ValueToneBrushConverter ToneConverter = new ValueToneBrushConverter();

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ModernDataGridControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ModernDataGridControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(ModernDataGridControl),
                new PropertyMetadata(true));

        /// <summary>
        /// Raised when a row is right-clicked. The row under the cursor is selected
        /// first (so <see cref="SelectedItem"/> is up to date), then this fires — a host
        /// can show a context menu at the cursor in the handler.
        /// </summary>
        public event EventHandler RowRightClicked;

        public ModernDataGridControl()
        {
            this.InitializeComponent();
            this.InnerDataGrid.Sorting += this.InnerDataGrid_Sorting;
            this.InnerDataGrid.PreviewMouseRightButtonUp += this.InnerDataGrid_PreviewMouseRightButtonUp;
        }

        /// <summary>
        /// Selects the row under a right-click and raises <see cref="RowRightClicked"/>.
        /// Right-clicking empty space (no row) is ignored.
        /// </summary>
        private void InnerDataGrid_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject node = e.OriginalSource as DependencyObject;
            while (node != null && !(node is DataGridRow))
            {
                node = VisualTreeHelper.GetParent(node);
            }

            DataGridRow row = node as DataGridRow;
            if (row == null)
            {
                return;
            }

            row.IsSelected = true;
            this.SelectedItem = row.Item;

            EventHandler handler = this.RowRightClicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Suppresses header-click sorting when the grid has no rows, so an empty grid
        /// neither reorders nor shows a sort indicator on the headers.
        /// </summary>
        private void InnerDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            if (this.InnerDataGrid.Items.Count == 0)
            {
                e.Handled = true;
            }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)this.GetValue(IsReadOnlyProperty); }
            set { this.SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Replaces the grid's columns with the given definitions. Text columns and
        /// badge columns (rendered with <c>ModernBadgeControl</c>) are supported.
        /// Passing null or an empty set restores automatic column generation.
        /// </summary>
        public void SetColumns(IEnumerable<ModernDataGridColumn> definitions)
        {
            this.InnerDataGrid.Columns.Clear();

            if (definitions == null)
            {
                this.InnerDataGrid.AutoGenerateColumns = true;
                return;
            }

            List<ModernDataGridColumn> list = new List<ModernDataGridColumn>(definitions);
            if (list.Count == 0)
            {
                this.InnerDataGrid.AutoGenerateColumns = true;
                return;
            }

            this.InnerDataGrid.AutoGenerateColumns = false;
            foreach (ModernDataGridColumn definition in list)
            {
                DataGridColumn column;
                if (definition.IsCombo)
                {
                    column = this.BuildComboColumn(definition);
                }
                else if (definition.IsBadge)
                {
                    column = BuildBadgeColumn(definition);
                }
                else
                {
                    column = BuildTextColumn(definition);
                }

                column.Header = definition.Header;
                column.Width = ParseWidth(definition.Width);
                this.InnerDataGrid.Columns.Add(column);
            }
        }

        /// <summary>
        /// Builds an editable combo column: an always-live <see cref="ComboBox"/> in the
        /// cell (single click opens it) two-way bound to the column's
        /// <see cref="ModernDataGridColumn.Binding"/> path. When
        /// <see cref="ModernDataGridColumn.ConditionPath"/> matches
        /// <see cref="ModernDataGridColumn.ConditionValue"/> the combo is hidden and a
        /// read-only dash is shown instead; when <see cref="ModernDataGridColumn.ValueTones"/>
        /// is set the selected text is tinted by value.
        /// </summary>
        private DataGridColumn BuildComboColumn(ModernDataGridColumn definition)
        {
            FrameworkElementFactory combo = new FrameworkElementFactory(typeof(ComboBox), "PART_Combo");
            combo.SetValue(ComboBox.ItemsSourceProperty, definition.ComboOptions);

            Binding selection = new Binding(definition.Binding);
            selection.Mode = BindingMode.TwoWay;
            selection.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            combo.SetBinding(ComboBox.SelectedItemProperty, selection);

            object comboStyle = this.TryFindResource("GridEditComboStyle");
            if (comboStyle is Style)
            {
                combo.SetValue(FrameworkElement.StyleProperty, comboStyle);
            }

            if (definition.ValueTones != null)
            {
                Binding foreground = new Binding(definition.Binding);
                foreground.Converter = ToneConverter;
                foreground.ConverterParameter = definition.ValueTones;
                combo.SetBinding(ComboBox.ForegroundProperty, foreground);
            }

            DataTemplate template = new DataTemplate();

            if (string.IsNullOrEmpty(definition.ConditionPath))
            {
                template.VisualTree = combo;
            }
            else
            {
                FrameworkElementFactory dash = new FrameworkElementFactory(typeof(TextBlock), "PART_Dash");
                dash.SetValue(TextBlock.TextProperty, "-");
                dash.SetValue(TextBlock.FontSizeProperty, 13.0);
                dash.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
                dash.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                dash.SetValue(UIElement.VisibilityProperty, Visibility.Collapsed);
                object secondary = this.TryFindResource("Brush.TextSecondary");
                if (secondary is Brush)
                {
                    dash.SetValue(TextBlock.ForegroundProperty, secondary);
                }

                FrameworkElementFactory grid = new FrameworkElementFactory(typeof(Grid));
                grid.AppendChild(combo);
                grid.AppendChild(dash);
                template.VisualTree = grid;

                DataTrigger trigger = new DataTrigger();
                trigger.Binding = new Binding(definition.ConditionPath);
                trigger.Value = definition.ConditionValue;
                trigger.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed, "PART_Combo"));
                trigger.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Visible, "PART_Dash"));
                template.Triggers.Add(trigger);
            }

            DataGridTemplateColumn column = new DataGridTemplateColumn();
            column.CellTemplate = template;
            column.SortMemberPath = definition.Binding;
            column.CanUserSort = true;
            return column;
        }

        private static DataGridColumn BuildTextColumn(ModernDataGridColumn definition)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Binding = new Binding(definition.Binding);
            column.SortMemberPath = definition.Binding;
            if (definition.CenterText)
            {
                Style style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
                column.ElementStyle = style;
            }

            return column;
        }

        private static DataGridColumn BuildBadgeColumn(ModernDataGridColumn definition)
        {
            FrameworkElementFactory badge = new FrameworkElementFactory(typeof(ModernBadgeControl));
            badge.SetBinding(ModernBadgeControl.BadgeTextProperty, new Binding(definition.Binding));
            badge.SetBinding(ModernBadgeControl.BadgeTypeProperty, new Binding(definition.TonePath));
            badge.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            badge.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);

            DataTemplate template = new DataTemplate();
            template.VisualTree = badge;

            DataGridTemplateColumn column = new DataGridTemplateColumn();
            column.CellTemplate = template;
            // Template columns don't auto-sort; sort by the badge's label path.
            column.SortMemberPath = definition.Binding;
            column.CanUserSort = true;
            return column;
        }

        private static DataGridLength ParseWidth(string width)
        {
            if (string.IsNullOrEmpty(width))
            {
                return new DataGridLength(1, DataGridLengthUnitType.Auto);
            }

            if (width == "*")
            {
                return new DataGridLength(1, DataGridLengthUnitType.Star);
            }

            if (width.EndsWith("*"))
            {
                string factorText = width.Substring(0, width.Length - 1);
                double factor;
                if (double.TryParse(factorText, out factor))
                {
                    return new DataGridLength(factor, DataGridLengthUnitType.Star);
                }

                return new DataGridLength(1, DataGridLengthUnitType.Star);
            }

            if (string.Equals(width, "Auto", StringComparison.OrdinalIgnoreCase))
            {
                return new DataGridLength(1, DataGridLengthUnitType.Auto);
            }

            double pixels;
            if (double.TryParse(width, out pixels))
            {
                return new DataGridLength(pixels);
            }

            return new DataGridLength(1, DataGridLengthUnitType.Auto);
        }
    }
}
