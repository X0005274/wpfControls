using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Styled checkbox with a visible checkmark, title, and optional description.
    /// </summary>
    public partial class ModernCheckBoxControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernCheckBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernCheckBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked",
                typeof(bool),
                typeof(ModernCheckBoxControl),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernCheckBoxControl),
                new PropertyMetadata(true));

        public ModernCheckBoxControl()
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

        public bool IsChecked
        {
            get { return (bool)this.GetValue(IsCheckedProperty); }
            set { this.SetValue(IsCheckedProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }
    }
}
