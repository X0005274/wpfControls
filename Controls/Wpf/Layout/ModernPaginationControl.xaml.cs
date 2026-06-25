using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Layout
{
    /// <summary>
    /// 페이지 이동 컨트롤입니다. 이전/다음 버튼과 페이지 번호 버튼을 보여 줍니다.
    /// 전체 페이지 수(TotalPages)와 현재 페이지(CurrentPage)를 지정하면 화면을 그립니다.
    /// 페이지가 바뀌면 PageChanged 이벤트가 발생합니다.
    /// </summary>
    public partial class ModernPaginationControl : UserControl
    {
        // 한 번에 보여 줄 페이지 번호 버튼의 최대 개수입니다.
        private const int MaxNumberButtons = 7;

        /// <summary>전체 페이지 수입니다(기본 1).</summary>
        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register(
                "TotalPages", typeof(int), typeof(ModernPaginationControl),
                new PropertyMetadata(1, OnPagingChanged));

        /// <summary>현재 페이지 번호(1부터)입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                "CurrentPage", typeof(int), typeof(ModernPaginationControl),
                new FrameworkPropertyMetadata(
                    1,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPagingChanged));

        /// <summary>페이지가 바뀌었을 때 발생하는 이벤트입니다.</summary>
        public event EventHandler PageChanged;

        public ModernPaginationControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.RebuildButtons();
        }

        /// <summary>전체 페이지 수입니다.</summary>
        public int TotalPages
        {
            get { return (int)this.GetValue(TotalPagesProperty); }
            set { this.SetValue(TotalPagesProperty, value); }
        }

        /// <summary>현재 페이지 번호(1부터)입니다.</summary>
        public int CurrentPage
        {
            get { return (int)this.GetValue(CurrentPageProperty); }
            set { this.SetValue(CurrentPageProperty, value); }
        }

        private static void OnPagingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernPaginationControl)d).RebuildButtons();
        }

        // 현재 상태에 맞춰 버튼들을 다시 만듭니다.
        private void RebuildButtons()
        {
            if (this.ButtonsPanel == null)
            {
                return;
            }

            int total = this.TotalPages < 1 ? 1 : this.TotalPages;
            int current = this.CurrentPage;
            if (current < 1)
            {
                current = 1;
            }

            if (current > total)
            {
                current = total;
            }

            this.ButtonsPanel.Children.Clear();

            // 이전 버튼
            this.ButtonsPanel.Children.Add(this.MakeArrowButton("◀", current > 1, current - 1));

            // 보여 줄 페이지 번호 범위를 계산합니다(현재 페이지가 가운데 오도록).
            int half = MaxNumberButtons / 2;
            int start = current - half;
            if (start < 1)
            {
                start = 1;
            }

            int end = start + MaxNumberButtons - 1;
            if (end > total)
            {
                end = total;
                start = end - MaxNumberButtons + 1;
                if (start < 1)
                {
                    start = 1;
                }
            }

            for (int page = start; page <= end; page++)
            {
                this.ButtonsPanel.Children.Add(this.MakeNumberButton(page, page == current));
            }

            // 다음 버튼
            this.ButtonsPanel.Children.Add(this.MakeArrowButton("▶", current < total, current + 1));
        }

        // 페이지 번호 버튼을 만듭니다.
        private Button MakeNumberButton(int page, bool isCurrent)
        {
            Button button = new Button();
            button.Content = page.ToString(CultureInfo.InvariantCulture);
            button.Style = (Style)this.FindResource(isCurrent ? "PageButtonSelectedStyle" : "PageButtonStyle");
            int target = page;
            button.Click += (sender, e) => this.GoTo(target);
            return button;
        }

        // 이전/다음 화살표 버튼을 만듭니다.
        private Button MakeArrowButton(string glyph, bool enabled, int target)
        {
            Button button = new Button();
            button.Content = glyph;
            button.Style = (Style)this.FindResource("PageButtonStyle");
            button.IsEnabled = enabled;
            int destination = target;
            button.Click += (sender, e) => this.GoTo(destination);
            return button;
        }

        // 지정한 페이지로 이동합니다(값이 실제로 바뀌면 이벤트 발생).
        private void GoTo(int page)
        {
            if (page < 1 || page > this.TotalPages || page == this.CurrentPage)
            {
                return;
            }

            this.CurrentPage = page;
            if (this.PageChanged != null)
            {
                this.PageChanged(this, EventArgs.Empty);
            }
        }
    }
}
