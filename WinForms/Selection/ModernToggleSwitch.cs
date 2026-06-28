using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Selection
{
    /// <summary>
    /// 토글 스위치 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernToggleSwitchControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernToggleSwitch : WpfElementHostBase<com.example.Controls.Wpf.Selection.ModernToggleSwitchControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernToggleSwitch()
        {
            this.Size = new Size(320, 56);
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

        /// <summary>켜짐 여부</summary>
        [Category("모던 컨트롤")]
        [Description("켜짐 여부")]
        [DefaultValue(false)]
        public bool IsOn
        {
            get { return this.Wpf.IsOn; }
            set { this.Wpf.IsOn = value; }
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
