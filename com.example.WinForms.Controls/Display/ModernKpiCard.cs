using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 지표 카드(KPI) 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernKpiCardControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernKpiCard : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernKpiCardControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernKpiCard()
        {
            this.Size = new Size(200, 110);
        }

        /// <summary>항목 이름</summary>
        [Category("모던 컨트롤")]
        [Description("항목 이름")]
        [DefaultValue("")]
        public string Label
        {
            get { return this.Wpf.Label; }
            set { this.Wpf.Label = value; }
        }

        /// <summary>현재 값</summary>
        [Category("모던 컨트롤")]
        [Description("현재 값")]
        [DefaultValue("")]
        public string Value
        {
            get { return this.Wpf.Value; }
            set { this.Wpf.Value = value; }
        }

        /// <summary>값 아래 보조 설명</summary>
        [Category("모던 컨트롤")]
        [Description("값 아래 보조 설명")]
        [DefaultValue("")]
        public string Caption
        {
            get { return this.Wpf.Caption; }
            set { this.Wpf.Caption = value; }
        }
    }
}
