using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// Styled label with a title, optional description, and a required asterisk.
    /// </summary>
    public partial class ModernLabelControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernLabelControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernLabelControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired",
                typeof(bool),
                typeof(ModernLabelControl),
                new PropertyMetadata(false));

        public ModernLabelControl()
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

        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }
    }
}
