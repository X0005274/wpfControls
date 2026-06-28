using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 단계 표시 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernStepIndicatorControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernStepIndicator : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernStepIndicatorControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernStepIndicator()
        {
            this.Size = new Size(400, 32);
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IEnumerable ItemsSource
        {
            get { return this.Wpf.ItemsSource; }
            set { this.Wpf.ItemsSource = value; }
        }

        /// <summary>현재 단계 번호(0부터)</summary>
        [Category("모던 컨트롤")]
        [Description("현재 단계 번호(0부터)")]
        [DefaultValue(0)]
        public int CurrentStep
        {
            get { return this.Wpf.CurrentStep; }
            set { this.Wpf.CurrentStep = value; }
        }
    }
}
