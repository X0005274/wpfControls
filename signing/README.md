# Code signing — com.example

릴리스 실행파일(`com.example.samples.exe`)과 라이브러리(`com.example.dll`)는
**자체 서명(self-signed) 코드서명 인증서**로 서명합니다. 이 폴더에는 그 인증서의
**공개 부분**과 신뢰 등록 스크립트가 들어 있습니다.

## 파일

| 파일 | 설명 |
|---|---|
| `com.example-codesign.cer` | 공개 코드서명 인증서 (개인키 없음). 신뢰 등록·GPO 배포용. |
| `install-trust.ps1` | `.cer`를 이 PC의 신뢰 저장소에 등록하는 스크립트. |

## 왜 필요한가

자체 서명 인증서는 발급 체인의 루트가 시스템에 신뢰되어 있지 않으므로, 서명만으로는
SmartScreen "알 수 없는 게시자" 경고가 사라지지 않습니다. 대상 PC의
**신뢰할 수 있는 루트 인증 기관(Trusted Root)** 과 **신뢰할 수 있는 게시자(Trusted Publishers)**
에 위 `.cer`를 등록하면, 이 인증서로 서명된 바이너리가 경고 없이 실행됩니다.

## 대상 PC에서 신뢰 등록

관리자 권한 PowerShell에서:

```powershell
powershell -ExecutionPolicy Bypass -File .\install-trust.ps1
```

도메인 환경이라면 GPO(*컴퓨터 구성 → 정책 → Windows 설정 → 보안 설정 → 공개 키 정책*)로
`com.example-codesign.cer`를 **신뢰할 수 있는 루트**와 **신뢰할 수 있는 게시자**에
배포하는 것을 권장합니다.

## 새 릴리스 서명 방법

개인키(`.pfx`)는 **저장소에 두지 않습니다.** 서명 담당자의 안전한 위치에 보관된
`.pfx`(또는 서명용 PC의 `Cert:\CurrentUser\My` 인증서)로 서명합니다.

```powershell
# 인증서가 현재 사용자 저장소에 있는 경우 (thumbprint로 지정)
$cert = Get-Item Cert:\CurrentUser\My\5B9CEC0ACF75B7508D68406F493D2D20CB0EB435
Set-AuthenticodeSignature -FilePath .\com.example.samples.exe -Certificate $cert -HashAlgorithm SHA256
Set-AuthenticodeSignature -FilePath .\com.example.dll      -Certificate $cert -HashAlgorithm SHA256

# 서명 확인 (신뢰 등록된 PC에서는 Valid)
(Get-AuthenticodeSignature .\com.example.samples.exe).Status
```

같은 인증서로 계속 서명하면 대상 PC들은 **신뢰 등록을 한 번만** 하면 됩니다.

## ⚠️ 보안 주의

- `.pfx`(개인키)는 **절대** 저장소에 커밋하거나 릴리스에 포함하지 마세요. 이 폴더에는
  공개 `.cer`만 둡니다.
- 인증서를 신뢰 등록하면 이 키로 서명된 모든 콘텐츠를 PC가 신뢰하게 됩니다.
  개인키 유출 시 즉시 인증서를 폐기(신뢰 저장소에서 제거)하고 재발급하세요.
