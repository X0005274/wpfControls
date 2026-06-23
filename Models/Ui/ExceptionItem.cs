using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Item model for the 승인·예외처리 (approval / exception) screen.
    /// Mirrors the hr_payroll_system exception list + detail fields. Labels are
    /// stored pre-resolved (Korean) and each badge carries a tone string
    /// ("neutral" / "info" / "warning" / "danger" / "success") that the view maps
    /// to token brushes. This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class ExceptionItem : INotifyPropertyChanged
    {
        private string exceptionId;
        private string employeeId;
        private string employeeName;
        private string departmentCode;
        private string typeLabel;
        private string severityLabel;
        private string severityTone;
        private string statusLabel;
        private string statusTone;
        private string reason;
        private string beforeValue;
        private string afterValue;
        private string impactAmount;
        private string comment;

        public ExceptionItem()
        {
            this.exceptionId = string.Empty;
            this.employeeId = string.Empty;
            this.employeeName = string.Empty;
            this.departmentCode = string.Empty;
            this.typeLabel = string.Empty;
            this.severityLabel = string.Empty;
            this.severityTone = "neutral";
            this.statusLabel = string.Empty;
            this.statusTone = "neutral";
            this.reason = string.Empty;
            this.beforeValue = string.Empty;
            this.afterValue = string.Empty;
            this.impactAmount = string.Empty;
            this.comment = string.Empty;
        }

        public string ExceptionId
        {
            get { return this.exceptionId; }
            set
            {
                if (this.exceptionId != value)
                {
                    this.exceptionId = value;
                    this.OnPropertyChanged("ExceptionId");
                }
            }
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

        public string DepartmentCode
        {
            get { return this.departmentCode; }
            set
            {
                if (this.departmentCode != value)
                {
                    this.departmentCode = value;
                    this.OnPropertyChanged("DepartmentCode");
                }
            }
        }

        public string TypeLabel
        {
            get { return this.typeLabel; }
            set
            {
                if (this.typeLabel != value)
                {
                    this.typeLabel = value;
                    this.OnPropertyChanged("TypeLabel");
                }
            }
        }

        public string SeverityLabel
        {
            get { return this.severityLabel; }
            set
            {
                if (this.severityLabel != value)
                {
                    this.severityLabel = value;
                    this.OnPropertyChanged("SeverityLabel");
                }
            }
        }

        public string SeverityTone
        {
            get { return this.severityTone; }
            set
            {
                if (this.severityTone != value)
                {
                    this.severityTone = value;
                    this.OnPropertyChanged("SeverityTone");
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

        public string Reason
        {
            get { return this.reason; }
            set
            {
                if (this.reason != value)
                {
                    this.reason = value;
                    this.OnPropertyChanged("Reason");
                }
            }
        }

        public string BeforeValue
        {
            get { return this.beforeValue; }
            set
            {
                if (this.beforeValue != value)
                {
                    this.beforeValue = value;
                    this.OnPropertyChanged("BeforeValue");
                }
            }
        }

        public string AfterValue
        {
            get { return this.afterValue; }
            set
            {
                if (this.afterValue != value)
                {
                    this.afterValue = value;
                    this.OnPropertyChanged("AfterValue");
                }
            }
        }

        public string ImpactAmount
        {
            get { return this.impactAmount; }
            set
            {
                if (this.impactAmount != value)
                {
                    this.impactAmount = value;
                    this.OnPropertyChanged("ImpactAmount");
                }
            }
        }

        public string Comment
        {
            get { return this.comment; }
            set
            {
                if (this.comment != value)
                {
                    this.comment = value;
                    this.OnPropertyChanged("Comment");
                }
            }
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
