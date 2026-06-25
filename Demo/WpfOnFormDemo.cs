using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using com.example.Controls.Wpf.Screens;
using com.example.Models.Ui;

namespace com.example.Demo
{
    /// <summary>
    /// Places WPF controls individually on a WinForms Form (each in its own
    /// ElementHost), but with the SAME design (tokens, status badges, cards) and
    /// the SAME behavior as EmployeeBrowserScreen: autocomplete IME-safe 이름
    /// filtering, 조회, department tree filter, double-click detail with 수정/삭제,
    /// and persistence to the shared employees.tsv store.
    /// </summary>
    public partial class WpfOnFormDemo : Form
    {
        private const string AllNode = "전체 부서";

        private readonly List<EmployeeBrowserRow> employees;
        private readonly ObservableCollection<EmployeeBrowserRow> displayed;
        // Name combo carries code (사번) + display name.
        private readonly ObservableCollection<ComboBoxItemModel> nameOptions;
        private readonly string storePath;
        private System.Windows.ResourceDictionary styles;

        private System.Windows.Controls.ComboBox nameCombo;
        private System.Windows.Controls.Button searchButton;
        private System.Windows.Controls.TreeView deptTree;
        private System.Windows.Controls.DataGrid grid;

        private ICollectionView namesView;
        private string nameFilterText;
        private bool suppressNameFilter;
        private System.Windows.Controls.TextBox nameEditBox;

        public WpfOnFormDemo()
        {
            this.InitializeComponent();
            this.employees = new List<EmployeeBrowserRow>();
            this.displayed = new ObservableCollection<EmployeeBrowserRow>();
            this.nameOptions = new ObservableCollection<ComboBoxItemModel>();
            this.nameFilterText = string.Empty;
            this.suppressNameFilter = false;
            this.storePath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                "com.example", "employees.tsv");

            this.LoadStyles();
            this.LoadEmployees();
            this.BuildWpfControls();
            this.BuildDepartmentTree();
            this.PopulateNameCombo();
            this.ShowEmployeesForDepartment(AllNode);
        }

        private void LoadStyles()
        {
            this.styles = new System.Windows.ResourceDictionary();
            this.styles.Source = new System.Uri(
                "/com.example;component/Themes/BrowserControlStyles.xaml", System.UriKind.Relative);
        }

        private System.Windows.Media.Brush Brush(string key)
        {
            return this.styles[key] as System.Windows.Media.Brush;
        }

        // ----- Build the styled WPF controls and drop each onto its ElementHost -----

        private void BuildWpfControls()
        {
            // 1) WPF ComboBox (styled, autocomplete) on comboHost
            this.nameCombo = new System.Windows.Controls.ComboBox();
            this.nameCombo.Style = this.styles["Browser.FilterCombo"] as System.Windows.Style;
            this.nameCombo.IsEditable = true;
            this.nameCombo.IsTextSearchEnabled = false;
            this.nameCombo.StaysOpenOnEdit = true;
            this.nameCombo.SelectedValuePath = "Code";
            this.nameCombo.FontFamily = (this.styles["Font.Family"] as System.Windows.Media.FontFamily);
            this.nameCombo.AddHandler(
                System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                new System.Windows.Controls.TextChangedEventHandler(this.NameCombo_TextChanged));
            this.nameCombo.SelectionChanged += this.NameCombo_SelectionChanged;
            this.comboHost.Child = this.nameCombo;

            // 2) WPF primary Button (조회) on searchButtonHost
            this.searchButton = new System.Windows.Controls.Button();
            this.searchButton.Style = this.styles["Browser.PrimaryButton"] as System.Windows.Style;
            this.searchButton.Content = "조회";
            this.searchButton.FontFamily = (this.styles["Font.Family"] as System.Windows.Media.FontFamily);
            this.searchButton.Click += this.SearchButton_Click;
            this.searchButtonHost.Child = this.searchButton;

            // 3) WPF TreeView (styled) inside a card on treeHost
            this.deptTree = new System.Windows.Controls.TreeView();
            this.deptTree.BorderThickness = new System.Windows.Thickness(0);
            this.deptTree.Background = System.Windows.Media.Brushes.Transparent;
            this.deptTree.FontFamily = (this.styles["Font.Family"] as System.Windows.Media.FontFamily);
            this.deptTree.ItemContainerStyle = this.styles["Browser.TreeItem"] as System.Windows.Style;
            this.deptTree.SelectedItemChanged += this.DeptTree_SelectedItemChanged;
            this.treeHost.Child = this.BuildCard("부서", this.deptTree);

            // 4) WPF DataGrid (styled, with 재직상태 badge) inside a card on gridHost
            this.grid = new System.Windows.Controls.DataGrid();
            this.grid.AutoGenerateColumns = false;
            this.grid.IsReadOnly = true;
            this.grid.HeadersVisibility = System.Windows.Controls.DataGridHeadersVisibility.Column;
            this.grid.CanUserAddRows = false;
            this.grid.CanUserDeleteRows = false;
            this.grid.CanUserResizeRows = false;
            this.grid.FontFamily = (this.styles["Font.Family"] as System.Windows.Media.FontFamily);
            this.grid.SelectionMode = System.Windows.Controls.DataGridSelectionMode.Single;
            this.grid.SelectionUnit = System.Windows.Controls.DataGridSelectionUnit.FullRow;
            this.grid.GridLinesVisibility = System.Windows.Controls.DataGridGridLinesVisibility.Horizontal;
            this.grid.HorizontalGridLinesBrush = this.Brush("Brush.BorderSubtle");
            this.grid.BorderThickness = new System.Windows.Thickness(0);
            this.grid.Background = System.Windows.Media.Brushes.Transparent;
            this.grid.RowBackground = this.Brush("Brush.Surface");
            this.grid.SetValue(System.Windows.Controls.ItemsControl.AlternationCountProperty, 2);
            this.grid.ColumnHeaderStyle = this.styles["Browser.GridHeader"] as System.Windows.Style;
            this.grid.RowStyle = this.styles["Browser.GridRow"] as System.Windows.Style;
            this.grid.CellStyle = this.styles["Browser.GridCell"] as System.Windows.Style;
            this.AddTextColumn("사번", "EmployeeId", 90);
            this.AddTextColumn("이름", "Name", 100);
            this.AddStarColumn("직급/직책", "Position");
            this.AddTextColumn("고용형태", "EmploymentType", 110);
            this.AddTextColumn("입사일", "HireDate", 120);
            this.AddBadgeColumn("재직상태", 110);
            this.grid.ItemsSource = this.displayed;
            this.grid.MouseDoubleClick += this.Grid_MouseDoubleClick;

            System.Windows.Controls.Border gridBorder = new System.Windows.Controls.Border();
            gridBorder.CornerRadius = (System.Windows.CornerRadius)this.styles["Radius.Md"];
            gridBorder.BorderThickness = new System.Windows.Thickness(1);
            gridBorder.BorderBrush = this.Brush("Brush.BorderSubtle");
            gridBorder.ClipToBounds = true;
            gridBorder.Child = this.grid;
            this.gridHost.Child = this.BuildCard("사원 목록", gridBorder);
        }

        /// <summary>Wraps content in the standard card (surface border + title).</summary>
        private System.Windows.Controls.Border BuildCard(string title, System.Windows.UIElement content)
        {
            System.Windows.Controls.Border card = new System.Windows.Controls.Border();
            card.Background = this.Brush("Brush.Surface");
            card.BorderBrush = this.Brush("Brush.BorderSubtle");
            card.BorderThickness = new System.Windows.Thickness(1);
            card.CornerRadius = (System.Windows.CornerRadius)this.styles["Radius.Lg"];
            card.Padding = new System.Windows.Thickness(16);

            System.Windows.Controls.DockPanel panel = new System.Windows.Controls.DockPanel();
            System.Windows.Controls.TextBlock header = new System.Windows.Controls.TextBlock();
            header.Text = title;
            header.Style = this.styles["Browser.CardTitle"] as System.Windows.Style;
            System.Windows.Controls.DockPanel.SetDock(header, System.Windows.Controls.Dock.Top);
            panel.Children.Add(header);
            panel.Children.Add(content);

            card.Child = panel;
            return card;
        }

        private void AddTextColumn(string header, string path, double width)
        {
            System.Windows.Controls.DataGridTextColumn column = new System.Windows.Controls.DataGridTextColumn();
            column.Header = header;
            column.Binding = new System.Windows.Data.Binding(path);
            column.Width = new System.Windows.Controls.DataGridLength(width);
            this.grid.Columns.Add(column);
        }

        private void AddStarColumn(string header, string path)
        {
            System.Windows.Controls.DataGridTextColumn column = new System.Windows.Controls.DataGridTextColumn();
            column.Header = header;
            column.Binding = new System.Windows.Data.Binding(path);
            column.Width = new System.Windows.Controls.DataGridLength(1, System.Windows.Controls.DataGridLengthUnitType.Star);
            this.grid.Columns.Add(column);
        }

        private void AddBadgeColumn(string header, double width)
        {
            System.Windows.Controls.DataGridTemplateColumn column = new System.Windows.Controls.DataGridTemplateColumn();
            column.Header = header;
            column.Width = new System.Windows.Controls.DataGridLength(width);
            column.CellTemplate = this.styles["Browser.StatusBadgeTemplate"] as System.Windows.DataTemplate;
            this.grid.Columns.Add(column);
        }

        // ----- Persistence (shared store with EmployeeBrowserScreen) -----

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
                        loaded.Add(this.NewRow(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]));
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
            catch (System.UnauthorizedAccessException)
            {
                return false;
            }
        }

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
            }
            catch (System.UnauthorizedAccessException)
            {
            }
        }

        // ----- Tree / list -----

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
            this.nameOptions.Clear();
            foreach (EmployeeBrowserRow row in this.employees)
            {
                this.nameOptions.Add(new ComboBoxItemModel(row.EmployeeId, row.Name));
            }

            this.nameCombo.ItemsSource = this.nameOptions;
            this.namesView = System.Windows.Data.CollectionViewSource.GetDefaultView(this.nameOptions);
            this.namesView.Filter = this.NameFilter;
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

        // ----- IME-safe name filtering -----

        private bool NameFilter(object item)
        {
            if (string.IsNullOrEmpty(this.nameFilterText))
            {
                return true;
            }

            ComboBoxItemModel option = item as ComboBoxItemModel;
            return option != null
                && option.Name.IndexOf(this.nameFilterText, System.StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void NameCombo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.suppressNameFilter || this.namesView == null)
            {
                return;
            }

            // Read the inner editable TextBox (ComboBox.Text lags during Hangul IME).
            if (this.nameEditBox == null)
            {
                this.nameEditBox = e.OriginalSource as System.Windows.Controls.TextBox;
            }

            this.nameFilterText = this.CurrentNameText();
            this.namesView.Refresh();

            if (this.nameFilterText.Length > 0 && !this.nameCombo.IsDropDownOpen)
            {
                this.nameCombo.Dispatcher.BeginInvoke(
                    new System.Action(this.OpenNameDropdown),
                    System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private string CurrentNameText()
        {
            if (this.nameEditBox != null)
            {
                return this.nameEditBox.Text;
            }

            return this.nameCombo.Text == null ? string.Empty : this.nameCombo.Text;
        }

        private void OpenNameDropdown()
        {
            if (!this.nameCombo.IsDropDownOpen && this.nameFilterText.Length > 0)
            {
                this.nameCombo.IsDropDownOpen = true;
            }
        }

        private void NameCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.nameCombo.SelectedItem == null)
            {
                return;
            }

            this.suppressNameFilter = true;
            this.nameCombo.IsDropDownOpen = false;
            this.nameCombo.Dispatcher.BeginInvoke(
                new System.Action(this.ClearNameFilterSuppression),
                System.Windows.Threading.DispatcherPriority.Input);
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

        // ----- Interaction -----

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

        private void SearchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string query = this.CurrentNameText().Trim();
            EmployeeBrowserRow match = null;

            // Use the picked item's Code (사번) when its name matches the text.
            ComboBoxItemModel selected = this.nameCombo.SelectedItem as ComboBoxItemModel;
            if (selected != null && string.Equals(selected.Name, query, System.StringComparison.Ordinal))
            {
                match = this.FindByCode(selected.Code);
            }
            else if (query.Length == 0)
            {
                this.SelectDepartmentNode(AllNode);
                return;
            }
            else
            {
                match = this.FindByName(query);
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

        private EmployeeBrowserRow FindByCode(string code)
        {
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (row.EmployeeId == code)
                {
                    return row;
                }
            }

            return null;
        }

        private EmployeeBrowserRow FindByName(string name)
        {
            foreach (EmployeeBrowserRow row in this.employees)
            {
                if (string.Equals(row.Name, name, System.StringComparison.OrdinalIgnoreCase))
                {
                    return row;
                }
            }

            return null;
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

        private void Grid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EmployeeBrowserRow row = this.grid.SelectedItem as EmployeeBrowserRow;
            if (row == null)
            {
                return;
            }

            EmployeeDetailWindow window = new EmployeeDetailWindow(row);
            window.ShowDialog();

            if (window.Result == EmployeeDetailResult.Deleted)
            {
                this.employees.Remove(row);
                this.BuildDepartmentTree();
                this.PopulateNameCombo();
                this.SaveToFile();
            }
            else if (window.Result == EmployeeDetailResult.Edited)
            {
                this.grid.Items.Refresh();
                this.SaveToFile();
            }
        }

        // ----- Sample seed -----

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
    }
}
