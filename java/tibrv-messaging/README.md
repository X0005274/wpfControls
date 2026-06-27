# tibrv-messaging (Java)

.NET `com.example.Messaging.Rendezvous` 라이브러리의 **Java 버전**입니다.
TIBCO Rendezvous(RV)로 **서버 Method를 Request/Reply** 하며, .NET과 **동일한 통신 계약**을
사용합니다 — [`../../docs/rv-contract.md`](../../docs/rv-contract.md) 참고.
클래스 이름도 .NET 쪽(`Tibrv*`)과 1:1로 대응합니다.

## 전제 조건

- JDK 8 이상, Maven.
- **TIBCO Rendezvous Java API(`tibrvj.jar`)** 가 설치돼 있어야 합니다. 다음을 설정하세요:
  - `TIBRV_HOME` = TIBCO RV 설치 폴더 (예: `C:\TIBCO\tibrv\8.6`).
  - 빌드는 `$(TIBRV_HOME)/lib/tibrvj.jar` 를 참조합니다(경로가 다르면 `-Dtibrv.jar=<경로>` 로 지정).
- 실행 시에는 RV **네이티브 라이브러리**(`$TIBRV_HOME/bin`)에 접근할 수 있어야 하고
  (Windows는 `PATH`, 또는 `-Djava.library.path`), `rvd` 데몬이 동작 중이어야 합니다.

## 빌드

```bash
cd java/tibrv-messaging
mvn package      # tibrvj.jar 이 없으면 빌드 초기에 안내와 함께 멈춥니다
```

## 실행 클래스 (Eclipse에서 바로 실행)

`com.example.messaging.rendezvous.app` 패키지에 실행용 `main` 이 있습니다.
Eclipse에서 각 클래스를 우클릭 → `Run As → Java Application`.

| 클래스 | 역할 |
|---|---|
| `EmployeeService` | 응답 서비스를 띄움 — `RPC.EMPLOYEE.GET` Subject 수신 후 응답 |
| `EmployeeClient`  | 테스트 클라이언트 — `GetEmployee` 요청 1건 전송 후 응답 출력(인자로 사번 지정 가능) |

먼저 `rvd` 가 떠 있어야 하고, RV 네이티브 라이브러리가 안 잡히면 Run Configuration의
VM arguments 에 `-Djava.library.path=${env_var:TIBRV_HOME}/bin` 을 추가하세요.
`EmployeeService` 를 먼저 실행한 뒤 `EmployeeClient` 를 실행해 확인합니다.

## 구성 클래스

| 클래스 | 역할 |
|---|---|
| `TibrvSettings` | 접속 정보(Service / Network / Daemon) |
| `TibrvConnection` | RV 환경 + Transport 를 열고 닫음(AutoCloseable) |
| `TibrvMessage` | 이름=값 필드 메시지(문자열/정수/실수). `method` 필드로 기능 구분 |
| `TibrvRequestClient` | 요청을 보내고 응답을 기다림(동기) |
| `TibrvReplyServer` | 요청을 듣고 처리해 응답을 보냄(백그라운드 dispatcher) |
| `TibrvMethodRouter` | `method` 이름별 처리 함수 등록/분기 |
| `TibrvMethodHandler` | 처리 함수 인터페이스: `요청 -> 응답` |

## 사용 예 — 클라이언트(요청)

```java
import com.example.messaging.rendezvous.*;

// 1) 접속 정보로 연결을 엽니다(try-with-resources 로 자동 정리).
TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
try (TibrvConnection connection = new TibrvConnection(settings)) {
    TibrvRequestClient client = new TibrvRequestClient(connection);

    // 2) 요청 메시지를 만듭니다. Method 로 서버 기능을 지정합니다.
    TibrvMessage request = new TibrvMessage();
    request.setMethod("GetEmployee");
    request.setString("empNo", "E-10293");

    // 3) 주소(Subject)로 보내고 최대 5초간 응답을 기다립니다.
    TibrvMessage reply = client.request("RPC.EMPLOYEE.GET", request, 5.0);

    // 4) 응답을 확인합니다(null 이면 시간 초과).
    if (reply == null) {
        // 응답 없음(타임아웃) 처리
    } else {
        String name = reply.getString("name");
        int age = reply.getInt("age", 0);
    }
}
```

## 사용 예 — 서버(응답)

```java
import com.example.messaging.rendezvous.*;

TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
TibrvConnection connection = new TibrvConnection(settings);

// 1) 기능(Method)별 처리 함수를 등록합니다.
TibrvMethodRouter router = new TibrvMethodRouter();
router.register("GetEmployee", request -> {
    String empNo = request.getString("empNo");

    // (여기서 실제 업무 로직/DB 조회 수행)
    TibrvMessage reply = new TibrvMessage();
    reply.setString("status", "OK");
    reply.setString("name", "홍길동");
    reply.setInt("age", 42);
    return reply;
});

// 2) 주소(Subject)로 들어오는 요청을 듣는 서버를 시작합니다.
//    라우터의 handle 을 처리 함수로 넘깁니다.
TibrvReplyServer server = new TibrvReplyServer(connection, "RPC.EMPLOYEE.GET", router::handle);

// ... 프로그램이 동작하는 동안 서버 유지 ...

// 3) 종료 시 정리
server.close();
connection.close();
```

## 주의

- `TibrvConnection` / `TibrvReplyServer` 는 다 쓰면 반드시 `close()` 하세요
  (try-with-resources 권장).
- 응답 처리 함수는 백그라운드 dispatcher 스레드에서 동작하므로, 거기서 화면(UI)을
  직접 만지면 안 됩니다(애플리케이션에서 UI 스레드로 넘겨 처리하세요).
