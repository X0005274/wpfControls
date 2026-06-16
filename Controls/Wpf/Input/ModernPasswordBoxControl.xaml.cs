using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// Password input with a show/hide toggle. The password value is exposed
    /// through the <see cref="Password"/> CLR property rather than a
    /// DependencyProperty, to avoid persisting the secret in the binding system.
    /// </summary>
    public partial class ModernPasswordBoxControl : UserControl
    {
        private bool isSyncing;
        private bool isPasswordVisible;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernPasswordBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernPasswordBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired",
                typeof(bool),
                typeof(ModernPasswordBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernPasswordBoxControl),
                new PropertyMetadata(true));

        public ModernPasswordBoxControl()
        {
            this.isSyncing = false;
            this.isPasswordVisible = false;
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)this.GetValue(DescriptionProperty); }
            set { this.SetValue(DescriptionProperty, value); }
        }

        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current password text.
        /// </summary>
        public string Password
        {
            get
            {
                if (this.isPasswordVisible)
                {
                    return this.PlainBox.Text;
                }

                return this.PwBox.Password;
            }

            set
            {
                this.isSyncing = true;
                this.PwBox.Password = value;
                this.PlainBox.Text = value;
                this.isSyncing = false;
            }
        }

        private void PwBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            this.PlainBox.Text = this.PwBox.Password;
            this.isSyncing = false;
        }

        private void PlainBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            this.PwBox.Password = this.PlainBox.Text;
            this.isSyncing = false;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            this.isPasswordVisible = !this.isPasswordVisible;

            if (this.isPasswordVisible)
            {
                this.PlainBox.Visibility = Visibility.Visible;
                this.PwBox.Visibility = Visibility.Collapsed;
                this.ToggleButton.Content = "Hide";
                this.PlainBox.Focus();
                this.PlainBox.CaretIndex = this.PlainBox.Text.Length;
            }
            else
            {
                this.PwBox.Visibility = Visibility.Visible;
                this.PlainBox.Visibility = Visibility.Collapsed;
                this.ToggleButton.Content = "Show";
                this.PwBox.Focus();
            }
        }
    }
}
