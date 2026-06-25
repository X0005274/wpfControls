using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 경로 표시 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernBreadcrumbControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernBreadcrumb : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernBreadcrumbControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernBreadcrumb()
        {
            this.Size = new Size(320, 24);
        }

        /// <summary>경로 문자열(/ 로 구분, 예: 홈/직원/상세)</summary>
        [Category("모던 컨트롤")]
        [Description("경로 문자열(/ 로 구분, 예: 홈/직원/상세)")]
        [DefaultValue("")]
        public string Path
        {
            get { return this.Wpf.Path; }
            set { this.Wpf.Path = value; }
        }
    }
}
