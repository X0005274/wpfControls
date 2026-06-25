# Embedded fonts — Pretendard

The UI font is embedded as an assembly **Resource** so it renders identically on
every machine, even where Pretendard is not installed system-wide.

## What is here

Static Pretendard weights (one file per weight). The UI uses
`Normal` / `Medium` / `SemiBold` / `Bold`:

```
Assets/Fonts/
├── Pretendard-Regular.otf
├── Pretendard-Medium.otf
├── Pretendard-SemiBold.otf
└── Pretendard-Bold.otf
```

Pretendard is open source (SIL Open Font License 1.1), so embedding/redistribution
is permitted. Source: https://github.com/orioncactus/pretendard

## Important — file format

- Use the **static** `.otf` (or `.ttf`) files from `public/static/`.
- **Do NOT** use the variable font (`PretendardVariable.ttf`). WPF (.NET Framework
  4.8) does not support OpenType variable fonts — weights collapse to one master.
- These static files report split Win32 names (`Pretendard Medium`,
  `Pretendard SemiBold`), but WPF groups all four into the single `Pretendard`
  family and picks the correct weight file per `FontWeight` (verified in-app).

## How it is wired up

- `com.example.csproj` embeds every `*.otf` / `*.ttf` in this folder as a
  `Resource` (wildcard — no per-file edits needed).
- `Themes/Tokens.xaml` exposes the family via the `Font.Family` token:

  ```xml
  <FontFamily x:Key="Font.Family">pack://application:,,,/com.example;component/Assets/Fonts/#Pretendard, Segoe UI, Malgun Gothic</FontFamily>
  ```

  `Segoe UI` / `Malgun Gothic` remain as fallbacks if the resource ever fails to load.

## Text crispness

All controls set, on their root element:

```
TextOptions.TextFormattingMode="Display"
TextOptions.TextRenderingMode="ClearType"
UseLayoutRounding="True"
```

`Display` snaps glyphs to the pixel grid (WPF's default `Ideal` mode looks soft at
small UI sizes). These are inherited by all child text.

## After adding/replacing files

A command-line / CI `msbuild` re-globs automatically. In Visual Studio, **unload
and reload the project** once after changing font files so the IDE re-evaluates the
wildcard and embeds them. Always do a full **Rebuild** (incremental build may miss
newly added font files).
