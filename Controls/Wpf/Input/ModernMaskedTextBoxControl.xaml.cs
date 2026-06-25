using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 형식이 정해진 숫자를 입력받는 컨트롤입니다.
    /// (예: 주민등록번호, 사업자등록번호, 전화번호)
    ///
    /// Mask 속성에 형식을 지정합니다.
    ///  - '0' : 숫자가 들어갈 자리
    ///  - 그 외 글자(예: '-') : 구분 기호. 숫자를 입력하면 자동으로 끼워 넣습니다.
    /// 예) 주민등록번호 = "000000-0000000",  전화번호 = "000-0000-0000"
    /// </summary>
    public partial class ModernMaskedTextBoxControl : UserControl
    {
        // 화면 글자와 Text 속성이 서로를 갱신하다 무한 반복되는 것을 막는 잠금 플래그입니다.
        private bool isSyncing;

        /// <summary>입력란 위에 표시할 제목입니다.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(string), typeof(ModernMaskedTextBoxControl),
                new PropertyMetadata(string.Empty));

        /// <summary>입력 형식입니다('0'=숫자 자리, 그 외=구분 기호).</summary>
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register(
                "Mask", typeof(string), typeof(ModernMaskedTextBoxControl),
                new PropertyMetadata("000000-0000000", OnMaskChanged));

        /// <summary>형식이 적용된 현재 글자입니다(예: "901010-1234567").</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(ModernMaskedTextBoxControl),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnTextChanged));

        /// <summary>값이 비었을 때 흐리게 보여줄 안내 문구입니다.</summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText", typeof(string), typeof(ModernMaskedTextBoxControl),
                new PropertyMetadata(string.Empty));

        /// <summary>필수 입력 여부입니다(제목 옆에 * 표시).</summary>
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register(
                "IsRequired", typeof(bool), typeof(ModernMaskedTextBoxControl),
                new PropertyMetadata(false, OnIsRequiredChanged));

        /// <summary>입력 가능 여부입니다(기본 true).</summary>
        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled", typeof(bool), typeof(ModernMaskedTextBoxControl),
                new PropertyMetadata(true));

        public ModernMaskedTextBoxControl()
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

        /// <summary>입력 형식입니다('0'=숫자, 그 외=구분 기호).</summary>
        public string Mask
        {
            get { return (string)this.GetValue(MaskProperty); }
            set { this.SetValue(MaskProperty, value); }
        }

        /// <summary>형식이 적용된 현재 글자입니다.</summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>값이 비었을 때 보여줄 안내 문구입니다.</summary>
        public string PlaceholderText
        {
            get { return (string)this.GetValue(PlaceholderTextProperty); }
            set { this.SetValue(PlaceholderTextProperty, value); }
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

        /// <summary>
        /// 구분 기호를 뺀 숫자만 돌려줍니다(예: "9010101234567").
        /// 서버 전송 등 실제 값이 필요할 때 사용하세요.
        /// </summary>
        public string GetDigitsOnly()
        {
            return ExtractDigits(this.Text);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateRequiredMark();
            this.UpdatePlaceholder();
            this.SyncTextToBox();
        }

        private static void OnMaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernMaskedTextBoxControl control = (ModernMaskedTextBoxControl)d;
            control.SyncTextToBox();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernMaskedTextBoxControl control = (ModernMaskedTextBoxControl)d;
            control.SyncTextToBox();
        }

        private static void OnIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernMaskedTextBoxControl control = (ModernMaskedTextBoxControl)d;
            control.UpdateRequiredMark();
        }

        // 숫자만 입력되도록 막습니다(구분 기호는 자동으로 들어갑니다).
        private void InnerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsDigit(ch))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        // 글자가 바뀌면 형식을 다시 적용하고 Text 속성에 반영합니다.
        private void InnerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            string digits = ExtractDigits(this.InnerTextBox.Text);
            string formatted = ApplyMask(digits, this.Mask);

            this.isSyncing = true;
            this.InnerTextBox.Text = formatted;
            this.InnerTextBox.CaretIndex = formatted.Length; // 커서를 맨 뒤로
            this.Text = formatted;
            this.isSyncing = false;

            this.UpdatePlaceholder();
        }

        // Text 속성(또는 Mask) 값을 화면 텍스트박스에 반영합니다.
        private void SyncTextToBox()
        {
            if (this.InnerTextBox == null || this.isSyncing)
            {
                return;
            }

            string formatted = ApplyMask(ExtractDigits(this.Text), this.Mask);
            this.isSyncing = true;
            this.InnerTextBox.Text = formatted;
            this.isSyncing = false;
            this.UpdatePlaceholder();
        }

        private void UpdatePlaceholder()
        {
            if (this.Placeholder != null && this.InnerTextBox != null)
            {
                this.Placeholder.Visibility = string.IsNullOrEmpty(this.InnerTextBox.Text)
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

        // 문자열에서 숫자만 골라냅니다.
        private static string ExtractDigits(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (char ch in value)
            {
                if (char.IsDigit(ch))
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString();
        }

        // 숫자들을 형식(mask)에 맞춰 구분 기호와 함께 배치합니다.
        private static string ApplyMask(string digits, string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                return digits;
            }

            StringBuilder builder = new StringBuilder();
            int digitIndex = 0;
            foreach (char maskChar in mask)
            {
                if (digitIndex >= digits.Length)
                {
                    break;
                }

                if (maskChar == '0')
                {
                    // 숫자 자리: 다음 숫자를 채웁니다.
                    builder.Append(digits[digitIndex]);
                    digitIndex++;
                }
                else
                {
                    // 구분 기호: 그대로 넣습니다.
                    builder.Append(maskChar);
                }
            }

            return builder.ToString();
        }
    }
}
