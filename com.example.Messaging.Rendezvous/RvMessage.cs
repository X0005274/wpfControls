using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Rendezvous 메시지를 "이름 = 값" 필드들의 묶음으로 쉽게 다루도록 감싼 클래스입니다.
    ///
    /// 원래 TIBCO 메시지(Message)는 다루기가 까다로워서, 여기서는 문자열/정수/실수
    /// 필드를 넣고 꺼내는 간단한 메서드만 제공합니다.
    /// "메서드명(Method)"이라는 특별한 필드를 정해 두어, 서버가 어떤 기능을 호출할지
    /// 구분하는 데 사용합니다.
    /// </summary>
    public sealed class RvMessage
    {
        /// <summary>서버에서 호출할 기능 이름을 담는 약속된 필드 이름입니다.</summary>
        public const string MethodFieldName = "method";

        /// <summary>실제 TIBCO 메시지 객체입니다(라이브러리 내부에서만 사용).</summary>
        internal Message Inner { get; private set; }

        /// <summary>빈 메시지를 새로 만듭니다.</summary>
        public RvMessage()
        {
            this.Inner = new Message();
        }

        /// <summary>기존 TIBCO 메시지를 감싸 RvMessage로 만듭니다(내부 전용).</summary>
        internal RvMessage(Message message)
        {
            this.Inner = message;
        }

        /// <summary>
        /// 서버에서 호출할 기능 이름입니다(예: "GetEmployee").
        /// 내부적으로는 "method" 필드에 저장됩니다.
        /// </summary>
        public string Method
        {
            get { return this.GetString(MethodFieldName); }
            set { this.SetString(MethodFieldName, value); }
        }

        /// <summary>문자열 필드를 추가/설정합니다.</summary>
        public void SetString(string fieldName, string value)
        {
            this.Inner.AddField(fieldName, value ?? string.Empty);
        }

        /// <summary>정수 필드를 추가/설정합니다.</summary>
        public void SetInt(string fieldName, int value)
        {
            this.Inner.AddField(fieldName, value);
        }

        /// <summary>실수(double) 필드를 추가/설정합니다.</summary>
        public void SetDouble(string fieldName, double value)
        {
            this.Inner.AddField(fieldName, value);
        }

        /// <summary>
        /// 문자열 필드 값을 읽습니다. 필드가 없으면 빈 문자열을 돌려줍니다.
        /// </summary>
        public string GetString(string fieldName)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return string.Empty;
            }

            return field.Value.ToString();
        }

        /// <summary>
        /// 정수 필드 값을 읽습니다. 없거나 숫자가 아니면 기본값(defaultValue)을 돌려줍니다.
        /// </summary>
        public int GetInt(string fieldName, int defaultValue)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return defaultValue;
            }

            int result;
            if (int.TryParse(field.Value.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 실수(double) 필드 값을 읽습니다. 없거나 숫자가 아니면 기본값을 돌려줍니다.
        /// </summary>
        public double GetDouble(string fieldName, double defaultValue)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return defaultValue;
            }

            double result;
            if (double.TryParse(field.Value.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>해당 이름의 필드가 들어 있는지 확인합니다.</summary>
        public bool HasField(string fieldName)
        {
            return this.Inner.GetField(fieldName) != null;
        }
    }
}
