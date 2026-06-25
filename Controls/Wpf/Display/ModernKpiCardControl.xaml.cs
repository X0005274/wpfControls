using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// 대시보드용 숫자(KPI) 카드입니다. 항목 이름(Label), 큰 값(Value),
    /// 보조 설명(Caption)을 한 장의 카드로 보여 줍니다.
    /// 예) Label="총 직원", Value="1,234", Caption="지난달 대비 +5"
    /// </summary>
    public partial class ModernKpiCardControl : UserControl
    {
        /// <summary>항목 이름입니다(예: "총 직원").</summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                "Label", typeof(string), typeof(ModernKpiCardControl),
                new PropertyMetadata(string.Empty));

        /// <summary>크게 표시할 값입니다(예: "1,234").</summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(string), typeof(ModernKpiCardControl),
                new PropertyMetadata(string.Empty));

        /// <summary>값 아래에 표시할 보조 설명입니다(비우면 숨김).</summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register(
                "Caption", typeof(string), typeof(ModernKpiCardControl),
                new PropertyMetadata(string.Empty, OnCaptionChanged));

        public ModernKpiCardControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.UpdateCaptionVisibility();
        }

        /// <summary>항목 이름입니다.</summary>
        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        /// <summary>크게 표시할 값입니다.</summary>
        public string Value
        {
            get { return (string)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>값 아래에 표시할 보조 설명입니다.</summary>
        public string Caption
        {
            get { return (string)this.GetValue(CaptionProperty); }
            set { this.SetValue(CaptionProperty, value); }
        }

        private static void OnCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernKpiCardControl)d).UpdateCaptionVisibility();
        }

        private void UpdateCaptionVisibility()
        {
            if (this.CaptionText != null)
            {
                this.CaptionText.Visibility = string.IsNullOrEmpty(this.Caption) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
