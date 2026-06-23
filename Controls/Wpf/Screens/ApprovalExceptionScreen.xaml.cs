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
    /// Shell of the hr_payroll_system 승인·예외처리 (approval / exception) screen.
    /// Layout mirrors the web screen: header (title + 신규등록) → 검색 필터바
    /// → 3-column board (예외 목록 / 상세 / 조치) → 감사 이력.
    /// Search/Reset filter the in-memory placeholder items; selecting an item fills
    /// the detail panel; 조치 실행 appends an audit-log row and updates the item's
    /// status; 신규등록 raises <see cref="CreateRequested"/> for a host and falls
    /// back to appending a placeholder item when no host is listening.
    /// </summary>
    public partial class ApprovalExceptionScreen : UserControl
    {
        private const string AllOption = "전체";
        private const string SelectPrompt = "선택하세요";

        private readonly List<ExceptionItem> allItems;
        private readonly ObservableCollection<ExceptionItem> items;
        private readonly ObservableCollection<AuditLogRow> auditLogs;
        private int generatedSequence;

        /// <summary>
        /// Raised when 신규등록 is clicked. A host can subscribe to open a
        /// registration screen; when there is no subscriber the shell appends a
        /// placeholder item instead so the button still demonstrates behavior.
        /// </summary>
        public event EventHandler CreateRequested;

        public ApprovalExceptionScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.allItems = new List<ExceptionItem>();
            this.items = new ObservableCollection<ExceptionItem>();
            this.auditLogs = new ObservableCollection<AuditLogRow>();
            this.generatedSequence = 0;
            this.ExceptionListBox.ItemsSource = this.items;
            this.AuditGrid.ItemsSource = this.auditLogs;
            this.LoadPlaceholderItems();
            this.ApplyFilter();
        }

        /// <summary>Exposes the filtered exception list bound to the picker.</summary>
        public ObservableCollection<ExceptionItem> Items
        {
            get { return this.items; }
        }

        /// <summary>Exposes the audit-log rows bound to the grid.</summary>
        public ObservableCollection<AuditLogRow> AuditLogs
        {
            get { return this.auditLogs; }
        }

        /// <summary>
        /// Applies the page background token in code-behind (after
        /// InitializeComponent) because a StaticResource on the root element is
        /// evaluated before the merged Tokens.xaml dictionary is loaded.
        /// </summary>
        private void ApplyBackground()
        {
            object resource = this.TryFindResource("Brush.Background");
            Brush background = resource as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        private void LoadPlaceholderItems()
        {
            this.allItems.Add(this.CreateItem(
                "X1001", "E1003", "이철수", "D300", "근태 누락", "높음", "warning",
                "미처리", "warning", "5월 22일 출근 기록이 없습니다.",
                "근무시간 0h", "근무시간 8h", "₩120,000", "-"));
            this.allItems.Add(this.CreateItem(
                "X1002", "E1010", "정수민", "D100", "음수/비정상 금액", "치명", "danger",
                "검토중", "info", "공제 후 실지급액이 음수입니다.",
                "-₩45,000", "₩0", "₩45,000", "공제 항목 재확인 필요"));
            this.allItems.Add(this.CreateItem(
                "X1003", "E1021", "한지우", "D200", "계좌정보 오류", "중간", "info",
                "보류", "neutral", "지급 계좌번호 형식이 올바르지 않습니다.",
                "110-XXX", "110-234-567890", "₩0", "-"));
            this.allItems.Add(this.CreateItem(
                "X1004", "E1007", "오세훈", "D100", "승인 안 된 연장근무", "경미", "neutral",
                "처리완료", "success", "연장근무 12시간이 사전 승인되지 않았습니다.",
                "연장 0h", "연장 12h", "₩96,000", "팀장 사후 승인 완료"));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.ApplyFilter();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.DepartmentCodeBox.Text = string.Empty;
            this.KeywordBox.Text = string.Empty;
            this.ExceptionTypeCombo.SelectedIndex = 0;
            this.SeverityCombo.SelectedIndex = 0;
            this.StatusCombo.SelectedIndex = 0;
            this.ApplyFilter();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = this.CreateRequested;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
                return;
            }

            this.generatedSequence = this.generatedSequence + 1;
            string newId = "X9" + this.generatedSequence.ToString("D3");
            ExceptionItem item = this.CreateItem(
                newId, "E9999", "신규대상", "D100", "근태 누락", "중간", "info",
                "미처리", "warning", "신규 등록된 예외입니다.",
                "-", "-", "₩0", "-");
            this.allItems.Insert(0, item);
            this.ApplyFilter();
        }

        /// <summary>Rebuilds the displayed list from the master set using the filter inputs.</summary>
        private void ApplyFilter()
        {
            string departmentText = this.DepartmentCodeBox.Text.Trim();
            string keyword = this.KeywordBox.Text.Trim();
            string type = this.GetSelectedComboText(this.ExceptionTypeCombo);
            string severity = this.GetSelectedComboText(this.SeverityCombo);
            string status = this.GetSelectedComboText(this.StatusCombo);

            this.items.Clear();
            foreach (ExceptionItem item in this.allItems)
            {
                if (this.Matches(item, departmentText, keyword, type, severity, status))
                {
                    this.items.Add(item);
                }
            }

            this.ListEmptyText.Visibility = this.items.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;

            // Auto-select the first item, matching the web screen behavior.
            if (this.items.Count > 0)
            {
                this.ExceptionListBox.SelectedItem = this.items[0];
            }
            else
            {
                this.ExceptionListBox.SelectedItem = null;
                this.ShowDetail(null);
            }
        }

        private bool Matches(
            ExceptionItem item,
            string departmentText,
            string keyword,
            string type,
            string severity,
            string status)
        {
            if (departmentText.Length > 0
                && item.DepartmentCode.IndexOf(departmentText, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return false;
            }

            if (keyword.Length > 0
                && item.EmployeeId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0
                && item.EmployeeName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return false;
            }

            if (type.Length > 0 && type != AllOption && item.TypeLabel != type)
            {
                return false;
            }

            if (severity.Length > 0 && severity != AllOption && item.SeverityLabel != severity)
            {
                return false;
            }

            if (status.Length > 0 && status != AllOption && item.StatusLabel != status)
            {
                return false;
            }

            return true;
        }

        private void ExceptionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExceptionItem selected = this.ExceptionListBox.SelectedItem as ExceptionItem;
            this.ShowDetail(selected);
        }

        private void ShowDetail(ExceptionItem item)
        {
            if (item == null)
            {
                this.DetailEmptyPanel.Visibility = Visibility.Visible;
                this.DetailContent.Visibility = Visibility.Collapsed;
                return;
            }

            this.DetailEmptyPanel.Visibility = Visibility.Collapsed;
            this.DetailContent.Visibility = Visibility.Visible;

            this.DetailName.Text = item.NameWithId;
            this.DetailType.Text = item.TypeLabel;
            this.DetailSeverityText.Text = item.SeverityLabel;
            this.DetailSeverityBadge.Tag = item.SeverityTone;
            this.DetailStatusText.Text = item.StatusLabel;
            this.DetailStatusBadge.Tag = item.StatusTone;
            this.DetailReason.Text = item.Reason;
            this.DetailBefore.Text = item.BeforeValue;
            this.DetailAfter.Text = item.AfterValue;
            this.DetailImpact.Text = item.ImpactAmount;
            this.DetailComment.Text = item.Comment;
        }

        private void SubmitActionButton_Click(object sender, RoutedEventArgs e)
        {
            ExceptionItem selected = this.ExceptionListBox.SelectedItem as ExceptionItem;
            if (selected == null)
            {
                MessageBox.Show("먼저 예외를 선택하세요.", "조치", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string action = this.GetSelectedComboText(this.ActionCombo);
            string reasonCode = this.GetSelectedComboText(this.ReasonCodeCombo);
            if (action.Length == 0 || action == SelectPrompt
                || reasonCode.Length == 0 || reasonCode == SelectPrompt)
            {
                MessageBox.Show("조치와 사유코드를 선택하세요.", "조치", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Append an audit-log row capturing who/when/what.
            AuditLogRow log = new AuditLogRow();
            log.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            log.Actor = "관리자";
            log.Action = action;
            log.ReasonCode = reasonCode;
            log.BeforeValue = selected.StatusLabel;
            this.ApplyActionToStatus(selected, action);
            log.AfterValue = selected.StatusLabel;
            log.Comment = this.ActionCommentBox.Text.Trim().Length > 0
                ? this.ActionCommentBox.Text.Trim()
                : "-";
            this.auditLogs.Insert(0, log);

            // Reflect the new status in the detail panel and reset the form.
            this.ShowDetail(selected);
            this.ActionCombo.SelectedIndex = 0;
            this.ReasonCodeCombo.SelectedIndex = 0;
            this.ActionCommentBox.Text = string.Empty;
        }

        /// <summary>Maps a chosen action to the resulting status label/tone.</summary>
        private void ApplyActionToStatus(ExceptionItem item, string action)
        {
            if (action == "승인" || action == "검토완료")
            {
                item.StatusLabel = "처리완료";
                item.StatusTone = "success";
            }
            else if (action == "반려")
            {
                item.StatusLabel = "반려";
                item.StatusTone = "danger";
            }
            else if (action == "보류")
            {
                item.StatusLabel = "보류";
                item.StatusTone = "neutral";
            }
            else if (action == "수정요청")
            {
                item.StatusLabel = "검토중";
                item.StatusTone = "info";
            }
        }

        private string GetSelectedComboText(ComboBox comboBox)
        {
            ComboBoxItem selected = comboBox.SelectedItem as ComboBoxItem;
            if (selected == null || selected.Content == null)
            {
                return string.Empty;
            }

            return selected.Content.ToString();
        }

        private ExceptionItem CreateItem(
            string exceptionId,
            string employeeId,
            string employeeName,
            string departmentCode,
            string typeLabel,
            string severityLabel,
            string severityTone,
            string statusLabel,
            string statusTone,
            string reason,
            string beforeValue,
            string afterValue,
            string impactAmount,
            string comment)
        {
            ExceptionItem item = new ExceptionItem();
            item.ExceptionId = exceptionId;
            item.EmployeeId = employeeId;
            item.EmployeeName = employeeName;
            item.DepartmentCode = departmentCode;
            item.TypeLabel = typeLabel;
            item.SeverityLabel = severityLabel;
            item.SeverityTone = severityTone;
            item.StatusLabel = statusLabel;
            item.StatusTone = statusTone;
            item.Reason = reason;
            item.BeforeValue = beforeValue;
            item.AfterValue = afterValue;
            item.ImpactAmount = impactAmount;
            item.Comment = comment;
            return item;
        }
    }
}
