using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// List view that uses a GridView layout internally with styled column
    /// headers and rows. Columns are added through the <see cref="Columns"/>
    /// collection.
    /// </summary>
    public partial class ModernListViewControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ModernListViewControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ModernListViewControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ModernListViewControl()
        {
            this.InitializeComponent();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets the GridView column collection used by the internal list view.
        /// Add <see cref="GridViewColumn"/> instances to define the layout.
        /// </summary>
        public GridViewColumnCollection Columns
        {
            get { return this.MainGridView.Columns; }
        }
    }
}
