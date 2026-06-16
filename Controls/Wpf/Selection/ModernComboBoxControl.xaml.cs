using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Styled dropdown with rounded corners, custom arrow, and hover/selected
    /// item states. The control is an editable, searchable auto-suggest combo:
    /// typing opens the dropdown and filters the items in real time
    /// (case-insensitive, StartsWith or Contains). DisplayMemberPath /
    /// SelectedValuePath / SelectedItem / SelectedValue binding is preserved.
    /// </summary>
    public partial class ModernComboBoxControl : UserControl
    {
        private ICollectionView itemsView;
        private string currentFilterText;
        private bool suppressTextChanged;
        private TextBox editableTextBox;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ModernComboBoxControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(ModernComboBoxControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(
                "SelectedValuePath",
                typeof(string),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired",
                typeof(bool),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(true));

        public static readonly DependencyProperty FilterModeProperty =
            DependencyProperty.Register(
                "FilterMode",
                typeof(ComboBoxFilterMode),
                typeof(ModernComboBoxControl),
                new PropertyMetadata(ComboBoxFilterMode.StartsWith, OnFilterModeChanged));

        public ModernComboBoxControl()
        {
            this.currentFilterText = string.Empty;

            this.InitializeComponent();

            // Catch the editable text box's TextChanged as it bubbles up to the ComboBox.
            this.InnerComboBox.AddHandler(
                TextBoxBase.TextChangedEvent,
                new TextChangedEventHandler(this.InnerComboBox_TextChanged));

            this.Loaded += this.OnLoaded;
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
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

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        public string DisplayMemberPath
        {
            get { return (string)this.GetValue(DisplayMemberPathProperty); }
            set { this.SetValue(DisplayMemberPathProperty, value); }
        }

        public string SelectedValuePath
        {
            get { return (string)this.GetValue(SelectedValuePathProperty); }
            set { this.SetValue(SelectedValuePathProperty, value); }
        }

        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets how the typed text is matched against item display text.
        /// </summary>
        public ComboBoxFilterMode FilterMode
        {
            get { return (ComboBoxFilterMode)this.GetValue(FilterModeProperty); }
            set { this.SetValue(FilterModeProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernComboBoxControl control = (ModernComboBoxControl)d;
            // A new list means the previous filter text no longer applies.
            control.currentFilterText = string.Empty;
            control.AttachView();
        }

        private static void OnFilterModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernComboBoxControl control = (ModernComboBoxControl)d;
            control.RefreshFilter();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.AttachView();

            // Resolve the editable text box so the typed text can be restored if the
            // ComboBox wipes it when the selected item is filtered out (see below).
            if (this.editableTextBox == null && this.InnerComboBox.Template != null)
            {
                this.editableTextBox =
                    this.InnerComboBox.Template.FindName("PART_EditableTextBox", this.InnerComboBox) as TextBox;
            }
        }

        private void InnerComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ignore text changes that the control itself causes while applying the
            // filter (the ComboBox resets its editor text when the selected item is
            // filtered out of the view).
            if (this.suppressTextChanged)
            {
                return;
            }

            string text = this.InnerComboBox.Text ?? string.Empty;

            // Ignore the text change that merely echoes the current selection
            // (i.e. the user picked an item). This prevents the dropdown from
            // reopening after a selection while still treating real edits as searches.
            object selected = this.InnerComboBox.SelectedItem;
            if (selected != null &&
                string.Equals(text, this.GetDisplayText(selected), StringComparison.Ordinal))
            {
                this.currentFilterText = text;
                return;
            }

            this.currentFilterText = text;

            this.suppressTextChanged = true;
            try
            {
                this.ApplyFilter();

                // If filtering removed the previously selected item, the ComboBox
                // resets the editor text to empty; restore the in-progress search text.
                if ((this.InnerComboBox.Text ?? string.Empty) != text)
                {
                    this.InnerComboBox.Text = text;
                    if (this.editableTextBox != null)
                    {
                        this.editableTextBox.CaretIndex = text.Length;
                    }
                }
            }
            finally
            {
                this.suppressTextChanged = false;
            }

            // Open the dropdown automatically as the user types.
            if (!this.InnerComboBox.IsDropDownOpen)
            {
                this.InnerComboBox.IsDropDownOpen = true;
            }
        }

        /// <summary>
        /// Binds the live filter to the current ItemsSource's default collection view.
        /// </summary>
        private void AttachView()
        {
            IEnumerable source = this.ItemsSource;
            if (source == null)
            {
                this.itemsView = null;
                return;
            }

            ICollectionView view = CollectionViewSource.GetDefaultView(source);
            this.itemsView = view;
            if (view != null)
            {
                view.Filter = this.FilterItem;
            }
        }

        /// <summary>
        /// Re-evaluates the filter against the current typed text. Refreshing the
        /// ICollectionView (not ComboBox.Items.Refresh) is what re-runs the predicate.
        /// </summary>
        private void RefreshFilter()
        {
            this.ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (this.itemsView == null)
            {
                this.AttachView();
            }

            if (this.itemsView != null)
            {
                if (this.itemsView.Filter == null)
                {
                    this.itemsView.Filter = this.FilterItem;
                }

                this.itemsView.Refresh();
            }
        }

        private bool FilterItem(object item)
        {
            if (item == null)
            {
                return false;
            }

            string text = this.currentFilterText;
            if (string.IsNullOrEmpty(text))
            {
                // Empty input restores the full list.
                return true;
            }

            string display = this.GetDisplayText(item);
            if (display == null)
            {
                return false;
            }

            if (this.FilterMode == ComboBoxFilterMode.Contains)
            {
                return display.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return display.StartsWith(text, StringComparison.OrdinalIgnoreCase);
        }

        private string GetDisplayText(object item)
        {
            string path = this.DisplayMemberPath;
            if (!string.IsNullOrEmpty(path))
            {
                PropertyInfo property = item.GetType().GetProperty(path);
                if (property != null)
                {
                    object value = property.GetValue(item, null);
                    return value == null ? null : value.ToString();
                }
            }

            return item.ToString();
        }
    }
}
