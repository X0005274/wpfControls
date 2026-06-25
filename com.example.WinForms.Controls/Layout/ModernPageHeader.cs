using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 화면 머리글 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernPageHeaderControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernPageHeader : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernPageHeaderControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernPageHeader()
        {
            this.Size = new Size(480, 64);
        }

        /// <summary>컨트롤 위에 표시할 제목</summary>
        [Category("모던 컨트롤")]
        [Description("컨트롤 위에 표시할 제목")]
        [DefaultValue("")]
        public string Title
        {
            get { return this.Wpf.Title; }
            set { this.Wpf.Title = value; }
        }

        /// <summary>부제목(비우면 숨김)</summary>
        [Category("모던 컨트롤")]
        [Description("부제목(비우면 숨김)")]
        [DefaultValue("")]
        public string Subtitle
        {
            get { return this.Wpf.Subtitle; }
            set { this.Wpf.Subtitle = value; }
        }

        /// <summary>안에 넣을 내용(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object InnerContent
        {
            get { return this.Wpf.InnerContent; }
            set { this.Wpf.InnerContent = value; }
        }
    }
}
