using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Data
{
    /// <summary>
    /// 트리 뷰 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernTreeViewControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernTreeView : WpfElementHostBase<com.example.Controls.Wpf.Data.ModernTreeViewControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernTreeView()
        {
            this.Size = new Size(320, 300);
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IEnumerable ItemsSource
        {
            get { return this.Wpf.ItemsSource; }
            set { this.Wpf.ItemsSource = value; }
        }

        /// <summary>선택된 항목(코드에서 사용)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return this.Wpf.SelectedItem; }
            set { this.Wpf.SelectedItem = value; }
        }
    }
}
