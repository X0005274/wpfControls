# Feedback 헬퍼 (토스트 · 로딩 · 모달)

화면에 끌어다 놓는 컨트롤이 아니라, **코드에서 호출**하는 피드백 헬퍼들입니다.
모두 `com.example` 어셈블리에 들어 있고, WinForms 폼에서 그대로 호출할 수 있습니다.

## 1) ModernToast — 잠깐 떴다 사라지는 알림

```csharp
using com.example.Controls.Wpf.Feedback;

ModernToast.Show("저장되었습니다.", MessageKind.Success);
ModernToast.Show("네트워크 오류", MessageKind.Error, 5);   // 5초 표시
```

- 화면 오른쪽 아래에 나타나 지정 시간 후 자동으로 사라집니다.
- 종류(MessageKind): Info / Success / Warning / Error → 색이 달라집니다.

## 2) ModernDialog — 확인/알림 모달

```csharp
// 확인/취소 (확인을 누르면 true)
bool yes = ModernDialog.Confirm("삭제 확인", "정말 삭제할까요?", this.Handle);
if (yes)
{
    // 삭제 진행
}

// 알림(확인 버튼만)
ModernDialog.Alert("완료", "처리가 끝났습니다.", MessageKind.Success, this.Handle);
```

- WinForms 폼에서 부를 때 `this.Handle` 을 넘기면 폼 가운데에 모달로 뜹니다.
- 핸들을 안 넘기면(`IntPtr.Zero`) 화면 가운데에 표시됩니다.

## 3) LoadingOverlay — 로딩 중 화면 덮개

```csharp
// 폼 전체를 덮고 스피너 표시
LoadingOverlay overlay = LoadingOverlay.Show(this, "조회 중...");

// ※ 실제 작업은 반드시 백그라운드 스레드에서!
//    (UI 스레드를 막으면 스피너가 돌지 않습니다.)
System.Threading.Tasks.Task.Run(() =>
{
    // ... 오래 걸리는 작업 (예: TIBCO Request) ...
    this.BeginInvoke(new System.Action(() => overlay.Dispose()));  // UI 스레드에서 닫기
});
```

- `Show(컨트롤)` 은 그 컨트롤(폼/패널) 위에 반투명 덮개 + 스피너를 올립니다.
- 작업이 끝나면 UI 스레드에서 `Dispose()` 로 덮개를 치웁니다.
- TIBCO 동기 요청(`TibrvRequestClient.Request`)처럼 시간이 걸리는 호출과 잘 어울립니다.

## 메모

- 토스트/모달은 WPF 창을 사용하지만, WinForms 앱(STA 스레드)에서 별도 설정 없이 동작합니다.
- 색상은 `Themes/Tokens.xaml` 토큰을 그대로 사용해 다른 컨트롤과 톤이 일치합니다.
