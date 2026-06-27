# Rendezvous Messaging Contract

Language-agnostic wire contract shared by the **.NET** library
(`com.example.Messaging.Rendezvous`) and the **Java** library
(`java/tibrv-messaging`). Both implement the same `Tibrv*` types against this
contract, so a client in one language can talk to a server in the other.

## Connection

A connection is defined by three TIBCO Rendezvous values:

| Value | Meaning | Example |
|---|---|---|
| Service | UDP service / port | `"7500"` (empty = RV default) |
| Network | NIC / multicast address | `""` (usually empty) |
| Daemon  | `rvd` daemon address | `"tcp:7500"` or `"tcp:host:7500"` |

## Message

A message is a flat set of **name = value** fields. Supported field types: `string`,
`int`, `double`.

- One reserved field, **`method`** (constant `MethodFieldName` / `METHOD_FIELD_NAME`),
  names the server feature being invoked (e.g. `"GetEmployee"`).
- Both sides read/write fields by name; missing fields read back as empty/`default`.

## Request / Reply

1. The client sets the request's `method` field and any input fields, then sends it to
   a **Subject** and waits up to N seconds for a reply (`null` on timeout).
2. The server listens on that Subject, routes by `method` to a handler, and the
   handler returns a reply message.
3. Unknown `method` → the server replies with `status = "ERROR"`,
   `message = "Unknown method: <name>"`.

### Subjects

Subjects are dotted names. Convention for request/reply (RPC-style) features:

```
RPC.<DOMAIN>.<ACTION>      e.g.  RPC.EMPLOYEE.GET
```

### Example exchange — `RPC.EMPLOYEE.GET`

Request fields:

| Field | Type | Notes |
|---|---|---|
| `method` | string | `"GetEmployee"` |
| `empNo` | string | employee id, e.g. `"E-10293"` |

Reply fields:

| Field | Type | Notes |
|---|---|---|
| `status` | string | `"OK"` or `"ERROR"` |
| `name` | string | employee name |
| `age` | int | employee age |
| `message` | string | error detail (only when `status = "ERROR"`) |

## Keeping both libraries in sync

When you add or change a feature (subject, `method` name, or fields), update this file
**first**, then implement it identically in both `com.example.Messaging.Rendezvous`
(.NET) and `java/tibrv-messaging` (Java).
