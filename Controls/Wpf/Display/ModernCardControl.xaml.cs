using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// 내용을 담는 카드 컨테이너입니다. 제목/부제목(선택)과 본문(InnerContent)을
    /// 둥근 테두리 안에 보기 좋게 배치합니다.
    /// </summary>
    public partial class ModernCardControl : UserControl
    {
        /// <summary>카드 제목입니다(비우면 숨김).</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernCardControl),
                new PropertyMetadata(string.Empty, OnTextChanged));

        /// <summary>카드 부제목입니다(비우면 숨김).</summary>
        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register(
                "Subtitle", typeof(string), typeof(ModernCardControl),
                new PropertyMetadata(string.Empty, OnTextChanged));

        /// <summary>카드 본문에 넣을 내용입니다(코드에서 설정).</summary>
        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent", typeof(object), typeof(ModernCardControl),
                new PropertyMetadata(null));

        public ModernCardControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.UpdateVisibility();
        }

        /// <summary>카드 제목입니다.</summary>
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        /// <summary>카드 부제목입니다.</summary>
        public string Subtitle
        {
            get { return (string)this.GetValue(SubtitleProperty); }
            set { this.SetValue(SubtitleProperty, value); }
        }

        /// <summary>카드 본문에 넣을 내용입니다.</summary>
        public object InnerContent
        {
            get { return this.GetValue(InnerContentProperty); }
            set { this.SetValue(InnerContentProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernCardControl)d).UpdateVisibility();
        }

        // 제목/부제목이 비어 있으면 해당 줄을 숨깁니다.
        private void UpdateVisibility()
        {
            if (this.TitleText != null)
            {
                this.TitleText.Visibility = string.IsNullOrEmpty(this.Title) ? Visibility.Collapsed : Visibility.Visible;
            }

            if (this.SubtitleText != null)
            {
                this.SubtitleText.Visibility = string.IsNullOrEmpty(this.Subtitle) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
