using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Rendezvous 통신 연결을 나타냅니다.
    /// 생성하면 RV 환경을 켜고 접속 경로(Transport)를 엽니다.
    /// 다 쓰면 반드시 Dispose() 하거나 using 블록으로 감싸 정리해야 합니다.
    /// </summary>
    public sealed class RendezvousConnection : IDisposable
    {
        private NetTransport transport;
        private bool environmentOpened;

        /// <summary>
        /// 설정값(Service/Network/Daemon)으로 연결을 엽니다.
        /// </summary>
        /// <param name="settings">접속 정보</param>
        public RendezvousConnection(RendezvousSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            // RV 환경을 켭니다. (내부적으로 참조 횟수를 세므로 Open/Close 짝을 맞춰야 합니다.)
            Environment.Open();
            this.environmentOpened = true;

            // 실제 통신 경로를 엽니다.
            this.transport = new NetTransport(settings.Service, settings.Network, settings.Daemon);
        }

        /// <summary>
        /// 라이브러리 내부에서 사용하는 실제 Transport 객체입니다.
        /// </summary>
        internal Transport InnerTransport
        {
            get { return this.transport; }
        }

        /// <summary>연결을 닫고 자원을 정리합니다.</summary>
        public void Dispose()
        {
            if (this.transport != null)
            {
                this.transport.Destroy();
                this.transport = null;
            }

            if (this.environmentOpened)
            {
                Environment.Close();
                this.environmentOpened = false;
            }
        }
    }
}
