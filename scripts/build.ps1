<#
.SYNOPSIS
    Compiles the native secdev workstation and custom engine.
#>

[CmdletBinding()]
param(
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $projectRoot

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "   secdev // Native Engine & Workstation Compiler" -ForegroundColor Cyan
Write-Host "========================================================`n" -ForegroundColor Cyan

# 1. Compile Native Desktop Workstation (DirectX / Direct3D accelerated)
Write-Host "[1/2] Compiling Native Workstation GUI (Direct3D Native)..." -ForegroundColor Yellow
$guiProject = Join-Path $projectRoot "src\gui\gui.csproj"
$buildOut = Join-Path $projectRoot "build"

dotnet build $guiProject -c $Configuration -o $buildOut --nologo

if (Test-Path "$buildOut\secdev.exe") {
    Write-Host "[+] Compiled successfully: $buildOut\secdev.exe" -ForegroundColor Green
} else {
    Write-Host "[-] GUI Compilation failed." -ForegroundColor Red
}

Write-Host "`n[2/2] Compilation Summary:" -ForegroundColor Yellow
Write-Host "  -> Binary location: $buildOut\secdev.exe" -ForegroundColor Green
Write-Host "  -> To launch right now from terminal, type:" -ForegroundColor White
Write-Host "     secdev`n" -ForegroundColor Yellow
