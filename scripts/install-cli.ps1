<#
.SYNOPSIS
    Installs the 'secdev' terminal command into the User's PATH.
.DESCRIPTION
    Creates a dedicated launcher in $HOME\.secdev\bin and appends it to the User
    Environment Variable PATH without requiring Administrator elevation.
    Does NOT create any desktop icons or start menu entries.
#>

[CmdletBinding()]
param(
    [string]$TargetName = "secdev"
)

$ErrorActionPreference = "Stop"

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "   secdev // Terminal Command Registration" -ForegroundColor Cyan
Write-Host "========================================================`n" -ForegroundColor Cyan

$userBinDir = Join-Path $env:USERPROFILE ".secdev\bin"
if (-not (Test-Path $userBinDir)) {
    New-Item -ItemType Directory -Path $userBinDir -Force | Out-Null
    Write-Host "[+] Created dedicated CLI directory: $userBinDir" -ForegroundColor Green
}

# Source project root
$projectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$cmdPath = Join-Path $userBinDir "$TargetName.cmd"
$ps1Path = Join-Path $userBinDir "$TargetName.ps1"

# Generate CMD wrapper (compatible with cmd.exe, PowerShell, bash, etc.)
$cmdContent = @"
@echo off
setlocal
set "PROJECT_ROOT=$projectRoot"
if exist "%PROJECT_ROOT%\build\secdev.exe" (
    start "" "%PROJECT_ROOT%\build\secdev.exe" %*
) else (
    echo [secdev] Binary not yet compiled in %PROJECT_ROOT%\build\secdev.exe.
    echo [secdev] Compiling or building native target...
    powershell -NoProfile -ExecutionPolicy Bypass -File "%PROJECT_ROOT%\scripts\build.ps1"
    if exist "%PROJECT_ROOT%\build\secdev.exe" (
        start "" "%PROJECT_ROOT%\build\secdev.exe" %*
    )
)
endlocal
"@

Set-Content -Path $cmdPath -Value $cmdContent -Encoding ASCII
Write-Host "[+] Generated CMD launcher: $cmdPath" -ForegroundColor Green

# Also place shim into existing User PATH directories (.dotnet\tools) so already-open terminals work without restart
$dotnetToolsDir = Join-Path $env:USERPROFILE ".dotnet\tools"
if (Test-Path $dotnetToolsDir) {
    $shimPath = Join-Path $dotnetToolsDir "$TargetName.cmd"
    Set-Content -Path $shimPath -Value $cmdContent -Encoding ASCII
    Write-Host "[+] Installed active-session shim: $shimPath" -ForegroundColor Green
}

# Add to User PATH if not already present
$userPath = [Environment]::GetEnvironmentVariable("PATH", "User")
if ($userPath -notlike "*$userBinDir*") {
    $newPath = "$userPath;$userBinDir"
    [Environment]::SetEnvironmentVariable("PATH", $newPath, "User")
    $env:PATH = "$env:PATH;$userBinDir"
    Write-Host "[+] Added $userBinDir to User PATH." -ForegroundColor Green
    Write-Host "    (Available in all new terminal sessions immediately)" -ForegroundColor Cyan
} else {
    Write-Host "[+] $userBinDir is already in User PATH." -ForegroundColor Yellow
}

Write-Host "`n[SUCCESS] The command '$TargetName' is now registered!" -ForegroundColor Green
Write-Host "Try typing in any terminal:" -ForegroundColor White
Write-Host "    $TargetName --help" -ForegroundColor Yellow
Write-Host "    $TargetName --audit <file>" -ForegroundColor Yellow
Write-Host "    $TargetName" -ForegroundColor Yellow
Write-Host "`n(Zero desktop icons, launches strictly from console)`n" -ForegroundColor DarkGray
