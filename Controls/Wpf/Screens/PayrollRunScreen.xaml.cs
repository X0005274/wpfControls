using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Screens
{
    /// <summary>
    /// Shell of the hr_payroll_system 급여계산 (payroll run) screen.
    /// Layout mirrors the web screen: header (title + 신규등록) → 조건 바
    /// → 진행 상태 → 요약 카드 → 계산 결과 → 급여 명세 → 액션 패널.
    /// The action panel drives a status state machine (준비중 → 데이터마감 → 계산완료
    /// → 승인대기 → 지급완료); 계산실행 populates results, summary, and progress.
    /// Selecting a result row fills the 급여 명세 panel. This is a UI shell only.
    /// </summary>
    public partial class PayrollRunScreen : UserControl
    {
        // Status order indices (api-contract 4-8).
        private const int OrderReady = 0;
        private const int OrderInputClosed = 1;
        private const int OrderCalculated = 3;
        private const int OrderReviewed = 4;
        private const int OrderApprovalRequested = 5;
        private const int OrderFinalized = 6;

        private readonly ObservableCollection<PayrollResultRow> results;
        private int currentOrder;

        /// <summary>Raised when 신규등록 is clicked (host opens a registration screen).</summary>
        public event EventHandler CreateRequested;

        /// <summary>Raised when 오류검토 is clicked (host navigates to 승인·예외처리).</summary>
        public event EventHandler ReviewErrorsRequested;

        public PayrollRunScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.results = new ObservableCollection<PayrollResultRow>();
            this.currentOrder = OrderReady;
            this.ResultGrid.ItemsSource = this.results;
            this.PopulateRunTypeCombos();
            this.RefreshForStatus();
        }

        /// <summary>
        /// Fills the 계산구분 combos with code (RunType) + label so the selection
        /// yields a code (REGULAR/ADDITIONAL/SETTLEMENT) while displaying 정기/추가/정산.
        /// </summary>
        private void PopulateRunTypeCombos()
        {
            this.RunTypeCombo.ItemsSource = this.BuildRunTypeOptions();
            this.RunTypeCombo.SelectedIndex = 0;
            this.NewRunTypeCombo.ItemsSource = this.BuildRunTypeOptions();
            this.NewRunTypeCombo.SelectedIndex = 0;
        }

        private List<ComboBoxItemModel> BuildRunTypeOptions()
        {
            List<ComboBoxItemModel> options = new List<ComboBoxItemModel>();
            options.Add(new ComboBoxItemModel("REGULAR", "정기"));
            options.Add(new ComboBoxItemModel("ADDITIONAL", "추가"));
            options.Add(new ComboBoxItemModel("SETTLEMENT", "정산"));
            return options;
        }

        /// <summary>Exposes the displayed result rows.</summary>
        public ObservableCollection<PayrollResultRow> Results
        {
            get { return this.results; }
        }

        private void ApplyBackground()
        {
            Brush background = this.TryFindResource("Brush.Background") as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        // ----- Condition bar -----

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // 조회: reload the cycle for the chosen condition — reset to 준비중.
            this.currentOrder = OrderReady;
            this.results.Clear();
            this.ShowDetail(null);
            this.RefreshForStatus();
        }

        // ----- Header -----

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Notify any host (cross-boundary demo), then open the in-screen form.
            EventHandler handler = this.CreateRequested;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            this.ShowCreatePage();
        }

        // ----- Create page (급여 회차 등록) -----

        private void ShowCreatePage()
        {
            this.NewMonthBox.Text = DateTime.Today.ToString("yyyy-MM");
            this.NewGroupBox.Text = "MONTHLY";
            this.NewRunTypeCombo.SelectedIndex = 0;
            this.MainPage.Visibility = Visibility.Collapsed;
            this.CreatePage.Visibility = Visibility.Visible;
        }

        private void ShowMainPage()
        {
            this.CreatePage.Visibility = Visibility.Collapsed;
            this.MainPage.Visibility = Visibility.Visible;
        }

        private void CancelCreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMainPage();
        }

        private void SaveCreateButton_Click(object sender, RoutedEventArgs e)
        {
            string month = this.NewMonthBox.Text.Trim();
            string group = this.NewGroupBox.Text.Trim();
            if (month.Length == 0 || group.Length == 0)
            {
                MessageBox.Show("급여월과 급여그룹은 필수입니다.", "회차 등록",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Apply the registered condition to the run condition bar and start a
            // fresh cycle in the 준비중(READY) state. (등록 직후 상태 = READY)
            this.PayrollGroupBox.Text = group;
            // Sync the registered 계산구분 by its code (RunType), not by index.
            this.RunTypeCombo.SelectedValue = this.NewRunTypeCombo.SelectedValue;
            this.currentOrder = OrderReady;
            this.results.Clear();
            this.ShowDetail(null);
            this.RefreshForStatus();

            this.ShowMainPage();
        }

        // ----- Action panel -----

        private void CloseInputButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentOrder == OrderReady)
            {
                this.currentOrder = OrderInputClosed;
                this.RefreshForStatus();
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentOrder == OrderInputClosed)
            {
                this.currentOrder = OrderCalculated;
                this.LoadSampleResults();
                this.RefreshForStatus();
            }
        }

        private void RecalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentOrder == OrderCalculated || this.currentOrder == OrderReviewed)
            {
                this.currentOrder = OrderCalculated;
                this.LoadSampleResults();
                this.ShowDetail(null);
                this.RefreshForStatus();
            }
        }

        private void RequestApprovalButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentOrder == OrderCalculated || this.currentOrder == OrderReviewed)
            {
                this.currentOrder = OrderApprovalRequested;
                this.RefreshForStatus();
            }
        }

        private void FinalizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentOrder == OrderApprovalRequested)
            {
                this.currentOrder = OrderFinalized;
                this.RefreshForStatus();
            }
        }

        private void ReviewErrorsButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = this.ReviewErrorsRequested;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return;
            }

            MessageBox.Show("승인·예외처리 화면으로 이동해 오류를 검토합니다.",
                "오류검토", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ----- Result selection / detail -----

        private void ResultGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PayrollResultRow selected = this.ResultGrid.SelectedItem as PayrollResultRow;
            this.ShowDetail(selected);
        }

        private void ShowDetail(PayrollResultRow row)
        {
            if (row == null)
            {
                this.DetailHint.Visibility = Visibility.Visible;
                this.DetailPanel.Visibility = Visibility.Collapsed;
                this.PdfButton.IsEnabled = false;
                this.EmailButton.IsEnabled = false;
                return;
            }

            this.DetailHint.Visibility = Visibility.Collapsed;
            this.DetailPanel.Visibility = Visibility.Visible;
            this.PdfButton.IsEnabled = true;
            this.EmailButton.IsEnabled = true;

            this.DetailSubjectText.Text = row.NameWithId;
            this.EarningsItems.ItemsSource = row.Earnings;
            this.DeductionsItems.ItemsSource = row.Deductions;

            long earningTotal = this.SumLines(row.Earnings);
            long deductionTotal = this.SumLines(row.Deductions);
            this.EarningTotalText.Text = this.Money(earningTotal);
            this.DeductionTotalText.Text = this.Money(deductionTotal);
            this.DetailNetText.Text = this.Money(earningTotal - deductionTotal);
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("급여명세서(PDF)를 생성합니다.", "급여명세서",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("급여명세 이메일을 발송합니다.", "이메일 발송",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ----- Status refresh -----

        private void RefreshForStatus()
        {
            // Current-status badge.
            this.CurrentStatusText.Text = this.StatusLabel(this.currentOrder);
            this.CurrentStatusBadge.Tag = this.currentOrder == OrderFinalized ? "success" : "info";

            // Progress steps.
            this.SetStep(this.Step1Dot, this.Step1Label, this.currentOrder >= OrderInputClosed);
            this.SetStep(this.Step2Dot, this.Step2Label, this.currentOrder >= OrderCalculated);
            this.SetStep(this.Step3Dot, this.Step3Label, this.currentOrder >= OrderReviewed);
            this.SetStep(this.Step4Dot, this.Step4Label, this.currentOrder >= OrderApprovalRequested);
            this.SetStep(this.Step5Dot, this.Step5Label, this.currentOrder >= OrderFinalized);

            // Action button enablement.
            this.CloseInputButton.IsEnabled = this.currentOrder == OrderReady;
            this.CalculateButton.IsEnabled = this.currentOrder == OrderInputClosed;
            this.RecalculateButton.IsEnabled =
                this.currentOrder == OrderCalculated || this.currentOrder == OrderReviewed;
            this.RequestApprovalButton.IsEnabled =
                this.currentOrder == OrderCalculated || this.currentOrder == OrderReviewed;
            this.FinalizeButton.IsEnabled = this.currentOrder == OrderApprovalRequested;

            this.RefreshSummary();

            this.ResultEmptyText.Visibility = this.results.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void RefreshSummary()
        {
            long gross = 0L;
            long deduction = 0L;
            long net = 0L;
            int errorCount = 0;
            foreach (PayrollResultRow row in this.results)
            {
                gross = gross + row.BaseSalary + row.AllowanceAmount;
                deduction = deduction + row.DeductionAmount;
                net = net + row.NetPayAmount;
                if (row.CalcStatusLabel == "오류")
                {
                    errorCount = errorCount + 1;
                }
            }

            this.SumTargetText.Text = this.results.Count.ToString() + "명";
            this.SumGrossText.Text = this.Money(gross);
            this.SumDeductionText.Text = this.Money(deduction);
            this.SumNetText.Text = this.Money(net);
            this.SumErrorText.Text = errorCount.ToString() + "건";

            if (errorCount > 0)
            {
                this.SumErrorCard.Background = this.TryFindResource("Brush.ErrorBackground") as Brush;
                this.SumErrorCard.BorderBrush = this.TryFindResource("Brush.ErrorBorder") as Brush;
                this.SumErrorText.Foreground = this.TryFindResource("Brush.ErrorBorder") as Brush;
                this.ReviewErrorsButton.Content = "오류검토 (" + errorCount.ToString() + ")";
                this.ReviewErrorsButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.SumErrorCard.Background = this.TryFindResource("Brush.SurfaceAlt") as Brush;
                this.SumErrorCard.BorderBrush = this.TryFindResource("Brush.BorderSubtle") as Brush;
                this.SumErrorText.Foreground = this.TryFindResource("Brush.TextPrimary") as Brush;
                this.ReviewErrorsButton.Visibility = Visibility.Collapsed;
            }
        }

        private void SetStep(System.Windows.Shapes.Ellipse dot, TextBlock label, bool done)
        {
            if (done)
            {
                dot.Fill = this.TryFindResource("Brush.Accent") as Brush;
                label.Foreground = this.TryFindResource("Brush.TextPrimary") as Brush;
                label.FontWeight = FontWeights.SemiBold;
            }
            else
            {
                dot.Fill = this.TryFindResource("Brush.Border") as Brush;
                label.Foreground = this.TryFindResource("Brush.TextSecondary") as Brush;
                label.FontWeight = FontWeights.Normal;
            }
        }

        // ----- Sample data -----

        private void LoadSampleResults()
        {
            this.results.Clear();

            this.results.Add(this.BuildRow(
                "E1001", "홍길동", "인사팀", 3200000L,
                new PayrollLineRow[]
                {
                    new PayrollLineRow("직책수당", 250000L),
                    new PayrollLineRow("식대", 200000L),
                },
                new PayrollLineRow[]
                {
                    new PayrollLineRow("국민연금", 144000L),
                    new PayrollLineRow("건강보험", 113000L),
                    new PayrollLineRow("장기요양", 14000L),
                    new PayrollLineRow("고용보험", 29000L),
                    new PayrollLineRow("소득세", 119000L),
                    new PayrollLineRow("지방소득세", 11900L),
                },
                "계산완료", "success"));

            this.results.Add(this.BuildRow(
                "E1002", "김영희", "재무팀", 2800000L,
                new PayrollLineRow[]
                {
                    new PayrollLineRow("자격수당", 150000L),
                    new PayrollLineRow("식대", 200000L),
                },
                new PayrollLineRow[]
                {
                    new PayrollLineRow("국민연금", 126000L),
                    new PayrollLineRow("건강보험", 99000L),
                    new PayrollLineRow("장기요양", 12000L),
                    new PayrollLineRow("고용보험", 26000L),
                    new PayrollLineRow("소득세", 92000L),
                    new PayrollLineRow("지방소득세", 9200L),
                },
                "계산완료", "success"));

            this.results.Add(this.BuildRow(
                "E1003", "이철수", "개발팀", 0L,
                new PayrollLineRow[]
                {
                    new PayrollLineRow("기본급(미산정)", 0L),
                },
                new PayrollLineRow[]
                {
                    new PayrollLineRow("공제(미산정)", 0L),
                },
                "오류", "danger"));
        }

        private PayrollResultRow BuildRow(
            string employeeId,
            string employeeName,
            string departmentName,
            long baseSalary,
            PayrollLineRow[] allowanceLines,
            PayrollLineRow[] deductionLines,
            string statusLabel,
            string statusTone)
        {
            PayrollResultRow row = new PayrollResultRow();
            row.EmployeeId = employeeId;
            row.EmployeeName = employeeName;
            row.DepartmentName = departmentName;
            row.BaseSalary = baseSalary;
            row.CalcStatusLabel = statusLabel;
            row.CalcStatusTone = statusTone;

            // 기본급 is always the first earning line.
            row.Earnings.Add(new PayrollLineRow("기본급", baseSalary));
            long allowance = 0L;
            foreach (PayrollLineRow line in allowanceLines)
            {
                row.Earnings.Add(line);
                allowance = allowance + line.Amount;
            }

            long deduction = 0L;
            foreach (PayrollLineRow line in deductionLines)
            {
                row.Deductions.Add(line);
                deduction = deduction + line.Amount;
            }

            row.AllowanceAmount = allowance;
            row.DeductionAmount = deduction;
            row.NetPayAmount = baseSalary + allowance - deduction;
            return row;
        }

        private long SumLines(List<PayrollLineRow> lines)
        {
            long total = 0L;
            foreach (PayrollLineRow line in lines)
            {
                total = total + line.Amount;
            }

            return total;
        }

        private string Money(long value)
        {
            return "₩" + value.ToString("#,0");
        }

        private string StatusLabel(int order)
        {
            if (order == OrderReady)
            {
                return "준비중";
            }
            if (order == OrderInputClosed)
            {
                return "데이터마감";
            }
            if (order == OrderCalculated)
            {
                return "계산완료";
            }
            if (order == OrderReviewed)
            {
                return "검토완료";
            }
            if (order == OrderApprovalRequested)
            {
                return "승인대기";
            }
            if (order == OrderFinalized)
            {
                return "지급완료";
            }

            return "준비중";
        }
    }
}
