namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Holds the three values needed to connect to TIBCO Rendezvous. Together they
    /// determine a single communication path (Transport).
    ///
    /// - Service : UDP service/port to use (e.g. "7500"). Empty uses the RV default.
    /// - Network : network card / multicast address (e.g. ";225.1.1.1"). Usually empty.
    /// - Daemon  : rvd daemon address (e.g. "tcp:7500" or "tcp:host:7500").
    /// </summary>
    public sealed class TibrvSettings
    {
        /// <summary>UDP service (port). An empty string uses the RV default.</summary>
        public string Service { get; set; }

        /// <summary>Network (NIC / multicast) value. Usually an empty string.</summary>
        public string Network { get; set; }

        /// <summary>rvd daemon address (e.g. "tcp:7500").</summary>
        public string Daemon { get; set; }

        /// <summary>
        /// Initializes every value to an empty string (i.e. connect to the default local RV daemon).
        /// </summary>
        public TibrvSettings()
        {
            this.Service = string.Empty;
            this.Network = string.Empty;
            this.Daemon = string.Empty;
        }

        /// <summary>Creates settings from the three explicit values.</summary>
        /// <param name="service">UDP service/port</param>
        /// <param name="network">network value</param>
        /// <param name="daemon">rvd daemon address</param>
        public TibrvSettings(string service, string network, string daemon)
        {
            this.Service = service ?? string.Empty;
            this.Network = network ?? string.Empty;
            this.Daemon = daemon ?? string.Empty;
        }
    }
}
