# com.example.WinForms.Controls

WPF 모던 컨트롤(`com.example`)을 **WinForms 폼의 도구 상자(Toolbox)에서 드래그**해
쓸 수 있게 `ElementHost`로 감싼 래퍼 컨트롤 모음입니다.

## 회사 솔루션에 추가하는 방법

1. **솔루션 우클릭 → 추가 → 기존 프로젝트** 로 다음 두 프로젝트를 추가합니다.
   - `com.example.csproj` (WPF 컨트롤 본체)
   - `com.example.WinForms.Controls.csproj` (이 프로젝트, 위를 참조함)
2. **폼이 있는 프로젝트**에서 `com.example.WinForms.Controls` 를 **참조 추가** 합니다.
3. **한 번 빌드**합니다. → 도구 상자에 `com.example.WinForms.Controls Components` 그룹으로
   컨트롤들이 자동으로 나타납니다.
4. 폼 디자이너에서 도구 상자의 컨트롤을 **폼 위로 드래그**하면 끝.
   속성창(F4)에서 한글 설명과 함께 속성을 설정할 수 있습니다(분류: "모던 컨트롤").

> 도구 상자에 안 보이면: 도구 상자 빈 곳 우클릭 → "항목 선택" → .NET 구성요소 탭에서
> 이 어셈블리를 찾아 체크하거나, 솔루션을 다시 빌드하세요.

## 제공 컨트롤(27종)

| 분류 | 컨트롤 |
|---|---|
| 입력 | `ModernTextBox`, `ModernPasswordBox`, `ModernRichTextBox`, `ModernSearchBox`, `ModernButton`, `ModernNumericUpDown` |
| 선택 | `ModernComboBox`, `ModernMultiSelectComboBox`(체크 콤보), `ModernCheckBox`, `ModernRadioButtonGroup`, `ModernToggleSwitch`, `ModernListBox`, `ModernSlider` |
| 표시 | `ModernLabel`, `ModernProgressBar`, `ModernStatusBar`, `ModernBadge`, `ModernDatePicker` |
| 데이터 | `ModernDataGrid`, `MatteDataGrid`, `ModernListView`, `ModernTreeView`, `ModernTabControl` |
| 레이아웃 | `ModernExpander`, `ModernGroupBox`, `ModernFormSection`, `ModernScrollSection` |

`ModernButton`, `ModernNumericUpDown` 은 이번에 새로 추가한 컨트롤입니다.

## 코드에서 다루기

목록 데이터처럼 디자이너 속성창에 넣기 어려운 값은 **코드에서** 설정합니다.

```csharp
// 폼 생성자 등에서
this.modernComboBox1.DisplayMemberPath = "Name";
this.modernComboBox1.SelectedValuePath = "Code";
this.modernComboBox1.ItemsSource = employeeList;   // IEnumerable

this.modernButton1.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
this.modernButton1.Click += (sender, e) => { /* 클릭 처리 */ };

this.modernDataGrid1.ItemsSource = rows;           // 그리드 데이터
```

- 글자/필수여부/사용가능 같은 단순 속성은 **속성창**에서 바로 설정됩니다.
- 목록(`ItemsSource`), 선택값(`SelectedItem`/`SelectedValue`), 내용(`InnerContent`) 등은
  코드에서 설정하도록 속성창에서는 숨겨 두었습니다.

## 참고(디자인 타임)

- WPF를 호스팅하므로, 디자이너에서 컨트롤 내부가 늦게 그려지거나 단순하게 보일 수 있습니다.
  **실행하면 정상**으로 렌더링됩니다.
- `Modern*` 컨트롤은 임베드된 한글 폰트(Pretendard)를 사용하므로 별도 폰트 설치가 필요 없습니다.
