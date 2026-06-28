using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 아바타(원형 이니셜) 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernAvatarControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernAvatar : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernAvatarControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernAvatar()
        {
            this.Size = new Size(40, 40);
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

        /// <summary>원의 지름(픽셀)</summary>
        [Category("모던 컨트롤")]
        [Description("원의 지름(픽셀)")]
        [DefaultValue(40.0)]
        public double Diameter
        {
            get { return this.Wpf.Diameter; }
            set { this.Wpf.Diameter = value; }
        }
    }
}
