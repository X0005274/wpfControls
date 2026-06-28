using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 여러 줄 서식 텍스트 편집 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernRichTextBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernRichTextBox : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernRichTextBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernRichTextBox()
        {
            this.Size = new Size(320, 160);
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

        /// <summary>제목 아래에 표시할 보조 설명</summary>
        [Category("모던 컨트롤")]
        [Description("제목 아래에 표시할 보조 설명")]
        [DefaultValue("")]
        public string Description
        {
            get { return this.Wpf.Description; }
            set { this.Wpf.Description = value; }
        }

        /// <summary>필수 입력 여부(제목 옆에 * 표시)</summary>
        [Category("모던 컨트롤")]
        [Description("필수 입력 여부(제목 옆에 * 표시)")]
        [DefaultValue(false)]
        public bool IsRequired
        {
            get { return this.Wpf.IsRequired; }
            set { this.Wpf.IsRequired = value; }
        }

        /// <summary>읽기 전용 여부</summary>
        [Category("모던 컨트롤")]
        [Description("읽기 전용 여부")]
        [DefaultValue(false)]
        public bool IsReadOnly
        {
            get { return this.Wpf.IsReadOnly; }
            set { this.Wpf.IsReadOnly = value; }
        }

        /// <summary>입력/조작 가능 여부</summary>
        [Category("모던 컨트롤")]
        [Description("입력/조작 가능 여부")]
        [DefaultValue(true)]
        public bool IsEditorEnabled
        {
            get { return this.Wpf.IsEditorEnabled; }
            set { this.Wpf.IsEditorEnabled = value; }
        }

        /// <summary>서식 없는 본문 텍스트</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PlainText
        {
            get { return this.Wpf.PlainText; }
            set { this.Wpf.PlainText = value; }
        }
    }
}
