using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// Tabbed container based on the standard WPF TabControl with modern,
    /// underline-style tab headers and a padded content area. Tabs are added
    /// through the <see cref="Items"/> collection (add <see cref="TabItem"/>
    /// instances).
    /// </summary>
    public partial class ModernTabControl : UserControl
    {
        public ModernTabControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the tab item collection of the internal TabControl.
        /// </summary>
        public ItemCollection Items
        {
            get { return this.InnerTabControl.Items; }
        }

        /// <summary>
        /// Gets or sets the currently selected tab index.
        /// </summary>
        public int SelectedIndex
        {
            get { return this.InnerTabControl.SelectedIndex; }
            set { this.InnerTabControl.SelectedIndex = value; }
        }
    }
}
