# com.example — Modern WPF Control Library (WinForms / ElementHost)

Pure WPF UserControls (no third-party UI frameworks), targeting **.NET Framework 4.8**,
designed to be hosted inside existing WinForms forms through
`System.Windows.Forms.Integration.ElementHost`.

---

## 1. Folder structure

```
wpfControls
├── com.example.sln                       # 2 projects: commons + samples
├── com.example.commons.csproj            # control library (assembly: com.example.dll)
├── INTEGRATION.md
├── Properties/AssemblyInfo.cs
├── Themes/                               # Tokens.xaml, Controls.xaml (design tokens + shared styles)
├── Models/Ui/                            # UI item models (INotifyPropertyChanged)
├── Controls/Wpf/                         # pure-WPF UserControls (the "*Control" types)
│   ├── Input/        ModernTextBoxControl, ModernPasswordBoxControl, ModernRichTextBoxControl,
│   │                 ModernSearchBoxControl, ModernButtonControl, ModernNumericUpDownControl, …
│   ├── Selection/    ModernComboBoxControl, ModernMultiSelectComboBoxControl, ModernCheckBoxControl,
│   │                 ModernRadioButtonGroupControl, ModernToggleSwitchControl, ModernListBoxControl, ModernSliderControl
│   ├── Display/      ModernLabelControl, ModernProgressBarControl, ModernStatusBarControl,
│   │                 ModernBadgeControl, ModernDatePickerControl, ModernChipControl, ModernKpiCardControl, …
│   ├── Data/         ModernDataGridControl, MatteDataGridControl, ModernListViewControl, ModernTreeViewControl, ModernTabControl
│   ├── Layout/       ModernExpanderControl, ModernGroupBoxControl, ModernFormSectionControl, ModernScrollSectionControl, …
│   └── Feedback/     ModernDialogWindow, ModernToastWindow, ModernLoadingPanelControl, ModernSpinnerControl
├── WinForms/                             # WinForms wrappers (ElementHost hosts; namespaces com.example.WinForms.Controls.*)
│   ├── Hosting/WpfElementHostBase.cs     # base class every wrapper derives from
│   └── Input/ Selection/ Display/ Data/ Layout/   # "Modern*" wrappers (the "*Control" minus the suffix)
├── Messaging/                            # TIBCO RV (Tibrv*) — compiled into com.example.dll only when TIBCO is present
└── com.example.samples/                  # runnable example gallery (assembly: com.example.samples.exe)
    ├── SampleShellForm.cs                # left-nav gallery shell
    ├── LotReceiveForm / EquipmentLotForm / InputSampleForm / …
    └── Screens/                          # example WPF business screens (Payroll, Employee Mgmt, …)
```

> `com.example.commons` keeps **assembly name `com.example`** (so XAML pack URIs `/com.example;component/…`
> and namespaces are unchanged). Wrappers + messaging are merged in, so a host adds just this one project.

---

## 2. Required assembly references

The library (`com.example.commons.csproj`, output `com.example.dll`) already references these:

- `PresentationCore`
- `PresentationFramework`
- `WindowsBase`
- `System.Xaml`
- `System`, `System.Core`, `System.Xml`, `System.Xml.Linq`, `System.Data`, `Microsoft.CSharp`
- `System.Windows.Forms`, `System.Drawing`
- `WindowsFormsIntegration`  *(provides `ElementHost`)*
- `TIBCO.Rendezvous`  *(conditional — only when `$(TibrvAssembly)` / env `TIBRV_HOME` exists; otherwise the messaging code is skipped and the controls still build)*

**Your existing WinForms host project** must add references to:

- `WindowsFormsIntegration`
- `PresentationCore`, `PresentationFramework`, `WindowsBase`
- A project reference (or assembly reference) to **com.example.commons** (`com.example.dll`).

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
   `com.example.commons.csproj`. If you add a control by copy/paste, replicate both the
   `<Page>` and the `<Compile><DependentUpon>` entries.

2. **`x:Class` mismatch.** The XAML `x:Class` must exactly equal
   `namespace.ClassName` of the code-behind (e.g.
   `com.example.Controls.Wpf.Input.ModernTextBoxControl`). A mismatch causes a
   compile error or a runtime `InitializeComponent` failure.

3. **Class library is not WPF-enabled.** `com.example.commons` must keep the WPF
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

---

## ModernComboBoxControl — searchable auto-suggest

`ModernComboBoxControl` is an **editable** combo: typing opens the dropdown and filters
the items in real time (case-insensitive). The match mode is configurable via the
`FilterMode` DependencyProperty (`com.example.Controls.Wpf.Selection.ComboBoxFilterMode`):

- `ComboBoxFilterMode.StartsWith` (default) — display text must start with the typed text.
- `ComboBoxFilterMode.Contains` — display text must contain the typed text anywhere.

Behavior: an empty text box restores the full list; no matches show an empty dropdown
(the control does not break); `ItemsSource`, `SelectedItem`, `SelectedValue`,
`DisplayMemberPath`, and `SelectedValuePath` continue to work as before.

```csharp
using com.example.Controls.Wpf.Selection;
using com.example.Models.Ui;

ModernComboBoxControl combo = new ModernComboBoxControl();
combo.Title = "Country";
combo.DisplayMemberPath = "Name";
combo.SelectedValuePath = "Code";
combo.FilterMode = ComboBoxFilterMode.Contains;   // or StartsWith (default)

List<ComboBoxItemModel> items = new List<ComboBoxItemModel>();
items.Add(new ComboBoxItemModel("KR", "South Korea"));
items.Add(new ComboBoxItemModel("US", "United States"));
combo.ItemsSource = items;
```

### Failure points — editable ComboBox filtering & binding

1. **Editable mode needs `PART_EditableTextBox`.** An editable `ComboBox` requires a
   `TextBox` named exactly `PART_EditableTextBox` in its `ControlTemplate`. If a custom
   template omits it, the control throws / does not accept typing. This template
   includes it and shows it via an `IsEditable` trigger.

2. **Disable the built-in text search.** Leave `IsTextSearchEnabled="False"`. With it on,
   WPF's own type-ahead fights the custom filter (auto-selecting/auto-completing items)
   and produces flicker and wrong selections.

3. **Keep the popup open while editing.** `StaysOpenOnEdit="True"` is required, otherwise
   editing the text closes the dropdown and the suggestions never show.

4. **Filter the `ICollectionView`, not `ComboBox.Items.Refresh()`.** Re-running the
   predicate requires refreshing the collection view returned by
   `CollectionViewSource.GetDefaultView(ItemsSource)`. Calling `ComboBox.Items.Refresh()`
   with a cached predicate does **not** re-evaluate the filter — the list appears stuck
   on the previous result.

5. **First-keystroke selection reset.** If an item is already selected, filtering it out
   of the view makes the editable ComboBox reset its text to empty, which re-triggers the
   filter and restores the full list — so the *first* search silently fails. The control
   clears `SelectedItem` when a real edit begins to prevent this. If you reimplement
   filtering, reproduce this guard.

6. **Selection echo.** Picking an item sets the editor text to that item's display text,
   which raises `TextChanged`. The control ignores a change whose text equals the current
   selection's display text, so the dropdown does not reopen after a selection.

7. **`DisplayMemberPath` drives the filter text.** Matching uses the item's
   `DisplayMemberPath` value (via reflection on a simple property name), falling back to
   `ToString()`. Nested/complex paths are not resolved for filtering — expose a simple
   string property (e.g. `Name`) or rely on `ToString()`.

8. **Shared collection instances.** Filtering uses the source's *default* collection view.
   If the **same** list instance is bound to another `ItemsControl`, the filter affects
   both. Give each searchable combo its own collection instance.

9. **`IsSynchronizedWithCurrentItem="False"`.** Set on the inner combo so selection is not
   tied to the view's current item; otherwise refreshing the filtered view can move the
   selection unexpectedly.
```
