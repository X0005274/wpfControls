using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 로딩 중임을 나타내는, 끊임없이 회전하는 원형 스피너입니다.
    /// 화면에 보이면 자동으로 회전 애니메이션이 시작됩니다.
    /// </summary>
    public partial class ModernSpinnerControl : UserControl
    {
        public ModernSpinnerControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 0도 → 360도를 1초에 한 바퀴, 무한 반복합니다.
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            this.ArcRotate.BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, animation);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // 화면에서 사라지면 애니메이션을 멈춰 자원을 아낍니다.
            this.ArcRotate.BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, null);
        }
    }
}
