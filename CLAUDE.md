# CLAUDE.md

Enterprise Windows desktop app. Existing UI is WinForms; new reusable UI is **pure WPF** hosted in WinForms through `ElementHost`. Modernize the UI incrementally — do not change existing business logic unless explicitly asked.

## Environment

- IDE: Visual Studio 2026 · Runtime: .NET Framework 4.8 · Language: C# (7.3)
- New UI: pure WPF `UserControl`, hosted via `System.Windows.Forms.Integration.ElementHost`
- Root namespace: `com.example`

## Absolute rules

- Never use `var`. Use explicit types (`string`, `int`, `List<T>`, `ElementHost`, concrete class names).
- No third-party UI libraries (MahApps, ModernWpf, MaterialDesign, DevExpress, Telerik, Syncfusion, etc.). Pure WPF only, unless the user explicitly requests otherwise.
- Output complete, compilable code only — no pseudocode, placeholders, TODOs, or omitted sections. Never assume the user will fill in missing parts.
- Use only syntax supported by .NET Framework 4.8 / C# 7.3.
- Do not refactor existing WinForms business logic unless explicitly asked.

## C# style

- Explicit access modifier on every type and member.
- PascalCase for types, methods, properties, and public members; camelCase for locals, parameters, and private fields.
- Braces on every control block (`if`/`else`/`for`/`foreach`/`while`/`using`) — no brace-less one-liners.
- Descriptive names; single-letter names only for trivial loop counters.
- Split long initialization into named private methods.

## WPF component rules

- Base every reusable component on `UserControl`.
- Expose every bindable value as a `DependencyProperty` with a CLR wrapper. Register two-way values with `FrameworkPropertyMetadataOptions.BindsTwoWayByDefault`.
- Expose secrets (e.g. password text) through a plain CLR property, never a `DependencyProperty`.
- Keep visual styling in XAML; prefer simple, stable visual trees over complex templates.
- Visual design: modern, clean, conservative enterprise look. No flashy gradients, heavy shadows, or unnecessary animation.
- Every component must render and function when hosted in WinForms via `ElementHost`.

Canonical DependencyProperty:

```csharp
public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register(
        "Title", typeof(string), typeof(MyControl), new PropertyMetadata(string.Empty));

public string Title
{
    get { return (string)this.GetValue(TitleProperty); }
    set { this.SetValue(TitleProperty, value); }
}
```

## WinForms integration

When integration is requested, deliver all of:

- The WPF `UserControl` files (XAML + code-behind).
- A WinForms hosting example using `ElementHost` — set `host.Child` and give the host an explicit size (`Dock`/`Height`/`Anchor`).
- The required assembly references.

Host project must reference `WindowsFormsIntegration` plus `PresentationCore`, `PresentationFramework`, `WindowsBase`. Run WPF on an STA thread (`[STAThread]`).

```csharp
ElementHost host = new ElementHost();
host.Dock = DockStyle.Top;
host.Height = 80;
host.Child = new ModernTextBoxControl();
this.Controls.Add(host);
```

## Namespace & folder layout

```
com.example
├── Controls/Wpf
│   ├── Input       (TextBox, PasswordBox, SearchBox, RichTextBox)
│   ├── Selection   (ComboBox, CheckBox, RadioButton, ToggleSwitch, ListBox, Slider)
│   ├── Display     (Label, ProgressBar, StatusBar, Badge, DatePicker)
│   ├── Data        (DataGrid, ListView, TreeView, TabControl)
│   └── Layout      (Expander, GroupBox, FormSection, ScrollSection)
└── Models/Ui       (ComboBoxItemModel, etc.)
```

- Place each control in the folder matching its category; place item models in `Models/Ui`.

## Standard components

### Input
- `ModernTextBoxControl` — labeled input; placeholder, required, read-only, enabled states
- `ModernPasswordBoxControl` — password input with show/hide toggle
- `ModernRichTextBoxControl` — multi-line formatted text editor
- `ModernSearchBoxControl` — search input with clear button and search event

### Selection
- `ModernComboBoxControl` — `DisplayMemberPath`, `SelectedValuePath`, `SelectedValue`, `SelectedItem`
- `ModernCheckBoxControl` — styled checkbox with title and description
- `ModernRadioButtonGroupControl` — radio group with bindable selected value
- `ModernToggleSwitchControl` — on/off toggle (WPF `ToggleButton`)
- `ModernListBoxControl` — selectable list with `ItemsSource` binding
- `ModernSliderControl` — range slider with min/max and current-value display

### Display
- `ModernLabelControl` — title and optional description
- `ModernProgressBarControl` — determinate and indeterminate
- `ModernStatusBarControl` — status indicator and message
- `ModernBadgeControl` — colored badge for status/count
- `ModernDatePickerControl` — date selection with title and required indicator

### Data
- `ModernDataGridControl` — column definitions and row selection
- `ModernListViewControl` — list view with custom item template
- `ModernTreeViewControl` — hierarchical tree with bindable items
- `ModernTabControl` — tabbed container

### Layout
- `ModernExpanderControl` — collapsible section with header
- `ModernGroupBoxControl` — titled group container
- `ModernFormSectionControl` — grid-based labeled form section
- `ModernScrollSectionControl` — scrollable container with fixed header

## Models & files

- One public type per file; the file name matches the type name (and `x:Class` for controls).
- UI item models implement `INotifyPropertyChanged`.
- Expose mutable bound collections as `ObservableCollection<T>`.
- `ItemsSource` DPs are typed `IEnumerable`; `SelectedItem`/`SelectedValue` are typed `object` to stay binding-agnostic.

## XAML & build correctness — verify before finishing

- [ ] `x:Class`, code-behind namespace + class name, file name, and folder all match exactly.
- [ ] Code-behind class is `partial`.
- [ ] Each `x:Name` is unique and does not collide with a DependencyProperty's CLR property name.
- [ ] Each `.xaml` uses Build Action `Page` with `<Generator>MSBuild:Compile</Generator>`; the `.csproj` has both the `Page` entry and the matching `Compile` + `DependentUpon` entry.
- [ ] The `com.example` library is WPF-enabled: WPF `ProjectTypeGuids` and references to `PresentationCore`, `PresentationFramework`, `WindowsBase`, `System.Xaml`.
- [ ] All `using` statements, referenced model classes, and event handlers exist.
- [ ] No `var`; no third-party UI dependency.
- [ ] If `InitializeComponent` could fail, state the exact cause and the fix.

## Code output format

When code is requested, present in this order:

1. Folder structure
2. File list
3. Full code for every file (no omissions)
4. WinForms `ElementHost` integration example
5. Required references
6. Common build/integration failure points

## Response behavior

- Ask one short clarifying question only when requirements are genuinely ambiguous; otherwise proceed.
- Lead with code; keep prose brief and practical.
- When a build or integration failure is likely, name the exact failure point and the fix.
