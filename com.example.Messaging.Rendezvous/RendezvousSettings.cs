namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// TIBCO Rendezvous 접속에 필요한 3가지 정보를 담는 설정 클래스입니다.
    /// 이 세 값이 하나의 "통신 경로(Transport)"를 결정합니다.
    ///
    /// - Service : 통신에 사용할 UDP 서비스/포트 (예: "7500"). 빈 값이면 기본값 사용.
    /// - Network : 사용할 네트워크 카드/멀티캐스트 주소 (예: ";225.1.1.1"). 보통 빈 값.
    /// - Daemon  : rvd 데몬 접속 주소 (예: "tcp:7500" 또는 "tcp:호스트:7500").
    /// </summary>
    public sealed class RendezvousSettings
    {
        /// <summary>UDP 서비스(포트) 값입니다. 빈 문자열이면 RV 기본값을 사용합니다.</summary>
        public string Service { get; set; }

        /// <summary>네트워크(랜카드/멀티캐스트) 값입니다. 보통 빈 문자열입니다.</summary>
        public string Network { get; set; }

        /// <summary>rvd 데몬 주소입니다(예: "tcp:7500").</summary>
        public string Daemon { get; set; }

        /// <summary>
        /// 모든 값을 빈 문자열로 초기화합니다(= RV 기본 로컬 데몬에 접속).
        /// </summary>
        public RendezvousSettings()
        {
            this.Service = string.Empty;
            this.Network = string.Empty;
            this.Daemon = string.Empty;
        }

        /// <summary>세 값을 직접 지정해 설정을 만듭니다.</summary>
        /// <param name="service">UDP 서비스/포트</param>
        /// <param name="network">네트워크 값</param>
        /// <param name="daemon">rvd 데몬 주소</param>
        public RendezvousSettings(string service, string network, string daemon)
        {
            this.Service = service ?? string.Empty;
            this.Network = network ?? string.Empty;
            this.Daemon = daemon ?? string.Empty;
        }
    }
}
