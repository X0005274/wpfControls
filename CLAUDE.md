# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

Enterprise Windows desktop app. Existing UI is WinForms; new reusable UI is **pure WPF** hosted in WinForms through `ElementHost`. Modernize the UI incrementally — do not change existing business logic unless explicitly asked.

## Environment

- IDE: Visual Studio 2026 (VS 18) · Runtime: .NET Framework 4.8 · Language: C# (7.3)
- Root namespace: `com.example`
- Use only syntax supported by .NET Framework 4.8 / C# 7.3.

## Build & run

There is no test project; "verify" means a clean build plus running the demo.

```bash
# Build everything (run from repo root). Building the Demo transitively builds the libs.
msbuild com.example.sln /t:Build /p:Configuration=Debug

# This machine's MSBuild (VS 2026 = VS 18):
"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" \
  Demo/com.example.Demo.csproj /t:Build /p:Configuration=Debug /v:minimal /clp:ErrorsOnly

# Run the WinForms demo host
Demo/bin/Debug/com.example.Demo.exe
```

- A running `com.example.Demo.exe` locks the build output (MSB3027). **Kill it before rebuilding.**
- In Visual Studio: set **com.example.Demo** as the startup project and press **F5**.
- Many source files are UTF-8 **without BOM** and contain Korean comments. Do not edit them with tools that re-encode (e.g. PowerShell `Get-Content`/`Set-Content` without an explicit `UTF8Encoding($false)`), or the Korean will be corrupted.

## Solution structure (4 projects)

| Project | Output | Role |
|---|---|---|
| `com.example` (repo root `com.example.csproj`) | WPF class library | The reusable **pure-WPF** UserControls + the design-token theme. |
| `com.example.WinForms.Controls` | WinForms class library | Thin **wrappers** that host each WPF control in an `ElementHost` so legacy WinForms forms/designer can drop them in. |
| `com.example.Messaging.Rendezvous` | class library | TIBCO Rendezvous messaging (request/reply client + server, message router). Non-UI; independent of the control libraries. |
| `Demo` (`Demo/com.example.Demo.csproj`) | WinExe | WinForms host that exercises the controls. Startup object is `SampleShellForm`. |

## Architecture (the parts that span files)

### Design tokens are the single source of truth
`Themes/Tokens.xaml` is a `ResourceDictionary` of every color, font size, spacing, radius, and elevation value. **Controls must consume tokens and never hardcode hex / px / font-weight** — change a token once and every control and screen follows. Controls pull it in via merged dictionary `Source="/com.example;component/Themes/Tokens.xaml"`. The design language (Windows Fluent: accent `#0078D4`, Segoe UI, control radius 4 / card radius 8, a fixed type ramp, structural-elements-SemiBold / body-Regular) is documented in `STYLEGUIDE.md`. When a value recurs, add a token rather than a literal.

### Two parallel control sets, one source of behavior
For most controls there is a WPF control (`Controls/Wpf/...`) **and** a WinForms wrapper (`com.example.WinForms.Controls/...`) of the same name minus the `Control` suffix (e.g. `ModernDataGridControl` ↔ `ModernDataGrid`). The wrapper holds no real logic — behavior and styling live in the WPF control. **Put grid/badge/sort/visual changes in the WPF control so every host inherits them**; the wrapper only re-exposes inner `DependencyProperty`s as CLR properties and surfaces events (via `DependencyPropertyDescriptor.AddValueChanged` on the inner control's DP).

### The wrapper / ElementHost pattern is designer-safe by design
All wrappers derive from `com.example.WinForms.Controls/Hosting/WpfElementHostBase<TWpf>`. It creates the inner WPF control in its constructor and hosts it as the `ElementHost.Child` **at runtime only** (skipped at design-time, restored in `OnHandleCreated`). It is marked `[Designer(ControlDesigner)]` so the VS form designer treats the wrapper as an opaque control instead of re-serializing the WPF `Child` — without this, dragging a wrapper in the designer breaks the form. When adding a new wrapper, inherit this base; do not host the WPF control yourself.

### The Demo is a menu-driven shell
`Demo/SampleShellForm` is a left-nav gallery: each sample screen is an ordinary `Form` embedded into the content panel as a non-top-level child. **Register a new sample in one line** in `SampleShellForm.RegisterSamples()` (`this.AddSample("Title", () => new YourForm())`); the shell generates the nav button and handles embedding/switching. `LotReceiveForm` is the worked example (Fab/Lot/State query → resizable Lot/Wafer split grids → count/state/execution footer cards).

## Absolute rules

- Never use `var`. Use explicit types everywhere.
- No third-party UI libraries (MahApps, ModernWpf, MaterialDesign, DevExpress, Telerik, Syncfusion, etc.). Pure WPF only, unless the user explicitly requests otherwise.
- Output complete, compilable code only — no pseudocode, placeholders, TODOs, or omitted sections.
- Do not refactor existing WinForms business logic unless explicitly asked.
- **Pretendard font is not allowed** (corporate policy). Use Segoe UI (the `Font.Family` token already falls back to Malgun Gothic for Korean).

## C# style

- Explicit access modifier on every type and member.
- PascalCase for types/methods/properties/public members; camelCase for locals/parameters/private fields.
- Braces on every control block — no brace-less one-liners.
- One public type per file; file name matches the type name (and `x:Class` for controls).
- Split long initialization into named private methods.

## WPF component rules

- Base every reusable component on `UserControl`.
- Expose every bindable value as a `DependencyProperty` with a CLR wrapper. Register two-way values with `FrameworkPropertyMetadataOptions.BindsTwoWayByDefault`.
- Expose secrets (e.g. password text) through a plain CLR property, never a `DependencyProperty`.
- `ItemsSource` DPs are typed `IEnumerable`; `SelectedItem`/`SelectedValue` are typed `object` to stay binding-agnostic. Bound collections are `ObservableCollection<T>`; UI item models (`Models/Ui`) implement `INotifyPropertyChanged`.
- Keep styling in XAML and visual trees simple; conservative enterprise look (no flashy gradients/shadows/animation).
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

Host project must reference `WindowsFormsIntegration` plus `PresentationCore`, `PresentationFramework`, `WindowsBase`, `System.Xaml`, and the `com.example` library. Run WPF on an STA thread (`[STAThread]`). Give each `ElementHost` an explicit size (`Dock`/`Height`/`Anchor`).

## XAML & build correctness — verify before finishing

- `x:Class`, code-behind namespace + class name, file name, and folder all match exactly; code-behind class is `partial`.
- Each `.xaml` uses Build Action `Page` with `<Generator>MSBuild:Compile</Generator>`; the `.csproj` has both the `Page` entry and the matching `Compile` + `DependentUpon` entry. New `.cs` files need a `Compile` entry too (these projects use classic, non-SDK `.csproj` with explicit item lists).
- Each `x:Name` is unique and does not collide with a DependencyProperty's CLR property name.
- No `var`; no third-party UI dependency.

## Response behavior

- Ask one short clarifying question only when requirements are genuinely ambiguous; otherwise proceed.
- Lead with code; keep prose brief and practical.
- When a build or integration failure is likely, name the exact failure point and the fix.
- The full control catalog and per-control API surface live in `README.md` / `INTEGRATION.md` — consult them instead of re-listing controls here.
