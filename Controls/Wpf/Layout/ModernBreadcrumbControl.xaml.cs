using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// 현재 위치 경로를 보여 주는 컨트롤입니다(예: 홈 › 직원 › 상세).
    /// Path 에 "/" 로 구분한 문자열을 넣으면 조각으로 나누어 표시합니다.
    /// 예) Path = "홈/직원관리/상세"
    /// </summary>
    public partial class ModernBreadcrumbControl : UserControl
    {
        /// <summary>"/" 로 구분한 경로 문자열입니다(예: "홈/직원/상세").</summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path", typeof(string), typeof(ModernBreadcrumbControl),
                new PropertyMetadata(string.Empty, OnPathChanged));

        public ModernBreadcrumbControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.Rebuild();
        }

        /// <summary>"/" 로 구분한 경로 문자열입니다.</summary>
        public string Path
        {
            get { return (string)this.GetValue(PathProperty); }
            set { this.SetValue(PathProperty, value); }
        }

        private static void OnPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernBreadcrumbControl)d).Rebuild();
        }

        // 경로 문자열을 "/" 로 잘라 조각과 구분 기호(›)를 만들어 채웁니다.
        private void Rebuild()
        {
            if (this.SegmentsPanel == null)
            {
                return;
            }

            this.SegmentsPanel.Children.Clear();

            if (string.IsNullOrEmpty(this.Path))
            {
                return;
            }

            string[] segments = this.Path.Split('/');
            for (int i = 0; i < segments.Length; i++)
            {
                bool isLast = i == segments.Length - 1;

                // 경로 조각 텍스트(마지막 조각은 진하게 강조)
                TextBlock segment = new TextBlock();
                segment.Text = segments[i].Trim();
                segment.FontSize = 12;
                segment.VerticalAlignment = VerticalAlignment.Center;
                segment.Foreground = (Brush)this.FindResource(isLast ? "Brush.TextPrimary" : "Brush.TextSecondary");
                if (isLast)
                {
                    segment.FontWeight = FontWeights.SemiBold;
                }

                this.SegmentsPanel.Children.Add(segment);

                // 마지막이 아니면 구분 기호를 덧붙입니다.
                if (!isLast)
                {
                    TextBlock separator = new TextBlock();
                    separator.Text = "  ›  ";
                    separator.FontSize = 12;
                    separator.VerticalAlignment = VerticalAlignment.Center;
                    separator.Foreground = (Brush)this.FindResource("Brush.TextPlaceholder");
                    this.SegmentsPanel.Children.Add(separator);
                }
            }
        }
    }
}
