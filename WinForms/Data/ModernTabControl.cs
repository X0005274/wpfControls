using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Data
{
    /// <summary>
    /// 탭 컨트롤 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernTabControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernTabControl : WpfElementHostBase<com.example.Controls.Wpf.Data.ModernTabControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernTabControl()
        {
            this.Size = new Size(480, 300);
        }

        /// <summary>선택된 탭 번호(0부터 시작)</summary>
        [Category("모던 컨트롤")]
        [Description("선택된 탭 번호(0부터 시작)")]
        [DefaultValue(0)]
        public int SelectedIndex
        {
            get { return this.Wpf.SelectedIndex; }
            set { this.Wpf.SelectedIndex = value; }
        }

        /// <summary>탭 항목 모음(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Controls.ItemCollection Items
        {
            get { return this.Wpf.Items; }
        }
    }
}
