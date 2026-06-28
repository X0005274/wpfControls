using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Display
{
    /// <summary>
    /// 진행 막대 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernProgressBarControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernProgressBar : WpfElementHostBase<com.example.Controls.Wpf.Display.ModernProgressBarControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernProgressBar()
        {
            this.Size = new Size(320, 48);
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
        [DefaultValue(0.0)]
        public double Value
        {
            get { return this.Wpf.Value; }
            set { this.Wpf.Value = value; }
        }

        /// <summary>입력 가능한 최솟값</summary>
        [Category("모던 컨트롤")]
        [Description("입력 가능한 최솟값")]
        [DefaultValue(0.0)]
        public double Minimum
        {
            get { return this.Wpf.Minimum; }
            set { this.Wpf.Minimum = value; }
        }

        /// <summary>입력 가능한 최댓값</summary>
        [Category("모던 컨트롤")]
        [Description("입력 가능한 최댓값")]
        [DefaultValue(100.0)]
        public double Maximum
        {
            get { return this.Wpf.Maximum; }
            set { this.Wpf.Maximum = value; }
        }

        /// <summary>진행률을 알 수 없는 상태 표시 여부</summary>
        [Category("모던 컨트롤")]
        [Description("진행률을 알 수 없는 상태 표시 여부")]
        [DefaultValue(false)]
        public bool IsIndeterminate
        {
            get { return this.Wpf.IsIndeterminate; }
            set { this.Wpf.IsIndeterminate = value; }
        }
    }
}
