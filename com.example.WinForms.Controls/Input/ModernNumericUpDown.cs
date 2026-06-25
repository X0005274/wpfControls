using System.ComponentModel;
using System.Drawing;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Input
{
    /// <summary>
    /// 숫자 입력 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernNumericUpDownControl 을 ElementHost로 감싼 래퍼입니다.)
    /// </summary>
    [ToolboxItem(true)]
    public class ModernNumericUpDown : WpfElementHostBase<com.example.Controls.Wpf.Input.ModernNumericUpDownControl>
    {
        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernNumericUpDown()
        {
            this.Size = new Size(200, 72);
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

        /// <summary>버튼을 한 번 누를 때 증감하는 값</summary>
        [Category("모던 컨트롤")]
        [Description("버튼을 한 번 누를 때 증감하는 값")]
        [DefaultValue(1.0)]
        public double Step
        {
            get { return this.Wpf.Step; }
            set { this.Wpf.Step = value; }
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

        /// <summary>값이 바뀌었을 때 발생</summary>
        [Description("값이 바뀌었을 때 발생")]
        public event System.EventHandler ValueChanged
        {
            add { this.Wpf.ValueChanged += value; }
            remove { this.Wpf.ValueChanged -= value; }
        }
    }
}
