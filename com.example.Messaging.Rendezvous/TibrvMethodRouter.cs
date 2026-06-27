using System;
using System.Collections.Generic;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Looks up and runs the right handler based on a request message's Method value.
    ///
    /// When a server exposes several features, register a handler per feature and this
    /// router calls the matching one based on the incoming request's Method name. Pass
    /// this object's Handle method as the handler of a TibrvReplyServer.
    /// </summary>
    public sealed class TibrvMethodRouter
    {
        // Feature name -> handler. Case-insensitive.
        private readonly Dictionary<string, Func<TibrvMessage, TibrvMessage>> handlers;

        /// <summary>Creates an empty router.</summary>
        public TibrvMethodRouter()
        {
            this.handlers = new Dictionary<string, Func<TibrvMessage, TibrvMessage>>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Registers a feature name and the handler that processes it.
        /// </summary>
        /// <param name="method">feature name (e.g. "GetEmployee")</param>
        /// <param name="handler">function that takes a request and returns a reply</param>
        public void Register(string method, Func<TibrvMessage, TibrvMessage> handler)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("method is empty.", "method");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.handlers[method] = handler;
        }

        /// <summary>
        /// Finds and runs the handler registered for the request's Method value.
        /// Returns an error reply when the feature is not registered.
        /// Use this method as the handler of a TibrvReplyServer.
        /// </summary>
        public TibrvMessage Handle(TibrvMessage request)
        {
            string method = request.Method;

            Func<TibrvMessage, TibrvMessage> handler;
            if (string.IsNullOrEmpty(method) || !this.handlers.TryGetValue(method, out handler))
            {
                // Unhandled feature: build and return an error reply.
                TibrvMessage error = new TibrvMessage();
                error.SetString("status", "ERROR");
                error.SetString("message", "Unknown method: " + method);
                return error;
            }

            return handler(request);
        }
    }
}
