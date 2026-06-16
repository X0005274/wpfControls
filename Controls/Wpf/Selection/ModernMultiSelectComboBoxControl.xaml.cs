using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Multi-select dropdown: each row is a checkbox, the dropdown stays open
    /// while toggling items, and the collapsed field shows a comma-separated
    /// summary of the selected items. Supports ItemsSource and two-way
    /// SelectedItems binding. Hostable in WinForms through ElementHost.
    /// </summary>
    public partial class ModernMultiSelectComboBoxControl : UserControl
    {
        private readonly ObservableCollection<MultiSelectItem> internalItems;
        private bool isSyncing;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata("Select..."));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata(string.Empty, OnDisplayMemberPathChanged));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems",
                typeof(IList),
                typeof(ModernMultiSelectComboBoxControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedItemsChanged));

        public static readonly DependencyProperty SummaryTextProperty =
            DependencyProperty.Register(
                "SummaryText",
                typeof(string),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernMultiSelectComboBoxControl),
                new PropertyMetadata(true));

        public ModernMultiSelectComboBoxControl()
        {
            this.internalItems = new ObservableCollection<MultiSelectItem>();
            this.isSyncing = false;
            this.InitializeComponent();
            this.ItemsHost.ItemsSource = this.internalItems;
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)this.GetValue(PlaceholderTextProperty); }
            set { this.SetValue(PlaceholderTextProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public string DisplayMemberPath
        {
            get { return (string)this.GetValue(DisplayMemberPathProperty); }
            set { this.SetValue(DisplayMemberPathProperty, value); }
        }

        /// <summary>
        /// The checked source items. Two-way bindable; set to a new list whenever
        /// the selection changes.
        /// </summary>
        public IList SelectedItems
        {
            get { return (IList)this.GetValue(SelectedItemsProperty); }
            set { this.SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>Comma-separated display text of the selected items (read-only output).</summary>
        public string SummaryText
        {
            get { return (string)this.GetValue(SummaryTextProperty); }
            set { this.SetValue(SummaryTextProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernMultiSelectComboBoxControl)d).RebuildItems();
        }

        private static void OnDisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernMultiSelectComboBoxControl)d).RebuildItems();
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernMultiSelectComboBoxControl control = (ModernMultiSelectComboBoxControl)d;
            if (!control.isSyncing)
            {
                control.SyncChecksFromSelectedItems();
            }
        }

        private void RebuildItems()
        {
            foreach (MultiSelectItem existing in this.internalItems)
            {
                existing.PropertyChanged -= this.OnItemPropertyChanged;
            }

            this.internalItems.Clear();

            if (this.ItemsSource != null)
            {
                foreach (object source in this.ItemsSource)
                {
                    MultiSelectItem wrapper = new MultiSelectItem(source, this.GetDisplayText(source));
                    wrapper.PropertyChanged += this.OnItemPropertyChanged;
                    this.internalItems.Add(wrapper);
                }
            }

            this.SyncChecksFromSelectedItems();
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            if (e.PropertyName == "IsSelected")
            {
                this.UpdateSelectedItemsFromChecks();
                this.UpdateSummary();
            }
        }

        private void SyncChecksFromSelectedItems()
        {
            this.isSyncing = true;
            IList selected = this.SelectedItems;
            foreach (MultiSelectItem wrapper in this.internalItems)
            {
                wrapper.IsSelected = selected != null && selected.Contains(wrapper.Item);
            }

            this.isSyncing = false;
            this.UpdateSummary();
        }

        private void UpdateSelectedItemsFromChecks()
        {
            List<object> result = new List<object>();
            foreach (MultiSelectItem wrapper in this.internalItems)
            {
                if (wrapper.IsSelected)
                {
                    result.Add(wrapper.Item);
                }
            }

            this.isSyncing = true;
            this.SelectedItems = result;
            this.isSyncing = false;
        }

        private void UpdateSummary()
        {
            StringBuilder builder = new StringBuilder();
            foreach (MultiSelectItem wrapper in this.internalItems)
            {
                if (wrapper.IsSelected)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(wrapper.Display);
                }
            }

            this.SummaryText = builder.ToString();
        }

        private string GetDisplayText(object item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            string path = this.DisplayMemberPath;
            if (!string.IsNullOrEmpty(path))
            {
                PropertyInfo property = item.GetType().GetProperty(path);
                if (property != null)
                {
                    object value = property.GetValue(item, null);
                    return value == null ? string.Empty : value.ToString();
                }
            }

            return item.ToString();
        }
    }
}
