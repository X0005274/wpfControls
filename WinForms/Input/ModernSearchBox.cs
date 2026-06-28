using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 검색 입력 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernSearchBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernSearchBox : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernSearchBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernSearchBox()
        {
            this.Size = new Size(320, 48);
        }

        /// <summary>검색어 텍스트</summary>
        [Category("모던 컨트롤")]
        [Description("검색어 텍스트")]
        [DefaultValue("")]
        public string SearchText
        {
            get { return this.Wpf.SearchText; }
            set { this.Wpf.SearchText = value; }
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

        /// <summary>입력/조작 가능 여부</summary>
        [Category("모던 컨트롤")]
        [Description("입력/조작 가능 여부")]
        [DefaultValue(true)]
        public bool IsEditorEnabled
        {
            get { return this.Wpf.IsEditorEnabled; }
            set { this.Wpf.IsEditorEnabled = value; }
        }

        /// <summary>검색(Enter 또는 검색 버튼)이 요청되었을 때 발생</summary>
        [Description("검색(Enter 또는 검색 버튼)이 요청되었을 때 발생")]
        public event System.Windows.RoutedEventHandler SearchRequested
        {
            add { this.Wpf.SearchRequested += value; }
            remove { this.Wpf.SearchRequested -= value; }
        }
    }
}
