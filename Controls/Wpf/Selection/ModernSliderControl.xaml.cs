using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Range slider with styled track and thumb that displays the current value.
    /// </summary>
    public partial class ModernSliderControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernSliderControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(ModernSliderControl),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(ModernSliderControl),
                new PropertyMetadata(100.0));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(ModernSliderControl),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernSliderControl),
                new PropertyMetadata(true));

        public ModernSliderControl()
        {
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }
    }
}
