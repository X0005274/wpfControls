using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using com.example.Controls.Wpf.Data;
using com.example.Controls.Wpf.Display;
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

            return Section("Input", new UIElement[] { name, employeeId, password, notes, search });
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

            return Section(
                "Selection",
                new UIElement[] { comboStartsWith, comboContains, check, radio, toggle, listBox, slider });
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

            return Section(
                "Display",
                new UIElement[] { label, progress, indeterminate, info, success, warning, error, badges, datePicker });
        }

        // ----------------------------------------------------------------- Data

        private static UIElement BuildDataSection()
        {
            List<DemoRow> rows = SampleRows();

            ModernDataGridControl grid = new ModernDataGridControl();
            grid.ItemsSource = rows;
            grid.Height = 180;

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

            return Section("Data", new UIElement[] { grid, listView, tree, tabHost });
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

            return Section(
                "Layout",
                new UIElement[] { expander, groupBox, formSection, scrollSection });
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
