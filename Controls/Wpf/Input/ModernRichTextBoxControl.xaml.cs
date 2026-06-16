using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// Multi-line rich text editor. The plain-text content is exposed through
    /// the <see cref="PlainText"/> CLR property.
    /// </summary>
    public partial class ModernRichTextBoxControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernRichTextBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(ModernRichTextBoxControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired",
                typeof(bool),
                typeof(ModernRichTextBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(ModernRichTextBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernRichTextBoxControl),
                new PropertyMetadata(true));

        public ModernRichTextBoxControl()
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

        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)this.GetValue(IsReadOnlyProperty); }
            set { this.SetValue(IsReadOnlyProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the plain-text content of the editor.
        /// </summary>
        public string PlainText
        {
            get
            {
                TextRange range = new TextRange(
                    this.InnerRichTextBox.Document.ContentStart,
                    this.InnerRichTextBox.Document.ContentEnd);
                return range.Text;
            }

            set
            {
                this.InnerRichTextBox.Document.Blocks.Clear();
                this.InnerRichTextBox.Document.Blocks.Add(
                    new Paragraph(new Run(value ?? string.Empty)));
            }
        }
    }
}
