using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 배지 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernBadgeControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernBadge : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernBadgeControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernBadge()
        {
            this.Size = new Size(80, 28);
        }

        /// <summary>배지에 표시할 글자</summary>
        [Category("모던 컨트롤")]
        [Description("배지에 표시할 글자")]
        [DefaultValue("")]
        public string BadgeText
        {
            get { return this.Wpf.BadgeText; }
            set { this.Wpf.BadgeText = value; }
        }

        /// <summary>배지 종류(색상)</summary>
        [Category("모던 컨트롤")]
        [Description("배지 종류(색상)")]
        [DefaultValue("")]
        public string BadgeType
        {
            get { return this.Wpf.BadgeType; }
            set { this.Wpf.BadgeType = value; }
        }
    }
}
