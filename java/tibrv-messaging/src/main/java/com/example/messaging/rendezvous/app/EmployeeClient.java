package com.example.messaging.rendezvous.app;

import com.example.messaging.rendezvous.TibrvConnection;
import com.example.messaging.rendezvous.TibrvMessage;
import com.example.messaging.rendezvous.TibrvRequestClient;
import com.example.messaging.rendezvous.TibrvSettings;

/**
 * Runnable test client for {@link EmployeeService}: sends one GetEmployee request and
 * prints the reply (or a timeout message). Optional program argument: the employee id.
 *
 * Run from Eclipse with Run As -> Java Application while EmployeeService is running.
 * Requires TIBRV_HOME and rvd (see EmployeeService for the native-library note).
 */
public final class EmployeeClient {

    private EmployeeClient() {
    }

    public static void main(String[] args) throws Exception {
        String empNo = args.length > 0 ? args[0] : "E-10293";

        TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
        try (TibrvConnection connection = new TibrvConnection(settings)) {
            TibrvRequestClient client = new TibrvRequestClient(connection);

            TibrvMessage request = new TibrvMessage();
            request.setMethod("GetEmployee");
            request.setString("empNo", empNo);

            // Send and wait up to 5 seconds for the reply.
            TibrvMessage reply = client.request("RPC.EMPLOYEE.GET", request, 5.0);
            if (reply == null) {
                System.out.println("No reply (timed out). Is EmployeeService running?");
                return;
            }

            System.out.println("status = " + reply.getString("status"));
            System.out.println("name   = " + reply.getString("name"));
            System.out.println("age    = " + reply.getInt("age", 0));
        }
    }
}
