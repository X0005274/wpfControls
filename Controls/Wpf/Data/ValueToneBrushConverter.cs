using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// Maps a cell value to a foreground brush using a value → tone dictionary passed as
    /// the converter parameter (tone in success/danger/warning/neutral). The tone colors
    /// match the design-token text brushes used by <c>ModernBadgeControl</c>, so a
    /// color-coded combo reads with the same color language as the badges. Values with no
    /// mapping return <see cref="DependencyProperty.UnsetValue"/> so the control's default
    /// foreground applies.
    /// </summary>
    public class ValueToneBrushConverter : IValueConverter
    {
        private static readonly Brush SuccessBrush = Freeze(Color.FromRgb(0x15, 0x80, 0x3D));
        private static readonly Brush DangerBrush = Freeze(Color.FromRgb(0xB9, 0x1C, 0x1C));
        private static readonly Brush WarningBrush = Freeze(Color.FromRgb(0xB4, 0x53, 0x09));
        private static readonly Brush NeutralBrush = Freeze(Color.FromRgb(0x37, 0x41, 0x51));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string key = value == null ? null : value.ToString();
            IDictionary<string, string> map = parameter as IDictionary<string, string>;
            if (string.IsNullOrEmpty(key) || map == null)
            {
                return DependencyProperty.UnsetValue;
            }

            string tone;
            if (!map.TryGetValue(key, out tone))
            {
                return DependencyProperty.UnsetValue;
            }

            return BrushForTone(tone);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        private static object BrushForTone(string tone)
        {
            string key = (tone ?? string.Empty).Trim().ToLowerInvariant();
            if (key == "success")
            {
                return SuccessBrush;
            }
            if (key == "danger" || key == "error")
            {
                return DangerBrush;
            }
            if (key == "warning")
            {
                return WarningBrush;
            }
            if (key == "neutral")
            {
                return NeutralBrush;
            }

            return DependencyProperty.UnsetValue;
        }

        private static Brush Freeze(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }
    }
}
