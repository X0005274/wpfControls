using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 금액 입력 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernCurrencyBoxControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernCurrencyBox : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernCurrencyBoxControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernCurrencyBox()
        {
            this.Size = new Size(240, 72);
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

        /// <summary>현재 값</summary>
        [Category("모던 컨트롤")]
        [Description("현재 값")]
        public decimal Value
        {
            get { return this.Wpf.Value; }
            set { this.Wpf.Value = value; }
        }

        /// <summary>금액 앞에 붙일 통화 기호(예: ₩)</summary>
        [Category("모던 컨트롤")]
        [Description("금액 앞에 붙일 통화 기호(예: ₩)")]
        [DefaultValue("₩")]
        public string Prefix
        {
            get { return this.Wpf.Prefix; }
            set { this.Wpf.Prefix = value; }
        }

        /// <summary>소수점 이하 자릿수</summary>
        [Category("모던 컨트롤")]
        [Description("소수점 이하 자릿수")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return this.Wpf.DecimalPlaces; }
            set { this.Wpf.DecimalPlaces = value; }
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
    }
}
