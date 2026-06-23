using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the employee management grid (직원관리 목록).
    /// Mirrors the columns used by the hr_payroll_system employee list screen:
    /// 사번 / 이름 / 부서 / 직급·직책 / 고용형태 / 입사일 / 재직상태 / 급여대상.
    /// This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class EmployeeRow : INotifyPropertyChanged
    {
        private string employeeId;
        private string employeeName;
        private string departmentName;
        private string positionName;
        private string employmentType;
        private string hireDate;
        private string employmentStatus;
        private string payrollTarget;

        public EmployeeRow()
        {
            this.employeeId = string.Empty;
            this.employeeName = string.Empty;
            this.departmentName = string.Empty;
            this.positionName = string.Empty;
            this.employmentType = string.Empty;
            this.hireDate = string.Empty;
            this.employmentStatus = string.Empty;
            this.payrollTarget = string.Empty;
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

        public string EmployeeName
        {
            get { return this.employeeName; }
            set
            {
                if (this.employeeName != value)
                {
                    this.employeeName = value;
                    this.OnPropertyChanged("EmployeeName");
                }
            }
        }

        public string DepartmentName
        {
            get { return this.departmentName; }
            set
            {
                if (this.departmentName != value)
                {
                    this.departmentName = value;
                    this.OnPropertyChanged("DepartmentName");
                }
            }
        }

        public string PositionName
        {
            get { return this.positionName; }
            set
            {
                if (this.positionName != value)
                {
                    this.positionName = value;
                    this.OnPropertyChanged("PositionName");
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

        public string EmploymentStatus
        {
            get { return this.employmentStatus; }
            set
            {
                if (this.employmentStatus != value)
                {
                    this.employmentStatus = value;
                    this.OnPropertyChanged("EmploymentStatus");
                }
            }
        }

        public string PayrollTarget
        {
            get { return this.payrollTarget; }
            set
            {
                if (this.payrollTarget != value)
                {
                    this.payrollTarget = value;
                    this.OnPropertyChanged("PayrollTarget");
                }
            }
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
