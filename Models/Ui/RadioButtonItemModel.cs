using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Item model for <c>ModernRadioButtonGroupControl</c>.
    /// Code is used as the selected value; Name is the displayed caption.
    /// </summary>
    public class RadioButtonItemModel : INotifyPropertyChanged
    {
        private string code;
        private string name;

        public RadioButtonItemModel()
        {
            this.code = string.Empty;
            this.name = string.Empty;
        }

        public RadioButtonItemModel(string code, string name)
        {
            this.code = code;
            this.name = name;
        }

        public string Code
        {
            get { return this.code; }
            set
            {
                if (this.code != value)
                {
                    this.code = value;
                    this.OnPropertyChanged("Code");
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
