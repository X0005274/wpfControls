using System;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// Determinate / indeterminate progress bar with a rounded track and a
    /// percentage caption. The percentage is hidden while indeterminate.
    /// </summary>
    public partial class ModernProgressBarControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernProgressBarControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(ModernProgressBarControl),
                new PropertyMetadata(0.0, OnProgressChanged));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(ModernProgressBarControl),
                new PropertyMetadata(0.0, OnProgressChanged));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(ModernProgressBarControl),
                new PropertyMetadata(100.0, OnProgressChanged));

        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register(
                "IsIndeterminate",
                typeof(bool),
                typeof(ModernProgressBarControl),
                new PropertyMetadata(false, OnProgressChanged));

        public ModernProgressBarControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
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

        public bool IsIndeterminate
        {
            get { return (bool)this.GetValue(IsIndeterminateProperty); }
            set { this.SetValue(IsIndeterminateProperty, value); }
        }

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernProgressBarControl control = (ModernProgressBarControl)d;
            control.UpdatePercentText();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdatePercentText();
        }

        private void UpdatePercentText()
        {
            if (this.PercentTextBlock == null)
            {
                return;
            }

            if (this.IsIndeterminate)
            {
                this.PercentTextBlock.Visibility = Visibility.Collapsed;
                return;
            }

            this.PercentTextBlock.Visibility = Visibility.Visible;

            double range = this.Maximum - this.Minimum;
            double percent;
            if (range <= 0.0)
            {
                percent = 0.0;
            }
            else
            {
                percent = ((this.Value - this.Minimum) / range) * 100.0;
            }

            if (percent < 0.0)
            {
                percent = 0.0;
            }
            else if (percent > 100.0)
            {
                percent = 100.0;
            }

            this.PercentTextBlock.Text = Math.Round(percent).ToString() + "%";
        }
    }
}
