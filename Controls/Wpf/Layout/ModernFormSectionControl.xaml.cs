using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// Grid-based labeled form section. Renders a section title header and hosts
    /// form rows in a shared-size scope so label and input columns align.
    /// Child rows are supplied through <see cref="InnerContent"/>.
    /// </summary>
    [ContentProperty("InnerContent")]
    public partial class ModernFormSectionControl : UserControl
    {
        public static readonly DependencyProperty SectionTitleProperty =
            DependencyProperty.Register(
                "SectionTitle",
                typeof(string),
                typeof(ModernFormSectionControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent",
                typeof(object),
                typeof(ModernFormSectionControl),
                new PropertyMetadata(null));

        public ModernFormSectionControl()
        {
            this.InitializeComponent();
        }

        public string SectionTitle
        {
            get { return (string)this.GetValue(SectionTitleProperty); }
            set { this.SetValue(SectionTitleProperty, value); }
        }

        public object InnerContent
        {
            get { return this.GetValue(InnerContentProperty); }
            set { this.SetValue(InnerContentProperty, value); }
        }
    }
}
