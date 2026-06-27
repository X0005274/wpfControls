# com.example.Messaging.Rendezvous

TIBCO Rendezvous(RV)로 **서버 Method를 Request/Reply** 하는 통신 라이브러리입니다.
접속은 **Service / Network / Daemon**, 메시지 주소는 **Subject**, 데이터는 **이름=값 필드**로 주고받습니다.

## 전제 조건

- 이 프로젝트는 **TIBCO Rendezvous .NET 어셈블리(`TIBCO.Rendezvous.dll`)** 가 설치된 PC에서만 빌드됩니다.
- 환경변수 **`TIBRV_HOME`** 을 TIBCO RV 설치 폴더로 지정하세요(예: `C:\TIBCO\tibrv\8.6`).
  - csproj가 `$(TIBRV_HOME)\bin\TIBCO.Rendezvous.dll` 을 참조합니다.
  - 경로가 다르면 csproj의 `<TibrvAssembly>` 값을 직접 수정하세요.
- RV 런타임(rvd 데몬)이 동작 중이어야 실제 통신이 됩니다.
- API 이름은 TIBCO RV 8.x .NET API 기준입니다. 버전이 다르면 일부 호출명만 조정하면 됩니다.

## 구성 클래스

| 클래스 | 역할 |
|---|---|
| `TibrvSettings` | 접속 정보(Service/Network/Daemon) |
| `TibrvConnection` | RV 환경 + Transport를 열고 닫음(IDisposable) |
| `TibrvMessage` | 이름=값 필드 메시지(문자열/정수/실수). `Method` 필드로 기능 구분 |
| `TibrvRequestClient` | 요청을 보내고 응답을 기다림(동기) |
| `TibrvReplyServer` | 요청을 듣고 처리해 응답을 보냄(백그라운드) |
| `TibrvMethodRouter` | Method 이름별 처리 함수 등록/분기 |

## 사용 예 — 클라이언트(요청)

```csharp
using com.example.Messaging.Rendezvous;

// 1) 접속 정보로 연결을 엽니다(다 쓰면 using 으로 자동 정리).
TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
using (TibrvConnection connection = new TibrvConnection(settings))
{
    TibrvRequestClient client = new TibrvRequestClient(connection);

    // 2) 요청 메시지를 만듭니다. Method 로 서버 기능을 지정합니다.
    TibrvMessage request = new TibrvMessage();
    request.Method = "GetEmployee";
    request.SetString("empNo", "E-10293");

    // 3) 주소(Subject)로 보내고 최대 5초간 응답을 기다립니다.
    TibrvMessage reply = client.Request("RPC.EMPLOYEE.GET", request, 5.0);

    // 4) 응답을 확인합니다(null 이면 시간 초과).
    if (reply == null)
    {
        // 응답 없음(타임아웃) 처리
    }
    else
    {
        string name = reply.GetString("name");
        int age = reply.GetInt("age", 0);
    }
}
```

## 사용 예 — 서버(응답)

```csharp
using com.example.Messaging.Rendezvous;

TibrvSettings settings = new TibrvSettings("7500", "", "tcp:7500");
TibrvConnection connection = new TibrvConnection(settings);

// 1) 기능(Method)별 처리 함수를 등록합니다.
TibrvMethodRouter router = new TibrvMethodRouter();
router.Register("GetEmployee", delegate (TibrvMessage request)
{
    string empNo = request.GetString("empNo");

    // (여기서 실제 업무 로직/DB 조회 수행)
    TibrvMessage reply = new TibrvMessage();
    reply.SetString("status", "OK");
    reply.SetString("name", "홍길동");
    reply.SetInt("age", 42);
    return reply;
});

// 2) 주소(Subject)로 들어오는 요청을 듣는 서버를 시작합니다.
//    라우터의 Handle 을 처리 함수로 넘깁니다.
TibrvReplyServer server = new TibrvReplyServer(
    connection, "RPC.EMPLOYEE.GET", router.Handle);

// ... 프로그램이 동작하는 동안 서버 유지 ...

// 3) 종료 시 정리
server.Dispose();
connection.Dispose();
```

## 주의

- `TibrvConnection` / `TibrvReplyServer` 는 다 쓰면 반드시 `Dispose()` 하세요(`using` 권장).
- 응답 서버는 백그라운드 스레드에서 동작하므로, 처리 함수에서 화면 컨트롤을 직접 만지면 안 됩니다
  (WinForms라면 `Control.Invoke` 로 UI 스레드에 넘기세요).
