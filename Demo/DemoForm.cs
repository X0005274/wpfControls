using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using com.example.Controls.Wpf.Screens;

namespace com.example.Demo
{
    /// <summary>
    /// WinForms host form. A native WinForms header and status bar surround a
    /// single ElementHost (Dock = Fill). The header carries native WinForms
    /// nav buttons that swap the hosted WPF content between the control gallery
    /// and the 직원관리 (employee management) screen. The WPF search box raises a
    /// routed event handled here to update the native WinForms status bar —
    /// demonstrating cross-boundary interaction.
    /// </summary>
    public class DemoForm : Form
    {
        private ElementHost wpfHost;
        private Panel headerPanel;
        private Label headerLabel;
        private Button galleryButton;
        private Button dashboardButton;
        private Button employeeButton;
        private Button approvalButton;
        private Button payrollButton;
        private Button browserButton;
        private Button hostFormButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        private System.Windows.FrameworkElement galleryView;
        private System.Windows.FrameworkElement dashboardView;
        private System.Windows.FrameworkElement employeeView;
        private System.Windows.FrameworkElement approvalView;
        private System.Windows.FrameworkElement payrollView;
        private System.Windows.FrameworkElement browserView;

        public DemoForm()
        {
            this.InitializeLayout();
        }

        private void InitializeLayout()
        {
            this.Text = "com.example - WPF Modern Controls hosted in WinForms (ElementHost)";
            this.Size = new Size(1120, 840);
            this.MinimumSize = new Size(720, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // --- Native WinForms header ---
            this.headerPanel = new Panel();
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 52;
            this.headerPanel.BackColor = Color.FromArgb(37, 99, 235);

            this.headerLabel = new Label();
            this.headerLabel.Text = "WinForms Host   -   WPF Modern Controls via ElementHost";
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.headerLabel.Padding = new Padding(16, 0, 0, 0);

            // --- Native WinForms nav buttons (swap the hosted WPF view) ---
            this.dashboardButton = this.CreateNavButton("대시보드");
            this.dashboardButton.Click += this.OnDashboardButtonClick;

            this.employeeButton = this.CreateNavButton("직원관리");
            this.employeeButton.Click += this.OnEmployeeButtonClick;

            this.approvalButton = this.CreateNavButton("승인·예외");
            this.approvalButton.Click += this.OnApprovalButtonClick;

            this.payrollButton = this.CreateNavButton("급여계산");
            this.payrollButton.Click += this.OnPayrollButtonClick;

            this.galleryButton = this.CreateNavButton("컨트롤 갤러리");
            this.galleryButton.Click += this.OnGalleryButtonClick;

            this.browserButton = this.CreateNavButton("사원조회");
            this.browserButton.Click += this.OnBrowserButtonClick;

            // Opens a separate WinForms Form that hosts individual WPF controls,
            // each in its own ElementHost (combo / tree / grid).
            this.hostFormButton = this.CreateNavButton("WPF on Form");
            this.hostFormButton.Click += this.OnHostFormButtonClick;

            // Docked right; the last added docks right-most. Visual order (left → right):
            // WPF on Form | 사원조회 | 대시보드 | 직원관리 | 승인·예외 | 급여계산 | 컨트롤 갤러리.
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.hostFormButton);
            this.headerPanel.Controls.Add(this.browserButton);
            this.headerPanel.Controls.Add(this.dashboardButton);
            this.headerPanel.Controls.Add(this.employeeButton);
            this.headerPanel.Controls.Add(this.approvalButton);
            this.headerPanel.Controls.Add(this.payrollButton);
            this.headerPanel.Controls.Add(this.galleryButton);

            // --- Native WinForms status bar ---
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.statusLabel.Text = "Ready";
            this.statusStrip.Items.Add(this.statusLabel);

            // --- WPF content hosted through ElementHost ---
            this.galleryView = GalleryBuilder.Build(this.OnWpfSearchRequested);

            this.wpfHost = new ElementHost();
            this.wpfHost.Dock = DockStyle.Fill;
            this.wpfHost.Child = this.galleryView;

            // Add the Fill control first, then the docked header and status bar.
            this.Controls.Add(this.wpfHost);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.statusStrip);
        }

        private Button CreateNavButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            button.Dock = DockStyle.Right;
            button.Width = 130;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(29, 78, 216);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            return button;
        }

        private void OnGalleryButtonClick(object sender, EventArgs e)
        {
            this.wpfHost.Child = this.galleryView;
            this.statusLabel.Text = "컨트롤 갤러리";
        }

        private void OnHostFormButtonClick(object sender, EventArgs e)
        {
            // A separate top-level WinForms Form with WPF controls placed one-by-one
            // via individual ElementHosts.
            WpfOnFormDemo form = new WpfOnFormDemo();
            form.Show(this);
            this.statusLabel.Text = "WPF on Form 열림 (ElementHost x N: 콤보/트리/그리드)";
        }

        private void OnBrowserButtonClick(object sender, EventArgs e)
        {
            if (this.browserView == null)
            {
                this.browserView = new com.example.Controls.Wpf.Screens.EmployeeBrowserScreen();
            }

            this.wpfHost.Child = this.browserView;
            this.statusLabel.Text = "사원조회";
        }

        private void OnDashboardButtonClick(object sender, EventArgs e)
        {
            if (this.dashboardView == null)
            {
                DashboardScreen screen = new DashboardScreen();
                screen.ExceptionSelected += this.OnDashboardExceptionSelected;
                this.dashboardView = screen;
            }

            this.wpfHost.Child = this.dashboardView;
            this.statusLabel.Text = "대시보드";
        }

        private void OnDashboardExceptionSelected(object sender, EventArgs e)
        {
            // 대시보드 오류·예외 → 승인·예외처리 화면 진입. (IA 연결 흐름)
            this.OnApprovalButtonClick(sender, e);
            this.statusLabel.Text = "오류·예외 선택 → 승인·예외처리 화면으로 이동";
        }

        private void OnEmployeeButtonClick(object sender, EventArgs e)
        {
            if (this.employeeView == null)
            {
                EmployeeManagementScreen screen = new EmployeeManagementScreen();
                screen.CreateRequested += this.OnEmployeeCreateRequested;
                this.employeeView = screen;
            }

            this.wpfHost.Child = this.employeeView;
            this.statusLabel.Text = "직원관리";
        }

        private void OnApprovalButtonClick(object sender, EventArgs e)
        {
            if (this.approvalView == null)
            {
                ApprovalExceptionScreen screen = new ApprovalExceptionScreen();
                screen.CreateRequested += this.OnApprovalCreateRequested;
                this.approvalView = screen;
            }

            this.wpfHost.Child = this.approvalView;
            this.statusLabel.Text = "승인·예외처리";
        }

        private void OnApprovalCreateRequested(object sender, EventArgs e)
        {
            this.statusLabel.Text = "예외 신규등록 요청됨 (WPF 승인·예외처리 화면)";
        }

        private void OnPayrollButtonClick(object sender, EventArgs e)
        {
            if (this.payrollView == null)
            {
                PayrollRunScreen screen = new PayrollRunScreen();
                screen.CreateRequested += this.OnPayrollCreateRequested;
                screen.ReviewErrorsRequested += this.OnPayrollReviewErrors;
                this.payrollView = screen;
            }

            this.wpfHost.Child = this.payrollView;
            this.statusLabel.Text = "급여계산";
        }

        private void OnPayrollCreateRequested(object sender, EventArgs e)
        {
            this.statusLabel.Text = "급여 회차 신규등록 요청됨 (WPF 급여계산 화면)";
        }

        private void OnPayrollReviewErrors(object sender, EventArgs e)
        {
            // 오류검토는 승인·예외처리 화면으로 연결한다. (IA 연결 흐름)
            this.OnApprovalButtonClick(sender, e);
            this.statusLabel.Text = "오류검토 → 승인·예외처리 화면으로 이동";
        }

        /// <summary>
        /// Handles the WPF EmployeeManagementScreen.CreateRequested event and
        /// reports it on the native WinForms status bar. A real host would open a
        /// registration screen here.
        /// </summary>
        private void OnEmployeeCreateRequested(object sender, EventArgs e)
        {
            this.statusLabel.Text = "신규등록 요청됨 (WPF 직원관리 화면)";
        }

        /// <summary>
        /// Handles the WPF ModernSearchBoxControl.SearchRequested event on the
        /// shared UI thread and updates the native WinForms status bar.
        /// </summary>
        private void OnWpfSearchRequested(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                this.statusLabel.Text = "Search cleared.";
            }
            else
            {
                this.statusLabel.Text = "Search requested from WPF control: \"" + term + "\"";
            }
        }
    }
}
