using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using com.example.Controls.Wpf.Display;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// Data grid with a styled header row, alternating row colors, and row
    /// hover / selection highlighting.
    /// </summary>
    public partial class ModernDataGridControl : UserControl
    {
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

        public ModernDataGridControl()
        {
            this.InitializeComponent();
            this.InnerDataGrid.Sorting += this.InnerDataGrid_Sorting;
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
                DataGridColumn column = definition.IsBadge
                    ? BuildBadgeColumn(definition)
                    : BuildTextColumn(definition);
                column.Header = definition.Header;
                column.Width = ParseWidth(definition.Width);
                this.InnerDataGrid.Columns.Add(column);
            }
        }

        private static DataGridColumn BuildTextColumn(ModernDataGridColumn definition)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            column.Binding = new Binding(definition.Binding);
            column.SortMemberPath = definition.Binding;
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
