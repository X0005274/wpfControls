using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// 사용자를 나타내는 원형 아바타입니다. 가운데에 이니셜(이름 첫 글자 등)을
    /// 표시합니다. Text 에 보여 줄 글자를, Diameter 로 크기를 지정합니다.
    /// </summary>
    public partial class ModernAvatarControl : UserControl
    {
        /// <summary>아바타 가운데에 표시할 글자(이니셜 등)입니다.</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(ModernAvatarControl),
                new PropertyMetadata(string.Empty));

        /// <summary>원의 지름(픽셀)입니다. 글자 크기도 이에 맞춰 조절됩니다(기본 40).</summary>
        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register(
                "Diameter", typeof(double), typeof(ModernAvatarControl),
                new PropertyMetadata(40.0, OnDiameterChanged));

        public ModernAvatarControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.ApplyDiameter();
        }

        /// <summary>아바타 가운데에 표시할 글자입니다.</summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>원의 지름(픽셀)입니다.</summary>
        public double Diameter
        {
            get { return (double)this.GetValue(DiameterProperty); }
            set { this.SetValue(DiameterProperty, value); }
        }

        private static void OnDiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernAvatarControl)d).ApplyDiameter();
        }

        // 지름에 맞춰 원 크기와 글자 크기를 정합니다(글자는 지름의 약 40%).
        private void ApplyDiameter()
        {
            if (this.AvatarRoot == null || this.InitialsText == null)
            {
                return;
            }

            this.AvatarRoot.Width = this.Diameter;
            this.AvatarRoot.Height = this.Diameter;
            this.InitialsText.FontSize = this.Diameter * 0.4;
        }
    }
}
