using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 확인/알림 모달 창의 화면입니다.
    /// 보통 직접 만들지 않고 ModernDialog.Confirm / ModernDialog.Alert 로 사용합니다.
    /// </summary>
    public partial class ModernDialogWindow : Window
    {
        /// <summary>
        /// 대화상자를 구성합니다.
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">본문 메시지</param>
        /// <param name="kind">종류(색상)</param>
        /// <param name="showCancel">취소 버튼 표시 여부(알림이면 false)</param>
        public ModernDialogWindow(string title, string message, MessageKind kind, bool showCancel)
        {
            this.InitializeComponent();

            this.TitleText.Text = title ?? string.Empty;
            this.MessageText.Text = message ?? string.Empty;
            this.CancelButton.Visibility = showCancel ? Visibility.Visible : Visibility.Collapsed;

            // 종류에 따라 상단 색 막대 색을 바꿉니다.
            this.AccentBar.Background = this.GetAccentBrush(kind);
        }

        // 종류별 색을 토큰(Tokens.xaml)에서 가져옵니다.
        private Brush GetAccentBrush(MessageKind kind)
        {
            string key;
            switch (kind)
            {
                case MessageKind.Success:
                    key = "Brush.Success";
                    break;
                case MessageKind.Warning:
                    key = "Brush.Warning";
                    break;
                case MessageKind.Error:
                    key = "Brush.ErrorBorder";
                    break;
                default:
                    key = "Brush.Accent";
                    break;
            }

            return (Brush)this.FindResource(key);
        }

        // 제목 영역을 끌면 창을 움직일 수 있게 합니다.
        private void TitleArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // 확인: 결과를 true 로 하고 닫습니다.
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        // 취소: 결과를 false 로 하고 닫습니다.
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
