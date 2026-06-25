using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 버튼 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernButtonControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernButton : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernButtonControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernButton()
        {
            this.Size = new Size(120, 40);
            // WPF 버튼이 눌리면 WinForms 표준 Click 이벤트를 발생시킵니다.
            this.Wpf.Click += (sender, args) => this.OnClick(System.EventArgs.Empty);
        }

        /// <summary>입력하거나 표시할 텍스트</summary>
        [Category("모던 컨트롤")]
        [Description("입력하거나 표시할 텍스트")]
        [DefaultValue("버튼")]
        public new string Text
        {
            get { return this.Wpf.Text; }
            set { this.Wpf.Text = value; }
        }

        /// <summary>버튼 종류(Primary/Secondary/Danger)</summary>
        [Category("모던 컨트롤")]
        [Description("버튼 종류(Primary/Secondary/Danger)")]
        [DefaultValue(com.example.Controls.Wpf.Input.ButtonKind.Primary)]
        public com.example.Controls.Wpf.Input.ButtonKind Kind
        {
            get { return this.Wpf.Kind; }
            set { this.Wpf.Kind = value; }
        }
    }
}
