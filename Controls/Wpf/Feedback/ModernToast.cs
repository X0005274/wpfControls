namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 화면 오른쪽 아래에 잠깐 나타났다 사라지는 알림(토스트)을 띄우는 헬퍼입니다.
    ///
    /// 예)
    ///   ModernToast.Show("저장되었습니다.", MessageKind.Success);
    ///   ModernToast.Show("저장 실패", MessageKind.Error, 5);
    /// </summary>
    public static class ModernToast
    {
        /// <summary>기본 종류(정보), 3초 동안 알림을 보여 줍니다.</summary>
        /// <param name="message">표시할 메시지</param>
        public static void Show(string message)
        {
            Show(message, MessageKind.Info, 3.0);
        }

        /// <summary>종류를 지정해 3초 동안 알림을 보여 줍니다.</summary>
        public static void Show(string message, MessageKind kind)
        {
            Show(message, kind, 3.0);
        }

        /// <summary>종류와 표시 시간(초)을 지정해 알림을 보여 줍니다.</summary>
        /// <param name="message">표시할 메시지</param>
        /// <param name="kind">종류(색상)</param>
        /// <param name="seconds">머무는 시간(초)</param>
        public static void Show(string message, MessageKind kind, double seconds)
        {
            ModernToastWindow window = new ModernToastWindow(message, kind, seconds);
            window.Show();
        }
    }
}
