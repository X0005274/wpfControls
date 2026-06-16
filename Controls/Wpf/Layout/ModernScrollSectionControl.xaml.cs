using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// Scrollable container with a fixed (non-scrolling) header and a scrollable
    /// content area below. Child content is supplied through
    /// <see cref="InnerContent"/>.
    /// </summary>
    [ContentProperty("InnerContent")]
    public partial class ModernScrollSectionControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
                "HeaderText",
                typeof(string),
                typeof(ModernScrollSectionControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent",
                typeof(object),
                typeof(ModernScrollSectionControl),
                new PropertyMetadata(null));

        public ModernScrollSectionControl()
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
