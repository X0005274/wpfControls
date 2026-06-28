using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 스크롤 영역 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernScrollSectionControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernScrollSection : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernScrollSectionControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernScrollSection()
        {
            this.Size = new Size(320, 300);
        }

        /// <summary>머리글 텍스트</summary>
        [Category("모던 컨트롤")]
        [Description("머리글 텍스트")]
        [DefaultValue("")]
        public string HeaderText
        {
            get { return this.Wpf.HeaderText; }
            set { this.Wpf.HeaderText = value; }
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
