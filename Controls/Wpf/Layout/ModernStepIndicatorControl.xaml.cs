using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// 여러 단계로 이루어진 작업의 진행 상태를 보여 주는 컨트롤입니다.
    /// 동그라미(번호) + 단계 이름을 가로로 나열하고, 현재 단계까지를 강조합니다.
    /// ItemsSource 에 단계 이름 목록을, CurrentStep 으로 현재 단계 번호(0부터)를 지정합니다.
    /// </summary>
    public partial class ModernStepIndicatorControl : UserControl
    {
        /// <summary>단계 이름 목록입니다(각 항목의 글자는 ToString 결과).</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource", typeof(IEnumerable), typeof(ModernStepIndicatorControl),
                new PropertyMetadata(null, OnChanged));

        /// <summary>현재 진행 중인 단계 번호(0부터)입니다.</summary>
        public static readonly DependencyProperty CurrentStepProperty =
            DependencyProperty.Register(
                "CurrentStep", typeof(int), typeof(ModernStepIndicatorControl),
                new PropertyMetadata(0, OnChanged));

        public ModernStepIndicatorControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.Rebuild();
        }

        /// <summary>단계 이름 목록입니다.</summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>현재 진행 중인 단계 번호(0부터)입니다.</summary>
        public int CurrentStep
        {
            get { return (int)this.GetValue(CurrentStepProperty); }
            set { this.SetValue(CurrentStepProperty, value); }
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernStepIndicatorControl)d).Rebuild();
        }

        // 단계 목록과 현재 단계에 맞춰 동그라미/선/라벨을 다시 만듭니다.
        private void Rebuild()
        {
            if (this.StepsPanel == null)
            {
                return;
            }

            this.StepsPanel.Children.Clear();
            if (this.ItemsSource == null)
            {
                return;
            }

            Brush accent = (Brush)this.FindResource("Brush.Accent");
            Brush muted = (Brush)this.FindResource("Brush.BorderSubtle");
            Brush primaryText = (Brush)this.FindResource("Brush.TextPrimary");
            Brush secondaryText = (Brush)this.FindResource("Brush.TextSecondary");

            // 단계 개수를 먼저 셉니다.
            int total = 0;
            foreach (object ignored in this.ItemsSource)
            {
                total++;
            }

            int index = 0;
            foreach (object item in this.ItemsSource)
            {
                bool isDoneOrCurrent = index <= this.CurrentStep;

                // 단계가 처음이 아니면 앞에 연결선을 넣습니다.
                if (index > 0)
                {
                    Border connector = new Border();
                    connector.Width = 28;
                    connector.Height = 2;
                    connector.VerticalAlignment = VerticalAlignment.Center;
                    connector.Margin = new Thickness(6, 0, 6, 0);
                    connector.Background = index <= this.CurrentStep ? accent : muted;
                    this.StepsPanel.Children.Add(connector);
                }

                // 동그라미(번호) + 라벨
                StackPanel stepPanel = new StackPanel();
                stepPanel.Orientation = Orientation.Horizontal;
                stepPanel.VerticalAlignment = VerticalAlignment.Center;

                Border circle = new Border();
                circle.Width = 24;
                circle.Height = 24;
                circle.CornerRadius = new CornerRadius(12);
                circle.Background = isDoneOrCurrent ? accent : muted;

                TextBlock number = new TextBlock();
                number.Text = (index + 1).ToString(CultureInfo.InvariantCulture);
                number.FontSize = 12;
                number.FontWeight = FontWeights.SemiBold;
                number.HorizontalAlignment = HorizontalAlignment.Center;
                number.VerticalAlignment = VerticalAlignment.Center;
                number.Foreground = isDoneOrCurrent ? Brushes.White : secondaryText;
                circle.Child = number;
                stepPanel.Children.Add(circle);

                TextBlock label = new TextBlock();
                label.Text = item == null ? string.Empty : item.ToString();
                label.FontSize = 13;
                label.Margin = new Thickness(8, 0, 0, 0);
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Foreground = isDoneOrCurrent ? primaryText : secondaryText;
                if (index == this.CurrentStep)
                {
                    label.FontWeight = FontWeights.SemiBold;
                }

                stepPanel.Children.Add(label);
                this.StepsPanel.Children.Add(stepPanel);

                index++;
            }
        }
    }
}
