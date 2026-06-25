using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 숫자 입력 컨트롤입니다. 텍스트로 직접 입력하거나 오른쪽 위/아래 버튼으로
    /// 값을 올리고 내릴 수 있습니다. 값은 항상 Minimum~Maximum 범위로 보정됩니다.
    /// </summary>
    public partial class ModernNumericUpDownControl : UserControl
    {
        // Value 속성과 텍스트박스가 서로를 갱신하다 무한 반복되는 것을 막는 잠금 플래그입니다.
        private bool isSyncing;

        /// <summary>입력란 위에 표시할 제목입니다. 비워두면 제목 줄이 사라집니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernNumericUpDownControl),
                new PropertyMetadata(string.Empty, OnTitleChanged));

        /// <summary>현재 숫자 값입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(ModernNumericUpDownControl),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged));

        /// <summary>입력 가능한 최솟값입니다(기본 0).</summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(ModernNumericUpDownControl),
                new PropertyMetadata(0.0));

        /// <summary>입력 가능한 최댓값입니다(기본 100).</summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(ModernNumericUpDownControl),
                new PropertyMetadata(100.0));

        /// <summary>위/아래 버튼을 한 번 누를 때 변하는 값(기본 1)입니다.</summary>
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register(
                "Step",
                typeof(double),
                typeof(ModernNumericUpDownControl),
                new PropertyMetadata(1.0));

        /// <summary>입력 가능 여부입니다. false면 읽기 전용처럼 동작합니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernNumericUpDownControl),
                new PropertyMetadata(true));

        /// <summary>값이 바뀌었을 때 발생하는 이벤트입니다.</summary>
        public event EventHandler ValueChanged;

        public ModernNumericUpDownControl()
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

        /// <summary>현재 숫자 값입니다.</summary>
        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>입력 가능한 최솟값입니다.</summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>입력 가능한 최댓값입니다.</summary>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>위/아래 버튼을 한 번 누를 때 변하는 값입니다.</summary>
        public double Step
        {
            get { return (double)this.GetValue(StepProperty); }
            set { this.SetValue(StepProperty, value); }
        }

        /// <summary>입력 가능 여부입니다.</summary>
        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 처음 표시될 때 제목 표시 여부와 텍스트를 한 번 맞춰 줍니다.
            this.UpdateTitleVisibility();
            this.UpdateTextFromValue();
        }

        // 제목 값이 바뀌면 제목 줄을 보이거나 숨깁니다.
        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernNumericUpDownControl control = (ModernNumericUpDownControl)d;
            control.UpdateTitleVisibility();
        }

        // Value가 바뀌면 텍스트를 갱신하고 ValueChanged 이벤트를 알립니다.
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernNumericUpDownControl control = (ModernNumericUpDownControl)d;
            control.UpdateTextFromValue();
            if (control.ValueChanged != null)
            {
                control.ValueChanged(control, EventArgs.Empty);
            }
        }

        private void UpdateTitleVisibility()
        {
            if (this.TitleText != null)
            {
                this.TitleText.Visibility = string.IsNullOrEmpty(this.Title)
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        // Value 값을 텍스트박스 글자로 반영합니다.
        private void UpdateTextFromValue()
        {
            if (this.ValueTextBox == null || this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            this.ValueTextBox.Text = this.Value.ToString(CultureInfo.CurrentCulture);
            this.isSyncing = false;
        }

        // 위 버튼: Step만큼 증가
        private void UpManager(double direction)
        {
            double next = this.Value + (this.Step * direction);
            this.Value = this.Clamp(next);
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            this.UpManager(1.0);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            this.UpManager(-1.0);
        }

        // 값이 범위를 벗어나면 최소/최대로 보정합니다.
        private double Clamp(double value)
        {
            if (value < this.Minimum)
            {
                return this.Minimum;
            }

            if (value > this.Maximum)
            {
                return this.Maximum;
            }

            return value;
        }

        // 숫자/소수점/음수 기호만 입력되도록 허용합니다.
        private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string candidate = e.Text;
            foreach (char ch in candidate)
            {
                bool isAllowed = char.IsDigit(ch) || ch == '.' || ch == '-';
                if (!isAllowed)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        // 포커스를 잃으면 입력한 텍스트를 숫자로 바꿔 Value에 반영합니다.
        private void ValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.CommitText();
        }

        // Enter 키를 누르면 즉시 반영합니다.
        private void ValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CommitText();
            }
        }

        // 텍스트박스 글자를 숫자로 해석해 Value에 적용합니다(실패하면 원래 값 복원).
        private void CommitText()
        {
            if (this.ValueTextBox == null || this.isSyncing)
            {
                return;
            }

            double parsed;
            if (double.TryParse(this.ValueTextBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out parsed))
            {
                this.Value = this.Clamp(parsed);
            }
            else
            {
                // 숫자로 못 바꾸면 현재 값을 다시 보여 줍니다.
                this.UpdateTextFromValue();
            }
        }
    }
}
