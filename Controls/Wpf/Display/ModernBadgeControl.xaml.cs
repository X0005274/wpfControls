using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// Pill-shaped badge whose background and foreground are determined by
    /// <see cref="BadgeType"/> (Info, Success, Warning, Error, Neutral).
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

            Color background;
            Color foreground;

            if (string.Equals(type, "Info", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFEFF6FF");
                foreground = (Color)ColorConverter.ConvertFromString("#FF1D4ED8");
            }
            else if (string.Equals(type, "Success", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFECFDF3");
                foreground = (Color)ColorConverter.ConvertFromString("#FF15803D");
            }
            else if (string.Equals(type, "Warning", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFFFFBEB");
                foreground = (Color)ColorConverter.ConvertFromString("#FFB45309");
            }
            else if (string.Equals(type, "Error", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFFEF2F2");
                foreground = (Color)ColorConverter.ConvertFromString("#FFB91C1C");
            }
            else
            {
                background = (Color)ColorConverter.ConvertFromString("#FFF3F4F6");
                foreground = (Color)ColorConverter.ConvertFromString("#FF374151");
            }

            this.BadgeBorder.Background = new SolidColorBrush(background);
            this.BadgeTextBlock.Foreground = new SolidColorBrush(foreground);
        }
    }
}
