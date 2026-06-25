using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 구분선 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernDividerControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernDivider : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernDividerControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernDivider()
        {
            this.Size = new Size(320, 24);
        }

        /// <summary>입력하거나 표시할 텍스트</summary>
        [Category("모던 컨트롤")]
        [Description("입력하거나 표시할 텍스트")]
        [DefaultValue("")]
        public new string Text
        {
            get { return this.Wpf.Text; }
            set { this.Wpf.Text = value; }
        }
    }
}
