using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Renders a vertical group of styled radio buttons from an
    /// <see cref="ItemsSource"/> of <see cref="RadioButtonItemModel"/>.
    /// The selected item's Code is exposed through <see cref="SelectedValue"/>.
    /// </summary>
    public partial class ModernRadioButtonGroupControl : UserControl
    {
        private readonly string groupName;
        private bool isSyncing;

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ModernRadioButtonGroupControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ModernRadioButtonGroupControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(ModernRadioButtonGroupControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedValueChanged));

        public static readonly DependencyProperty IsEditorEnabledProperty =
            DependencyProperty.Register(
                "IsEditorEnabled",
                typeof(bool),
                typeof(ModernRadioButtonGroupControl),
                new PropertyMetadata(true, OnIsEditorEnabledChanged));

        public ModernRadioButtonGroupControl()
        {
            this.groupName = "RbGroup_" + Guid.NewGuid().ToString("N");
            this.isSyncing = false;
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        public bool IsEditorEnabled
        {
            get { return (bool)this.GetValue(IsEditorEnabledProperty); }
            set { this.SetValue(IsEditorEnabledProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernRadioButtonGroupControl control = (ModernRadioButtonGroupControl)d;
            control.RebuildItems();
        }

        private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernRadioButtonGroupControl control = (ModernRadioButtonGroupControl)d;
            control.SyncCheckedState();
        }

        private static void OnIsEditorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernRadioButtonGroupControl control = (ModernRadioButtonGroupControl)d;
            bool enabled = (bool)e.NewValue;
            foreach (object child in control.ItemsHost.Children)
            {
                RadioButton button = child as RadioButton;
                if (button != null)
                {
                    button.IsEnabled = enabled;
                }
            }
        }

        private void RebuildItems()
        {
            this.ItemsHost.Children.Clear();

            if (this.ItemsSource == null)
            {
                return;
            }

            Style itemStyle = (Style)this.FindResource("ModernRadioButtonStyle");

            foreach (object item in this.ItemsSource)
            {
                RadioButtonItemModel model = item as RadioButtonItemModel;
                if (model == null)
                {
                    continue;
                }

                RadioButton button = new RadioButton();
                button.Style = itemStyle;
                button.GroupName = this.groupName;
                button.Content = model.Name;
                button.Tag = model.Code;
                button.IsEnabled = this.IsEditorEnabled;
                button.Checked += this.RadioButton_Checked;
                this.ItemsHost.Children.Add(button);
            }

            this.SyncCheckedState();
        }

        private void SyncCheckedState()
        {
            if (this.isSyncing)
            {
                return;
            }

            this.isSyncing = true;

            object selected = this.SelectedValue;
            foreach (object child in this.ItemsHost.Children)
            {
                RadioButton button = child as RadioButton;
                if (button == null)
                {
                    continue;
                }

                bool matches = selected != null && object.Equals(button.Tag, selected);
                button.IsChecked = matches;
            }

            this.isSyncing = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.isSyncing)
            {
                return;
            }

            RadioButton button = (RadioButton)sender;
            this.SelectedValue = button.Tag;
        }
    }
}
