using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 폼 섹션 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernFormSectionControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernFormSection : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernFormSectionControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernFormSection()
        {
            this.Size = new Size(480, 200);
        }

        /// <summary>섹션 제목</summary>
        [Category("모던 컨트롤")]
        [Description("섹션 제목")]
        [DefaultValue("")]
        public string SectionTitle
        {
            get { return this.Wpf.SectionTitle; }
            set { this.Wpf.SectionTitle = value; }
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
