namespace com.example.Controls.Wpf.Feedback
{
    /// <summary>
    /// 알림/대화상자의 종류(색상·아이콘 의미)를 나타내는 열거형입니다.
    /// 토스트(ModernToast)와 대화상자(ModernDialog)에서 공통으로 사용합니다.
    /// </summary>
    public enum MessageKind
    {
        /// <summary>일반 정보(파란색 계열).</summary>
        Info,

        /// <summary>성공/완료(초록색 계열).</summary>
        Success,

        /// <summary>주의/경고(주황색 계열).</summary>
        Warning,

        /// <summary>오류/실패(빨간색 계열).</summary>
        Error
    }
}
