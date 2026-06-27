# Fluent Enterprise WPF — Style Guide

A Windows **Fluent**‑aligned design system for the `com.example` WPF control library
(pure WPF `UserControl`s hosted in WinForms via `ElementHost`).

**Single source of truth:** [`Themes/Tokens.xaml`](Themes/Tokens.xaml).
Controls must consume tokens and never hardcode hex / px / font weights. Change a
token once and every control and screen follows.

---

## 1. Why this guide exists (diagnosis)

The controls consumed tokens well, but the **token values themselves did not converge
on one design language**, which produced a "flat / unpolished" feel. Root causes:

1. **Mixed languages** — accent was Tailwind blue (`#2563EB`), not Fluent.
2. **No type ramp** — irregular sizes (9·11·13·15·22…); body 13 < Fluent 14.
3. **No weight rule** — bold/medium/normal drifted, flattening hierarchy.
4. **Radius mismatch** — controls at 6px (Fluent uses 4; cards 8).
5. **Elevation unused** — one popup shadow only.
6. **Overlapping tokens** — several near‑identical light grays.
7. **No documented usage rules** — tokens existed, guidance didn't (cause of 2 & 3).

This guide fixes the values and, more importantly, **documents the rules**.

---

## 2. Color

Accent is **one** Fluent communication‑blue ramp. Neutrals are a quiet cool‑gray
ramp. Semantic colors are **separate from the accent** so "needs attention" reads at
a glance.

### Accent & interaction
| Token | Value | Use |
|---|---|---|
| `Brush.Accent` | `#0078D4` | Primary action, selected/active, focus |
| `Brush.AccentHover` | `#106EBE` | Primary hover |
| `Brush.AccentPressed` | `#005A9E` | Primary pressed |
| `Brush.SelectedBackground` | `#CFE4F7` | Selected row/item fill |
| `Brush.SelectedText` | `#005A9E` | Selected text |
| `Brush.FocusBorder` | `#0078D4` | Focused input border |
| `Brush.FocusRing` | `#330078D4` | Focus ring (translucent) |

### Neutral & surface
| Token | Value | Use |
|---|---|---|
| `Brush.TextPrimary` | `#111827` | Body / data |
| `Brush.TextSecondary` | `#6B7280` | Labels, secondary |
| `Brush.TextPlaceholder` | `#9CA3AF` | Placeholder |
| `Brush.Border` | `#D1D5DB` | Control / card border |
| `Brush.BorderSubtle` | `#E5E7EB` | Dividers, gridlines |
| `Brush.BorderHover` | `#9CA3AF` | Hover border |
| `Brush.Surface` | `#FFFFFF` | Card / input surface |
| `Brush.SurfaceAlt` | `#F9FAFB` | Zebra rows |
| `Brush.HeaderBackground` | `#F3F4F6` | Section header fill |
| `Brush.GridHeaderBackground` | `#EFF3FA` | Data‑grid header (faint cool tint) |
| `Brush.HoverBackground` | `#F3F4F6` | Row / item hover |

### Semantic (kept distinct from accent)
| Role | Text | Background |
|---|---|---|
| Success | `Brush.SuccessText #15803D` | `Brush.SuccessBackground #ECFDF3` |
| Warning | `Brush.WarningText #B45309` | `Brush.WarningBackground #FFFBEB` |
| Error / Danger | `Brush.ErrorText #B91C1C` | `Brush.ErrorBackground #FEF2F2` |
| Info | `Brush.InfoText #1D4ED8` | `Brush.InfoBackground #EFF6FF` |
| Neutral | `Brush.NeutralText #374151` | `Brush.NeutralBackground #F3F4F6` |

`ModernBadgeControl` accepts `Info / Success / Warning / Error / Danger / Neutral`
(`Danger` is an alias for `Error`).

---

## 3. Typography

Font: **Segoe UI** (`Font.Family`, Malgun Gothic fallback) — Fluent's system face,
compatible with locked‑down corporate environments. Hierarchy comes from **size +
weight only**.

> **Weight rule:** structural elements (titles, field labels, grid headers) are
> **SemiBold**; body, data, and badges stay **Regular**. This single rule is what
> keeps the UI from looking flat.

| Role | Token | Size | Weight |
|---|---|---|---|
| Display | `Font.Size.Display` | 28 | SemiBold |
| Page title | `Font.Size.PageTitle` | 22 | SemiBold |
| Heading | `Font.Size.Heading` | 20 | SemiBold |
| Subtitle / section | `Font.Size.Subtitle` / `Text.Title` | 16 | SemiBold |
| Body / data | `Font.Size.Body` / `Text.Body` | 14 | Regular |
| Caption / label | `Font.Size.Label` / `Text.Label` | 12 | SemiBold |
| Helper / caption | `Font.Size.Helper` / `Text.Helper` | 12 | Regular |

- Placeholders render **italic** in `Brush.TextPlaceholder`.
- Badges render **italic + Regular** (calm, not competing with data).
- Reusable text styles: `Text.Title`, `Text.Label`, `Text.Body`, `Text.Helper`.

---

## 4. Spacing, radius, elevation

### Spacing — 4px grid
`Space.Xs 4` · `Space.Sm 8` · `Space.Md 12` · `Space.Lg 16` · `Space.Xl 24`
Layout gaps/padding use these; don't invent off‑grid values.

### Radius (Fluent)
| Token | Value | Use |
|---|---|---|
| `Radius.Sm` | 4 | small chips/insets |
| `Radius.Md` | 4 | **controls** (buttons, inputs) |
| `Radius.Lg` | 8 | **cards / dialogs** |
| `Radius.Pill` | 10 | badges / chips |

### Elevation
| Token | Use |
|---|---|
| `Shadow.Card` | subtle resting shadow for cards |
| `Shadow.Popup` | flyouts / popups (one step deeper) |

---

## 5. Components

### Buttons — `ModernButtonControl` / `ModernButton`
- `Kind`: **Primary** (accent, the one strong action) · **Secondary** (white + border;
  hover = pale‑blue accent tint) · **Danger** (red, hard‑to‑undo actions).
- Hover/pressed use real **color** states (not opacity); disabled dims.
- Text is **SemiBold**; `IconGlyph` (Segoe MDL2) shows a contextual icon.
- `IsButtonEnabled` toggles enabled state.

### Badges — `ModernBadgeControl`
Non‑interactive status. Pastel background + semantic text, **italic + Regular**,
`Radius.Pill`. Set `BadgeType` to drive the tone.

### Inputs — `ModernTextBoxControl`, etc.
Focus → accent border + focus ring; hover → border emphasis; placeholder italic.

### Data grid — `ModernDataGridControl` / `ModernDataGrid`
- **Auto column width** (`ColumnWidth=Auto`): each column fits its data + header.
  Many columns → horizontal scroll; few → room to add more.
- **Header‑click sorting** with a simple **▲ / ▼** indicator (accent). Text columns
  sort automatically; badge/template columns sort via `SortMemberPath`.
- Zebra rows (`SurfaceAlt`); selection = `SelectedBackground` (no weight change).
- Define columns via `AddTextColumn(header, path)` / `AddBadgeColumn(header, label, tonePath)`.

### Cards
Rounded (`Radius.Lg`) white surfaces on a gray canvas, separated by 16px gaps.
In WinForms, use `Demo/CardPanel.cs` (rounded + bordered) to match the WPF grid card.

---

## 6. Before → After (Fluent migration)

| Aspect | Before | After (Fluent) |
|---|---|---|
| Accent | `#2563EB` (Tailwind) | `#0078D4` (Fluent) |
| Accent hover / pressed | `#1D4ED8` / `#1E40AF` | `#106EBE` / `#005A9E` |
| Selection | `#DBEAFE` / navy | `#CFE4F7` / `#005A9E` |
| Body size | 13 | **14** |
| Control radius | 6 | **4** (cards 8) |
| Hierarchy | uniform weight (flat) | structure SemiBold / body Regular |
| Elevation | popup only | Card + Popup |
| Type ramp | 9·13·15·22… ad hoc | 12 / 14 / 16 / 22 / 28 |
| Badge / pill radius | hardcoded 10·12 | `Radius.Pill` token |

---

## 7. Design intent

- **Native trust** — an enterprise Windows app earns trust by looking part of the OS;
  Fluent accent + Segoe UI + 4px radius make it read as "part of the system".
- **Hierarchy before color** — the flat feel came from missing hierarchy, not missing
  color; only structural elements are emphasized.
- **Restrained accent** — one blue ramp; semantic colors separate, so only what needs
  attention stands out.
- **Single source** — every value lives in `Themes/Tokens.xaml`; change once, applies
  everywhere — drift is prevented structurally.
- **Calm data** — badges and selection are muted so tables stay readable.

---

## 8. Do / Don't

- ✅ Reference tokens (`{StaticResource Brush.Accent}`); ❌ hardcode hex / px / weight.
- ✅ Use the weight rule (structure SemiBold, body Regular); ❌ bold everything.
- ✅ One accent; ❌ multiple competing accent hues.
- ✅ Add a new token to `Tokens.xaml` when a value recurs; ❌ duplicate literals.
- ✅ Verify in `ElementHost` (WinForms host) before finishing.
