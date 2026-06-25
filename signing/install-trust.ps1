#Requires -RunAsAdministrator
<#
  com.example 코드서명 인증서 신뢰 등록 스크립트
  - 자체 서명(self-signed) 코드서명 인증서의 공개 부분을 이 PC의
    'Trusted Root Certification Authorities'와 'Trusted Publishers'에 등록합니다.
  - 등록 후에는 이 인증서로 서명된 com.example.Demo.exe / com.example.dll 이
    SmartScreen '알 수 없는 게시자' 경고 없이 실행됩니다.

  실행 방법: 관리자 권한 PowerShell에서
      powershell -ExecutionPolicy Bypass -File .\install-trust.ps1

  ※ 이 작업은 해당 인증서를 '신뢰 앵커'로 추가합니다. 이 인증서로 서명된
     모든 콘텐츠를 PC가 신뢰하게 되므로, 개인키(.pfx)는 절대 외부에 노출하지 마세요.
     배포 시에는 GPO로 같은 .cer 를 도메인 PC들에 배포하는 것을 권장합니다.
#>
param(
    [string]$CerPath = (Join-Path $PSScriptRoot 'com.example-codesign.cer')
)

if (-not (Test-Path $CerPath)) {
    Write-Error ("인증서 파일을 찾을 수 없습니다: " + $CerPath)
    exit 1
}

Import-Certificate -FilePath $CerPath -CertStoreLocation 'Cert:\LocalMachine\Root' | Out-Null
Write-Host "[OK] Trusted Root Certification Authorities 에 등록"

Import-Certificate -FilePath $CerPath -CertStoreLocation 'Cert:\LocalMachine\TrustedPublisher' | Out-Null
Write-Host "[OK] Trusted Publishers 에 등록"

Write-Host ""
Write-Host "완료. 이제 서명된 com.example.Demo.exe 를 경고 없이 실행할 수 있습니다."
Write-Host "(제거하려면: 인증서 관리자(certlm.msc)에서 '신뢰할 수 있는 루트'와 '신뢰할 수 있는 게시자'의"
Write-Host " 'com.example Code Signing' 항목을 삭제하세요.)"
