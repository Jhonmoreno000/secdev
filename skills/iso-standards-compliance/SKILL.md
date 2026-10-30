---
name: iso-standards-compliance
category: Quality & ISO Governance
source: public-catalog/iso-25010-27001-12207
tokens_cost: 155
description: Software quality evaluation under ISO/IEC 25010, information security controls under ISO/IEC 27001, and lifecycle ISO/IEC 12207.
---
# Skill: ISO Standards & Quality Governance

## 1. ISO/IEC 25010 Product Quality Model
Evaluate and design software across the 8 mandatory quality characteristics:
1. **Functional Suitability:** Completeness, correctness, appropriateness.
2. **Performance Efficiency:** Time behavior (p95/p99 latency), resource utilization (CPU/RAM/VRAM), capacity.
3. **Compatibility:** Co-existence and interoperability via standard protocols.
4. **Usability:** Recognizability, operability, user error protection, accessibility.
5. **Reliability:** Maturity, availability, fault tolerance, recoverability (zero silent `catch` blocks).
6. **Security:** Confidentiality, integrity, non-repudiation, accountability, authenticity.
7. **Maintainability:** Modularity, reusability, analyzability, modifiability (low cyclomatic complexity <= 10), testability.
8. **Portability:** Adaptability, installability, replaceability.

## 2. ISO/IEC 27001 Security Controls
- Enforce least privilege (A.9), cryptographic key separation (A.10), secure development lifecycle (A.14), and tamper-evident audit logging (A.12.4).
