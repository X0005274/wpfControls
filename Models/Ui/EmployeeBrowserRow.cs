using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the 사원 조회 (employee browser) screen's right-hand list.
    /// Columns: 사번 / 이름 / 직급·직책 / 고용형태 / 입사일 / 재직상태.
    /// 재직상태 carries a badge tone ("success" / "warning" / "danger" …) so the
    /// view renders it with the same status badge used on 승인·예외 / 급여계산.
    /// This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class EmployeeBrowserRow : INotifyPropertyChanged
    {
        private string employeeId;
        private string name;
        private string position;
        private string employmentType;
        private string hireDate;
        private string statusLabel;
        private string statusTone;
        private string department;

        public EmployeeBrowserRow()
        {
            this.employeeId = string.Empty;
            this.name = string.Empty;
            this.position = string.Empty;
            this.employmentType = string.Empty;
            this.hireDate = string.Empty;
            this.statusLabel = string.Empty;
            this.statusTone = "neutral";
            this.department = string.Empty;
        }

        public string EmployeeId
        {
            get { return this.employeeId; }
            set
            {
                if (this.employeeId != value)
                {
                    this.employeeId = value;
                    this.OnPropertyChanged("EmployeeId");
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public string Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }

        public string EmploymentType
        {
            get { return this.employmentType; }
            set
            {
                if (this.employmentType != value)
                {
                    this.employmentType = value;
                    this.OnPropertyChanged("EmploymentType");
                }
            }
        }

        public string HireDate
        {
            get { return this.hireDate; }
            set
            {
                if (this.hireDate != value)
                {
                    this.hireDate = value;
                    this.OnPropertyChanged("HireDate");
                }
            }
        }

        public string StatusLabel
        {
            get { return this.statusLabel; }
            set
            {
                if (this.statusLabel != value)
                {
                    this.statusLabel = value;
                    this.OnPropertyChanged("StatusLabel");
                }
            }
        }

        public string StatusTone
        {
            get { return this.statusTone; }
            set
            {
                if (this.statusTone != value)
                {
                    this.statusTone = value;
                    this.OnPropertyChanged("StatusTone");
                }
            }
        }

        public string Department
        {
            get { return this.department; }
            set
            {
                if (this.department != value)
                {
                    this.department = value;
                    this.OnPropertyChanged("Department");
                }
            }
        }

        /// <summary>
        /// Maps a 재직상태 to its badge tone, shared by the browser screen and the
        /// detail window: 재직=success, 휴직=warning(높음), 퇴직=danger(치명).
        /// </summary>
        public static string ToneForStatus(string status)
        {
            if (status == "휴직")
            {
                return "warning";
            }
            if (status == "퇴직")
            {
                return "danger";
            }

            return "success";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
