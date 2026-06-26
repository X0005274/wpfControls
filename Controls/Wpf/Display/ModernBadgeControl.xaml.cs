using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// Pill-shaped badge whose background and foreground are determined by
    /// <see cref="BadgeType"/> (Info, Success, Warning, Error, Neutral).
    /// "Danger" is accepted as an alias for "Error" (common severity vocabulary).
    /// </summary>
    public partial class ModernBadgeControl : UserControl
    {
        public static readonly DependencyProperty BadgeTextProperty =
            DependencyProperty.Register(
                "BadgeText",
                typeof(string),
                typeof(ModernBadgeControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BadgeTypeProperty =
            DependencyProperty.Register(
                "BadgeType",
                typeof(string),
                typeof(ModernBadgeControl),
                new PropertyMetadata("Neutral", OnBadgeTypeChanged));

        public ModernBadgeControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        public string BadgeText
        {
            get { return (string)this.GetValue(BadgeTextProperty); }
            set { this.SetValue(BadgeTextProperty, value); }
        }

        public string BadgeType
        {
            get { return (string)this.GetValue(BadgeTypeProperty); }
            set { this.SetValue(BadgeTypeProperty, value); }
        }

        private static void OnBadgeTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernBadgeControl control = (ModernBadgeControl)d;
            control.ApplyBadgeType();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ApplyBadgeType();
        }

        private void ApplyBadgeType()
        {
            if (this.BadgeBorder == null || this.BadgeTextBlock == null)
            {
                return;
            }

            string type = this.BadgeType ?? "Neutral";

            string backgroundKey;
            string foregroundKey;

            if (string.Equals(type, "Info", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.InfoBackground";
                foregroundKey = "Brush.InfoText";
            }
            else if (string.Equals(type, "Success", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.SuccessBackground";
                foregroundKey = "Brush.SuccessText";
            }
            else if (string.Equals(type, "Warning", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.WarningBackground";
                foregroundKey = "Brush.WarningText";
            }
            else if (string.Equals(type, "Error", StringComparison.OrdinalIgnoreCase)
                || string.Equals(type, "Danger", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.ErrorBackground";
                foregroundKey = "Brush.ErrorText";
            }
            else
            {
                backgroundKey = "Brush.NeutralBackground";
                foregroundKey = "Brush.NeutralText";
            }

            this.BadgeBorder.Background = this.ResolveBrush(backgroundKey);
            this.BadgeTextBlock.Foreground = this.ResolveBrush(foregroundKey);
        }

        private Brush ResolveBrush(string resourceKey)
        {
            object resource = this.TryFindResource(resourceKey);
            SolidColorBrush brush = resource as SolidColorBrush;
            if (brush != null)
            {
                return brush;
            }

            return Brushes.Transparent;
        }
    }
}
