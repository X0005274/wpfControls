using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// Status bar that colors itself based on <see cref="StatusType"/>
    /// (Info, Success, Warning, Error) and shows a colored indicator.
    /// </summary>
    public partial class ModernStatusBarControl : UserControl
    {
        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register(
                "StatusText",
                typeof(string),
                typeof(ModernStatusBarControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty StatusTypeProperty =
            DependencyProperty.Register(
                "StatusType",
                typeof(string),
                typeof(ModernStatusBarControl),
                new PropertyMetadata("Info", OnStatusTypeChanged));

        public ModernStatusBarControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        public string StatusText
        {
            get { return (string)this.GetValue(StatusTextProperty); }
            set { this.SetValue(StatusTextProperty, value); }
        }

        public string StatusType
        {
            get { return (string)this.GetValue(StatusTypeProperty); }
            set { this.SetValue(StatusTypeProperty, value); }
        }

        private static void OnStatusTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernStatusBarControl control = (ModernStatusBarControl)d;
            control.ApplyStatusType();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ApplyStatusType();
        }

        private void ApplyStatusType()
        {
            if (this.StatusBorder == null || this.Indicator == null)
            {
                return;
            }

            string type = this.StatusType ?? "Info";

            Color background;
            Color border;
            Color accent;

            if (string.Equals(type, "Success", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFECFDF3");
                border = (Color)ColorConverter.ConvertFromString("#FFA6F4C5");
                accent = (Color)ColorConverter.ConvertFromString("#FF16A34A");
            }
            else if (string.Equals(type, "Warning", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFFFFBEB");
                border = (Color)ColorConverter.ConvertFromString("#FFFDE68A");
                accent = (Color)ColorConverter.ConvertFromString("#FFD97706");
            }
            else if (string.Equals(type, "Error", StringComparison.OrdinalIgnoreCase))
            {
                background = (Color)ColorConverter.ConvertFromString("#FFFEF2F2");
                border = (Color)ColorConverter.ConvertFromString("#FFFECACA");
                accent = (Color)ColorConverter.ConvertFromString("#FFDC2626");
            }
            else
            {
                background = (Color)ColorConverter.ConvertFromString("#FFEFF6FF");
                border = (Color)ColorConverter.ConvertFromString("#FFBFDBFE");
                accent = (Color)ColorConverter.ConvertFromString("#FF2563EB");
            }

            this.StatusBorder.Background = new SolidColorBrush(background);
            this.StatusBorder.BorderBrush = new SolidColorBrush(border);
            this.Indicator.Fill = new SolidColorBrush(accent);
        }
    }
}
