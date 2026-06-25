using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// 서버에 요청(Request)을 보내고 응답(Reply)을 기다리는 클라이언트입니다.
    /// 동기 방식이라, 응답이 오거나 시간이 다 될 때까지 이 호출은 멈춰 기다립니다.
    /// </summary>
    public sealed class RendezvousRequestClient
    {
        private readonly RendezvousConnection connection;

        /// <summary>이미 열린 연결을 받아 클라이언트를 만듭니다.</summary>
        /// <param name="connection">RendezvousConnection 객체</param>
        public RendezvousRequestClient(RendezvousConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            this.connection = connection;
        }

        /// <summary>
        /// 지정한 Subject(주소)로 요청을 보내고 응답을 기다립니다.
        /// </summary>
        /// <param name="subject">보낼 주소(예: "RPC.EMPLOYEE.GET")</param>
        /// <param name="request">보낼 요청 메시지(Method 필드로 기능 구분)</param>
        /// <param name="timeoutSeconds">최대 대기 시간(초). 응답이 늦으면 null 반환.</param>
        /// <returns>서버가 보낸 응답 메시지. 시간이 초과되면 null.</returns>
        public RvMessage Request(string subject, RvMessage request, double timeoutSeconds)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("subject 가 비어 있습니다.", "subject");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // 보낼 주소를 메시지에 지정합니다.
            request.Inner.SendSubject = subject;

            // 요청을 보내고 응답을 기다립니다.
            // SendRequest 는 응답이 오면 그 메시지를, 시간이 초과되면 null 을 돌려줍니다.
            Message reply = this.connection.InnerTransport.SendRequest(request.Inner, timeoutSeconds);
            if (reply == null)
            {
                return null;
            }

            return new RvMessage(reply);
        }
    }
}
