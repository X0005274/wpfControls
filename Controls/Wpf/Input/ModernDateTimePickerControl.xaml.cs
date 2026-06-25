using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 날짜와 시각을 함께 고르는 컨트롤입니다.
    /// 달력(DatePicker)으로 날짜를, 시/분 콤보로 시각을 고릅니다.
    /// 선택한 값은 SelectedDateTime 속성(DateTime?)으로 읽고 씁니다(미선택이면 null).
    /// </summary>
    public partial class ModernDateTimePickerControl : UserControl
    {
        private bool isSyncing;

        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernDateTimePickerControl),
                new PropertyMetadata(string.Empty));

        /// <summary>선택된 날짜+시각입니다. 날짜를 안 골랐으면 null 입니다.</summary>
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register(
                "SelectedDateTime", typeof(DateTime?), typeof(ModernDateTimePickerControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedDateTimeChanged));

        /// <summary>필수 입력 여부입니다(제목 옆에 * 표시).</summary>
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired", typeof(bool), typeof(ModernDateTimePickerControl),
                new PropertyMetadata(false, OnIsRequiredChanged));

        /// <summary>입력 가능 여부입니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled", typeof(bool), typeof(ModernDateTimePickerControl),
                new PropertyMetadata(true));

        public ModernDateTimePickerControl()
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

        /// <summary>선택된 날짜+시각입니다(미선택이면 null).</summary>
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)this.GetValue(SelectedDateTimeProperty); }
            set { this.SetValue(SelectedDateTimeProperty, value); }
        }

        /// <summary>필수 입력 여부입니다.</summary>
        public bool IsRequired
        {
            get { return (bool)this.GetValue(IsRequiredProperty); }
            set { this.SetValue(IsRequiredProperty, value); }
        }

        /// <summary>입력 가능 여부입니다.</summary>
        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateRequiredMark();
            this.HourCombo.ItemsSource = BuildNumberStrings(24);
            this.MinuteCombo.ItemsSource = BuildNumberStrings(60);
            this.SyncValueToControls();
        }

        private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernDateTimePickerControl)d).SyncValueToControls();
        }

        private static void OnIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernDateTimePickerControl)d).UpdateRequiredMark();
        }

        // 달력/시/분 중 하나라도 바뀌면 SelectedDateTime을 다시 계산합니다.
        private void OnPartChanged(object sender, RoutedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            if (this.InnerDatePicker.SelectedDate.HasValue)
            {
                DateTime date = this.InnerDatePicker.SelectedDate.Value.Date;
                int hour = this.HourCombo.SelectedIndex < 0 ? 0 : this.HourCombo.SelectedIndex;
                int minute = this.MinuteCombo.SelectedIndex < 0 ? 0 : this.MinuteCombo.SelectedIndex;
                this.SelectedDateTime = date.AddHours(hour).AddMinutes(minute);
            }
            else
            {
                // 날짜를 안 골랐으면 값 없음으로 둡니다.
                this.SelectedDateTime = null;
            }

            this.isSyncing = false;
        }

        // SelectedDateTime 값을 달력과 시/분 콤보에 반영합니다.
        private void SyncValueToControls()
        {
            if (this.InnerDatePicker == null || this.HourCombo == null || this.MinuteCombo == null || this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            if (this.SelectedDateTime.HasValue)
            {
                DateTime value = this.SelectedDateTime.Value;
                this.InnerDatePicker.SelectedDate = value.Date;
                this.HourCombo.SelectedIndex = value.Hour;
                this.MinuteCombo.SelectedIndex = value.Minute;
            }
            else
            {
                this.InnerDatePicker.SelectedDate = null;
                this.HourCombo.SelectedIndex = 0;
                this.MinuteCombo.SelectedIndex = 0;
            }

            this.isSyncing = false;
        }

        private void UpdateRequiredMark()
        {
            if (this.RequiredMark != null)
            {
                this.RequiredMark.Visibility = this.IsRequired ? Visibility.Visible : Visibility.Collapsed;
            }
        }

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
