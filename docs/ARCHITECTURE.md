# Architecture Specification // secdev

## 1. System Layers

```
+-------------------------------------------------------+
|                   Terminal / CLI                      |
|           (powershell / cmd / bash -> secdev)         |
+-------------------------------------------------------+
                           |
                           v
+-------------------------------------------------------+
|                    Core Controller                    |
|   - CLI Flag Parser (--audit, --cli, --version)       |
|   - Config & State Manager                            |
|   - Lock-Free Token RingBuffer                        |
+-------------------------------------------------------+
           |                                  |
           v                                  v
+-----------------------+          +--------------------+
| Native Graphics Engine|          | Local AI Backend   |
| - GLFW / Win32 Window |          | - llama.cpp (CUDA) |
| - Dynamic Quad Batcher|          | - Qwen2.5-Coder-7B |
| - SDF Font Rasterizer |          | - Hybrid VRAM/RAM  |
| - Tactical Cyber HUD  |          | - Offline Enforcer |
+-----------------------+          +--------------------+
```

## 2. Graphics Engine Pipeline

- **Double-Buffering & Zero-Allocation Render Loop:** The engine updates UI vertices into a pre-allocated dynamic GPU vertex buffer (`VBO`). No memory is allocated on the heap inside `Render()`.
- **Signed Distance Fields (SDF) Text:** Text glyphes are converted into signed distance textures via FreeType, allowing continuous zoom, crystal-clear sharpness, and high-performance rendering of millions of characters per second without GPU stalls.
- **Asynchronous Token Queue:** Inference runs on a background worker thread. When the LLM emits a token, it pushes to a thread-safe ring buffer. The graphics engine pops tokens on the next V-Sync frame and smooth-scrolls the output pane.

## 3. Sandboxing & Security

- All file reads and AST inspections are confined to designated target folders.
- No network socket is opened to external domains (strictly air-gapped).
