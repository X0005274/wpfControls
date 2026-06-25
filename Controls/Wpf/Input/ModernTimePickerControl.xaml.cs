using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 시각(시:분)을 고르는 컨트롤입니다. 시 콤보(00~23)와 분 콤보(00~59)로 이루어집니다.
    /// 선택한 값은 SelectedTime 속성(TimeSpan)으로 읽고 씁니다.
    /// </summary>
    public partial class ModernTimePickerControl : UserControl
    {
        private bool isSyncing;

        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernTimePickerControl),
                new PropertyMetadata(string.Empty));

        /// <summary>선택된 시각입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty SelectedTimeProperty =
            DependencyProperty.Register(
                "SelectedTime", typeof(TimeSpan), typeof(ModernTimePickerControl),
                new FrameworkPropertyMetadata(
                    TimeSpan.Zero,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedTimeChanged));

        /// <summary>입력 가능 여부입니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled", typeof(bool), typeof(ModernTimePickerControl),
                new PropertyMetadata(true));

        public ModernTimePickerControl()
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

        /// <summary>선택된 시각입니다.</summary>
        public TimeSpan SelectedTime
        {
            get { return (TimeSpan)this.GetValue(SelectedTimeProperty); }
            set { this.SetValue(SelectedTimeProperty, value); }
        }

        /// <summary>입력 가능 여부입니다.</summary>
        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 제목 표시 여부
            this.TitleText.Visibility = string.IsNullOrEmpty(this.Title) ? Visibility.Collapsed : Visibility.Visible;

            // 시(00~23) / 분(00~59) 콤보 항목을 채웁니다.
            this.HourCombo.ItemsSource = BuildNumberStrings(24);
            this.MinuteCombo.ItemsSource = BuildNumberStrings(60);

            // 현재 SelectedTime 값을 콤보에 반영합니다.
            this.SyncTimeToCombos();
        }

        private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTimePickerControl)d).SyncTimeToCombos();
        }

        // 콤보 선택이 바뀌면 SelectedTime을 새로 계산합니다.
        private void OnTimePartChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            int hour = this.HourCombo.SelectedIndex < 0 ? 0 : this.HourCombo.SelectedIndex;
            int minute = this.MinuteCombo.SelectedIndex < 0 ? 0 : this.MinuteCombo.SelectedIndex;

            this.isSyncing = true;
            this.SelectedTime = new TimeSpan(hour, minute, 0);
            this.isSyncing = false;
        }

        // SelectedTime 값을 두 콤보의 선택 위치로 반영합니다.
        private void SyncTimeToCombos()
        {
            if (this.HourCombo == null || this.MinuteCombo == null || this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            this.HourCombo.SelectedIndex = this.SelectedTime.Hours;
            this.MinuteCombo.SelectedIndex = this.SelectedTime.Minutes;
            this.isSyncing = false;
        }

        // "00", "01", ... 형태의 두 자리 문자열 목록을 만듭니다.
        private static List<string> BuildNumberStrings(int count)
        {
            List<string> list = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(i.ToString("00", CultureInfo.InvariantCulture));
            }

            return list;
        }
    }
}
