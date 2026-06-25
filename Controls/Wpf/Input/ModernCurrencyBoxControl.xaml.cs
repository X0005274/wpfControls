using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 금액을 입력받는 컨트롤입니다. 입력하는 동안 천 단위 콤마(예: 1,234,000)를
    /// 자동으로 넣어 주고, 통화 기호(₩ 등)를 앞에 표시합니다.
    /// 실제 숫자 값은 Value 속성(decimal)으로 읽고 씁니다.
    /// </summary>
    public partial class ModernCurrencyBoxControl : UserControl
    {
        private bool isSyncing;

        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernCurrencyBoxControl),
                new PropertyMetadata(string.Empty));

        /// <summary>금액 값입니다(양방향 바인딩 기본).</summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(decimal), typeof(ModernCurrencyBoxControl),
                new FrameworkPropertyMetadata(
                    decimal.Zero,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged));

        /// <summary>금액 앞에 붙일 통화 기호입니다(기본 "₩").</summary>
        public static readonly DependencyProperty PrefixProperty =
            DependencyProperty.Register(
                "Prefix", typeof(string), typeof(ModernCurrencyBoxControl),
                new PropertyMetadata("₩"));

        /// <summary>소수점 이하 자릿수입니다(기본 0 = 원 단위 정수).</summary>
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register(
                "DecimalPlaces", typeof(int), typeof(ModernCurrencyBoxControl),
                new PropertyMetadata(0, OnFormatChanged));

        /// <summary>필수 입력 여부입니다.</summary>
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired", typeof(bool), typeof(ModernCurrencyBoxControl),
                new PropertyMetadata(false, OnIsRequiredChanged));

        /// <summary>입력 가능 여부입니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled", typeof(bool), typeof(ModernCurrencyBoxControl),
                new PropertyMetadata(true));

        public ModernCurrencyBoxControl()
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

        /// <summary>금액 값입니다.</summary>
        public decimal Value
        {
            get { return (decimal)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>금액 앞에 붙일 통화 기호입니다.</summary>
        public string Prefix
        {
            get { return (string)this.GetValue(PrefixProperty); }
            set { this.SetValue(PrefixProperty, value); }
        }

        /// <summary>소수점 이하 자릿수입니다.</summary>
        public int DecimalPlaces
        {
            get { return (int)this.GetValue(DecimalPlacesProperty); }
            set { this.SetValue(DecimalPlacesProperty, value); }
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
            this.UpdateTextFromValue();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernCurrencyBoxControl)d).UpdateTextFromValue();
        }

        private static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernCurrencyBoxControl)d).UpdateTextFromValue();
        }

        private static void OnIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernCurrencyBoxControl)d).UpdateRequiredMark();
        }

        // 숫자와 소수점만 입력되도록 막습니다.
        private void InnerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsDigit(ch) && ch != '.')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        // 입력이 바뀌면 숫자로 해석해 Value에 반영하고, 천 단위 콤마로 다시 보여 줍니다.
        private void InnerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            string raw = this.InnerTextBox.Text.Replace(",", string.Empty);
            decimal parsed;
            if (string.IsNullOrEmpty(raw))
            {
                this.isSyncing = true;
                this.Value = decimal.Zero;
                this.InnerTextBox.Text = string.Empty;
                this.isSyncing = false;
                return;
            }

            if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.CurrentCulture, out parsed))
            {
                this.isSyncing = true;
                this.Value = parsed;
                string formatted = this.FormatValue(parsed);
                this.InnerTextBox.Text = formatted;
                this.InnerTextBox.CaretIndex = formatted.Length;
                this.isSyncing = false;
            }
        }

        // Value 값을 천 단위 콤마 글자로 만들어 화면에 표시합니다.
        private void UpdateTextFromValue()
        {
            if (this.InnerTextBox == null || this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;
            this.InnerTextBox.Text = this.FormatValue(this.Value);
            this.isSyncing = false;
        }

        // 예: 1234000 → "1,234,000"
        private string FormatValue(decimal value)
        {
            string pattern = "N" + (this.DecimalPlaces < 0 ? 0 : this.DecimalPlaces).ToString(CultureInfo.InvariantCulture);
            return value.ToString(pattern, CultureInfo.CurrentCulture);
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
