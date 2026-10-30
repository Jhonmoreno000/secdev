## Summary of Changes

<!-- Provide a clear, high-level summary of what this PR introduces or fixes. -->

## Type of Change

- [ ] `feat`: New feature or capability
- [ ] `fix`: Bug fix
- [ ] `sec`: Security patch or hardening
- [ ] `perf`: Performance optimization
- [ ] `refactor`: Code reorganization with no functional changes
- [ ] `engine`: Graphics engine, shaders or rendering pipeline modification
- [ ] `ai`: Model inference, prompts, or weights management
- [ ] `docs`: Documentation updates

## Subsystem Impact

- [ ] Graphics Engine (OpenGL/DirectX/wgpu/Shaders)
- [ ] Local AI Inference (`llama.cpp` bridge / CUDA VRAM offloading)
- [ ] Terminal CLI / Launch Pipeline
- [ ] Security Tools / AST Sandbox
- [ ] CI/CD & Build System

## Security & Verification Checklist

- [ ] **Offline Guarantee:** Code verifies 0 outgoing network requests outside localhost.
- [ ] **Secret Check:** No API tokens, private keys, or passwords committed.
- [ ] **Memory Safety:** No uninitialized buffers, dangling pointers, or memory leaks in custom engine code.
- [ ] **Performance Check:** 60+ FPS maintained without frame drops during AI token streaming.
- [ ] **Unit / Integration Tests:** Added or updated where applicable.
- [ ] Tested on Windows x64.
