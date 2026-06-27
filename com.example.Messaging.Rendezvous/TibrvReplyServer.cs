using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Listens for requests on a given Subject, processes each request, and sends back
    /// a reply.
    ///
    /// When a request arrives it calls the handler passed to the constructor and
    /// automatically replies to the requester with the result. A dedicated dispatcher
    /// thread runs in the background so the UI never freezes. Dispose() it when done.
    /// </summary>
    public sealed class TibrvReplyServer : IDisposable
    {
        private readonly TibrvConnection connection;
        private readonly Func<TibrvMessage, TibrvMessage> handler;
        private Queue queue;
        private Dispatcher dispatcher;
        private Listener listener;

        /// <summary>
        /// Starts the reply server.
        /// </summary>
        /// <param name="connection">an open RV connection</param>
        /// <param name="subject">subject to receive requests on (e.g. "RPC.EMPLOYEE.GET")</param>
        /// <param name="handler">function that takes a request and returns a reply message</param>
        public TibrvReplyServer(TibrvConnection connection, string subject, Func<TibrvMessage, TibrvMessage> handler)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("subject is empty.", "subject");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.connection = connection;
            this.handler = handler;

            // Create the queue that holds messages and the dedicated thread that drains it.
            this.queue = new Queue();
            this.listener = new Listener(this.queue, this.connection.InnerTransport, subject, null);
            this.listener.MessageReceived += this.OnMessageReceived;
            this.dispatcher = new Dispatcher(this.queue);
        }

        // Called whenever a request message arrives.
        private void OnMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            Message request = messageReceivedEventArgs.Message;

            // No reply subject means a one-way message, so do not reply.
            if (request.ReplySubject == null)
            {
                return;
            }

            // Wrap the request in our TibrvMessage and pass it to the handler.
            TibrvMessage requestMessage = new TibrvMessage(request);
            TibrvMessage replyMessage = this.handler(requestMessage);

            // If the handler returns null, send an empty reply.
            if (replyMessage == null)
            {
                replyMessage = new TibrvMessage();
            }

            // Send the reply back to the requester.
            this.connection.InnerTransport.SendReply(replyMessage.Inner, request);
        }

        /// <summary>Stops the server and releases resources.</summary>
        public void Dispose()
        {
            if (this.dispatcher != null)
            {
                this.dispatcher.Destroy();
                this.dispatcher = null;
            }

            if (this.listener != null)
            {
                this.listener.Destroy();
                this.listener = null;
            }

            if (this.queue != null)
            {
                this.queue.Destroy();
                this.queue = null;
            }
        }
    }
}
