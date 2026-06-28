# com.example — Modern WPF Control Library

A set of **24 pure-WPF UserControls** with a clean, conservative, enterprise look,
built for **.NET Framework 4.8** and designed to be hosted inside existing
**WinForms** applications through `ElementHost`.

- **No third-party UI frameworks** — no MahApps, ModernWpf, MaterialDesign, DevExpress, Telerik, or Syncfusion. Pure WPF only.
- **Bindable by design** — every reusable value is exposed as a `DependencyProperty`.
- **WinForms-first hosting** — each control drops into a `System.Windows.Forms.Integration.ElementHost`.
- **Searchable & multi-select** — an editable auto-suggest combo (`StartsWith` / `Contains` filter modes) and a multi-select combo with checkboxes.
- **Incremental modernization** — add modern UI to legacy WinForms forms without rewriting business logic.

> Namespace root: `com.example` · IDE: Visual Studio 2026 · Language: C#

---

## Screenshots

The included **WinForms sample gallery** (`com.example.samples`) renders the controls hosted in
`ElementHost`s, framed by a native WinForms shell (left-nav menu + sample screens).

### Input
![Input controls](docs/screenshots/01-input.png)

Labeled text box (required / placeholder / focus states), read-only text box, password box with show/hide toggle, multi-line rich text, and a search box.

### Selection
![Selection controls](docs/screenshots/02-selection.png)

Searchable auto-suggest combo box, multi-select combo with checkboxes, checkbox, radio button group, animated toggle switch, list box, and slider.

The multi-select combo keeps its dropdown open while you toggle checkboxes and shows a comma-separated summary in the collapsed field:

![Multi-select combo dropdown](docs/screenshots/07-multiselect.png)

### Display
![Display controls](docs/screenshots/03-display.png)

Label, determinate & indeterminate progress bars, color-coded status bars (Info / Success / Warning / Error), pill badges, and a date picker.

### Data
![Data controls](docs/screenshots/04-data.png)

DataGrid with alternating rows, GridView-based ListView, hierarchical TreeView with expand/collapse arrows, and an underline-style TabControl.

### Layout
![Layout controls](docs/screenshots/05-layout.png)

Animated Expander, GroupBox, FormSection (shared-size aligned label/input rows), and a ScrollSection with a fixed header.

---

## Controls

| Group | Controls |
|-------|----------|
| **Input** | `ModernTextBoxControl`, `ModernPasswordBoxControl`, `ModernRichTextBoxControl`, `ModernSearchBoxControl` |
| **Selection** | `ModernComboBoxControl` (searchable auto-suggest), `ModernMultiSelectComboBoxControl` (checkbox multi-select), `ModernCheckBoxControl`, `ModernRadioButtonGroupControl`, `ModernToggleSwitchControl`, `ModernListBoxControl`, `ModernSliderControl` |
| **Display** | `ModernLabelControl`, `ModernProgressBarControl`, `ModernStatusBarControl`, `ModernBadgeControl`, `ModernDatePickerControl` |
| **Data** | `ModernDataGridControl`, `ModernListViewControl`, `ModernTreeViewControl`, `ModernTabControl` |
| **Layout** | `ModernExpanderControl`, `ModernGroupBoxControl`, `ModernFormSectionControl`, `ModernScrollSectionControl` |

The full DependencyProperty / public-API surface for each control is documented in
[`INTEGRATION.md`](INTEGRATION.md).

---

## Solution layout

```
wpfControls
├── com.example.sln                  # 2 projects: commons + samples
├── com.example.commons.csproj       # control library  (assembly: com.example.dll)
│   ├── Themes/                      # Tokens.xaml + Controls.xaml (design tokens + shared styles)
│   ├── Controls/Wpf/                # pure-WPF UserControls — Input/ Selection/ Display/ Data/ Layout/ Feedback/
│   ├── WinForms/                    # WinForms wrappers (ElementHost hosts) — Hosting/ + same groups
│   ├── Messaging/                   # TIBCO RV (Tibrv*) — compiled only when TIBCO is present
│   └── Models/Ui/                   # UI item models (INotifyPropertyChanged)
├── INTEGRATION.md                   # hosting guide + API reference
└── com.example.samples/             # runnable example gallery (assembly: com.example.samples.exe)
    ├── com.example.samples.csproj
    ├── Program.cs                   # [STAThread] entry point
    ├── SampleShellForm.cs           # left-nav gallery shell (one line per sample)
    ├── LotReceiveForm / EquipmentLotForm / InputSampleForm / …
    └── Screens/                     # example WPF business screens (Payroll, Employee Mgmt, …)
```

> The wrappers and messaging are merged into `com.example.commons` (one DLL, `com.example.dll`),
> so a host solution adds **just that one project**. Add `com.example.commons` only — `com.example.samples`
> is the example gallery (run it; don't add it to your solution).

---

## Build & run

**Requirements:** Visual Studio 2026 (or MSBuild) with the .NET Framework 4.8 targeting pack.

```powershell
# Build the whole solution
msbuild com.example.sln /t:Rebuild /p:Configuration=Debug

# Run the WinForms sample gallery
.\com.example.samples\bin\Debug\com.example.samples.exe
```

In Visual Studio: set **com.example.samples** as the startup project and press **F5**.
Type in the demo's search box and press **Enter** to see a WPF routed event update the
native WinForms status bar.

---

## Hosting a control in WinForms

```csharp
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using com.example.Controls.Wpf.Input;

ModernTextBoxControl textBox = new ModernTextBoxControl();
textBox.Title = "Customer Name";
textBox.PlaceholderText = "Enter full name";
textBox.IsRequired = true;

ElementHost host = new ElementHost();
host.Dock = DockStyle.Top;
host.Height = 80;
host.Child = textBox;

this.Controls.Add(host);   // 'this' is your existing WinForms Form
```

The host project must reference `WindowsFormsIntegration`, `PresentationCore`,
`PresentationFramework`, `WindowsBase`, and the **`com.example.commons`** project (output
`com.example.dll`). See
[`INTEGRATION.md`](INTEGRATION.md) for ComboBox, SearchBox event, and layout examples,
plus common build/integration failure points.

---

## Conventions

- Explicit types everywhere (no `var`); explicit access modifiers; braces on all control blocks.
- XAML `x:Class`, code-behind namespace, and folder structure always match.
- Each `.xaml` builds as `Page`; each code-behind is a `partial` class.
