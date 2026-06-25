using System.Windows.Controls;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 로딩 오버레이의 내용물입니다. 화면을 살짝 어둡게 덮고 가운데에
    /// 회전 스피너와 안내 문구를 보여 줍니다.
    /// 보통 직접 쓰기보다 LoadingOverlay 헬퍼를 통해 사용합니다.
    /// </summary>
    public partial class ModernLoadingPanelControl : UserControl
    {
        public ModernLoadingPanelControl()
        {
            this.InitializeComponent();
        }

        /// <summary>스피너 아래에 표시할 안내 문구입니다(예: "저장 중...").</summary>
        public string Message
        {
            get { return this.MessageText.Text; }
            set { this.MessageText.Text = value; }
        }
    }
}
