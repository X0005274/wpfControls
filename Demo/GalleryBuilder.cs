using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using com.example.Controls.Wpf.Data;
using com.example.Controls.Wpf.Display;
using com.example.Controls.Wpf.Feedback;
using com.example.Controls.Wpf.Input;
using com.example.Controls.Wpf.Layout;
using com.example.Controls.Wpf.Selection;
using com.example.Models.Ui;

namespace com.example.Demo
{
    /// <summary>
    /// Builds a WPF visual tree containing every modern control with sample data.
    /// All construction is done in code (no XAML) so the demo project stays a
    /// plain WinForms executable. The returned element is assigned to
    /// <c>ElementHost.Child</c>.
    /// </summary>
    public static class GalleryBuilder
    {
        private static readonly Brush PageBackground = new SolidColorBrush(Color.FromRgb(0xF3, 0xF4, 0xF6));
        private static readonly Brush BodyText = new SolidColorBrush(Color.FromRgb(0x37, 0x41, 0x51));

        public static FrameworkElement Build(Action<string> onSearch)
        {
            StackPanel root = new StackPanel();
            root.Margin = new Thickness(24);
            root.Width = 860;
            root.HorizontalAlignment = HorizontalAlignment.Left;

            root.Children.Add(BuildInputSection(onSearch));
            root.Children.Add(BuildSelectionSection());
            root.Children.Add(BuildDisplaySection());
            root.Children.Add(BuildDataSection());
            root.Children.Add(BuildLayoutSection());
            root.Children.Add(BuildFeedbackSection());

            ScrollViewer scroller = new ScrollViewer();
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            scroller.Background = PageBackground;
            scroller.Content = root;
            return scroller;
        }

        // ---------------------------------------------------------------- Input

        private static UIElement BuildInputSection(Action<string> onSearch)
        {
            ModernTextBoxControl name = new ModernTextBoxControl();
            name.Title = "Full Name";
            name.PlaceholderText = "Enter full name";
            name.IsRequired = true;

            ModernTextBoxControl employeeId = new ModernTextBoxControl();
            employeeId.Title = "Employee ID";
            employeeId.Text = "E-10293";
            employeeId.IsReadOnly = true;

            ModernPasswordBoxControl password = new ModernPasswordBoxControl();
            password.Title = "Password";
            password.IsRequired = true;
            password.Password = "P@ssw0rd!";

            ModernRichTextBoxControl notes = new ModernRichTextBoxControl();
            notes.Title = "Notes";
            notes.PlainText = "Multi-line rich text editor." + Environment.NewLine + "Second line of notes.";
            notes.Height = 150;

            ModernSearchBoxControl search = new ModernSearchBoxControl();
            search.PlaceholderText = "Search employees... (type and press Enter)";
            if (onSearch != null)
            {
                search.SearchRequested += delegate(object sender, RoutedEventArgs e)
                {
                    onSearch(search.SearchText);
                };
            }

            // 버튼 3종(Primary/Secondary/Danger)을 한 줄에 배치합니다.
            WrapPanel buttonRow = new WrapPanel();
            buttonRow.Children.Add(NewButton("저장", ButtonKind.Primary));
            buttonRow.Children.Add(NewButton("취소", ButtonKind.Secondary));
            buttonRow.Children.Add(NewButton("삭제", ButtonKind.Danger));

            ModernNumericUpDownControl numeric = new ModernNumericUpDownControl();
            numeric.Title = "나이";
            numeric.Minimum = 0;
            numeric.Maximum = 120;
            numeric.Value = 30;

            ModernMaskedTextBoxControl masked = new ModernMaskedTextBoxControl();
            masked.Title = "주민등록번호";
            masked.Mask = "000000-0000000";
            masked.PlaceholderText = "앞 6자리 - 뒤 7자리";

            ModernCurrencyBoxControl currency = new ModernCurrencyBoxControl();
            currency.Title = "기본급";
            currency.Value = 3500000m;

            ModernDateTimePickerControl dateTime = new ModernDateTimePickerControl();
            dateTime.Title = "출근 일시";
            dateTime.SelectedDateTime = DateTime.Now;

            ModernTimePickerControl timePicker = new ModernTimePickerControl();
            timePicker.Title = "근무 시작 시각";
            timePicker.SelectedTime = new TimeSpan(9, 0, 0);

            ModernFilePickerControl filePicker = new ModernFilePickerControl();
            filePicker.Title = "증빙 파일";
            filePicker.Filter = "모든 파일|*.*";

            return Section(
                "Input",
                new UIElement[]
                {
                    name, employeeId, password, notes, search,
                    buttonRow, numeric, masked, currency, dateTime, timePicker, filePicker
                });
        }

        // ------------------------------------------------------------ Selection

        private static UIElement BuildSelectionSection()
        {
            // Searchable auto-suggest combo in StartsWith mode (e.g. type "u" -> United ...).
            ModernComboBoxControl comboStartsWith = new ModernComboBoxControl();
            comboStartsWith.Title = "Country (filter: StartsWith)";
            comboStartsWith.DisplayMemberPath = "Name";
            comboStartsWith.SelectedValuePath = "Code";
            comboStartsWith.FilterMode = ComboBoxFilterMode.StartsWith;
            comboStartsWith.ItemsSource = BuildCountryList();
            comboStartsWith.SelectedValue = "KR";

            // Same data in Contains mode (e.g. type "an" -> Germany, Japan).
            // Each combo gets its own list instance: filtering uses the source's
            // default collection view, so sharing one list would filter both.
            ModernComboBoxControl comboContains = new ModernComboBoxControl();
            comboContains.Title = "Country (filter: Contains)";
            comboContains.DisplayMemberPath = "Name";
            comboContains.SelectedValuePath = "Code";
            comboContains.FilterMode = ComboBoxFilterMode.Contains;
            comboContains.ItemsSource = BuildCountryList();
            comboContains.SelectedValue = "US";

            ModernCheckBoxControl check = new ModernCheckBoxControl();
            check.Title = "Subscribe to newsletter";
            check.Description = "Receive product updates and announcements.";
            check.IsChecked = true;

            ModernRadioButtonGroupControl radio = new ModernRadioButtonGroupControl();
            radio.Title = "Subscription plan";
            List<RadioButtonItemModel> plans = new List<RadioButtonItemModel>();
            plans.Add(new RadioButtonItemModel("basic", "Basic"));
            plans.Add(new RadioButtonItemModel("pro", "Professional"));
            plans.Add(new RadioButtonItemModel("ent", "Enterprise"));
            radio.ItemsSource = plans;
            radio.SelectedValue = "pro";

            ModernToggleSwitchControl toggle = new ModernToggleSwitchControl();
            toggle.Title = "Email notifications";
            toggle.Description = "Send a notification when a task completes.";
            toggle.IsOn = true;

            ModernListBoxControl listBox = new ModernListBoxControl();
            listBox.Title = "Departments";
            List<string> departments = new List<string>();
            departments.Add("Engineering");
            departments.Add("Finance");
            departments.Add("Human Resources");
            departments.Add("Sales");
            departments.Add("Support");
            listBox.ItemsSource = departments;
            listBox.Height = 140;

            ModernSliderControl slider = new ModernSliderControl();
            slider.Title = "Volume";
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Value = 35;

            // Multi-select combo with checkboxes; two items pre-selected.
            ModernMultiSelectComboBoxControl multiSelect = new ModernMultiSelectComboBoxControl();
            multiSelect.Title = "Skills (multi-select)";
            multiSelect.PlaceholderText = "Select skills...";
            multiSelect.DisplayMemberPath = "Name";
            List<ComboBoxItemModel> skills = new List<ComboBoxItemModel>();
            skills.Add(new ComboBoxItemModel("cs", "C#"));
            skills.Add(new ComboBoxItemModel("wpf", "WPF"));
            skills.Add(new ComboBoxItemModel("sql", "SQL"));
            skills.Add(new ComboBoxItemModel("az", "Azure"));
            skills.Add(new ComboBoxItemModel("dkr", "Docker"));
            multiSelect.ItemsSource = skills;
            List<object> preselected = new List<object>();
            preselected.Add(skills[0]);
            preselected.Add(skills[2]);
            multiSelect.SelectedItems = preselected;

            return Section(
                "Selection",
                new UIElement[] { comboStartsWith, comboContains, multiSelect, check, radio, toggle, listBox, slider });
        }

        private static List<ComboBoxItemModel> BuildCountryList()
        {
            List<ComboBoxItemModel> countries = new List<ComboBoxItemModel>();
            countries.Add(new ComboBoxItemModel("KR", "South Korea"));
            countries.Add(new ComboBoxItemModel("US", "United States"));
            countries.Add(new ComboBoxItemModel("JP", "Japan"));
            countries.Add(new ComboBoxItemModel("DE", "Germany"));
            return countries;
        }

        // -------------------------------------------------------------- Display

        private static UIElement BuildDisplaySection()
        {
            ModernLabelControl label = new ModernLabelControl();
            label.Title = "Project Code";
            label.Description = "Internal identifier used across reports.";
            label.IsRequired = true;

            ModernProgressBarControl progress = new ModernProgressBarControl();
            progress.Title = "Upload progress";
            progress.Minimum = 0;
            progress.Maximum = 100;
            progress.Value = 65;

            ModernProgressBarControl indeterminate = new ModernProgressBarControl();
            indeterminate.Title = "Synchronizing";
            indeterminate.IsIndeterminate = true;

            ModernStatusBarControl info = NewStatus("Connected to server.", "Info");
            ModernStatusBarControl success = NewStatus("Records saved successfully.", "Success");
            ModernStatusBarControl warning = NewStatus("Session will expire soon.", "Warning");
            ModernStatusBarControl error = NewStatus("Failed to reach the API.", "Error");

            WrapPanel badges = new WrapPanel();
            badges.Children.Add(NewBadge("Info", "Info"));
            badges.Children.Add(NewBadge("Active", "Success"));
            badges.Children.Add(NewBadge("Pending", "Warning"));
            badges.Children.Add(NewBadge("Failed", "Error"));
            badges.Children.Add(NewBadge("Draft", "Neutral"));

            ModernDatePickerControl datePicker = new ModernDatePickerControl();
            datePicker.Title = "Hire Date";
            datePicker.SelectedDate = DateTime.Today;
            datePicker.IsRequired = true;

            ModernDividerControl divider = new ModernDividerControl();
            divider.Text = "추가 정보";

            ModernAvatarControl avatar = new ModernAvatarControl();
            avatar.Text = "한";
            avatar.Diameter = 48;

            // 태그(칩) 여러 개를 한 줄에 배치합니다(마지막은 삭제 가능).
            WrapPanel chipRow = new WrapPanel();
            chipRow.Children.Add(NewChip("관리자", false));
            chipRow.Children.Add(NewChip("정규직", false));
            chipRow.Children.Add(NewChip("개발팀", true));

            ModernCardControl card = new ModernCardControl();
            card.Title = "공지";
            card.Subtitle = "2026년 정기 인사";
            card.InnerContent = MakeText("카드 컨테이너 안에는 어떤 내용이든 넣을 수 있습니다.");

            // KPI 카드 3개를 한 줄에 배치합니다(대시보드용).
            WrapPanel kpiRow = new WrapPanel();
            kpiRow.Children.Add(NewKpi("총 직원", "1,234", "지난달 대비 +5"));
            kpiRow.Children.Add(NewKpi("재직", "1,180", "95.6%"));
            kpiRow.Children.Add(NewKpi("휴직", "54", "4.4%"));

            return Section(
                "Display",
                new UIElement[]
                {
                    label, progress, indeterminate, info, success, warning, error, badges, datePicker,
                    divider, avatar, chipRow, card, kpiRow
                });
        }

        // ----------------------------------------------------------------- Data

        private static UIElement BuildDataSection()
        {
            List<DemoRow> rows = SampleRows();

            ModernDataGridControl grid = new ModernDataGridControl();
            grid.ItemsSource = rows;
            grid.Height = 180;
            grid.SelectedItem = rows[1];

            MatteDataGridControl matteGrid = new MatteDataGridControl();
            matteGrid.ItemsSource = SampleRows();
            matteGrid.Height = 180;

            ModernListViewControl listView = new ModernListViewControl();
            listView.Columns.Add(NewColumn("Code", "Code", 120));
            listView.Columns.Add(NewColumn("Name", "Name", 260));
            listView.Columns.Add(NewColumn("Status", "Status", 140));
            listView.ItemsSource = rows;
            listView.Height = 180;

            ModernTreeViewControl tree = new ModernTreeViewControl();
            tree.ItemsSource = SampleTree();
            tree.Height = 210;

            ModernTabControl tab = new ModernTabControl();
            tab.Items.Add(NewTab("General", "General tab content goes here."));
            tab.Items.Add(NewTab("Details", "Detailed information is shown on this tab."));
            tab.Items.Add(NewTab("History", "An audit history would appear here."));
            tab.SelectedIndex = 0;

            FrameworkElement tabHost = tab;
            tabHost.Height = 200;

            return Section("Data", new UIElement[] { grid, matteGrid, listView, tree, tabHost });
        }

        // --------------------------------------------------------------- Layout

        private static UIElement BuildLayoutSection()
        {
            ModernExpanderControl expander = new ModernExpanderControl();
            expander.HeaderText = "Advanced options";
            expander.IsExpanded = true;
            expander.InnerContent = MakeText(
                "This region is collapsible. Click the header to toggle the animated expand/collapse.");

            ModernGroupBoxControl groupBox = new ModernGroupBoxControl();
            groupBox.HeaderText = "Contact";
            StackPanel contact = new StackPanel();
            ModernTextBoxControl emailBox = new ModernTextBoxControl();
            emailBox.Title = "Email";
            emailBox.PlaceholderText = "name@example.com";
            contact.Children.Add(emailBox);
            groupBox.InnerContent = contact;

            ModernFormSectionControl formSection = new ModernFormSectionControl();
            formSection.SectionTitle = "Address";
            StackPanel rows = new StackPanel();
            rows.Children.Add(FormRow("Street", "123 Main St"));
            rows.Children.Add(FormRow("City", "Seoul"));
            rows.Children.Add(FormRow("Postal Code", "04524"));
            formSection.InnerContent = rows;

            ModernScrollSectionControl scrollSection = new ModernScrollSectionControl();
            scrollSection.HeaderText = "Terms & Conditions";
            scrollSection.Height = 180;
            StackPanel terms = new StackPanel();
            int i;
            for (i = 1; i <= 12; i = i + 1)
            {
                TextBlock line = MakeText("Clause " + i.ToString() + ": Sample scrollable content line.");
                line.Margin = new Thickness(0, 0, 0, 8);
                terms.Children.Add(line);
            }
            scrollSection.InnerContent = terms;

            ModernPageHeaderControl pageHeader = new ModernPageHeaderControl();
            pageHeader.Title = "직원 관리";
            pageHeader.Subtitle = "전체 1,234명";
            ModernButtonControl headerAction = new ModernButtonControl();
            headerAction.Text = "신규 등록";
            pageHeader.InnerContent = headerAction;

            ModernBreadcrumbControl breadcrumb = new ModernBreadcrumbControl();
            breadcrumb.Path = "홈/직원관리/상세";

            ModernSegmentedControl segmented = new ModernSegmentedControl();
            List<string> segments = new List<string>();
            segments.Add("전체");
            segments.Add("재직");
            segments.Add("휴직");
            segmented.ItemsSource = segments;
            segmented.SelectedIndex = 0;

            ModernStepIndicatorControl steps = new ModernStepIndicatorControl();
            List<string> stepList = new List<string>();
            stepList.Add("신청");
            stepList.Add("승인");
            stepList.Add("완료");
            steps.ItemsSource = stepList;
            steps.CurrentStep = 1;

            ModernPaginationControl pagination = new ModernPaginationControl();
            pagination.TotalPages = 10;
            pagination.CurrentPage = 3;

            return Section(
                "Layout",
                new UIElement[]
                {
                    pageHeader, breadcrumb, segmented, steps,
                    expander, groupBox, formSection, scrollSection, pagination
                });
        }

        // ------------------------------------------------------------- Feedback

        private static UIElement BuildFeedbackSection()
        {
            // 토스트/대화상자를 띄우는 데모 버튼들입니다.
            WrapPanel buttons = new WrapPanel();

            ModernButtonControl toastInfo = NewButton("정보 토스트", ButtonKind.Secondary);
            toastInfo.Click += delegate { ModernToast.Show("정보 알림입니다.", MessageKind.Info); };
            buttons.Children.Add(toastInfo);

            ModernButtonControl toastSuccess = NewButton("성공 토스트", ButtonKind.Primary);
            toastSuccess.Click += delegate { ModernToast.Show("저장되었습니다.", MessageKind.Success); };
            buttons.Children.Add(toastSuccess);

            ModernButtonControl toastError = NewButton("오류 토스트", ButtonKind.Danger);
            toastError.Click += delegate { ModernToast.Show("오류가 발생했습니다.", MessageKind.Error); };
            buttons.Children.Add(toastError);

            ModernButtonControl confirm = NewButton("확인 대화상자", ButtonKind.Secondary);
            confirm.Click += delegate
            {
                bool ok = ModernDialog.Confirm("삭제 확인", "선택한 항목을 삭제할까요?");
                ModernToast.Show(
                    ok ? "삭제를 진행합니다." : "취소했습니다.",
                    ok ? MessageKind.Success : MessageKind.Info);
            };
            buttons.Children.Add(confirm);

            ModernButtonControl alert = NewButton("알림 대화상자", ButtonKind.Secondary);
            alert.Click += delegate { ModernDialog.Alert("안내", "처리가 완료되었습니다.", MessageKind.Success); };
            buttons.Children.Add(alert);

            TextBlock note = MakeText(
                "토스트·대화상자는 폼에 올리는 컨트롤이 아니라 코드에서 호출하는 헬퍼입니다. "
                + "(로딩 오버레이는 WinForms 폼을 대상으로 하므로 이 WPF 갤러리에서는 생략했습니다.)");
            note.Margin = new Thickness(0, 8, 0, 0);

            return Section("Feedback (토스트 · 대화상자)", new UIElement[] { buttons, note });
        }

        // -------------------------------------------------------------- Helpers

        private static ModernGroupBoxControl Section(string title, UIElement[] children)
        {
            StackPanel panel = new StackPanel();
            int index;
            for (index = 0; index < children.Length; index = index + 1)
            {
                UIElement child = children[index];
                FrameworkElement element = child as FrameworkElement;
                if (element != null)
                {
                    element.Margin = new Thickness(0, 0, 0, 16);
                    element.HorizontalAlignment = HorizontalAlignment.Stretch;
                }

                panel.Children.Add(child);
            }

            ModernGroupBoxControl group = new ModernGroupBoxControl();
            group.HeaderText = title;
            group.InnerContent = panel;
            group.Margin = new Thickness(0, 0, 0, 20);
            return group;
        }

        private static ModernStatusBarControl NewStatus(string text, string type)
        {
            ModernStatusBarControl status = new ModernStatusBarControl();
            status.StatusText = text;
            status.StatusType = type;
            return status;
        }

        private static ModernBadgeControl NewBadge(string text, string type)
        {
            ModernBadgeControl badge = new ModernBadgeControl();
            badge.BadgeText = text;
            badge.BadgeType = type;
            badge.Margin = new Thickness(0, 0, 8, 0);
            return badge;
        }

        private static ModernButtonControl NewButton(string text, ButtonKind kind)
        {
            ModernButtonControl button = new ModernButtonControl();
            button.Text = text;
            button.Kind = kind;
            button.Margin = new Thickness(0, 0, 8, 0);
            return button;
        }

        private static ModernChipControl NewChip(string text, bool removable)
        {
            ModernChipControl chip = new ModernChipControl();
            chip.Text = text;
            chip.IsRemovable = removable;
            chip.Margin = new Thickness(0, 0, 8, 0);
            return chip;
        }

        private static ModernKpiCardControl NewKpi(string label, string value, string caption)
        {
            ModernKpiCardControl kpi = new ModernKpiCardControl();
            kpi.Label = label;
            kpi.Value = value;
            kpi.Caption = caption;
            kpi.Width = 180;
            kpi.Margin = new Thickness(0, 0, 12, 0);
            return kpi;
        }

        private static GridViewColumn NewColumn(string header, string path, double width)
        {
            GridViewColumn column = new GridViewColumn();
            column.Header = header;
            column.DisplayMemberBinding = new Binding(path);
            column.Width = width;
            return column;
        }

        private static TabItem NewTab(string header, string content)
        {
            TabItem tab = new TabItem();
            tab.Header = header;
            tab.Content = MakeText(content);
            return tab;
        }

        private static Grid FormRow(string label, string placeholder)
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 4, 0, 4);

            ColumnDefinition labelColumn = new ColumnDefinition();
            labelColumn.Width = GridLength.Auto;
            labelColumn.SharedSizeGroup = "FormLabel";

            ColumnDefinition inputColumn = new ColumnDefinition();
            inputColumn.Width = new GridLength(1, GridUnitType.Star);

            grid.ColumnDefinitions.Add(labelColumn);
            grid.ColumnDefinitions.Add(inputColumn);

            TextBlock caption = new TextBlock();
            caption.Text = label;
            caption.VerticalAlignment = VerticalAlignment.Center;
            caption.Margin = new Thickness(0, 0, 16, 0);
            caption.Foreground = BodyText;
            caption.FontSize = 13;
            Grid.SetColumn(caption, 0);

            ModernTextBoxControl box = new ModernTextBoxControl();
            box.PlaceholderText = placeholder;
            Grid.SetColumn(box, 1);

            grid.Children.Add(caption);
            grid.Children.Add(box);
            return grid;
        }

        private static TextBlock MakeText(string text)
        {
            TextBlock block = new TextBlock();
            block.Text = text;
            block.TextWrapping = TextWrapping.Wrap;
            block.Foreground = BodyText;
            block.FontSize = 13;
            return block;
        }

        private static List<DemoRow> SampleRows()
        {
            List<DemoRow> rows = new List<DemoRow>();
            rows.Add(new DemoRow("E-1001", "Jiwoo Han", "Active"));
            rows.Add(new DemoRow("E-1002", "Maria Garcia", "Active"));
            rows.Add(new DemoRow("E-1003", "Kenji Tanaka", "On Leave"));
            rows.Add(new DemoRow("E-1004", "Lukas Müller", "Inactive"));
            rows.Add(new DemoRow("E-1005", "Sofia Rossi", "Active"));
            return rows;
        }

        private static ObservableCollection<TreeViewItemModel> SampleTree()
        {
            TreeViewItemModel engineering = new TreeViewItemModel("Engineering");
            engineering.IsExpanded = true;
            engineering.Children.Add(new TreeViewItemModel("Backend"));
            engineering.Children.Add(new TreeViewItemModel("Frontend"));
            engineering.Children.Add(new TreeViewItemModel("QA"));

            TreeViewItemModel operations = new TreeViewItemModel("Operations");
            operations.Children.Add(new TreeViewItemModel("Logistics"));
            operations.Children.Add(new TreeViewItemModel("Facilities"));

            ObservableCollection<TreeViewItemModel> tree = new ObservableCollection<TreeViewItemModel>();
            tree.Add(engineering);
            tree.Add(operations);
            return tree;
        }
    }
}
