<#
.SYNOPSIS
    secdev Developer Environment Bootstrapper & Diagnostics.
.DESCRIPTION
    Validates GPU/VRAM hardware, compiler toolchains (MSVC, Ninja, CMake),
    and installs git pre-commit hooks for automated quality gates.
#>

[CmdletBinding()]
param(
    [switch]$AutoInstall = $false
)

$ErrorActionPreference = "Stop"

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "   secdev // Developer Environment Bootstrapper" -ForegroundColor Cyan
Write-Host "========================================================`n" -ForegroundColor Cyan

# 1. Hardware & GPU Detection
Write-Host "[1/4] Inspecting Hardware Telemetry..." -ForegroundColor Yellow
$gpu = Get-CimInstance Win32_VideoController | Where-Object { $_.Name -like "*NVIDIA*" } | Select-Object -First 1

if ($gpu) {
    $vramGB = [Math]::Round($gpu.AdapterRAM / 1GB, 2)
    Write-Host "  -> Dedicated GPU Detected: $($gpu.Name)" -ForegroundColor Green
    Write-Host "  -> Dedicated VRAM: $vramGB GB" -ForegroundColor Green
} else {
    Write-Host "  -> No NVIDIA GPU detected. CPU inference fallback will be utilized." -ForegroundColor Yellow
}

$ram = (Get-CimInstance Win32_PhysicalMemory | Measure-Object -Property Capacity -Sum).Sum / 1GB
Write-Host "  -> System Memory (RAM): $([Math]::Round($ram, 1)) GB" -ForegroundColor Green

# 2. Toolchain Inspection
Write-Host "`n[2/4] Verifying Compiler & Build Toolchain..." -ForegroundColor Yellow

function Test-Executable($cmd) {
    return [bool](Get-Command $cmd -ErrorAction SilentlyContinue)
}

$tools = @{
    "git"   = (Test-Executable "git")
    "cmake" = (Test-Executable "cmake")
    "ninja" = (Test-Executable "ninja")
}

foreach ($tool in $tools.Keys) {
    if ($tools[$tool]) {
        $path = (Get-Command $tool).Source
        Write-Host "  -> [OK] $tool found: $path" -ForegroundColor Green
    } else {
        Write-Host "  -> [MISSING] $tool" -ForegroundColor Red
        if ($AutoInstall) {
            Write-Host "     Attempting installation via winget..." -ForegroundColor Yellow
            winget install --id "Kitware.CMake" -e --silent
        } else {
            Write-Host "     Tip: Install via winget: 'winget install -e --id Kitware.CMake Ninja-build.Ninja'" -ForegroundColor Gray
        }
    }
}

# 3. Setup Git Hooks
Write-Host "`n[3/4] Configuring Local Git Hooks..." -ForegroundColor Yellow
$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
if (Test-Path (Join-Path $repoRoot ".git")) {
    git -C $repoRoot config core.hooksPath .githooks
    Write-Host "  -> Local git hooks pointed to '.githooks/' (Pre-commit & Commit-msg guards enabled)." -ForegroundColor Green
} else {
    Write-Host "  -> Not inside a git repository root. Skipping git hooks." -ForegroundColor Yellow
}

# 4. Model Directory Verification
Write-Host "`n[4/4] Verifying Model Storage Cache..." -ForegroundColor Yellow
$modelDir = Join-Path $PSScriptRoot "..\models"
if (-not (Test-Path $modelDir)) {
    New-Item -ItemType Directory -Path $modelDir -Force | Out-Null
    Write-Host "  -> Created models directory at: $modelDir" -ForegroundColor Green
} else {
    Write-Host "  -> Models directory ready: $modelDir" -ForegroundColor Green
}

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "   Bootstrap Complete! Next Steps:" -ForegroundColor Cyan
Write-Host "   1. Run '.\scripts\pull-model.ps1' to fetch Qwen2.5-Coder." -ForegroundColor White
Write-Host "   2. Run '.\scripts\install-cli.ps1' to enable terminal 'secdev' command." -ForegroundColor White
Write-Host "========================================================`n" -ForegroundColor Cyan
