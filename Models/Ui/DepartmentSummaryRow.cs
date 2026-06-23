using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the 부서별 현황 (department summary) table on the 대시보드 screen.
    /// Columns: 부서 / 인원 / 지급액 / 연장수당 / 오류건수.
    /// This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class DepartmentSummaryRow : INotifyPropertyChanged
    {
        private string departmentName;
        private int employeeCount;
        private long netPayAmount;
        private long overtimeAmount;
        private int exceptionCount;

        public DepartmentSummaryRow()
        {
            this.departmentName = string.Empty;
            this.employeeCount = 0;
            this.netPayAmount = 0L;
            this.overtimeAmount = 0L;
            this.exceptionCount = 0;
        }

        public DepartmentSummaryRow(
            string departmentName,
            int employeeCount,
            long netPayAmount,
            long overtimeAmount,
            int exceptionCount)
        {
            this.departmentName = departmentName;
            this.employeeCount = employeeCount;
            this.netPayAmount = netPayAmount;
            this.overtimeAmount = overtimeAmount;
            this.exceptionCount = exceptionCount;
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

        public int EmployeeCount
        {
            get { return this.employeeCount; }
            set
            {
                if (this.employeeCount != value)
                {
                    this.employeeCount = value;
                    this.OnPropertyChanged("EmployeeCount");
                    this.OnPropertyChanged("EmployeeCountText");
                }
            }
        }

        public long NetPayAmount
        {
            get { return this.netPayAmount; }
            set
            {
                if (this.netPayAmount != value)
                {
                    this.netPayAmount = value;
                    this.OnPropertyChanged("NetPayAmount");
                    this.OnPropertyChanged("NetPayText");
                }
            }
        }

        public long OvertimeAmount
        {
            get { return this.overtimeAmount; }
            set
            {
                if (this.overtimeAmount != value)
                {
                    this.overtimeAmount = value;
                    this.OnPropertyChanged("OvertimeAmount");
                    this.OnPropertyChanged("OvertimeText");
                }
            }
        }

        public int ExceptionCount
        {
            get { return this.exceptionCount; }
            set
            {
                if (this.exceptionCount != value)
                {
                    this.exceptionCount = value;
                    this.OnPropertyChanged("ExceptionCount");
                    this.OnPropertyChanged("ExceptionCountText");
                }
            }
        }

        public string EmployeeCountText
        {
            get { return this.employeeCount.ToString("#,0") + "명"; }
        }

        public string NetPayText
        {
            get { return "₩" + this.netPayAmount.ToString("#,0"); }
        }

        public string OvertimeText
        {
            get { return "₩" + this.overtimeAmount.ToString("#,0"); }
        }

        public string ExceptionCountText
        {
            get { return this.exceptionCount.ToString("#,0") + "건"; }
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
