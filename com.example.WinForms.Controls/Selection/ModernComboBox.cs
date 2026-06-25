using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Selection
{
    /// <summary>
    /// 콤보박스(자동완성) 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernComboBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernComboBox : WpfElementHostBase<com.example.Controls.Wpf.Selection.ModernComboBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernComboBox()
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

        /// <summary>선택된 값(코드에서 사용)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get { return this.Wpf.SelectedValue; }
            set { this.Wpf.SelectedValue = value; }
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

        /// <summary>항목에서 값으로 사용할 속성 이름</summary>
        [Category("모던 컨트롤")]
        [Description("항목에서 값으로 사용할 속성 이름")]
        [DefaultValue("")]
        public string SelectedValuePath
        {
            get { return this.Wpf.SelectedValuePath; }
            set { this.Wpf.SelectedValuePath = value; }
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

        /// <summary>입력어 매칭 방식(StartsWith/Contains)</summary>
        [Category("모던 컨트롤")]
        [Description("입력어 매칭 방식(StartsWith/Contains)")]
        [DefaultValue(com.example.Controls.Wpf.Selection.ComboBoxFilterMode.StartsWith)]
        public com.example.Controls.Wpf.Selection.ComboBoxFilterMode FilterMode
        {
            get { return this.Wpf.FilterMode; }
            set { this.Wpf.FilterMode = value; }
        }
    }
}
