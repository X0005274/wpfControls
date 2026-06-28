using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 태그/칩 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernChipControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernChip : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernChipControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernChip()
        {
            this.Size = new Size(90, 26);
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

        /// <summary>삭제(X) 버튼 표시 여부</summary>
        [Category("모던 컨트롤")]
        [Description("삭제(X) 버튼 표시 여부")]
        [DefaultValue(false)]
        public bool IsRemovable
        {
            get { return this.Wpf.IsRemovable; }
            set { this.Wpf.IsRemovable = value; }
        }

        /// <summary>삭제(X) 버튼을 눌렀을 때 발생</summary>
        [Description("삭제(X) 버튼을 눌렀을 때 발생")]
        public event System.EventHandler Removed
        {
            add { this.Wpf.Removed += value; }
            remove { this.Wpf.Removed -= value; }
        }
    }
}
