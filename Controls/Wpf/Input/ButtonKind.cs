namespace com.example.Controls.Wpf.Input
{
    /// <summary>
    /// 버튼의 종류(색상/강조 수준)를 나타내는 열거형입니다.
    /// 디자이너 속성창과 코드 모두에서 이 값으로 버튼 모양을 바꿉니다.
    /// </summary>
    public enum ButtonKind
    {
        /// <summary>주요 동작용 버튼(파란 배경, 흰 글자). 화면에서 가장 강조됩니다.</summary>
        Primary,

        /// <summary>보조 동작용 버튼(흰 배경, 테두리). 강조를 낮춘 일반 버튼입니다.</summary>
        Secondary,

        /// <summary>삭제 등 위험한 동작용 버튼(빨간 배경). 주의가 필요한 동작에 씁니다.</summary>
        Danger
    }
}
