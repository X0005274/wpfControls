using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Screens
{
    /// <summary>
    /// WPF version of the 사원 조회 (employee browser) screen, styled to match
    /// 승인·예외 / 급여계산: header → 조회 조건(autocomplete 이름 ComboBox + 조회)
    /// → 부서 TreeView (left) / 사원 목록 DataGrid (right).
    /// 재직상태 (재직/휴직/퇴직) renders with the same status badge used elsewhere:
    /// 재직=success, 휴직=warning(높음), 퇴직=danger(치명). This is a UI shell only.
    /// </summary>
    public partial class EmployeeBrowserScreen : UserControl
    {
        private const string AllNode = "전체 부서";

        private readonly List<EmployeeBrowserRow> employees;
        private readonly ObservableCollection<EmployeeBrowserRow> displayed;
        private readonly ObservableCollection<string> allNames;
        private ICollectionView namesView;
        private string nameFilterText;
        private bool suppressNameFilter;
        private TextBox nameEditBox;
        private readonly string storePath;

        public EmployeeBrowserScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.employees = new List<EmployeeBrowserRow>();
            this.displayed = new ObservableCollection<EmployeeBrowserRow>();
            this.allNames = new ObservableCollection<string>();
            this.nameFilterText = string.Empty;
            this.suppressNameFilter = false;
            this.storePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "com.example", "employees.tsv");
            this.EmployeeGrid.ItemsSource = this.displayed;
            this.LoadEmployees();
            this.BuildDepartmentTree();
            this.PopulateNameCombo();
            this.ShowEmployeesForDepartment(AllNode);

            // Filter the 이름 dropdown as the user types into the editable ComboBox.
            this.NameCombo.AddHandler(
                TextBoxBase.TextChangedEvent,
                new TextChangedEventHandler(this.NameCombo_TextChanged));
        }

        /// <summary>Exposes the rows currently shown in the list.</summary>
        public ObservableCollection<EmployeeBrowserRow> Displayed
        {
            get { return this.displayed; }
        }

        private void ApplyBackground()
        {
            Brush background = this.TryFindResource("Brush.Background") as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        /// <summary>
        /// Loads employees from the persisted store, or seeds the sample data (and
        /// writes it) on first run so edits/deletes survive across app restarts.
        /// </summary>
        private void LoadEmployees()
        {
            if (this.TryLoadFromFile())
            {
                return;
            }

            this.LoadSampleData();
            this.SaveToFile();
        }

        private bool TryLoadFromFile()
        {
            try
            {
                if (!File.Exists(this.storePath))
                {
                    return false;
                }

                string[] lines = File.ReadAllLines(this.storePath, Encoding.UTF8);
                List<EmployeeBrowserRow> loaded = new List<EmployeeBrowserRow>();
                foreach (string line in lines)
                {
                    if (line.Length == 0)
                    {
                        continue;
                    }

                    string[] parts = line.Split('\t');
                    if (parts.Length >= 7)
                    {
                        loaded.Add(this.NewRow(
                            parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]));
                    }
                }

                if (loaded.Count == 0)
                {
                    return false;
                }

                this.employees.Clear();
                this.employees.AddRange(loaded);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>Persists the current employee list so edits/deletes are kept.</summary>
        private void SaveToFile()
        {
            try
            {
                string directory = Path.GetDirectoryName(this.storePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                List<string> lines = new List<string>();
                foreach (EmployeeBrowserRow row in this.employees)
                {
                    lines.Add(string.Join("\t", new string[]
                    {
                        row.EmployeeId, row.Name, row.Position, row.EmploymentType,
                        row.HireDate, row.StatusLabel, row.Department
                    }));
                }

                File.WriteAllLines(this.storePath, lines, Encoding.UTF8);
            }
            catch (IOException)
            {
                // Persistence is best-effort in this shell; ignore write failures.
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private void LoadSampleData()
        {
            this.employees.Add(this.NewRow("E1001", "홍길동", "과장", "정규직", "2019-03-02", "재직", "인사팀"));
            this.employees.Add(this.NewRow("E1006", "강하늘", "사원", "정규직", "2025-02-01", "재직", "인사팀"));
            this.employees.Add(this.NewRow("E1002", "김영희", "대리", "정규직", "2021-07-15", "재직", "재무팀"));
            this.employees.Add(this.NewRow("E1010", "정수민", "사원", "계약직", "2022-09-01", "휴직", "재무팀"));
            this.employees.Add(this.NewRow("E1003", "이철수", "사원", "계약직", "2023-01-09", "휴직", "개발팀"));
            this.employees.Add(this.NewRow("E1005", "최지은", "선임", "임시직", "2024-05-02", "퇴직", "개발팀"));
            this.employees.Add(this.NewRow("E1021", "한지우", "책임", "정규직", "2018-04-10", "재직", "개발팀"));
            this.employees.Add(this.NewRow("E1004", "박민수", "차장", "정규직", "2017-11-20", "재직", "영업팀"));
            this.employees.Add(this.NewRow("E1007", "오세훈", "사원", "정규직", "2020-06-15", "퇴직", "영업팀"));
            this.employees.Add(this.NewRow("E1008", "서지훈", "대리", "정규직", "2019-08-22", "재직", "총무팀"));
        }

        private EmployeeBrowserRow NewRow(
            string employeeId,
            string name,
            string position,
            string employmentType,
            string hireDate,
            string status,
            string department)
        {
            EmployeeBrowserRow row = new EmployeeBrowserRow();
            row.EmployeeId = employeeId;
            row.Name = name;
            row.Position = position;
            row.EmploymentType = employmentType;
            row.HireDate = hireDate;
            row.StatusLabel = status;
            row.StatusTone = EmployeeBrowserRow.ToneForStatus(status);
            row.Department = department;
            return row;
        }

        private void BuildDepartmentTree()
        {
            this.DepartmentTree.Items.Clear();

            TreeViewItem root = new TreeViewItem();
            root.Header = AllNode;
            root.Tag = AllNode;
            root.IsExpanded = true;

            List<string> departments = new List<string>();
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (!departments.Contains(row.Department))
                {
                    departments.Add(row.Department);
                }
            }

            foreach (string department in departments)
            {
                TreeViewItem node = new TreeViewItem();
                node.Header = department + " (" + this.CountInDepartment(department) + ")";
                node.Tag = department;
                root.Items.Add(node);
            }

            this.DepartmentTree.Items.Add(root);
            root.IsSelected = true;
        }

        private int CountInDepartment(string department)
        {
            int count = 0;
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (row.Department == department)
                {
                    count = count + 1;
                }
            }

            return count;
        }

        private void PopulateNameCombo()
        {
            this.allNames.Clear();
            foreach (EmployeeBrowserRow row in this.employees)
            {
                this.allNames.Add(row.Name);
            }

            this.NameCombo.ItemsSource = this.allNames;
            this.namesView = CollectionViewSource.GetDefaultView(this.allNames);
            this.namesView.Filter = this.NameFilter;
        }

        private bool NameFilter(object item)
        {
            if (string.IsNullOrEmpty(this.nameFilterText))
            {
                return true;
            }

            string name = item as string;
            return name != null
                && name.IndexOf(this.nameFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void NameCombo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.suppressNameFilter || this.namesView == null)
            {
                return;
            }

            // Read the inner editable TextBox, NOT ComboBox.Text: while a Korean (IME)
            // syllable is being composed, ComboBox.Text is not updated yet, so the
            // filter would lag a keystroke behind. The TextBox reflects the live text.
            if (this.nameEditBox == null)
            {
                this.nameEditBox = e.OriginalSource as TextBox;
            }

            // Filter immediately (synchronous) so typing "김" filters right away.
            this.nameFilterText = this.CurrentNameText();
            this.namesView.Refresh();

            // Only the dropdown-open is deferred: opening it synchronously during IME
            // composition can cancel the in-progress Hangul syllable.
            if (this.nameFilterText.Length > 0 && !this.NameCombo.IsDropDownOpen)
            {
                this.Dispatcher.BeginInvoke(new Action(this.OpenNameDropdown), DispatcherPriority.Background);
            }
        }

        private string CurrentNameText()
        {
            if (this.nameEditBox != null)
            {
                return this.nameEditBox.Text;
            }

            return this.NameCombo.Text == null ? string.Empty : this.NameCombo.Text;
        }

        private void OpenNameDropdown()
        {
            if (!this.NameCombo.IsDropDownOpen && this.nameFilterText.Length > 0)
            {
                this.NameCombo.IsDropDownOpen = true;
            }
        }

        private void NameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When an item is picked, close the dropdown and skip the reopen that the
            // resulting text change would otherwise trigger.
            if (this.NameCombo.SelectedItem == null)
            {
                return;
            }

            this.suppressNameFilter = true;
            this.NameCombo.IsDropDownOpen = false;
            this.Dispatcher.BeginInvoke(
                new Action(this.ClearNameFilterSuppression),
                DispatcherPriority.Input);
        }

        private void ClearNameFilterSuppression()
        {
            this.suppressNameFilter = false;
            this.nameFilterText = this.CurrentNameText();
            if (this.namesView != null)
            {
                this.namesView.Refresh();
            }
        }

        private void EmployeeGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EmployeeBrowserRow row = this.EmployeeGrid.SelectedItem as EmployeeBrowserRow;
            if (row == null)
            {
                return;
            }

            EmployeeDetailWindow window = new EmployeeDetailWindow(row);
            window.ShowDialog();

            if (window.Result == EmployeeDetailResult.Deleted)
            {
                // Remove the employee and rebuild the tree/names; rebuilding selects
                // the root, which refills the list without the deleted row.
                this.employees.Remove(row);
                this.BuildDepartmentTree();
                this.PopulateNameCombo();
                this.SaveToFile();
            }
            else if (window.Result == EmployeeDetailResult.Edited)
            {
                // The row was updated in place; re-render the grid cells and persist.
                this.EmployeeGrid.Items.Refresh();
                this.SaveToFile();
            }
        }

        private void ShowEmployeesForDepartment(string department)
        {
            this.displayed.Clear();
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (department == AllNode || row.Department == department)
                {
                    this.displayed.Add(row);
                }
            }
        }

        private void DepartmentTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = this.DepartmentTree.SelectedItem as TreeViewItem;
            if (item != null)
            {
                string department = item.Tag as string;
                if (department != null)
                {
                    this.ShowEmployeesForDepartment(department);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = this.CurrentNameText().Trim();
            if (query.Length == 0)
            {
                this.SelectDepartmentNode(AllNode);
                return;
            }

            EmployeeBrowserRow match = null;
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (string.Equals(row.Name, query, StringComparison.OrdinalIgnoreCase))
                {
                    match = row;
                    break;
                }
            }

            if (match == null)
            {
                MessageBox.Show(
                    "'" + query + "' 사원을 찾을 수 없습니다.",
                    "조회", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Select the matching department node (fills the list), then highlight the row.
            this.SelectDepartmentNode(match.Department);
            this.SelectEmployeeRow(match.EmployeeId);
        }

        private void SelectDepartmentNode(string department)
        {
            if (this.DepartmentTree.Items.Count == 0)
            {
                return;
            }

            TreeViewItem root = this.DepartmentTree.Items[0] as TreeViewItem;
            if (root == null)
            {
                return;
            }

            if (department == AllNode)
            {
                root.IsSelected = true;
                return;
            }

            foreach (object child in root.Items)
            {
                TreeViewItem node = child as TreeViewItem;
                if (node != null && string.Equals(node.Tag as string, department))
                {
                    node.IsSelected = true;
                    return;
                }
            }

            root.IsSelected = true;
        }

        private void SelectEmployeeRow(string employeeId)
        {
            foreach (EmployeeBrowserRow row in this.displayed)
            {
                if (row.EmployeeId == employeeId)
                {
                    this.EmployeeGrid.SelectedItem = row;
                    this.EmployeeGrid.ScrollIntoView(row);
                    return;
                }
            }
        }

        private void EmployeeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
