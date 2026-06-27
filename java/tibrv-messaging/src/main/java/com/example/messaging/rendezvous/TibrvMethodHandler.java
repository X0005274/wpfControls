package com.example.messaging.rendezvous;

import com.tibco.tibrv.TibrvException;

/**
 * Handles one feature: takes a request message and returns a reply message.
 * Equivalent to the {@code Func&lt;TibrvMessage, TibrvMessage&gt;} delegate used on
 * the .NET side.
 */
public interface TibrvMethodHandler {

    TibrvMessage handle(TibrvMessage request) throws TibrvException;
}
