using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 둥근 모서리의 현대적인 버튼 컨트롤입니다.
    /// - Text: 버튼에 표시할 글자
    /// - Kind: 버튼 종류(Primary/Secondary/Danger)에 따라 색이 달라집니다.
    /// - Click: 버튼을 눌렀을 때 발생하는 이벤트
    /// </summary>
    public partial class ModernButtonControl : UserControl
    {
        /// <summary>버튼에 표시할 글자입니다.</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ModernButtonControl),
                new PropertyMetadata("버튼"));

        /// <summary>버튼 종류(색상/강조 수준)입니다. 기본값은 Primary입니다.</summary>
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register(
                "Kind",
                typeof(ButtonKind),
                typeof(ModernButtonControl),
                new PropertyMetadata(ButtonKind.Primary));

        /// <summary>
        /// 버튼을 클릭했을 때 발생하는 이벤트입니다.
        /// 내부 버튼의 Click을 바깥으로 그대로 전달합니다.
        /// </summary>
        public event RoutedEventHandler Click;

        public ModernButtonControl()
        {
            this.InitializeComponent();
        }

        /// <summary>버튼에 표시할 글자입니다.</summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>버튼 종류(Primary/Secondary/Danger)입니다.</summary>
        public ButtonKind Kind
        {
            get { return (ButtonKind)this.GetValue(KindProperty); }
            set { this.SetValue(KindProperty, value); }
        }

        // 내부 버튼이 눌리면 바깥쪽 Click 이벤트를 발생시킵니다.
        private void InnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
    }
}
