using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using com.example.Models.Ui;

namespace com.example.Demo
{
    /// <summary>
    /// Demonstrates placing WPF controls individually on a WinForms Form. Each WPF
    /// control (ComboBox / TreeView / DataGrid) is created in code and assigned as
    /// the Child of its own ElementHost (laid out by the designer). WPF types are
    /// fully qualified because their short names collide with WinForms controls.
    /// </summary>
    public partial class WpfOnFormDemo : Form
    {
        private const string AllNode = "전체 부서";

        private readonly List<EmployeeBrowserRow> employees;
        private readonly ObservableCollection<EmployeeBrowserRow> displayed;

        private System.Windows.Controls.ComboBox nameCombo;
        private System.Windows.Controls.TreeView deptTree;
        private System.Windows.Controls.DataGrid grid;

        public WpfOnFormDemo()
        {
            this.InitializeComponent();
            this.employees = new List<EmployeeBrowserRow>();
            this.displayed = new ObservableCollection<EmployeeBrowserRow>();
            this.LoadSampleData();
            this.BuildWpfControls();
            this.BuildDepartmentTree();
            this.PopulateNameCombo();
            this.ShowEmployeesForDepartment(AllNode);
        }

        /// <summary>Creates each WPF control and drops it onto its ElementHost.</summary>
        private void BuildWpfControls()
        {
            // 1) WPF ComboBox on comboHost
            this.nameCombo = new System.Windows.Controls.ComboBox();
            this.nameCombo.IsEditable = true;
            this.nameCombo.IsTextSearchEnabled = true;
            this.nameCombo.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            this.nameCombo.FontSize = 13;
            this.nameCombo.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            this.comboHost.Child = this.nameCombo;

            // 2) WPF TreeView on treeHost
            this.deptTree = new System.Windows.Controls.TreeView();
            this.deptTree.BorderThickness = new System.Windows.Thickness(0);
            this.deptTree.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            this.deptTree.FontSize = 13;
            this.deptTree.SelectedItemChanged += this.DeptTree_SelectedItemChanged;
            this.treeHost.Child = this.deptTree;

            // 3) WPF DataGrid on gridHost
            this.grid = new System.Windows.Controls.DataGrid();
            this.grid.AutoGenerateColumns = false;
            this.grid.IsReadOnly = true;
            this.grid.HeadersVisibility = System.Windows.Controls.DataGridHeadersVisibility.Column;
            this.grid.CanUserAddRows = false;
            this.grid.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            this.grid.FontSize = 13;
            this.grid.SelectionMode = System.Windows.Controls.DataGridSelectionMode.Single;
            this.grid.SelectionUnit = System.Windows.Controls.DataGridSelectionUnit.FullRow;
            this.grid.GridLinesVisibility = System.Windows.Controls.DataGridGridLinesVisibility.Horizontal;
            this.AddColumn("사번", "EmployeeId");
            this.AddColumn("이름", "Name");
            this.AddColumn("부서", "Department");
            this.AddColumn("직급/직책", "Position");
            this.AddColumn("고용형태", "EmploymentType");
            this.AddColumn("입사일", "HireDate");
            this.AddColumn("재직상태", "StatusLabel");
            this.grid.ItemsSource = this.displayed;
            this.gridHost.Child = this.grid;
        }

        private void AddColumn(string header, string path)
        {
            System.Windows.Controls.DataGridTextColumn column = new System.Windows.Controls.DataGridTextColumn();
            column.Header = header;
            column.Binding = new System.Windows.Data.Binding(path);
            this.grid.Columns.Add(column);
        }

        private void BuildDepartmentTree()
        {
            this.deptTree.Items.Clear();

            System.Windows.Controls.TreeViewItem root = new System.Windows.Controls.TreeViewItem();
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
                System.Windows.Controls.TreeViewItem node = new System.Windows.Controls.TreeViewItem();
                node.Header = department + " (" + this.CountInDepartment(department) + ")";
                node.Tag = department;
                root.Items.Add(node);
            }

            this.deptTree.Items.Add(root);
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
            this.nameCombo.Items.Clear();
            foreach (EmployeeBrowserRow row in this.employees)
            {
                this.nameCombo.Items.Add(row.Name);
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

        private void DeptTree_SelectedItemChanged(
            object sender,
            System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            System.Windows.Controls.TreeViewItem item =
                this.deptTree.SelectedItem as System.Windows.Controls.TreeViewItem;
            if (item != null)
            {
                string department = item.Tag as string;
                if (department != null)
                {
                    this.ShowEmployeesForDepartment(department);
                }
            }
        }

        private void SearchButton_Click(object sender, System.EventArgs e)
        {
            string query = this.nameCombo.Text == null ? string.Empty : this.nameCombo.Text.Trim();
            if (query.Length == 0)
            {
                this.SelectDepartmentNode(AllNode);
                return;
            }

            EmployeeBrowserRow match = null;
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (string.Equals(row.Name, query, System.StringComparison.OrdinalIgnoreCase))
                {
                    match = row;
                    break;
                }
            }

            if (match == null)
            {
                MessageBox.Show("'" + query + "' 사원을 찾을 수 없습니다.",
                    "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.SelectDepartmentNode(match.Department);
            this.SelectGridRow(match.EmployeeId);
        }

        private void SelectDepartmentNode(string department)
        {
            if (this.deptTree.Items.Count == 0)
            {
                return;
            }

            System.Windows.Controls.TreeViewItem root =
                this.deptTree.Items[0] as System.Windows.Controls.TreeViewItem;
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
                System.Windows.Controls.TreeViewItem node = child as System.Windows.Controls.TreeViewItem;
                if (node != null && string.Equals(node.Tag as string, department))
                {
                    node.IsSelected = true;
                    return;
                }
            }
        }

        private void SelectGridRow(string employeeId)
        {
            foreach (EmployeeBrowserRow row in this.displayed)
            {
                if (row.EmployeeId == employeeId)
                {
                    this.grid.SelectedItem = row;
                    this.grid.ScrollIntoView(row);
                    return;
                }
            }
        }

        private void LoadSampleData()
        {
            this.employees.Add(this.NewRow("E1001", "홍길동", "과장", "정규직", "2019-03-02", "재직", "인사팀"));
            this.employees.Add(this.NewRow("E1002", "김영희", "대리", "정규직", "2021-07-15", "재직", "재무팀"));
            this.employees.Add(this.NewRow("E1010", "정수민", "사원", "계약직", "2022-09-01", "휴직", "재무팀"));
            this.employees.Add(this.NewRow("E1003", "이철수", "사원", "계약직", "2023-01-09", "휴직", "개발팀"));
            this.employees.Add(this.NewRow("E1005", "최지은", "선임", "임시직", "2024-05-02", "퇴직", "개발팀"));
            this.employees.Add(this.NewRow("E1021", "한지우", "책임", "정규직", "2018-04-10", "재직", "개발팀"));
            this.employees.Add(this.NewRow("E1004", "박민수", "차장", "정규직", "2017-11-20", "재직", "영업팀"));
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
    }
}
