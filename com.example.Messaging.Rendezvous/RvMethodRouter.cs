using System;
using System.Collections.Generic;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// 요청 메시지의 Method 값에 따라 알맞은 처리 함수를 찾아 실행해 주는 도우미입니다.
    ///
    /// 서버에서 기능이 여러 개일 때, 기능마다 처리 함수를 등록해 두면
    /// 들어온 요청의 Method 이름을 보고 자동으로 해당 함수를 호출합니다.
    /// RendezvousReplyServer 의 handler 로 이 객체의 Handle 메서드를 넘기면 됩니다.
    /// </summary>
    public sealed class RvMethodRouter
    {
        // 기능 이름 → 처리 함수. 대소문자는 구분하지 않습니다.
        private readonly Dictionary<string, Func<RvMessage, RvMessage>> handlers;

        /// <summary>빈 라우터를 만듭니다.</summary>
        public RvMethodRouter()
        {
            this.handlers = new Dictionary<string, Func<RvMessage, RvMessage>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 기능 이름과 그 기능을 처리할 함수를 등록합니다.
        /// </summary>
        /// <param name="method">기능 이름(예: "GetEmployee")</param>
        /// <param name="handler">요청을 받아 응답을 만들어 돌려주는 함수</param>
        public void Register(string method, Func<RvMessage, RvMessage> handler)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("method 가 비어 있습니다.", "method");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.handlers[method] = handler;
        }

        /// <summary>
        /// 요청의 Method 값을 보고 등록된 함수를 찾아 실행합니다.
        /// 등록되지 않은 기능이면 오류 내용을 담은 응답을 돌려줍니다.
        /// 이 메서드를 RendezvousReplyServer 의 handler 로 사용하세요.
        /// </summary>
        public RvMessage Handle(RvMessage request)
        {
            string method = request.Method;

            Func<RvMessage, RvMessage> handler;
            if (string.IsNullOrEmpty(method) || !this.handlers.TryGetValue(method, out handler))
            {
                // 처리할 수 없는 기능: 오류 응답을 만들어 보냅니다.
                RvMessage error = new RvMessage();
                error.SetString("status", "ERROR");
                error.SetString("message", "알 수 없는 기능입니다: " + method);
                return error;
            }

            return handler(request);
        }
    }
}
