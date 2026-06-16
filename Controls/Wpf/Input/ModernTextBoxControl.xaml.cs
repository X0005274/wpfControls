using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// Labeled, single-line text input with placeholder, required indicator,
    /// read-only and disabled visual states. Designed for hosting in WinForms
    /// through ElementHost.
    /// </summary>
    public partial class ModernTextBoxControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ModernTextBoxControl),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired",
                typeof(bool),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernTextBoxControl),
                new PropertyMetadata(true));

        public ModernTextBoxControl()
        {
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)this.GetValue(DescriptionProperty); }
            set { this.SetValue(DescriptionProperty, value); }
        }

        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)this.GetValue(PlaceholderTextProperty); }
            set { this.SetValue(PlaceholderTextProperty, value); }
        }

        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)this.GetValue(IsReadOnlyProperty); }
            set { this.SetValue(IsReadOnlyProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }
    }
}
