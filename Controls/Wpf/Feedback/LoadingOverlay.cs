using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// WinForms 폼이나 컨트롤 위에 "로딩 중" 화면을 덮어 주는 헬퍼입니다.
    ///
    /// 사용 패턴(중요):
    ///   - 오버레이를 띄운 뒤, 실제 작업은 반드시 "백그라운드 스레드"에서 하세요.
    ///   - UI 스레드를 막는 동기 작업(예: 화면 멈춤) 중에는 스피너가 돌지 않습니다.
    ///   - 작업이 끝나면 UI 스레드에서 Dispose() 하여 오버레이를 치웁니다.
    ///
    /// 예)
    ///   LoadingOverlay overlay = LoadingOverlay.Show(this, "조회 중...");
    ///   System.Threading.Tasks.Task.Run(() =>
    ///   {
    ///       // ... 오래 걸리는 작업(TIBCO 요청 등) ...
    ///       this.BeginInvoke(new Action(() => overlay.Dispose()));  // UI 스레드에서 닫기
    ///   });
    /// </summary>
    public sealed class LoadingOverlay : IDisposable
    {
        private Control host;
        private ElementHost elementHost;

        private LoadingOverlay(Control host, string message)
        {
            this.host = host;

            // 덮개(반투명 배경 + 스피너 + 문구)를 만듭니다.
            ModernLoadingPanelControl panel = new ModernLoadingPanelControl();
            panel.Message = message;

            // WinForms 컨트롤 위에 올리려면 ElementHost로 감싸야 합니다.
            this.elementHost = new ElementHost();
            this.elementHost.Dock = DockStyle.Fill;
            this.elementHost.BackColorTransparent = true;
            this.elementHost.Child = panel;

            host.Controls.Add(this.elementHost);
            this.elementHost.BringToFront();
        }

        /// <summary>기본 문구("처리 중...")로 오버레이를 띄웁니다.</summary>
        /// <param name="host">덮을 대상(폼 또는 컨트롤)</param>
        public static LoadingOverlay Show(Control host)
        {
            return Show(host, "처리 중...");
        }

        /// <summary>안내 문구를 지정해 오버레이를 띄웁니다.</summary>
        /// <param name="host">덮을 대상(폼 또는 컨트롤)</param>
        /// <param name="message">스피너 아래에 표시할 문구</param>
        public static LoadingOverlay Show(Control host, string message)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            return new LoadingOverlay(host, message);
        }

        /// <summary>오버레이를 치웁니다(UI 스레드에서 호출).</summary>
        public void Dispose()
        {
            if (this.elementHost != null)
            {
                if (this.host != null)
                {
                    this.host.Controls.Remove(this.elementHost);
                }

                this.elementHost.Dispose();
                this.elementHost = null;
                this.host = null;
            }
        }
    }
}
