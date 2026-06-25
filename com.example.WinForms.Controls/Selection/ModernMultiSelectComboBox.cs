using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Selection
{
    /// <summary>
    /// 다중 선택 콤보(체크 콤보) 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernMultiSelectComboBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernMultiSelectComboBox : WpfElementHostBase<com.example.Controls.Wpf.Selection.ModernMultiSelectComboBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernMultiSelectComboBox()
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

        /// <summary>값이 비었을 때 흐리게 보여줄 안내 문구</summary>
        [Category("모던 컨트롤")]
        [Description("값이 비었을 때 흐리게 보여줄 안내 문구")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get { return this.Wpf.PlaceholderText; }
            set { this.Wpf.PlaceholderText = value; }
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IEnumerable ItemsSource
        {
            get { return this.Wpf.ItemsSource; }
            set { this.Wpf.ItemsSource = value; }
        }

        /// <summary>항목에서 화면에 표시할 속성 이름</summary>
        [Category("모던 컨트롤")]
        [Description("항목에서 화면에 표시할 속성 이름")]
        [DefaultValue("")]
        public string DisplayMemberPath
        {
            get { return this.Wpf.DisplayMemberPath; }
            set { this.Wpf.DisplayMemberPath = value; }
        }

        /// <summary>선택된 항목 목록(코드에서 사용)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IList SelectedItems
        {
            get { return this.Wpf.SelectedItems; }
            set { this.Wpf.SelectedItems = value; }
        }

        /// <summary>선택 요약 텍스트(자동 생성)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SummaryText
        {
            get { return this.Wpf.SummaryText; }
            set { this.Wpf.SummaryText = value; }
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
