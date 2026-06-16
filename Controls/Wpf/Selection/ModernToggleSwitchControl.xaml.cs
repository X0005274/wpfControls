using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// On/off toggle switch built on a ToggleButton. The thumb slides and the
    /// track color animates between gray (off) and blue (on) using a Storyboard.
    /// </summary>
    public partial class ModernToggleSwitchControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernToggleSwitchControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernToggleSwitchControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register(
                "IsOn",
                typeof(bool),
                typeof(ModernToggleSwitchControl),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernToggleSwitchControl),
                new PropertyMetadata(true));

        public ModernToggleSwitchControl()
        {
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

        public bool IsOn
        {
            get { return (bool)this.GetValue(IsOnProperty); }
            set { this.SetValue(IsOnProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }
    }
}
