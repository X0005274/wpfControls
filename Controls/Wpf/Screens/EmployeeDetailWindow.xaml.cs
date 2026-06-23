using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Screens
{
    /// <summary>Outcome of the detail window, read by the caller after ShowDialog.</summary>
    public enum EmployeeDetailResult
    {
        None,
        Edited,
        Deleted
    }

    /// <summary>
    /// Modal detail window shown when a row is double-clicked on the 사원 조회 screen.
    /// View mode shows the employee fields with a status badge; 수정 switches to an
    /// inline edit mode (직급·직책 / 고용형태 / 재직상태) that writes back to the row,
    /// and 삭제 removes it (with confirmation). This is a UI shell only.
    /// </summary>
    public partial class EmployeeDetailWindow : Window
    {
        private readonly EmployeeBrowserRow row;

        /// <summary>What the user did; the caller acts on this after ShowDialog returns.</summary>
        public EmployeeDetailResult Result { get; private set; }

        public EmployeeDetailWindow(EmployeeBrowserRow row)
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.row = row;
            this.Result = EmployeeDetailResult.None;
            this.PopulateFrom(row);
        }

        private void ApplyBackground()
        {
            Brush background = this.TryFindResource("Brush.Background") as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        private void PopulateFrom(EmployeeBrowserRow source)
        {
            if (source == null)
            {
                return;
            }

            this.HeaderName.Text = source.Name + " (" + source.EmployeeId + ")";
            this.HeaderSub.Text = source.Department + " · " + source.Position;
            this.ValueId.Text = source.EmployeeId;
            this.ValueName.Text = source.Name;
            this.ValueDept.Text = source.Department;
            this.ValuePosition.Text = source.Position;
            this.ValueType.Text = source.EmploymentType;
            this.ValueHireDate.Text = source.HireDate;
            this.StatusText.Text = source.StatusLabel;
            this.StatusBadge.Tag = source.StatusTone;
        }

        // ----- View / edit mode -----

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.EnterEditMode();
        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            this.ExitEditMode();
        }

        private void EnterEditMode()
        {
            this.EditPosition.Text = this.row.Position;
            this.SelectComboItem(this.EditType, this.row.EmploymentType);
            this.SelectComboItem(this.EditStatus, this.row.StatusLabel);

            this.ValuePosition.Visibility = Visibility.Collapsed;
            this.EditPosition.Visibility = Visibility.Visible;
            this.ValueType.Visibility = Visibility.Collapsed;
            this.EditType.Visibility = Visibility.Visible;
            this.StatusBadge.Visibility = Visibility.Collapsed;
            this.EditStatus.Visibility = Visibility.Visible;

            this.ViewButtons.Visibility = Visibility.Collapsed;
            this.EditButtons.Visibility = Visibility.Visible;
        }

        private void ExitEditMode()
        {
            this.ValuePosition.Visibility = Visibility.Visible;
            this.EditPosition.Visibility = Visibility.Collapsed;
            this.ValueType.Visibility = Visibility.Visible;
            this.EditType.Visibility = Visibility.Collapsed;
            this.StatusBadge.Visibility = Visibility.Visible;
            this.EditStatus.Visibility = Visibility.Collapsed;

            this.ViewButtons.Visibility = Visibility.Visible;
            this.EditButtons.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Write the edited values back to the shared row reference; the grid
            // updates via INotifyPropertyChanged.
            this.row.Position = this.EditPosition.Text.Trim();
            this.row.EmploymentType = this.GetSelectedComboText(this.EditType);
            string status = this.GetSelectedComboText(this.EditStatus);
            this.row.StatusLabel = status;
            this.row.StatusTone = EmployeeBrowserRow.ToneForStatus(status);

            this.PopulateFrom(this.row);
            this.ExitEditMode();
            this.Result = EmployeeDetailResult.Edited;
        }

        // ----- Delete -----

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show(
                this.row.Name + " (" + this.row.EmployeeId + ") 사원을 삭제하시겠습니까?",
                "삭제 확인", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (answer == MessageBoxResult.OK)
            {
                this.Result = EmployeeDetailResult.Deleted;
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ----- Helpers -----

        private void SelectComboItem(ComboBox comboBox, string content)
        {
            foreach (object item in comboBox.Items)
            {
                ComboBoxItem comboItem = item as ComboBoxItem;
                if (comboItem != null && comboItem.Content != null
                    && string.Equals(comboItem.Content.ToString(), content))
                {
                    comboBox.SelectedItem = comboItem;
                    return;
                }
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
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
    }
}
