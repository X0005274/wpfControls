using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 상태 표시줄 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernStatusBarControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernStatusBar : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernStatusBarControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernStatusBar()
        {
            this.Size = new Size(320, 36);
        }

        /// <summary>상태 메시지</summary>
        [Category("모던 컨트롤")]
        [Description("상태 메시지")]
        [DefaultValue("")]
        public string StatusText
        {
            get { return this.Wpf.StatusText; }
            set { this.Wpf.StatusText = value; }
        }

        /// <summary>상태 종류(예: info/success/warning/error)</summary>
        [Category("모던 컨트롤")]
        [Description("상태 종류(예: info/success/warning/error)")]
        [DefaultValue("")]
        public string StatusType
        {
            get { return this.Wpf.StatusType; }
            set { this.Wpf.StatusType = value; }
        }
    }
}
