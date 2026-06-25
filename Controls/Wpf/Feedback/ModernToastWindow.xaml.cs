using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 화면 오른쪽 아래에 잠깐 나타났다 스르륵 사라지는 알림(토스트) 창입니다.
    /// 보통 직접 만들지 않고 ModernToast.Show(...) 로 사용합니다.
    /// </summary>
    public partial class ModernToastWindow : Window
    {
        private readonly double seconds;

        /// <summary>
        /// 토스트 창을 구성합니다.
        /// </summary>
        /// <param name="message">표시할 메시지</param>
        /// <param name="kind">종류(색상)</param>
        /// <param name="seconds">사라지기까지 머무는 시간(초)</param>
        public ModernToastWindow(string message, MessageKind kind, double seconds)
        {
            this.InitializeComponent();

            this.MessageText.Text = message ?? string.Empty;
            this.ToastBorder.Background = this.GetKindBrush(kind);
            this.seconds = seconds <= 0 ? 3.0 : seconds;

            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 작업 영역(작업 표시줄 제외)의 오른쪽 아래에 위치시킵니다.
            Rect workArea = SystemParameters.WorkArea;
            this.Left = workArea.Right - this.ActualWidth - 8;
            this.Top = workArea.Bottom - this.ActualHeight - 8;

            // 부드럽게 나타납니다(0 → 1).
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(200)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);

            // 지정한 시간이 지나면 사라지기 시작합니다.
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(this.seconds);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                this.FadeOutAndClose();
            };
            timer.Start();
        }

        // 부드럽게 사라진 뒤 창을 닫습니다.
        private void FadeOutAndClose()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(250)));
            fadeOut.Completed += (s, e) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOut);
        }

        // 종류별 배경색을 토큰에서 가져옵니다.
        private Brush GetKindBrush(MessageKind kind)
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
    }
}
