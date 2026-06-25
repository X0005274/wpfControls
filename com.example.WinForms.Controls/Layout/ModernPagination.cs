using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Layout
{
    /// <summary>
    /// 페이지 이동 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernPaginationControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernPagination : WpfElementHostBase<com.example.Controls.Wpf.Layout.ModernPaginationControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernPagination()
        {
            this.Size = new Size(320, 40);
        }

        /// <summary>전체 페이지 수</summary>
        [Category("모던 컨트롤")]
        [Description("전체 페이지 수")]
        [DefaultValue(1)]
        public int TotalPages
        {
            get { return this.Wpf.TotalPages; }
            set { this.Wpf.TotalPages = value; }
        }

        /// <summary>현재 페이지 번호(1부터)</summary>
        [Category("모던 컨트롤")]
        [Description("현재 페이지 번호(1부터)")]
        [DefaultValue(1)]
        public int CurrentPage
        {
            get { return this.Wpf.CurrentPage; }
            set { this.Wpf.CurrentPage = value; }
        }

        /// <summary>페이지가 바뀌었을 때 발생</summary>
        [Description("페이지가 바뀌었을 때 발생")]
        public event System.EventHandler PageChanged
        {
            add { this.Wpf.PageChanged += value; }
            remove { this.Wpf.PageChanged -= value; }
        }
    }
}
