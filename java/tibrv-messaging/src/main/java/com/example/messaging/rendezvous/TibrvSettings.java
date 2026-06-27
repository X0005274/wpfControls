package com.example.messaging.rendezvous;

/**
 * Holds the three values needed to connect to TIBCO Rendezvous. Together they
 * determine a single communication path (Transport).
 *
 * <ul>
 *   <li>Service : UDP service/port to use (e.g. "7500"). Empty uses the RV default.</li>
 *   <li>Network : network card / multicast address (e.g. ";225.1.1.1"). Usually empty.</li>
 *   <li>Daemon  : rvd daemon address (e.g. "tcp:7500" or "tcp:host:7500").</li>
 * </ul>
 */
public final class TibrvSettings {

    private String service;
    private String network;
    private String daemon;

    /** Initializes every value to an empty string (connect to the default local RV daemon). */
    public TibrvSettings() {
        this("", "", "");
    }

    /**
     * Creates settings from the three explicit values.
     *
     * @param service UDP service/port
     * @param network network value
     * @param daemon  rvd daemon address
     */
    public TibrvSettings(String service, String network, String daemon) {
        this.service = service != null ? service : "";
        this.network = network != null ? network : "";
        this.daemon = daemon != null ? daemon : "";
    }

    /** @return UDP service (port); an empty string uses the RV default. */
    public String getService() {
        return this.service;
    }

    public void setService(String service) {
        this.service = service != null ? service : "";
    }

    /** @return network (NIC / multicast) value; usually an empty string. */
    public String getNetwork() {
        return this.network;
    }

    public void setNetwork(String network) {
        this.network = network != null ? network : "";
    }

    /** @return rvd daemon address (e.g. "tcp:7500"). */
    public String getDaemon() {
        return this.daemon;
    }

    public void setDaemon(String daemon) {
        this.daemon = daemon != null ? daemon : "";
    }
}
