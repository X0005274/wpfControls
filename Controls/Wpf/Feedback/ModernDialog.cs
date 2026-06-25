using System;
using System.Windows.Interop;

namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 확인/알림 모달 대화상자를 코드에서 간단히 띄우는 헬퍼입니다.
    /// 기본 MessageBox 대신 디자인이 일관된 대화상자를 보여 줍니다.
    ///
    /// 예) WinForms 폼에서:
    ///   bool yes = ModernDialog.Confirm("삭제 확인", "정말 삭제할까요?", this.Handle);
    ///   if (yes) { ... }
    ///   ModernDialog.Alert("저장됨", "정상적으로 저장되었습니다.", MessageKind.Success, this.Handle);
    /// </summary>
    public static class ModernDialog
    {
        /// <summary>확인/취소를 묻습니다. 확인을 누르면 true 를 돌려줍니다.</summary>
        /// <param name="title">제목</param>
        /// <param name="message">본문 메시지</param>
        public static bool Confirm(string title, string message)
        {
            return Confirm(title, message, IntPtr.Zero);
        }

        /// <summary>
        /// 확인/취소를 묻습니다(소유 창 지정).
        /// WinForms 폼이 부모면 ownerHandle 에 폼의 Handle 을 넘기면 가운데에 표시됩니다.
        /// </summary>
        public static bool Confirm(string title, string message, IntPtr ownerHandle)
        {
            ModernDialogWindow window = new ModernDialogWindow(title, message, MessageKind.Info, true);
            ApplyOwner(window, ownerHandle);
            bool? result = window.ShowDialog();
            return result == true;
        }

        /// <summary>알림(확인 버튼만)을 보여 줍니다.</summary>
        public static void Alert(string title, string message, MessageKind kind)
        {
            Alert(title, message, kind, IntPtr.Zero);
        }

        /// <summary>알림(확인 버튼만)을 보여 줍니다(소유 창 지정).</summary>
        public static void Alert(string title, string message, MessageKind kind, IntPtr ownerHandle)
        {
            ModernDialogWindow window = new ModernDialogWindow(title, message, kind, false);
            ApplyOwner(window, ownerHandle);
            window.ShowDialog();
        }

        // WinForms 폼 핸들을 WPF 창의 소유자로 연결합니다(가운데 정렬 + 모달 동작).
        private static void ApplyOwner(ModernDialogWindow window, IntPtr ownerHandle)
        {
            if (ownerHandle != IntPtr.Zero)
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                helper.Owner = ownerHandle;
            }
            else
            {
                // 소유 창이 없으면 화면 가운데에 띄웁니다.
                window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }
        }
    }
}
