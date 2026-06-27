using System;
using TIBCO.Rendezvous;

namespace com.example.Messaging.Rendezvous
{
    /// <summary>
    /// Wraps a Rendezvous message as a simple set of "name = value" fields.
    ///
    /// The raw TIBCO Message type is awkward to use, so this class exposes only simple
    /// methods to set and get string / int / double fields. A special "method" field
    /// is used so the server can tell which feature is being invoked.
    /// </summary>
    public sealed class TibrvMessage
    {
        /// <summary>The agreed field name that carries the server feature to invoke.</summary>
        public const string MethodFieldName = "method";

        /// <summary>The underlying TIBCO message object (library-internal use only).</summary>
        internal Message Inner { get; private set; }

        /// <summary>Creates a new, empty message.</summary>
        public TibrvMessage()
        {
            this.Inner = new Message();
        }

        /// <summary>Wraps an existing TIBCO message as a TibrvMessage (internal use only).</summary>
        internal TibrvMessage(Message message)
        {
            this.Inner = message;
        }

        /// <summary>
        /// The server feature to invoke (e.g. "GetEmployee").
        /// Stored internally in the "method" field.
        /// </summary>
        public string Method
        {
            get { return this.GetString(MethodFieldName); }
            set { this.SetString(MethodFieldName, value); }
        }

        /// <summary>Adds/sets a string field.</summary>
        public void SetString(string fieldName, string value)
        {
            this.Inner.AddField(fieldName, value ?? string.Empty);
        }

        /// <summary>Adds/sets an integer field.</summary>
        public void SetInt(string fieldName, int value)
        {
            this.Inner.AddField(fieldName, value);
        }

        /// <summary>Adds/sets a double field.</summary>
        public void SetDouble(string fieldName, double value)
        {
            this.Inner.AddField(fieldName, value);
        }

        /// <summary>
        /// Reads a string field. Returns an empty string when the field is absent.
        /// </summary>
        public string GetString(string fieldName)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return string.Empty;
            }

            return field.Value.ToString();
        }

        /// <summary>
        /// Reads an integer field. Returns defaultValue when absent or not a number.
        /// </summary>
        public int GetInt(string fieldName, int defaultValue)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return defaultValue;
            }

            int result;
            if (int.TryParse(field.Value.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// Reads a double field. Returns defaultValue when absent or not a number.
        /// </summary>
        public double GetDouble(string fieldName, double defaultValue)
        {
            MessageField field = this.Inner.GetField(fieldName);
            if (field == null || field.Value == null)
            {
                return defaultValue;
            }

            double result;
            if (double.TryParse(field.Value.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>Returns whether a field with the given name is present.</summary>
        public bool HasField(string fieldName)
        {
            return this.Inner.GetField(fieldName) != null;
        }
    }
}
