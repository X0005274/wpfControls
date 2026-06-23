using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Row model for the 감사 이력 (audit log) table on the 승인·예외처리 screen.
    /// Columns: 시각 / 처리자 / 조치 / 사유코드 / 변경 전 / 변경 후 / 메모.
    /// This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class AuditLogRow : INotifyPropertyChanged
    {
        private string time;
        private string actor;
        private string action;
        private string reasonCode;
        private string beforeValue;
        private string afterValue;
        private string comment;

        public AuditLogRow()
        {
            this.time = string.Empty;
            this.actor = string.Empty;
            this.action = string.Empty;
            this.reasonCode = string.Empty;
            this.beforeValue = string.Empty;
            this.afterValue = string.Empty;
            this.comment = string.Empty;
        }

        public string Time
        {
            get { return this.time; }
            set
            {
                if (this.time != value)
                {
                    this.time = value;
                    this.OnPropertyChanged("Time");
                }
            }
        }

        public string Actor
        {
            get { return this.actor; }
            set
            {
                if (this.actor != value)
                {
                    this.actor = value;
                    this.OnPropertyChanged("Actor");
                }
            }
        }

        public string Action
        {
            get { return this.action; }
            set
            {
                if (this.action != value)
                {
                    this.action = value;
                    this.OnPropertyChanged("Action");
                }
            }
        }

        public string ReasonCode
        {
            get { return this.reasonCode; }
            set
            {
                if (this.reasonCode != value)
                {
                    this.reasonCode = value;
                    this.OnPropertyChanged("ReasonCode");
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
