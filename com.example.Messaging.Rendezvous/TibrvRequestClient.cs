using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Sends a request to a server and waits for the reply. It is synchronous: the call
    /// blocks until a reply arrives or the timeout elapses.
    /// </summary>
    public sealed class TibrvRequestClient
    {
        private readonly TibrvConnection connection;

        /// <summary>Creates a client over an already-open connection.</summary>
        /// <param name="connection">a TibrvConnection object</param>
        public TibrvRequestClient(TibrvConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            this.connection = connection;
        }

        /// <summary>
        /// Sends a request to the given Subject and waits for the reply.
        /// </summary>
        /// <param name="subject">destination subject (e.g. "RPC.EMPLOYEE.GET")</param>
        /// <param name="request">request message (the Method field selects the feature)</param>
        /// <param name="timeoutSeconds">max wait time in seconds; returns null if the reply is late</param>
        /// <returns>the server's reply message, or null on timeout</returns>
        public TibrvMessage Request(string subject, TibrvMessage request, double timeoutSeconds)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("subject is empty.", "subject");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // Set the destination subject on the message.
            request.Inner.SendSubject = subject;

            // Send the request and wait for the reply.
            // SendRequest returns the reply message, or null if the timeout elapses.
            Message reply = this.connection.InnerTransport.SendRequest(request.Inner, timeoutSeconds);
            if (reply == null)
            {
                return null;
            }

            return new TibrvMessage(reply);
        }
    }
}
