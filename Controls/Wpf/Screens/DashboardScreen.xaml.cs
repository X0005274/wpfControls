using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Screens
{
    /// <summary>
    /// Shell of the hr_payroll_system 대시보드 (dashboard) screen.
    /// Layout mirrors the web screen: header → KPI 카드 4개 → (급여 주기 상태 / 오류·예외)
    /// → 월별 인건비 추이 (CSS-style bar chart) → 부서별 현황 테이블.
    /// The 월별 인건비 추이 chart is drawn with native WPF bars (no chart library),
    /// matching the project's "pure WPF" rule. This is a UI shell only.
    /// </summary>
    public partial class DashboardScreen : UserControl
    {
        private const double ChartBarMaxHeight = 150.0;

        private readonly ObservableCollection<ExceptionItem> recentExceptions;
        private readonly ObservableCollection<DepartmentSummaryRow> departments;

        /// <summary>
        /// Raised when a row in 오류·예외 is double-clicked. A host can subscribe to
        /// navigate to the 승인·예외처리 screen (IA 연결 흐름).
        /// </summary>
        public event EventHandler ExceptionSelected;

        public DashboardScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.recentExceptions = new ObservableCollection<ExceptionItem>();
            this.departments = new ObservableCollection<DepartmentSummaryRow>();
            this.ExceptionListBox.ItemsSource = this.recentExceptions;
            this.DepartmentGrid.ItemsSource = this.departments;
            this.LoadKpis();
            this.LoadCycleStatus();
            this.LoadRecentExceptions();
            this.LoadDepartments();
            this.BuildChart();
        }

        /// <summary>Exposes the recent-exception list bound to the panel.</summary>
        public ObservableCollection<ExceptionItem> RecentExceptions
        {
            get { return this.recentExceptions; }
        }

        /// <summary>Exposes the department summary rows bound to the grid.</summary>
        public ObservableCollection<DepartmentSummaryRow> Departments
        {
            get { return this.departments; }
        }

        private void ApplyBackground()
        {
            Brush background = this.TryFindResource("Brush.Background") as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        private void LoadKpis()
        {
            this.KpiTotalText.Text = "128명";
            this.KpiScheduledText.Text = "₩854,000,000";
            this.KpiPendingText.Text = "3건";

            int exceptionCount = 2;
            this.KpiExceptionText.Text = exceptionCount.ToString() + "건";
            if (exceptionCount > 0)
            {
                this.KpiExceptionCard.Background = this.TryFindResource("Brush.ErrorBackground") as Brush;
                this.KpiExceptionCard.BorderBrush = this.TryFindResource("Brush.ErrorBorder") as Brush;
                this.KpiExceptionText.Foreground = this.TryFindResource("Brush.ErrorBorder") as Brush;
            }
        }

        private void LoadCycleStatus()
        {
            // 승인대기(APPROVAL_REQUESTED) → tone warning.
            this.CycleStatusText.Text = "승인대기";
            this.CycleStatusBadge.Tag = "warning";
            this.LastCalculatedText.Text = "2026-06-23 09:15";
            this.ScheduledPayDateText.Text = "2026-06-25";
        }

        private void LoadRecentExceptions()
        {
            this.recentExceptions.Add(this.CreateException(
                "X1001", "E1003", "이철수", "근태 누락", "높음", "warning", "미처리", "warning"));
            this.recentExceptions.Add(this.CreateException(
                "X1002", "E1010", "정수민", "음수/비정상 금액", "치명", "danger", "검토중", "info"));
        }

        private void LoadDepartments()
        {
            this.departments.Add(new DepartmentSummaryRow("인사팀", 12, 96000000L, 3200000L, 0));
            this.departments.Add(new DepartmentSummaryRow("재무팀", 18, 152000000L, 5400000L, 1));
            this.departments.Add(new DepartmentSummaryRow("개발팀", 46, 388000000L, 18600000L, 1));
            this.departments.Add(new DepartmentSummaryRow("영업팀", 34, 286000000L, 9100000L, 0));
            this.departments.Add(new DepartmentSummaryRow("총무팀", 18, 132000000L, 2400000L, 0));
        }

        // ----- Monthly labor-cost bar chart -----

        private void BuildChart()
        {
            string[] months = new string[] { "2026-01", "2026-02", "2026-03", "2026-04", "2026-05", "2026-06" };
            long[] amounts = new long[] { 780000000L, 805000000L, 792000000L, 838000000L, 815000000L, 854000000L };

            long maxAmount = 0L;
            foreach (long amount in amounts)
            {
                if (amount > maxAmount)
                {
                    maxAmount = amount;
                }
            }

            Brush barBrush = this.TryFindResource("Brush.Accent") as Brush;
            Brush amountBrush = this.TryFindResource("Brush.TextPrimary") as Brush;
            Brush labelBrush = this.TryFindResource("Brush.TextSecondary") as Brush;
            CornerRadius barRadius = new CornerRadius(4, 4, 0, 0);

            this.ChartHost.Children.Clear();
            for (int index = 0; index < months.Length; index = index + 1)
            {
                double ratio = maxAmount > 0 ? (double)amounts[index] / (double)maxAmount : 0.0;
                double barHeight = ratio * ChartBarMaxHeight;
                if (barHeight < 6.0)
                {
                    barHeight = 6.0;
                }

                this.ChartHost.Children.Add(this.CreateChartColumn(
                    months[index], amounts[index], barHeight,
                    barBrush, barRadius, amountBrush, labelBrush));
            }
        }

        private FrameworkElement CreateChartColumn(
            string month,
            long amount,
            double barHeight,
            Brush barBrush,
            CornerRadius barRadius,
            Brush amountBrush,
            Brush labelBrush)
        {
            Grid column = new Grid();
            column.Width = 88.0;
            column.Margin = new Thickness(0, 0, 8, 0);
            column.RowDefinitions.Add(this.AutoRow());
            column.RowDefinitions.Add(this.StarRow());
            column.RowDefinitions.Add(this.AutoRow());

            TextBlock amountText = new TextBlock();
            amountText.Text = this.ToEokText(amount);
            amountText.FontSize = 11.0;
            amountText.FontWeight = FontWeights.SemiBold;
            amountText.Foreground = amountBrush;
            amountText.HorizontalAlignment = HorizontalAlignment.Center;
            amountText.Margin = new Thickness(0, 0, 0, 6);
            Grid.SetRow(amountText, 0);
            column.Children.Add(amountText);

            Border bar = new Border();
            bar.Width = 44.0;
            bar.Height = barHeight;
            bar.Background = barBrush;
            bar.CornerRadius = barRadius;
            bar.VerticalAlignment = VerticalAlignment.Bottom;
            bar.HorizontalAlignment = HorizontalAlignment.Center;
            bar.ToolTip = month + ": ₩" + amount.ToString("#,0");
            Grid.SetRow(bar, 1);
            column.Children.Add(bar);

            TextBlock monthText = new TextBlock();
            monthText.Text = month;
            monthText.FontSize = 11.0;
            monthText.Foreground = labelBrush;
            monthText.HorizontalAlignment = HorizontalAlignment.Center;
            monthText.Margin = new Thickness(0, 8, 0, 0);
            Grid.SetRow(monthText, 2);
            column.Children.Add(monthText);

            return column;
        }

        private RowDefinition AutoRow()
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            return row;
        }

        private RowDefinition StarRow()
        {
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Star);
            return row;
        }

        /// <summary>Compact currency for chart labels, e.g. "8.5억".</summary>
        private string ToEokText(long amount)
        {
            double eok = (double)amount / 100000000.0;
            return eok.ToString("0.0") + "억";
        }

        // ----- Interaction -----

        private void ExceptionListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExceptionItem selected = this.ExceptionListBox.SelectedItem as ExceptionItem;
            if (selected == null)
            {
                return;
            }

            EventHandler handler = this.ExceptionSelected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return;
            }

            MessageBox.Show(
                "승인·예외처리 화면으로 이동합니다: " + selected.NameWithId,
                "오류·예외", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ExceptionItem CreateException(
            string exceptionId,
            string employeeId,
            string employeeName,
            string typeLabel,
            string severityLabel,
            string severityTone,
            string statusLabel,
            string statusTone)
        {
            ExceptionItem item = new ExceptionItem();
            item.ExceptionId = exceptionId;
            item.EmployeeId = employeeId;
            item.EmployeeName = employeeName;
            item.TypeLabel = typeLabel;
            item.SeverityLabel = severityLabel;
            item.SeverityTone = severityTone;
            item.StatusLabel = statusLabel;
            item.StatusTone = statusTone;
            return item;
        }
    }
}
