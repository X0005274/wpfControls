using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// Collapsible section with an animated expand/collapse (Storyboard) and a
    /// rotating arrow. The expandable region accepts any WPF child through the
    /// <see cref="InnerContent"/> content property.
    /// </summary>
    [ContentProperty("InnerContent")]
    public partial class ModernExpanderControl : UserControl
    {
        private const double AnimationSeconds = 0.2;

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
                "HeaderText",
                typeof(string),
                typeof(ModernExpanderControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
                "IsExpanded",
                typeof(bool),
                typeof(ModernExpanderControl),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnIsExpandedChanged));

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent",
                typeof(object),
                typeof(ModernExpanderControl),
                new PropertyMetadata(null));

        public ModernExpanderControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        public string HeaderText
        {
            get { return (string)this.GetValue(HeaderTextProperty); }
            set { this.SetValue(HeaderTextProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)this.GetValue(IsExpandedProperty); }
            set { this.SetValue(IsExpandedProperty, value); }
        }

        public object InnerContent
        {
            get { return this.GetValue(InnerContentProperty); }
            set { this.SetValue(InnerContentProperty, value); }
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernExpanderControl control = (ModernExpanderControl)d;
            control.AnimateExpansion((bool)e.NewValue, true);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Apply the initial state without animation.
            this.AnimateExpansion(this.IsExpanded, false);
        }

        private void HeaderBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        private void AnimateExpansion(bool expanded, bool animate)
        {
            double targetScale = expanded ? 1.0 : 0.0;
            double targetAngle = expanded ? 90.0 : 0.0;

            if (!animate)
            {
                this.ContentScale.ScaleY = targetScale;
                this.ArrowRotation.Angle = targetAngle;
                return;
            }

            Duration duration = new Duration(System.TimeSpan.FromSeconds(AnimationSeconds));

            DoubleAnimation scaleAnimation = new DoubleAnimation();
            scaleAnimation.To = targetScale;
            scaleAnimation.Duration = duration;
            scaleAnimation.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut };

            DoubleAnimation rotateAnimation = new DoubleAnimation();
            rotateAnimation.To = targetAngle;
            rotateAnimation.Duration = duration;

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(scaleAnimation, this.ContentScale);
            Storyboard.SetTargetProperty(scaleAnimation, new PropertyPath(ScaleTransform.ScaleYProperty));
            Storyboard.SetTarget(rotateAnimation, this.ArrowRotation);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath(RotateTransform.AngleProperty));

            storyboard.Children.Add(scaleAnimation);
            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();
        }
    }
}
