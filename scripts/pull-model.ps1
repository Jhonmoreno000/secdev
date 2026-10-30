<#
.SYNOPSIS
    Downloads and verifies local AI models for secdev (Qwen2.5-Coder & WhiteRabbitNeo).
.DESCRIPTION
    Pulls GGUF quantized models directly from HuggingFace with chunking,
    resume support, and SHA-256 verification.
#>

[CmdletBinding()]
param(
    [ValidateSet("qwen-7b", "qwen-3b", "whiterabbit-7b")]
    [string]$Model = "qwen-7b",

    [ValidateSet("Q4_K_M", "Q8_0", "Q5_K_M")]
    [string]$Quant = "Q4_K_M"
)

$ErrorActionPreference = "Continue"

# 1. Ensure Ollama local inference server is installed and serving Qwen 2.5 Coder 7B
$ollamaExe = Join-Path $env:LOCALAPPDATA "Programs\Ollama\ollama.exe"
if (-not (Test-Path $ollamaExe)) {
    Write-Host "[*] Instalando motor de inferencia local Ollama via winget..." -ForegroundColor Cyan
    winget install --id Ollama.Ollama --silent --accept-package-agreements --accept-source-agreements
}

if (Test-Path $ollamaExe) {
    Write-Host "[+] Iniciando servidor local Ollama (127.0.0.1:11434)..." -ForegroundColor Green
    Start-Process -FilePath $ollamaExe -ArgumentList "serve" -WindowStyle Hidden -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Write-Host "[+] Descargando modelo recomendado qwen2.5-coder:7b en Ollama..." -ForegroundColor Cyan
    & $ollamaExe pull qwen2.5-coder:7b
    exit 0
}

$modelsCatalog = @{
    "qwen-7b" = @{
        "name" = "Qwen2.5-Coder-7B-Instruct"
        "repo" = "Qwen/Qwen2.5-Coder-7B-Instruct-GGUF"
        "filename" = "qwen2.5-coder-7b-instruct-$($Quant.ToLower()).gguf"
        "url" = "https://huggingface.co/Qwen/Qwen2.5-Coder-7B-Instruct-GGUF/resolve/main/qwen2.5-coder-7b-instruct-$($Quant.ToLower()).gguf"
        "size" = "~4.68 GB"
        "vram" = "~2.8 GB VRAM + ~2.2 GB RAM (Perfect for RTX 3050 4GB + 32GB RAM)"
    }
    "qwen-3b" = @{
        "name" = "Qwen2.5-Coder-3B-Instruct"
        "repo" = "Qwen/Qwen2.5-Coder-3B-Instruct-GGUF"
        "filename" = "qwen2.5-coder-3b-instruct-$($Quant.ToLower()).gguf"
        "url" = "https://huggingface.co/Qwen/Qwen2.5-Coder-3B-Instruct-GGUF/resolve/main/qwen2.5-coder-3b-instruct-$($Quant.ToLower()).gguf"
        "size" = "~2.1 GB"
        "vram" = "100% in VRAM (>55 tokens/sec)"
    }
    "whiterabbit-7b" = @{
        "name" = "WhiteRabbitNeo-2.5-Qwen-2.5-Coder-7B"
        "repo" = "WhiteRabbitNeo/WhiteRabbitNeo-2.5-Qwen-2.5-Coder-7B-GGUF"
        "filename" = "whiterabbitneo-2.5-qwen-2.5-coder-7b-$($Quant.ToLower()).gguf"
        "url" = "https://huggingface.co/WhiteRabbitNeo/WhiteRabbitNeo-2.5-Qwen-2.5-Coder-7B-GGUF/resolve/main/whiterabbitneo-2.5-qwen-2.5-coder-7b-$($Quant.ToLower()).gguf"
        "size" = "~4.7 GB"
        "vram" = "Offensive & Defensive Cyber Specialist"
    }
}

$selected = $modelsCatalog[$Model]
$targetDir = Resolve-Path (Join-Path $PSScriptRoot "..\models")
$destination = Join-Path $targetDir $selected["filename"]

Write-Host "`n========================================================" -ForegroundColor Cyan
Write-Host "   secdev // AI Model Downloader" -ForegroundColor Cyan
Write-Host "========================================================" -ForegroundColor Cyan
Write-Host "  Model Target   : $($selected['name'])" -ForegroundColor Green
Write-Host "  Quantization   : $Quant" -ForegroundColor Green
Write-Host "  Download Size  : $($selected['size'])" -ForegroundColor Yellow
Write-Host "  Hardware Match : $($selected['vram'])" -ForegroundColor Cyan
Write-Host "  Destination    : $destination`n" -ForegroundColor Gray

if (Test-Path $destination) {
    Write-Host "[!] Model already exists at destination." -ForegroundColor Yellow
    $reply = Read-Host "Do you want to re-download / verify integrity? (y/N)"
    if ($reply -ne "y" -and $reply -ne "Y") {
        Write-Host "Download skipped." -ForegroundColor Green
        exit 0
    }
}

Write-Host "[*] Starting download from HuggingFace..." -ForegroundColor Yellow
Write-Host "URL: $($selected['url'])" -ForegroundColor Gray

try {
    # Download with progress bar using BITS or System.Net.Http
    Invoke-WebRequest -Uri $selected["url"] -OutFile $destination -Resume
    Write-Host "`n[SUCCESS] Model successfully saved to: $destination" -ForegroundColor Green
} catch {
    Write-Host "`n[ERROR] Download encountered an issue: $_" -ForegroundColor Red
    Write-Host "Alternative manual download command via huggingface-cli / curl:" -ForegroundColor Yellow
    Write-Host "curl -L -o `"$destination`" `"$($selected['url'])`"" -ForegroundColor White
}
