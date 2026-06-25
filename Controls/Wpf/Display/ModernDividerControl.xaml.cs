using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// 가로 구분선입니다. Text 가 비어 있으면 단순한 선만, 값이 있으면
    /// 선 가운데에 글자를 표시합니다(예: "또는", "기본 정보").
    /// </summary>
    public partial class ModernDividerControl : UserControl
    {
        /// <summary>구분선 가운데에 표시할 글자입니다(선택).</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(ModernDividerControl),
                new PropertyMetadata(string.Empty, OnTextChanged));

        public ModernDividerControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.UpdateTextVisibility();
        }

        /// <summary>구분선 가운데에 표시할 글자입니다.</summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernDividerControl)d).UpdateTextVisibility();
        }

        // 글자가 없으면 가운데 글자 영역을 숨겨 선만 보이게 합니다.
        private void UpdateTextVisibility()
        {
            if (this.TextHolder != null)
            {
                this.TextHolder.Visibility = string.IsNullOrEmpty(this.Text)
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }
    }
}
