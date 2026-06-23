using System.Collections.Generic;
using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the 계산 결과 (payroll result) table on the 급여계산 screen.
    /// Columns: 사번 / 이름 / 부서 / 기본급 / 수당합계 / 공제합계 / 실지급액 / 계산상태.
    /// Carries its own earning/deduction line breakdown for the 급여 명세 panel.
    /// This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class PayrollResultRow : INotifyPropertyChanged
    {
        private string employeeId;
        private string employeeName;
        private string departmentName;
        private long baseSalary;
        private long allowanceAmount;
        private long deductionAmount;
        private long netPayAmount;
        private string calcStatusLabel;
        private string calcStatusTone;
        private readonly List<PayrollLineRow> earnings;
        private readonly List<PayrollLineRow> deductions;

        public PayrollResultRow()
        {
            this.employeeId = string.Empty;
            this.employeeName = string.Empty;
            this.departmentName = string.Empty;
            this.baseSalary = 0L;
            this.allowanceAmount = 0L;
            this.deductionAmount = 0L;
            this.netPayAmount = 0L;
            this.calcStatusLabel = string.Empty;
            this.calcStatusTone = "neutral";
            this.earnings = new List<PayrollLineRow>();
            this.deductions = new List<PayrollLineRow>();
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
                    this.OnPropertyChanged("NameWithId");
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
                    this.OnPropertyChanged("NameWithId");
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

        public long BaseSalary
        {
            get { return this.baseSalary; }
            set
            {
                if (this.baseSalary != value)
                {
                    this.baseSalary = value;
                    this.OnPropertyChanged("BaseSalary");
                    this.OnPropertyChanged("BaseSalaryText");
                }
            }
        }

        public long AllowanceAmount
        {
            get { return this.allowanceAmount; }
            set
            {
                if (this.allowanceAmount != value)
                {
                    this.allowanceAmount = value;
                    this.OnPropertyChanged("AllowanceAmount");
                    this.OnPropertyChanged("AllowanceText");
                }
            }
        }

        public long DeductionAmount
        {
            get { return this.deductionAmount; }
            set
            {
                if (this.deductionAmount != value)
                {
                    this.deductionAmount = value;
                    this.OnPropertyChanged("DeductionAmount");
                    this.OnPropertyChanged("DeductionText");
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

        public string CalcStatusLabel
        {
            get { return this.calcStatusLabel; }
            set
            {
                if (this.calcStatusLabel != value)
                {
                    this.calcStatusLabel = value;
                    this.OnPropertyChanged("CalcStatusLabel");
                }
            }
        }

        public string CalcStatusTone
        {
            get { return this.calcStatusTone; }
            set
            {
                if (this.calcStatusTone != value)
                {
                    this.calcStatusTone = value;
                    this.OnPropertyChanged("CalcStatusTone");
                }
            }
        }

        public List<PayrollLineRow> Earnings
        {
            get { return this.earnings; }
        }

        public List<PayrollLineRow> Deductions
        {
            get { return this.deductions; }
        }

        public string BaseSalaryText
        {
            get { return "₩" + this.baseSalary.ToString("#,0"); }
        }

        public string AllowanceText
        {
            get { return "₩" + this.allowanceAmount.ToString("#,0"); }
        }

        public string DeductionText
        {
            get { return "₩" + this.deductionAmount.ToString("#,0"); }
        }

        public string NetPayText
        {
            get { return "₩" + this.netPayAmount.ToString("#,0"); }
        }

        /// <summary>Display helper: "이름 (사번)".</summary>
        public string NameWithId
        {
            get { return this.employeeName + " (" + this.employeeId + ")"; }
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
