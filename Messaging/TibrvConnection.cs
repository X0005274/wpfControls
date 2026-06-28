using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Represents a Rendezvous connection. Creating it opens the RV environment and a
    /// transport (the connection path). Always Dispose() it (or wrap it in a using
    /// block) when finished.
    /// </summary>
    public sealed class TibrvConnection : IDisposable
    {
        private NetTransport transport;
        private bool environmentOpened;

        /// <summary>Opens a connection using the given settings (Service/Network/Daemon).</summary>
        /// <param name="settings">connection information</param>
        public TibrvConnection(TibrvSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            // Open the RV environment. (It is reference-counted internally, so Open/Close must be balanced.)
            Environment.Open();
            this.environmentOpened = true;

            // Open the actual communication path.
            this.transport = new NetTransport(settings.Service, settings.Network, settings.Daemon);
        }

        /// <summary>The underlying transport object used internally by the library.</summary>
        internal Transport InnerTransport
        {
            get { return this.transport; }
        }

        /// <summary>Closes the connection and releases resources.</summary>
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
