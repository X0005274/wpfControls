# Embedded fonts — 본고딕 (Noto Sans KR)

The UI font is embedded as an assembly **Resource** so it renders identically on
every machine, even where 본고딕 is not installed system-wide.

## What to put here

Drop the **static** Noto Sans KR font files (one file per weight) into this folder.
The UI uses `Normal` / `Medium` / `SemiBold` / `Bold`, so these four are present:

```
Assets/Fonts/
├── NotoSansKR-Regular.ttf
├── NotoSansKR-Medium.ttf
├── NotoSansKR-SemiBold.ttf
└── NotoSansKR-Bold.ttf
```

Static `.ttf` and `.otf` both work — verified: WPF groups the four files into the
single `Noto Sans KR` family and selects the correct weight file per `FontWeight`
(even though the static files report split Win32 names like `Noto Sans KR Medium`).
Providing all four gives the best fidelity; if some are missing, WPF synthesizes
the weight from whatever is present (slightly lower quality, still works).

### Where to download

- Google Fonts: https://fonts.google.com/noto/specimen/Noto+Sans+KR
  (download the family, then use the static `.otf` files from the archive)

## Important — file format

- Use **static** `.otf` (or static `.ttf`) files.
- **Do NOT** use the variable font (e.g. `NotoSansKR-VariableFont_wght.ttf`).
  WPF (.NET Framework 4.8) does **not** support OpenType variable fonts — a
  variable file embeds but renders only its default master, so weights collapse.

## How it is wired up

- `com.example.csproj` embeds every `*.otf` / `*.ttf` in this folder as a
  `Resource` (wildcard — no per-file edits needed).
- `Themes/Tokens.xaml` exposes the family via the `Font.Family` token:

  ```xml
  <FontFamily x:Key="Font.Family">pack://application:,,,/com.example;component/Assets/Fonts/#Noto Sans KR, Segoe UI, Malgun Gothic</FontFamily>
  ```

  `Segoe UI` / `Malgun Gothic` remain as fallbacks if the resource ever fails to load.

## After adding files

A command-line / CI `msbuild` re-globs automatically. In Visual Studio, **unload
and reload the project** (or close/reopen the solution) once after dropping in new
font files so the IDE re-evaluates the wildcard and embeds them.
