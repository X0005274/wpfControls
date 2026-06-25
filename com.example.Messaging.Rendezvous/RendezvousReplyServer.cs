using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// 특정 Subject(주소)로 들어오는 요청을 듣고 있다가, 받은 요청을 처리해
    /// 응답을 돌려보내는 서버입니다.
    ///
    /// 요청이 들어오면 생성자에서 받은 처리 함수(handler)를 호출하고,
    /// 그 결과 메시지를 요청자에게 자동으로 응답합니다.
    /// 내부에 전용 처리 스레드(Dispatcher)를 두어, 화면이 멈추지 않게 백그라운드에서 동작합니다.
    /// 다 쓰면 Dispose() 로 정리해야 합니다.
    /// </summary>
    public sealed class RendezvousReplyServer : IDisposable
    {
        private readonly RendezvousConnection connection;
        private readonly Func<RvMessage, RvMessage> handler;
        private Queue queue;
        private Dispatcher dispatcher;
        private Listener listener;

        /// <summary>
        /// 응답 서버를 시작합니다.
        /// </summary>
        /// <param name="connection">열린 RV 연결</param>
        /// <param name="subject">요청을 받을 주소(예: "RPC.EMPLOYEE.GET")</param>
        /// <param name="handler">요청을 받아 응답 메시지를 만들어 돌려주는 함수</param>
        public RendezvousReplyServer(RendezvousConnection connection, string subject, Func<RvMessage, RvMessage> handler)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("subject 가 비어 있습니다.", "subject");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.connection = connection;
            this.handler = handler;

            // 메시지를 담아둘 큐와, 그 큐를 처리할 전용 스레드를 만듭니다.
            this.queue = new Queue();
            this.listener = new Listener(this.queue, this.connection.InnerTransport, subject, null);
            this.listener.MessageReceived += this.OnMessageReceived;
            this.dispatcher = new Dispatcher(this.queue);
        }

        // 요청 메시지가 도착할 때마다 호출됩니다.
        private void OnMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            Message request = messageReceivedEventArgs.Message;

            // 응답을 보낼 주소(ReplySubject)가 없으면 단방향 메시지이므로 응답하지 않습니다.
            if (request.ReplySubject == null)
            {
                return;
            }

            // 요청을 우리 RvMessage 로 감싸 처리 함수에 넘깁니다.
            RvMessage requestMessage = new RvMessage(request);
            RvMessage replyMessage = this.handler(requestMessage);

            // 처리 함수가 null 을 돌려주면 빈 응답을 보냅니다.
            if (replyMessage == null)
            {
                replyMessage = new RvMessage();
            }

            // 요청자에게 응답을 보냅니다.
            this.connection.InnerTransport.SendReply(replyMessage.Inner, request);
        }

        /// <summary>서버를 멈추고 자원을 정리합니다.</summary>
        public void Dispose()
        {
            if (this.dispatcher != null)
            {
                this.dispatcher.Destroy();
                this.dispatcher = null;
            }

            if (this.listener != null)
            {
                this.listener.Destroy();
                this.listener = null;
            }

            if (this.queue != null)
            {
                this.queue.Destroy();
                this.queue = null;
            }
        }
    }
}
