<#
.SYNOPSIS
    Compiles the native secdev executable and graphics engine.
#>

[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$projectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $projectRoot

Write-Host "`n[*] Configuring CMake for secdev ($Configuration)..." -ForegroundColor Cyan

# Locate vswhere to set up developer environment if cmake/cl not in PATH
if (-not (Get-Command cmake -ErrorAction SilentlyContinue)) {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $vsPath = & $vswhere -latest -property installationPath
        $cmakeVs = Join-Path $vsPath "Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin\cmake.exe"
        if (Test-Path $cmakeVs) {
            $env:PATH = "$env:PATH;$(Split-Path $cmakeVs)"
            Write-Host "[+] Loaded CMake from Visual Studio: $cmakeVs" -ForegroundColor Green
        }
    }
}

if (-not (Test-Path "build")) {
    New-Item -ItemType Directory -Path "build" | Out-Null
}

cmake -B build -DCMAKE_BUILD_TYPE=$Configuration
cmake --build build --config $Configuration --parallel

if (Test-Path "build\$Configuration\secdev.exe") {
    Copy-Item "build\$Configuration\secdev.exe" "build\secdev.exe" -Force
}

if (Test-Path "build\secdev.exe") {
    Write-Host "`n[SUCCESS] Build complete: build\secdev.exe" -ForegroundColor Green
} else {
    Write-Host "`n[ERROR] Build finished but binary was not found." -ForegroundColor Red
}
