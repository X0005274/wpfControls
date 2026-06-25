using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// 여러 칸 중 하나를 고르는 분절형 선택 컨트롤입니다(탭처럼 생긴 토글 그룹).
    /// ItemsSource 에 보기 목록을, SelectedIndex 로 선택 위치를 정합니다.
    /// 선택이 바뀌면 SelectionChanged 이벤트가 발생합니다.
    /// </summary>
    public partial class ModernSegmentedControl : UserControl
    {
        /// <summary>칸으로 표시할 목록입니다(각 항목의 글자는 ToString 결과).</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource", typeof(IEnumerable), typeof(ModernSegmentedControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        /// <summary>선택된 칸의 번호(0부터)입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                "SelectedIndex", typeof(int), typeof(ModernSegmentedControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedIndexChanged));

        /// <summary>선택이 바뀌었을 때 발생하는 이벤트입니다.</summary>
        public event EventHandler SelectionChanged;

        public ModernSegmentedControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.Rebuild();
        }

        /// <summary>칸으로 표시할 목록입니다.</summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>선택된 칸의 번호(0부터)입니다.</summary>
        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernSegmentedControl)d).Rebuild();
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernSegmentedControl)d).Rebuild();
        }

        // 목록과 선택 상태에 맞춰 칸 버튼들을 다시 만듭니다.
        private void Rebuild()
        {
            if (this.SegmentsPanel == null)
            {
                return;
            }

            this.SegmentsPanel.Children.Clear();
            if (this.ItemsSource == null)
            {
                return;
            }

            int index = 0;
            foreach (object item in this.ItemsSource)
            {
                bool isSelected = index == this.SelectedIndex;

                Button button = new Button();
                button.Content = item == null ? string.Empty : item.ToString();
                button.Style = (Style)this.FindResource(isSelected ? "SegmentButtonSelectedStyle" : "SegmentButtonStyle");

                int target = index;
                button.Click += (sender, e) => this.Select(target);

                this.SegmentsPanel.Children.Add(button);
                index++;
            }
        }

        // 지정한 칸을 선택합니다(실제로 바뀌면 이벤트 발생).
        private void Select(int index)
        {
            if (index == this.SelectedIndex)
            {
                return;
            }

            this.SelectedIndex = index;
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }
    }
}
