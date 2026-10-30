---
name: token-context-optimizer
category: AI Context & Anti-Hallucination
source: public-catalog/context-compression-rag
tokens_cost: 95
description: Eliminates hallucinations and reduces prompt token consumption by 65-80% via AST symbol pruning and deterministic grounding.
---
# Skill: Token Optimizer & Anti-Hallucination Protocol

## 1. Context Compression Rules
- **AST-First Grounding:** Instead of sending full unrelated files, extract and index file signatures, imports, active function bodies, and detected diagnostic lines.
- **Selective Skill Injection:** Load only the domain skills matching the user's intent rather than concatenating all instructions.
- **Zero Hallucination Policy:**
  - Never invent non-existent APIs, libraries, or file paths.
  - Cite exact line numbers (`[Ln X]`) from the active editor buffer.
  - Return concise, directly applicable code diffs and verifiable engineering specifications without filler prose.
