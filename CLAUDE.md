# CLAUDE.md

## Project Overview

This project is an enterprise Windows desktop application built with Visual Studio 2026 and .NET Framework 4.8.

The existing UI is primarily based on WinForms.
New UI components must be implemented in pure WPF and hosted inside existing WinForms forms through ElementHost.

The goal is to modernize the UI incrementally while keeping existing business logic stable unless explicitly requested otherwise.

---

## Technical Environment

- IDE: Visual Studio 2026
- Runtime: .NET Framework 4.8
- Language: C#
- Existing UI: WinForms
- New reusable UI: pure WPF UserControl
- Hosting model: WinForms + ElementHost + WPF UserControl
- Root namespace: `com.example`

---

## Absolute Rules

- Do not use `var` under any circumstances.
- Always use explicit types such as `string`, `int`, `List<T>`, `ElementHost`, and concrete class names.
- Do not use external UI libraries unless explicitly requested.
- Use pure WPF only.
- Do not use MahApps, ModernWpf, MaterialDesign, DevExpress, Telerik, Syncfusion, or any other third-party UI framework unless explicitly requested.
- Generate complete, compilable code only.
- Do not generate partial examples, pseudocode, placeholders, or omitted sections.
- Do not assume the user will complete missing parts manually.
- Use syntax compatible with .NET Framework 4.8.
- Prefer maintainability, readability, and compatibility over compact or clever code.

---

## Code Style

- Use explicit access modifiers on all class members.
- Use PascalCase for class names, method names, property names, and public members.
- Use camelCase for local variables, method parameters, and private fields.
- Use braces for all control blocks: `if`, `else`, `for`, `foreach`, `while`, `using`.
- Do not write one-line control statements without braces.
- Avoid one-letter variable names except trivial loop counters.
- Prefer descriptive names over short names.
- Keep methods short and structurally clear.
- Split initialization into clearly named private methods when appropriate.

---

## Output Requirements

Whenever code is requested, always provide in this order:

1. Folder structure
2. File list
3. Full code for every file (no omissions)
4. WinForms integration example
5. Required references
6. Common build or integration failure points

---

## WPF Component Rules

- Use WPF UserControl as the base for all reusable components.
- Use DependencyProperty for all bindable properties exposed to callers.
- Keep visual styling in XAML when practical.
- Prefer simple, stable visual trees over unnecessarily complex templates.
- Design style must be modern, clean, and conservative for enterprise business software.
- All components must be hostable inside WinForms through ElementHost.

---

## WinForms Integration Rules

- New WPF components must work inside existing WinForms forms.
- When integration is requested, include:
  - WPF UserControl files
  - WinForms hosting example using `System.Windows.Forms.Integration.ElementHost`
  - Required assembly references
- Do not refactor existing WinForms business logic unless explicitly asked.

---

## Namespace and Folder Conventions

When adding code to the common library, use the following structure:

```
com.example
├── Controls
│   └── Wpf
│       ├── Input          (TextBox, PasswordBox, SearchBox, RichTextBox)
│       ├── Selection      (ComboBox, CheckBox, RadioButton, ToggleSwitch, ListBox, Slider)
│       ├── Display        (Label, ProgressBar, StatusBar, Badge, ToolTip, Popup)
│       ├── Data           (DataGrid, ListView, TreeView, TabControl)
│       └── Layout         (Expander, GroupBox, FormSection, ScrollSection)
└── Models
    └── Ui                 (ComboBoxItemModel, etc.)
```

Always ensure the following match exactly:
- XAML `x:Class`
- code-behind namespace
- class name
- folder structure

---

## Standard Component Targets

### Input
- `ModernTextBoxControl` — labeled text input with placeholder, required, read-only, enabled states
- `ModernPasswordBoxControl` — password input with show/hide toggle
- `ModernRichTextBoxControl` — multi-line formatted text editor
- `ModernSearchBoxControl` — search input with clear button and search event

### Selection
- `ModernComboBoxControl` — dropdown with DisplayMemberPath, SelectedValuePath, SelectedValue, SelectedItem
- `ModernCheckBoxControl` — styled checkbox with title and description
- `ModernRadioButtonGroupControl` — radio button group with bindable selected value
- `ModernToggleSwitchControl` — on/off toggle based on WPF ToggleButton with animation
- `ModernListBoxControl` — selectable list with ItemsSource binding
- `ModernSliderControl` — range slider with min, max, current value display

### Display
- `ModernLabelControl` — styled label with title and optional description
- `ModernProgressBarControl` — determinate and indeterminate progress bar
- `ModernStatusBarControl` — status bar with icon and message
- `ModernBadgeControl` — colored badge for status or count display
- `ModernDatePickerControl` — date selection with title and required indication

### Data
- `ModernDataGridControl` — data grid with column definitions and row selection
- `ModernListViewControl` — list view with custom item template
- `ModernTreeViewControl` — hierarchical tree view with bindable items
- `ModernTabControl` — tabbed container with icon and title support

### Layout
- `ModernExpanderControl` — collapsible section with header title
- `ModernGroupBoxControl` — titled group container
- `ModernFormSectionControl` — grid-based labeled form section
- `ModernScrollSectionControl` — scrollable container with fixed header

---

## Error Prevention Checklist

Before finalizing any code, verify all of the following:

- [ ] No `var`
- [ ] No missing `using` statements
- [ ] No missing class definitions
- [ ] No missing model classes
- [ ] No missing event handlers
- [ ] No namespace mismatches
- [ ] No XAML and code-behind mismatches
- [ ] No missing file names
- [ ] No missing folder paths
- [ ] No unsupported third-party dependency
- [ ] No hidden assumptions about InitializeComponent generation
- [ ] XAML Build Action is `Page`
- [ ] code-behind class uses `partial`
- [ ] `x:Class` matches the namespace and class name exactly

---

## XAML and Build Safety

- Ensure every XAML file compiles as a WPF page (Build Action = `Page`).
- Ensure `x:Class`, namespace, and partial class name match exactly.
- If there is any risk of `InitializeComponent` failure, explain the exact cause and prevention steps.
- If the project structure may prevent WPF XAML compilation, warn clearly and propose the safest structure.
- If `com.example` is a general Class Library, note that it must reference WPF assemblies and have WPF XAML build support enabled.

---

## Response Behavior

- If requirements are ambiguous, ask a short clarifying question before generating code.
- If the task is implementation-oriented, prioritize direct code output over long explanation.
- If a build or integration failure is likely, identify the exact failure point.
- Prefer practical enterprise-ready answers over tutorial-style discussion.
- Keep explanations brief. Focus on production-ready code.
