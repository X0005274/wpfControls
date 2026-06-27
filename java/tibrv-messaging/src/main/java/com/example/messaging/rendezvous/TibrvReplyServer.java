package com.example.messaging.rendezvous;

import com.tibco.tibrv.TibrvDispatcher;
import com.tibco.tibrv.TibrvException;
import com.tibco.tibrv.TibrvListener;
import com.tibco.tibrv.TibrvMsg;
import com.tibco.tibrv.TibrvMsgCallback;
import com.tibco.tibrv.TibrvQueue;

/**
 * Listens for requests on a given Subject, processes each request, and sends back a
 * reply.
 *
 * When a request arrives it calls the handler passed to the constructor and
 * automatically replies to the requester with the result. A dedicated dispatcher
 * thread runs in the background so the UI never freezes. close() it when done.
 */
public final class TibrvReplyServer implements AutoCloseable {

    private final TibrvConnection connection;
    private final TibrvMethodHandler handler;
    private TibrvQueue queue;
    private TibrvDispatcher dispatcher;
    private TibrvListener listener;

    /**
     * Starts the reply server.
     *
     * @param connection an open RV connection
     * @param subject    subject to receive requests on (e.g. "RPC.EMPLOYEE.GET")
     * @param handler    function that takes a request and returns a reply message
     * @throws TibrvException if the queue/listener/dispatcher cannot be created
     */
    public TibrvReplyServer(TibrvConnection connection, String subject, TibrvMethodHandler handler) throws TibrvException {
        if (connection == null) {
            throw new IllegalArgumentException("connection");
        }

        if (subject == null || subject.isEmpty()) {
            throw new IllegalArgumentException("subject is empty.");
        }

        if (handler == null) {
            throw new IllegalArgumentException("handler");
        }

        this.connection = connection;
        this.handler = handler;

        // Create the queue that holds messages and the dedicated thread that drains it.
        this.queue = new TibrvQueue();
        this.listener = new TibrvListener(this.queue, new TibrvMsgCallback() {
            @Override
            public void onMsg(TibrvListener listener, TibrvMsg message) {
                onRequest(message);
            }
        }, this.connection.innerTransport(), subject, null);
        this.dispatcher = new TibrvDispatcher(this.queue);
    }

    // Called whenever a request message arrives.
    private void onRequest(TibrvMsg request) {
        try {
            // No reply subject means a one-way message, so do not reply.
            if (request.getReplySubject() == null) {
                return;
            }

            // Wrap the request in our TibrvMessage and pass it to the handler.
            TibrvMessage requestMessage = new TibrvMessage(request);
            TibrvMessage replyMessage = this.handler.handle(requestMessage);

            // If the handler returns null, send an empty reply.
            if (replyMessage == null) {
                replyMessage = new TibrvMessage();
            }

            // Send the reply back to the requester.
            this.connection.innerTransport().sendReply(replyMessage.inner(), request);
        } catch (TibrvException failure) {
            // The TIBCO callback cannot throw checked exceptions; surface to stderr.
            System.err.println("TibrvReplyServer failed to handle a request: " + failure.getMessage());
        }
    }

    /** Stops the server and releases resources. */
    @Override
    public void close() {
        if (this.dispatcher != null) {
            this.dispatcher.destroy();
            this.dispatcher = null;
        }

        if (this.listener != null) {
            this.listener.destroy();
            this.listener = null;
        }

        if (this.queue != null) {
            this.queue.destroy();
            this.queue = null;
        }
    }
}
