using System.ComponentModel;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Presentation wrapper used by <see cref="ModernMultiSelectComboBoxControl"/>.
    /// Pairs a source item with its display text and a bindable selected state.
    /// </summary>
    public class MultiSelectItem : INotifyPropertyChanged
    {
        private readonly object item;
        private string display;
        private bool isSelected;

        public MultiSelectItem(object item, string display)
        {
            this.item = item;
            this.display = display;
            this.isSelected = false;
        }

        /// <summary>The original source item.</summary>
        public object Item
        {
            get { return this.item; }
        }

        /// <summary>Text shown next to the checkbox.</summary>
        public string Display
        {
            get { return this.display; }
            set
            {
                if (this.display != value)
                {
                    this.display = value;
                    this.OnPropertyChanged("Display");
                }
            }
        }

        /// <summary>Whether the item is checked.</summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
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
            return this.display;
        }
    }
}
