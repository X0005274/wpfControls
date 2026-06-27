package com.example.messaging.rendezvous.app;

import java.util.concurrent.CountDownLatch;

import com.example.messaging.rendezvous.TibrvConnection;
import com.example.messaging.rendezvous.TibrvMessage;
import com.example.messaging.rendezvous.TibrvMethodRouter;
import com.example.messaging.rendezvous.TibrvReplyServer;
import com.example.messaging.rendezvous.TibrvSettings;

/**
 * Runnable reply service: opens a connection, registers feature handlers, starts a
 * reply server on a subject, and keeps the process alive until shutdown.
 *
 * Run from Eclipse with Run As -> Java Application. Requires TIBRV_HOME and rvd; if
 * the RV native libraries are not on PATH, add the VM argument
 * -Djava.library.path=${env_var:TIBRV_HOME}/bin.
 */
public final class EmployeeService {

    private EmployeeService() {
    }

    public static void main(String[] args) throws Exception {
        TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
        TibrvConnection connection = new TibrvConnection(settings);

        // Register a handler per feature (Method).
        TibrvMethodRouter router = new TibrvMethodRouter();
        router.register("GetEmployee", request -> {
            String empNo = request.getString("empNo");
            System.out.println("GetEmployee request: " + empNo);

            TibrvMessage reply = new TibrvMessage();
            reply.setString("status", "OK");
            reply.setString("name", "Hong Gildong");
            reply.setInt("age", 42);
            return reply;
        });

        // Start listening for requests on the subject.
        TibrvReplyServer server =
                new TibrvReplyServer(connection, "RPC.EMPLOYEE.GET", router::handle);
        System.out.println("Service up. Listening on RPC.EMPLOYEE.GET ... (Ctrl+C to stop)");

        // The reply server runs on a background dispatcher thread; keep main alive
        // until a shutdown signal (Ctrl+C / Eclipse stop) arrives, then clean up.
        CountDownLatch stop = new CountDownLatch(1);
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            server.close();
            connection.close();
            stop.countDown();
        }));
        stop.await();
    }
}
