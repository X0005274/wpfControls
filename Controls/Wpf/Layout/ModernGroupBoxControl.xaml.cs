using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// Titled group container with a styled header (bottom border) and a padded
    /// content area. Child content is supplied through <see cref="InnerContent"/>.
    /// </summary>
    [ContentProperty("InnerContent")]
    public partial class ModernGroupBoxControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
                "HeaderText",
                typeof(string),
                typeof(ModernGroupBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent",
                typeof(object),
                typeof(ModernGroupBoxControl),
                new PropertyMetadata(null));

        public ModernGroupBoxControl()
        {
            this.InitializeComponent();
        }

        public string HeaderText
        {
            get { return (string)this.GetValue(HeaderTextProperty); }
            set { this.SetValue(HeaderTextProperty, value); }
        }

        public object InnerContent
        {
            get { return this.GetValue(InnerContentProperty); }
            set { this.SetValue(InnerContentProperty, value); }
        }
    }
}
