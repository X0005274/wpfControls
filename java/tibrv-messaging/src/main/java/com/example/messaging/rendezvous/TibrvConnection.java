package com.example.messaging.rendezvous;

import com.tibco.tibrv.Tibrv;
import com.tibco.tibrv.TibrvException;
import com.tibco.tibrv.TibrvRvdTransport;
import com.tibco.tibrv.TibrvTransport;

/**
 * Represents a Rendezvous connection. Creating it opens the RV environment and a
 * transport (the connection path). Always close() it (or use try-with-resources)
 * when finished.
 */
public final class TibrvConnection implements AutoCloseable {

    private TibrvRvdTransport transport;
    private boolean environmentOpened;

    /**
     * Opens a connection using the given settings (Service/Network/Daemon).
     *
     * @param settings connection information
     * @throws TibrvException if the RV environment or transport cannot be opened
     */
    public TibrvConnection(TibrvSettings settings) throws TibrvException {
        if (settings == null) {
            throw new IllegalArgumentException("settings");
        }

        // Open the RV environment. (It is reference-counted internally, so open/close must be balanced.)
        Tibrv.open(Tibrv.IMPL_NATIVE);
        this.environmentOpened = true;

        // Open the actual communication path.
        this.transport = new TibrvRvdTransport(settings.getService(), settings.getNetwork(), settings.getDaemon());
    }

    /** The underlying transport object used internally by the library. */
    TibrvTransport innerTransport() {
        return this.transport;
    }

    /** Closes the connection and releases resources. */
    @Override
    public void close() {
        if (this.transport != null) {
            this.transport.destroy();
            this.transport = null;
        }

        if (this.environmentOpened) {
            try {
                Tibrv.close();
            } catch (TibrvException ignored) {
                // Nothing actionable while closing down.
            }
            this.environmentOpened = false;
        }
    }
}
