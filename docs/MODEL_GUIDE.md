# AI Model Guide // secdev

## Recommended Hardware Fit

Your machine configuration:
* **GPU:** NVIDIA GeForce RTX 3050 Laptop (4 GB VRAM)
* **System RAM:** 32 GB RAM

### Model Allocations

| Model | Recommended Quantization | VRAM Allocation | System RAM Allocation | Use Case |
| :--- | :--- | :--- | :--- | :--- |
| **Qwen2.5-Coder-7B** | `Q4_K_M` (~4.68 GB) | ~2.8 GB (28 layers) | ~2.0 GB (residual) | General Coding, Vulnerability Hunting, Patch Creation |
| **WhiteRabbitNeo-2.5-7B** | `Q4_K_M` (~4.7 GB) | ~2.8 GB (28 layers) | ~2.1 GB (residual) | Red Team, Exploits, Malware Analysis, Fuzzing |
| **Qwen2.5-Coder-3B** | `Q8_0` (~3.4 GB) | 100% in VRAM | 0 GB | Ultra-Fast Real-Time Code Autocompletion |

## How to Switch Models

Edit `config/secdev.default.json`:
```json
{
  "ai": {
    "model_path": "models/whiterabbitneo-2.5-qwen-2.5-coder-7b-q4_k_m.gguf",
    "gpu_layers_offload": 28
  }
}
```
Or specify via terminal flag:
```bash
secdev --model models/whiterabbitneo-2.5-qwen-2.5-coder-7b-q4_k_m.gguf
```
