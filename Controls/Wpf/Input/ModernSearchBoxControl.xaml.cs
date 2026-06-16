using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// Search input with a clear button and a search button that raises the
    /// <see cref="SearchRequested"/> routed event (also raised on Enter).
    /// </summary>
    public partial class ModernSearchBoxControl : UserControl
    {
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(
                "SearchText",
                typeof(string),
                typeof(ModernSearchBoxControl),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(ModernSearchBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernSearchBoxControl),
                new PropertyMetadata(true));

        public static readonly RoutedEvent SearchRequestedEvent =
            EventManager.RegisterRoutedEvent(
                "SearchRequested",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ModernSearchBoxControl));

        public ModernSearchBoxControl()
        {
            this.InitializeComponent();
        }

        public event RoutedEventHandler SearchRequested
        {
            add { this.AddHandler(SearchRequestedEvent, value); }
            remove { this.RemoveHandler(SearchRequestedEvent, value); }
        }

        public string SearchText
        {
            get { return (string)this.GetValue(SearchTextProperty); }
            set { this.SetValue(SearchTextProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)this.GetValue(PlaceholderTextProperty); }
            set { this.SetValue(PlaceholderTextProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchText = string.Empty;
            this.InnerTextBox.Focus();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.RaiseSearchRequested();
        }

        private void InnerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.RaiseSearchRequested();
                e.Handled = true;
            }
        }

        private void RaiseSearchRequested()
        {
            this.RaiseEvent(new RoutedEventArgs(SearchRequestedEvent, this));
        }
    }
}
