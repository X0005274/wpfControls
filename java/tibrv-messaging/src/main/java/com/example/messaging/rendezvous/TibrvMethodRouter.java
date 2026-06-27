package com.example.messaging.rendezvous;

import java.util.Map;
import java.util.TreeMap;
import com.tibco.tibrv.TibrvException;

/**
 * Looks up and runs the right handler based on a request message's Method value.
 *
 * When a server exposes several features, register a handler per feature and this
 * router calls the matching one based on the incoming request's Method name. Pass
 * this object's {@link #handle(TibrvMessage)} method as the handler of a
 * {@link TibrvReplyServer}.
 */
public final class TibrvMethodRouter {

    // Feature name -> handler. Case-insensitive.
    private final Map<String, TibrvMethodHandler> handlers =
            new TreeMap<String, TibrvMethodHandler>(String.CASE_INSENSITIVE_ORDER);

    /**
     * Registers a feature name and the handler that processes it.
     *
     * @param method  feature name (e.g. "GetEmployee")
     * @param handler function that takes a request and returns a reply
     */
    public void register(String method, TibrvMethodHandler handler) {
        if (method == null || method.isEmpty()) {
            throw new IllegalArgumentException("method is empty.");
        }

        if (handler == null) {
            throw new IllegalArgumentException("handler");
        }

        this.handlers.put(method, handler);
    }

    /**
     * Finds and runs the handler registered for the request's Method value. Returns an
     * error reply when the feature is not registered. Use this method as the handler of
     * a {@link TibrvReplyServer}.
     */
    public TibrvMessage handle(TibrvMessage request) throws TibrvException {
        String method = request.getMethod();

        TibrvMethodHandler handler = (method == null || method.isEmpty())
                ? null
                : this.handlers.get(method);

        if (handler == null) {
            // Unhandled feature: build and return an error reply.
            TibrvMessage error = new TibrvMessage();
            error.setString("status", "ERROR");
            error.setString("message", "Unknown method: " + method);
            return error;
        }

        return handler.handle(request);
    }
}
