using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.example.Models.Ui
{
    /// <summary>
    /// Hierarchical item model for <c>ModernTreeViewControl</c>.
    /// </summary>
    public class TreeViewItemModel : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<TreeViewItemModel> children;
        private bool isExpanded;
        private bool isSelected;

        public TreeViewItemModel()
        {
            this.name = string.Empty;
            this.children = new ObservableCollection<TreeViewItemModel>();
            this.isExpanded = false;
            this.isSelected = false;
        }

        public TreeViewItemModel(string name)
            : this()
        {
            this.name = name;
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

        public ObservableCollection<TreeViewItemModel> Children
        {
            get { return this.children; }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    this.OnPropertyChanged("Children");
                }
            }
        }

        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

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
            return this.name;
        }
    }
}
