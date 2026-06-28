using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 분절 선택 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernSegmentedControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernSegmented : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernSegmentedControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernSegmented()
        {
            this.Size = new Size(280, 36);
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IEnumerable ItemsSource
        {
            get { return this.Wpf.ItemsSource; }
            set { this.Wpf.ItemsSource = value; }
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

        /// <summary>선택이 바뀌었을 때 발생</summary>
        [Description("선택이 바뀌었을 때 발생")]
        public event System.EventHandler SelectionChanged
        {
            add { this.Wpf.SelectionChanged += value; }
            remove { this.Wpf.SelectionChanged -= value; }
        }
    }
}
