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

            string backgroundKey;
            string borderKey;
            string accentKey;

            if (string.Equals(type, "Success", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.SuccessBackground";
                borderKey = "Brush.SuccessBorder";
                accentKey = "Brush.Success";
            }
            else if (string.Equals(type, "Warning", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.WarningBackground";
                borderKey = "Brush.WarningBorder";
                accentKey = "Brush.Warning";
            }
            else if (string.Equals(type, "Error", StringComparison.OrdinalIgnoreCase))
            {
                backgroundKey = "Brush.ErrorBackground";
                borderKey = "Brush.ErrorBorderSoft";
                accentKey = "Brush.ErrorBorder";
            }
            else
            {
                backgroundKey = "Brush.InfoBackground";
                borderKey = "Brush.InfoBorder";
                accentKey = "Brush.Accent";
            }

            Brush background = this.TryFindResource(backgroundKey) as Brush;
            Brush border = this.TryFindResource(borderKey) as Brush;
            Brush accent = this.TryFindResource(accentKey) as Brush;

            if (background != null)
            {
                this.StatusBorder.Background = background;
            }

            if (border != null)
            {
                this.StatusBorder.BorderBrush = border;
            }

            if (accent != null)
            {
                this.Indicator.Fill = accent;
            }
        }
    }
}
