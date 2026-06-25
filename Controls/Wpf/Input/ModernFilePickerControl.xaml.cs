using System;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 파일을 선택하는 컨트롤입니다. "찾아보기" 버튼을 누르면 파일 열기 창이 뜨고,
    /// 고른 파일의 경로가 왼쪽 칸에 표시됩니다.
    /// 선택된 경로는 FilePath 속성으로 읽고, 선택되면 FileSelected 이벤트가 발생합니다.
    /// </summary>
    public partial class ModernFilePickerControl : UserControl
    {
        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernFilePickerControl),
                new PropertyMetadata(string.Empty));

        /// <summary>선택된 파일의 전체 경로입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register(
                "FilePath", typeof(string), typeof(ModernFilePickerControl),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnFilePathChanged));

        /// <summary>파일이 비었을 때 보여줄 안내 문구입니다.</summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText", typeof(string), typeof(ModernFilePickerControl),
                new PropertyMetadata("선택된 파일 없음"));

        /// <summary>
        /// 파일 열기 창의 필터입니다(예: "엑셀 파일|*.xlsx|모든 파일|*.*").
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(
                "Filter", typeof(string), typeof(ModernFilePickerControl),
                new PropertyMetadata("모든 파일|*.*"));

        /// <summary>필수 입력 여부입니다(제목 옆에 * 표시).</summary>
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired", typeof(bool), typeof(ModernFilePickerControl),
                new PropertyMetadata(false, OnIsRequiredChanged));

        /// <summary>버튼 사용 가능 여부입니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled", typeof(bool), typeof(ModernFilePickerControl),
                new PropertyMetadata(true));

        /// <summary>파일이 선택되었을 때 발생하는 이벤트입니다.</summary>
        public event EventHandler FileSelected;

        public ModernFilePickerControl()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        /// <summary>선택된 파일의 전체 경로입니다.</summary>
        public string FilePath
        {
            get { return (string)this.GetValue(FilePathProperty); }
            set { this.SetValue(FilePathProperty, value); }
        }

        /// <summary>파일이 비었을 때 보여줄 안내 문구입니다.</summary>
        public string PlaceholderText
        {
            get { return (string)this.GetValue(PlaceholderTextProperty); }
            set { this.SetValue(PlaceholderTextProperty, value); }
        }

        /// <summary>파일 열기 창의 필터입니다.</summary>
        public string Filter
        {
            get { return (string)this.GetValue(FilterProperty); }
            set { this.SetValue(FilterProperty, value); }
        }

        /// <summary>필수 입력 여부입니다.</summary>
        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        /// <summary>버튼 사용 가능 여부입니다.</summary>
        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateRequiredMark();
            this.UpdatePlaceholder();
        }

        private static void OnFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFilePickerControl)d).UpdatePlaceholder();
        }

        private static void OnIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernFilePickerControl)d).UpdateRequiredMark();
        }

        // "찾아보기" 버튼: 파일 열기 창을 띄우고, 고른 경로를 FilePath에 저장합니다.
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = this.Filter;

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                this.FilePath = dialog.FileName;
                if (this.FileSelected != null)
                {
                    this.FileSelected(this, EventArgs.Empty);
                }
            }
        }

        private void UpdatePlaceholder()
        {
            if (this.Placeholder != null)
            {
                this.Placeholder.Visibility = string.IsNullOrEmpty(this.FilePath)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void UpdateRequiredMark()
        {
            if (this.RequiredMark != null)
            {
                this.RequiredMark.Visibility = this.IsRequired ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
