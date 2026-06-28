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
    /// Shell of the hr_payroll_system 직원관리 (employee management) list screen.
    /// Layout mirrors the web screen: header (title + 엑셀다운로드/신규등록) → 검색 필터바
    /// → 결과 개수 → 데이터 테이블 → 페이지네이션.
    /// Search/Reset filter the in-memory placeholder rows; 신규등록 raises
    /// <see cref="CreateRequested"/> for a host (e.g. to open a registration screen),
    /// and falls back to appending a placeholder row when no host is listening.
    /// </summary>
    public partial class EmployeeManagementScreen : UserControl
    {
        private const string AllOption = "전체";

        private readonly List<EmployeeRow> allRows;
        private readonly ObservableCollection<EmployeeRow> rows;

        /// <summary>
        /// Raised when 신규등록 is clicked. A host can subscribe to open a
        /// registration screen; when there is no subscriber the shell appends a
        /// placeholder row instead so the button still demonstrates behavior.
        /// </summary>
        public event EventHandler CreateRequested;

        public EmployeeManagementScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.allRows = new List<EmployeeRow>();
            this.rows = new ObservableCollection<EmployeeRow>();
            this.EmployeeGrid.ItemsSource = this.rows;
            this.LoadPlaceholderRows();
            this.ApplyFilter();
        }

        /// <summary>
        /// Exposes the filtered view bound to the grid so a host can inspect the
        /// currently displayed rows.
        /// </summary>
        public ObservableCollection<EmployeeRow> Rows
        {
            get { return this.rows; }
        }

        /// <summary>
        /// Applies the page background token. This is done in code-behind (after
        /// InitializeComponent) rather than as a root-element XAML attribute,
        /// because a StaticResource on the root element is evaluated before the
        /// merged Tokens.xaml dictionary is loaded and would throw.
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

        private void LoadPlaceholderRows()
        {
            this.allRows.Add(this.CreateRow("E1001", "홍길동", "인사팀", "과장", "정규직", "2019-03-02", "재직", "대상"));
            this.allRows.Add(this.CreateRow("E1002", "김영희", "재무팀", "대리", "정규직", "2021-07-15", "재직", "대상"));
            this.allRows.Add(this.CreateRow("E1003", "이철수", "개발팀", "사원", "계약직", "2023-01-09", "휴직", "제외"));
            this.allRows.Add(this.CreateRow("E1004", "박민수", "영업팀", "차장", "정규직", "2017-11-20", "재직", "대상"));
            this.allRows.Add(this.CreateRow("E1005", "최지은", "개발팀", "선임", "임시직", "2024-05-02", "퇴직", "제외"));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.ApplyFilter();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.KeywordBox.Text = string.Empty;
            this.DepartmentCodeBox.Text = string.Empty;
            this.SiteCodeBox.Text = string.Empty;
            this.EmploymentStatusCombo.SelectedIndex = 0;
            this.EmploymentTypeCombo.SelectedIndex = 0;
            this.HireDateFromPicker.SelectedDate = null;
            this.HireDateToPicker.SelectedDate = null;
            this.ApplyFilter();
        }

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

        // ----- Create page (직원 신규등록) -----

        private void ShowCreatePage()
        {
            this.ResetCreateForm();
            this.ListPage.Visibility = Visibility.Collapsed;
            this.CreatePage.Visibility = Visibility.Visible;
        }

        private void ShowListPage()
        {
            this.CreatePage.Visibility = Visibility.Collapsed;
            this.ListPage.Visibility = Visibility.Visible;
        }

        private void ResetCreateForm()
        {
            this.NewIdBox.Text = string.Empty;
            this.NewNameBox.Text = string.Empty;
            this.NewDeptCombo.SelectedIndex = 0;
            this.NewPositionCombo.SelectedIndex = 0;
            this.NewTypeCombo.SelectedIndex = 0;
            this.NewHireDatePicker.SelectedDate = null;
            this.NewStatusCombo.SelectedIndex = 0;
            this.NewBankBox.Text = string.Empty;
            this.NewTaxBox.Text = "GENERAL";
            this.NewDependentBox.Text = "1";
            this.NewEmailBox.Text = string.Empty;
            this.NewPhoneBox.Text = string.Empty;
            this.NewPayrollCheck.IsChecked = true;
            this.PhotoNameText.Text = "선택된 파일 없음";
        }

        private void CheckIdButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NewIdBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("사번을 입력하세요.", "중복확인", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show("사용 가능한 사번입니다.", "중복확인", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SuggestIdButton_Click(object sender, RoutedEventArgs e)
        {
            // Propose the next id after the highest existing E-number.
            int max = 1000;
            foreach (EmployeeRow row in this.allRows)
            {
                string id = row.EmployeeId;
                if (id.Length > 1 && id[0] == 'E')
                {
                    int parsed;
                    if (int.TryParse(id.Substring(1), out parsed) && parsed > max)
                    {
                        max = parsed;
                    }
                }
            }

            this.NewIdBox.Text = "E" + (max + 1).ToString();
        }

        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {
            // Shell: a real host would open an OpenFileDialog (≤600KB image).
            MessageBox.Show("로컬 이미지 파일을 선택합니다. (셸 데모)", "사진 선택",
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.PhotoNameText.Text = "employee_photo.png";
        }

        private void CancelCreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.ShowListPage();
        }

        private void SaveCreateButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeId = this.NewIdBox.Text.Trim();
            string employeeName = this.NewNameBox.Text.Trim();
            string department = this.GetSelectedComboText(this.NewDeptCombo);
            string position = this.GetSelectedComboText(this.NewPositionCombo);
            string type = this.GetSelectedComboText(this.NewTypeCombo);
            string status = this.GetSelectedComboText(this.NewStatusCombo);
            string hireDate = this.GetDateText(this.NewHireDatePicker);

            if (employeeId.Length == 0
                || employeeName.Length == 0
                || department == "선택"
                || type == "선택"
                || hireDate.Length == 0)
            {
                MessageBox.Show(
                    "필수 항목(사번, 이름, 부서, 고용형태, 입사일)을 입력하세요.",
                    "직원 등록", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string payrollTarget = this.NewPayrollCheck.IsChecked == true ? "대상" : "제외";
            EmployeeRow row = this.CreateRow(
                employeeId,
                employeeName,
                department,
                position == "선택" ? string.Empty : position,
                type,
                hireDate,
                status,
                payrollTarget);
            this.allRows.Insert(0, row);

            this.ShowListPage();
            this.ApplyFilter();
        }

        /// <summary>
        /// Rebuilds the displayed rows from <see cref="allRows"/> using the current
        /// filter inputs, then updates the total-count label.
        /// </summary>
        private void ApplyFilter()
        {
            string keyword = this.KeywordBox.Text.Trim();
            string departmentText = this.DepartmentCodeBox.Text.Trim();
            string status = this.GetSelectedComboText(this.EmploymentStatusCombo);
            string type = this.GetSelectedComboText(this.EmploymentTypeCombo);
            string hireFrom = this.GetDateText(this.HireDateFromPicker);
            string hireTo = this.GetDateText(this.HireDateToPicker);

            this.rows.Clear();
            foreach (EmployeeRow row in this.allRows)
            {
                if (this.Matches(row, keyword, departmentText, status, type, hireFrom, hireTo))
                {
                    this.rows.Add(row);
                }
            }

            this.TotalCountRun.Text = this.rows.Count.ToString();
        }

        private bool Matches(
            EmployeeRow row,
            string keyword,
            string departmentText,
            string status,
            string type,
            string hireFrom,
            string hireTo)
        {
            if (keyword.Length > 0
                && row.EmployeeId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0
                && row.EmployeeName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return false;
            }

            if (departmentText.Length > 0
                && row.DepartmentName.IndexOf(departmentText, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return false;
            }

            if (status.Length > 0 && status != AllOption && row.EmploymentStatus != status)
            {
                return false;
            }

            if (type.Length > 0 && type != AllOption && row.EmploymentType != type)
            {
                return false;
            }

            // HireDate is stored as yyyy-MM-dd, so ordinal string compare is a valid range check.
            if (hireFrom.Length > 0
                && string.Compare(row.HireDate, hireFrom, StringComparison.Ordinal) < 0)
            {
                return false;
            }

            if (hireTo.Length > 0
                && string.Compare(row.HireDate, hireTo, StringComparison.Ordinal) > 0)
            {
                return false;
            }

            return true;
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

        private string GetDateText(DatePicker datePicker)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                return datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }

        private EmployeeRow CreateRow(
            string employeeId,
            string employeeName,
            string departmentName,
            string positionName,
            string employmentType,
            string hireDate,
            string employmentStatus,
            string payrollTarget)
        {
            EmployeeRow row = new EmployeeRow();
            row.EmployeeId = employeeId;
            row.EmployeeName = employeeName;
            row.DepartmentName = departmentName;
            row.PositionName = positionName;
            row.EmploymentType = employmentType;
            row.HireDate = hireDate;
            row.EmploymentStatus = employmentStatus;
            row.PayrollTarget = payrollTarget;
            return row;
        }
    }
}
