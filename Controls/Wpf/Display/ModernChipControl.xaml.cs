using System;
using System.Windows;
using System.Windows.Controls;

namespace com.example.Controls.Wpf.Display
{
    /// <summary>
    /// 작고 둥근 태그(칩)입니다. 필터·키워드 표시에 씁니다.
    /// IsRemovable 이 true면 오른쪽에 삭제(✕) 버튼이 나타나고,
    /// 누르면 Removed 이벤트가 발생합니다.
    /// </summary>
    public partial class ModernChipControl : UserControl
    {
        /// <summary>칩에 표시할 글자입니다.</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(ModernChipControl),
                new PropertyMetadata(string.Empty));

        /// <summary>삭제(✕) 버튼 표시 여부입니다(기본 false).</summary>
        public static readonly DependencyProperty IsRemovableProperty =
            DependencyProperty.Register(
                "IsRemovable", typeof(bool), typeof(ModernChipControl),
                new PropertyMetadata(false, OnIsRemovableChanged));

        /// <summary>삭제(✕) 버튼을 눌렀을 때 발생하는 이벤트입니다.</summary>
        public event EventHandler Removed;

        public ModernChipControl()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) => this.UpdateRemoveButton();
        }

        /// <summary>칩에 표시할 글자입니다.</summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>삭제(✕) 버튼 표시 여부입니다.</summary>
        public bool IsRemovable
        {
            get { return (bool)this.GetValue(IsRemovableProperty); }
            set { this.SetValue(IsRemovableProperty, value); }
        }

        private static void OnIsRemovableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ModernChipControl)d).UpdateRemoveButton();
        }

        private void UpdateRemoveButton()
        {
            if (this.RemoveButton != null)
            {
                this.RemoveButton.Visibility = this.IsRemovable ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Removed != null)
            {
                this.Removed(this, EventArgs.Empty);
            }
        }
    }
}
