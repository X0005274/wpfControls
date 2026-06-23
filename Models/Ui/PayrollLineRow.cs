using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// One earning or deduction line of a payslip (급여 명세 항목별 내역) on the
    /// 급여계산 screen. This is a UI shell model only; it carries no business logic.
    /// </summary>
    public class PayrollLineRow : INotifyPropertyChanged
    {
        private string itemName;
        private long amount;

        public PayrollLineRow()
        {
            this.itemName = string.Empty;
            this.amount = 0L;
        }

        public PayrollLineRow(string itemName, long amount)
        {
            this.itemName = itemName;
            this.amount = amount;
        }

        public string ItemName
        {
            get { return this.itemName; }
            set
            {
                if (this.itemName != value)
                {
                    this.itemName = value;
                    this.OnPropertyChanged("ItemName");
                }
            }
        }

        public long Amount
        {
            get { return this.amount; }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged("Amount");
                    this.OnPropertyChanged("AmountText");
                }
            }
        }

        /// <summary>Currency display, e.g. "₩1,234,000".</summary>
        public string AmountText
        {
            get { return "₩" + this.amount.ToString("#,0"); }
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
