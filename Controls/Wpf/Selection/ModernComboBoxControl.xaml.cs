using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Styled dropdown with rounded corners, custom arrow, and hover/selected
    /// item states. Supports DisplayMemberPath / SelectedValuePath binding.
    /// </summary>
    public partial class ModernComboBoxControl : UserControl
    {
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
                new PropertyMetadata(null));

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

        public ModernComboBoxControl()
        {
            this.InitializeComponent();
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
    }
}
