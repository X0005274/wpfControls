package com.example.messaging.rendezvous;

import com.tibco.tibrv.TibrvException;
import com.tibco.tibrv.TibrvMsg;

/**
 * Sends a request to a server and waits for the reply. It is synchronous: the call
 * blocks until a reply arrives or the timeout elapses.
 */
public final class TibrvRequestClient {

    private final TibrvConnection connection;

    /**
     * Creates a client over an already-open connection.
     *
     * @param connection a {@link TibrvConnection} object
     */
    public TibrvRequestClient(TibrvConnection connection) {
        if (connection == null) {
            throw new IllegalArgumentException("connection");
        }

        this.connection = connection;
    }

    /**
     * Sends a request to the given Subject and waits for the reply.
     *
     * @param subject        destination subject (e.g. "RPC.EMPLOYEE.GET")
     * @param request        request message (the Method field selects the feature)
     * @param timeoutSeconds max wait time in seconds; returns null if the reply is late
     * @return the server's reply message, or null on timeout
     * @throws TibrvException if sending fails
     */
    public TibrvMessage request(String subject, TibrvMessage request, double timeoutSeconds) throws TibrvException {
        if (subject == null || subject.isEmpty()) {
            throw new IllegalArgumentException("subject is empty.");
        }

        if (request == null) {
            throw new IllegalArgumentException("request");
        }

        // Set the destination subject on the message.
        request.inner().setSendSubject(subject);

        // Send the request and wait for the reply.
        // sendRequest returns the reply message, or null if the timeout elapses.
        TibrvMsg reply = this.connection.innerTransport().sendRequest(request.inner(), timeoutSeconds);
        if (reply == null) {
            return null;
        }

        return new TibrvMessage(reply);
    }
}
