using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 형식 입력(마스크) 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernMaskedTextBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernMaskedTextBox : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernMaskedTextBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernMaskedTextBox()
        {
            this.Size = new Size(320, 72);
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

        /// <summary>입력 형식('0'=숫자 자리, 그 외 글자=구분 기호)</summary>
        [Category("모던 컨트롤")]
        [Description("입력 형식('0'=숫자 자리, 그 외 글자=구분 기호)")]
        [DefaultValue("000000-0000000")]
        public string Mask
        {
            get { return this.Wpf.Mask; }
            set { this.Wpf.Mask = value; }
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

        /// <summary>값이 비었을 때 흐리게 보여줄 안내 문구</summary>
        [Category("모던 컨트롤")]
        [Description("값이 비었을 때 흐리게 보여줄 안내 문구")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get { return this.Wpf.PlaceholderText; }
            set { this.Wpf.PlaceholderText = value; }
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

        /// <summary>입력/조작 가능 여부</summary>
        [Category("모던 컨트롤")]
        [Description("입력/조작 가능 여부")]
        [DefaultValue(true)]
        public bool IsEditorEnabled
        {
            get { return this.Wpf.IsEditorEnabled; }
            set { this.Wpf.IsEditorEnabled = value; }
        }
    }
}
