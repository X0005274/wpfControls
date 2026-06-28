using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 파일 선택 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernFilePickerControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernFilePicker : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernFilePickerControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernFilePicker()
        {
            this.Size = new Size(360, 72);
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

        /// <summary>선택된 파일 경로</summary>
        [Category("모던 컨트롤")]
        [Description("선택된 파일 경로")]
        [DefaultValue("")]
        public string FilePath
        {
            get { return this.Wpf.FilePath; }
            set { this.Wpf.FilePath = value; }
        }

        /// <summary>값이 비었을 때 흐리게 보여줄 안내 문구</summary>
        [Category("모던 컨트롤")]
        [Description("값이 비었을 때 흐리게 보여줄 안내 문구")]
        [DefaultValue("선택된 파일 없음")]
        public string PlaceholderText
        {
            get { return this.Wpf.PlaceholderText; }
            set { this.Wpf.PlaceholderText = value; }
        }

        /// <summary>파일 열기 창의 필터(예: 엑셀 파일|*.xlsx)</summary>
        [Category("모던 컨트롤")]
        [Description("파일 열기 창의 필터(예: 엑셀 파일|*.xlsx)")]
        [DefaultValue("모든 파일|*.*")]
        public string Filter
        {
            get { return this.Wpf.Filter; }
            set { this.Wpf.Filter = value; }
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

        /// <summary>파일이 선택되었을 때 발생</summary>
        [Description("파일이 선택되었을 때 발생")]
        public event System.EventHandler FileSelected
        {
            add { this.Wpf.FileSelected += value; }
            remove { this.Wpf.FileSelected -= value; }
        }
    }
}
