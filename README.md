# secdev // Native AI Workstation for Developers & Cybersecurity

> **High-Performance Local AI Terminal Workstation with Custom Hardware-Accelerated Graphics Engine & Cyber-Telemetry.**

[![CI Pipeline](https://github.com/ander/secdev/actions/workflows/ci.yml/badge.svg)](https://github.com/ander/secdev/actions/workflows/ci.yml)
[![Security Audit](https://github.com/ander/secdev/actions/workflows/security-audit.yml/badge.svg)](https://github.com/ander/secdev/actions/workflows/security-audit.yml)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows%20x64%20%7C%20Linux-emerald.svg)]()
[![Model](https://img.shields.io/badge/Model-Qwen2.5--Coder--7B--Instruct-cyan.svg)]()

---

## Overview

`secdev` is an ultra-fast, 100% offline local development and cybersecurity workstation. Unlike traditional web-wrapped tools (Electron), `secdev` runs on a **custom-built, hardware-accelerated native graphics engine** that launches instantly from the terminal via the `secdev` command.

### Key Capabilities

- **Zero-Latency Terminal Launch:** Invoke directly with `secdev` from PowerShell, CMD, or Windows Terminal. No desktop icons or start menu bloat.
- **Local AI Inference:** Powered by `Qwen2.5-Coder-7B-Instruct` (with support for `WhiteRabbitNeo` for offensive/defensive operations), running hybridly across your dedicated GPU (VRAM) and system RAM via high-performance C++/CUDA bindings.
- **Custom Native Graphics Engine:** Written from scratch with hardware shaders, Signed Distance Field (SDF) typography, and 120+ FPS decoupled render loops.
- **Tiling HUD Layout:** Split workstation combining AST vulnerability analysis, reverse engineering hex/disassembly inspection, and local LLM reasoning tokens.
- **100% Air-Gapped & Offline:** Absolute zero telemetry. Your exploits, zero-days, proprietary source code, and security audits never leave your machine.

---

## Branching Model

This repository enforces a strict, enterprise-grade GitFlow branching strategy:

- `main`: Production-stable releases. Only receives merge commits from `developer` via audited pull requests.
- `developer`: Main integration trunk. All feature branches merge here after passing CI and security gates.
- `feat/*`: Feature branches for active development and engine iterations.

---

## Quickstart

### Prerequisites
- Windows 10/11 x64
- NVIDIA GPU (RTX 3050 or higher recommended, 4GB+ VRAM)
- Git for Windows

### Automated Setup
```powershell
# 1. Clone repository
git clone https://github.com/ander/secdev.git
cd secdev

# 2. Run developer environment bootstrapper
powershell -ExecutionPolicy Bypass -File .\scripts\setup-dev.ps1

# 3. Download the recommended model (Qwen2.5-Coder-7B-Instruct GGUF)
powershell -ExecutionPolicy Bypass -File .\scripts\pull-model.ps1

# 4. Install the CLI shortcut (allows typing 'secdev' from anywhere)
powershell -ExecutionPolicy Bypass -File .\scripts\install-cli.ps1
```

---

## Security & Disclosure

Please refer to [SECURITY.md](SECURITY.md) for vulnerability reporting and security policies.

## License

Licensed under the [Apache License, Version 2.0](LICENSE).
