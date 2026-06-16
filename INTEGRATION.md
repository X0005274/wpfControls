# com.example — Modern WPF Control Library (WinForms / ElementHost)

Pure WPF UserControls (no third-party UI frameworks), targeting **.NET Framework 4.8**,
designed to be hosted inside existing WinForms forms through
`System.Windows.Forms.Integration.ElementHost`.

---

## 1. Folder structure

```
wpfControls
├── com.example.sln
├── com.example.csproj
├── INTEGRATION.md
├── Properties
│   └── AssemblyInfo.cs
├── Models
│   └── Ui
│       ├── ComboBoxItemModel.cs
│       ├── RadioButtonItemModel.cs
│       └── TreeViewItemModel.cs
└── Controls
    └── Wpf
        ├── Input
        │   ├── ModernTextBoxControl.xaml(.cs)
        │   ├── ModernPasswordBoxControl.xaml(.cs)
        │   ├── ModernRichTextBoxControl.xaml(.cs)
        │   └── ModernSearchBoxControl.xaml(.cs)
        ├── Selection
        │   ├── ModernComboBoxControl.xaml(.cs)
        │   ├── ModernCheckBoxControl.xaml(.cs)
        │   ├── ModernRadioButtonGroupControl.xaml(.cs)
        │   ├── ModernToggleSwitchControl.xaml(.cs)
        │   ├── ModernListBoxControl.xaml(.cs)
        │   └── ModernSliderControl.xaml(.cs)
        ├── Display
        │   ├── ModernLabelControl.xaml(.cs)
        │   ├── ModernProgressBarControl.xaml(.cs)
        │   ├── ModernStatusBarControl.xaml(.cs)
        │   ├── ModernBadgeControl.xaml(.cs)
        │   └── ModernDatePickerControl.xaml(.cs)
        ├── Data
        │   ├── ModernDataGridControl.xaml(.cs)
        │   ├── ModernListViewControl.xaml(.cs)
        │   ├── ModernTreeViewControl.xaml(.cs)
        │   └── ModernTabControl.xaml(.cs)
        └── Layout
            ├── ModernExpanderControl.xaml(.cs)
            ├── ModernGroupBoxControl.xaml(.cs)
            ├── ModernFormSectionControl.xaml(.cs)
            └── ModernScrollSectionControl.xaml(.cs)
```

---

## 2. Required assembly references

The library (`com.example.csproj`) already references these:

- `PresentationCore`
- `PresentationFramework`
- `WindowsBase`
- `System.Xaml`
- `System`, `System.Core`, `System.Xml`, `System.Xml.Linq`, `System.Data`, `Microsoft.CSharp`
- `System.Windows.Forms`
- `WindowsFormsIntegration`  *(provides `ElementHost`)*

**Your existing WinForms host project** must add references to:

- `WindowsFormsIntegration`
- `PresentationCore`, `PresentationFramework`, `WindowsBase`
- A project reference (or assembly reference) to **com.example**.

---

## 3. WinForms integration with ElementHost

### Option A — code-behind in an existing WinForms `Form`

```csharp
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using com.example.Controls.Wpf.Input;

namespace YourCompany.WinFormsApp
{
    public partial class CustomerForm : Form
    {
        private ElementHost textBoxHost;
        private ModernTextBoxControl modernTextBox;

        public CustomerForm()
        {
            this.InitializeComponent();
            this.InitializeWpfControls();
        }

        private void InitializeWpfControls()
        {
            this.modernTextBox = new ModernTextBoxControl();
            this.modernTextBox.Title = "Customer Name";
            this.modernTextBox.PlaceholderText = "Enter full name";
            this.modernTextBox.IsRequired = true;

            this.textBoxHost = new ElementHost();
            this.textBoxHost.Dock = DockStyle.Top;
            this.textBoxHost.Height = 80;
            this.textBoxHost.Child = this.modernTextBox;

            this.Controls.Add(this.textBoxHost);
        }

        private string ReadValue()
        {
            return this.modernTextBox.Text;
        }
    }
}
```

### Option B — ComboBox with `ComboBoxItemModel`

```csharp
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using com.example.Controls.Wpf.Selection;
using com.example.Models.Ui;

private void AddComboBox()
{
    ModernComboBoxControl combo = new ModernComboBoxControl();
    combo.Title = "Country";
    combo.DisplayMemberPath = "Name";
    combo.SelectedValuePath = "Code";

    List<ComboBoxItemModel> items = new List<ComboBoxItemModel>();
    items.Add(new ComboBoxItemModel("KR", "South Korea"));
    items.Add(new ComboBoxItemModel("US", "United States"));
    items.Add(new ComboBoxItemModel("JP", "Japan"));
    combo.ItemsSource = items;

    ElementHost host = new ElementHost();
    host.Dock = DockStyle.Top;
    host.Height = 70;
    host.Child = combo;
    this.Controls.Add(host);
}
```

### Option C — handling the SearchBox `SearchRequested` routed event

```csharp
using System.Windows;                       // RoutedEventArgs
using com.example.Controls.Wpf.Input;

ModernSearchBoxControl search = new ModernSearchBoxControl();
search.PlaceholderText = "Search orders...";
search.SearchRequested += this.OnSearchRequested;
// host it via ElementHost as above

private void OnSearchRequested(object sender, RoutedEventArgs e)
{
    ModernSearchBoxControl box = (ModernSearchBoxControl)sender;
    string term = box.SearchText;
    // run your query
}
```

### Layout controls (Expander / GroupBox / FormSection / ScrollSection)

These expose their child through the default content property `InnerContent`, so in
**XAML** you simply nest content:

```xml
<layout:ModernGroupBoxControl HeaderText="Address">
    <StackPanel>
        <!-- any WPF content -->
    </StackPanel>
</layout:ModernGroupBoxControl>
```

From **WinForms code-behind**, assign `InnerContent` directly:

```csharp
ModernGroupBoxControl group = new ModernGroupBoxControl();
group.HeaderText = "Address";
group.InnerContent = new ModernTextBoxControl { Title = "Street" };
```

`ModernFormSectionControl` is a shared-size scope. Give each row's label column the
`SharedSizeGroup="FormLabel"` so labels line up:

```xml
<layout:ModernFormSectionControl SectionTitle="Profile">
    <StackPanel>
        <Grid Margin="0,4">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" SharedSizeGroup="FormLabel" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <TextBlock Grid.Column="0" Text="Name" VerticalAlignment="Center" Margin="0,0,12,0" />
            <input:ModernTextBoxControl Grid.Column="1" />
        </Grid>
    </StackPanel>
</layout:ModernFormSectionControl>
```

---

## 4. Public surface (per component)

| Component | Bindable DependencyProperties | Other public members |
|---|---|---|
| ModernTextBoxControl | Title, Description, Text (2-way), PlaceholderText, IsRequired, IsReadOnly, IsEditorEnabled | — |
| ModernPasswordBoxControl | Title, Description, IsRequired, IsEditorEnabled | `string Password` |
| ModernRichTextBoxControl | Title, Description, IsRequired, IsReadOnly, IsEditorEnabled | `string PlainText` |
| ModernSearchBoxControl | SearchText (2-way), PlaceholderText, IsEditorEnabled | `SearchRequested` routed event |
| ModernComboBoxControl | Title, ItemsSource, SelectedItem (2-way), SelectedValue (2-way), DisplayMemberPath, SelectedValuePath, IsRequired, IsEditorEnabled | — |
| ModernCheckBoxControl | Title, Description, IsChecked (2-way), IsEditorEnabled | — |
| ModernRadioButtonGroupControl | Title, ItemsSource, SelectedValue (2-way), IsEditorEnabled | — |
| ModernToggleSwitchControl | Title, Description, IsOn (2-way), IsEditorEnabled | — |
| ModernListBoxControl | Title, ItemsSource, SelectedItem (2-way), DisplayMemberPath, IsEditorEnabled | — |
| ModernSliderControl | Title, Minimum, Maximum, Value (2-way), IsEditorEnabled | — |
| ModernLabelControl | Title, Description, IsRequired | — |
| ModernProgressBarControl | Title, Value, Minimum, Maximum, IsIndeterminate | — |
| ModernStatusBarControl | StatusText, StatusType (Info/Success/Warning/Error) | — |
| ModernBadgeControl | BadgeText, BadgeType (Info/Success/Warning/Error/Neutral) | — |
| ModernDatePickerControl | Title, Description, SelectedDate (2-way), IsRequired, IsEditorEnabled | — |
| ModernDataGridControl | ItemsSource, SelectedItem (2-way), IsReadOnly | — |
| ModernListViewControl | ItemsSource, SelectedItem (2-way) | `GridViewColumnCollection Columns` |
| ModernTreeViewControl | ItemsSource, SelectedItem (2-way) | — |
| ModernTabControl | — | `ItemCollection Items`, `int SelectedIndex` |
| ModernExpanderControl | HeaderText, IsExpanded (2-way), InnerContent (content) | — |
| ModernGroupBoxControl | HeaderText, InnerContent (content) | — |
| ModernFormSectionControl | SectionTitle, InnerContent (content) | — |
| ModernScrollSectionControl | HeaderText, InnerContent (content) | — |

---

## 5. Common build / integration failure points

1. **`InitializeComponent` does not exist / XAML not compiled.**
   Each `.xaml` must have Build Action **Page** with `Generator = MSBuild:Compile`, and
   the matching `.xaml.cs` must be a `partial` class. This is already configured in
   `com.example.csproj`. If you add a control by copy/paste, replicate both the
   `<Page>` and the `<Compile><DependentUpon>` entries.

2. **`x:Class` mismatch.** The XAML `x:Class` must exactly equal
   `namespace.ClassName` of the code-behind (e.g.
   `com.example.Controls.Wpf.Input.ModernTextBoxControl`). A mismatch causes a
   compile error or a runtime `InitializeComponent` failure.

3. **Class library is not WPF-enabled.** `com.example` must keep the WPF
   `ProjectTypeGuids` (`{60dc8134-...}`) and reference `PresentationCore`,
   `PresentationFramework`, `WindowsBase`, `System.Xaml`. Removing these breaks XAML
   markup compilation.

4. **Missing `WindowsFormsIntegration` in the host.** `ElementHost` lives in
   `WindowsFormsIntegration.dll`. The WinForms host project must reference it.

5. **Forgetting to set `ElementHost.Child`.** An `ElementHost` added to the form with
   no `Child` shows nothing. Always set `host.Child = yourWpfControl;`.

6. **Sizing.** WPF controls size to content. Give the `ElementHost` an explicit
   `Height`/`Dock`/`Anchor`, or the control may render at zero height inside WinForms.

7. **PasswordBox value.** `ModernPasswordBoxControl.Password` is a plain CLR property
   (not a DependencyProperty) by design — do not attempt to data-bind it.

8. **STA thread.** WPF requires an STA thread. WinForms `Main` is normally marked
   `[STAThread]`; keep it. Creating these controls on a background thread will throw.

9. **DataGrid/ListView columns.** `ModernDataGridControl` auto-generates columns from
   the bound items. `ModernListViewControl` requires you to add `GridViewColumn`s via
   its `Columns` property before binding `ItemsSource`.

10. **Two-way `SelectedValue` on the radio group.** Bind to `RadioButtonItemModel.Code`
    values; the control matches `SelectedValue` against each item's `Code`.
```
