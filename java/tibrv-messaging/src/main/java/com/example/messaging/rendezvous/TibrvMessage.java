package com.example.messaging.rendezvous;

import com.tibco.tibrv.TibrvException;
import com.tibco.tibrv.TibrvMsg;

/**
 * Wraps a Rendezvous message as a simple set of "name = value" fields.
 *
 * The raw TIBCO {@link TibrvMsg} type is awkward to use, so this class exposes only
 * simple methods to set and get string / int / double fields. A special "method"
 * field is used so the server can tell which feature is being invoked.
 */
public final class TibrvMessage {

    /** The agreed field name that carries the server feature to invoke. */
    public static final String METHOD_FIELD_NAME = "method";

    private final TibrvMsg inner;

    /** Creates a new, empty message. */
    public TibrvMessage() throws TibrvException {
        this.inner = new TibrvMsg();
    }

    /** Wraps an existing TIBCO message as a TibrvMessage (internal use only). */
    TibrvMessage(TibrvMsg message) {
        this.inner = message;
    }

    /** The underlying TIBCO message object (library-internal use only). */
    TibrvMsg inner() {
        return this.inner;
    }

    /**
     * The server feature to invoke (e.g. "GetEmployee"). Stored in the "method" field.
     */
    public String getMethod() throws TibrvException {
        return this.getString(METHOD_FIELD_NAME);
    }

    public void setMethod(String value) throws TibrvException {
        this.setString(METHOD_FIELD_NAME, value);
    }

    /** Adds/sets a string field. */
    public void setString(String fieldName, String value) throws TibrvException {
        this.inner.update(fieldName, value != null ? value : "");
    }

    /** Adds/sets an integer field. */
    public void setInt(String fieldName, int value) throws TibrvException {
        this.inner.update(fieldName, value);
    }

    /** Adds/sets a double field. */
    public void setDouble(String fieldName, double value) throws TibrvException {
        this.inner.update(fieldName, value);
    }

    /** Reads a string field. Returns an empty string when the field is absent. */
    public String getString(String fieldName) throws TibrvException {
        Object value = this.inner.get(fieldName);
        return value != null ? value.toString() : "";
    }

    /** Reads an integer field. Returns defaultValue when absent or not a number. */
    public int getInt(String fieldName, int defaultValue) throws TibrvException {
        Object value = this.inner.get(fieldName);
        if (value == null) {
            return defaultValue;
        }

        try {
            return Integer.parseInt(value.toString());
        } catch (NumberFormatException notANumber) {
            return defaultValue;
        }
    }

    /** Reads a double field. Returns defaultValue when absent or not a number. */
    public double getDouble(String fieldName, double defaultValue) throws TibrvException {
        Object value = this.inner.get(fieldName);
        if (value == null) {
            return defaultValue;
        }

        try {
            return Double.parseDouble(value.toString());
        } catch (NumberFormatException notANumber) {
            return defaultValue;
        }
    }

    /** Returns whether a field with the given name is present. */
    public boolean hasField(String fieldName) throws TibrvException {
        return this.inner.get(fieldName) != null;
    }
}
