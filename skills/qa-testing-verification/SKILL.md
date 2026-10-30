---
name: qa-testing-verification
category: Quality Assurance & Testing
source: public-catalog/test-pyramid-bva
tokens_cost: 130
description: Unit/Integration test architecture, Boundary Value Analysis (BVA), Equivalence Partitioning, and deterministic assertions.
---
# Skill: QA, Testing & Formal Verification

## 1. Test Design Techniques
- **Arrange-Act-Assert (AAA):** Structure every unit test into explicit setup, execution, and deterministic assertion phases.
- **Boundary Value Analysis (BVA):** Always test `min - 1`, `min`, `nominal`, `max`, `max + 1`, empty/null inputs, and overflow boundaries.
- **Deterministic Isolation:** Never depend on wall-clock time, external network sockets, or shared mutable global state in unit tests.

## 2. Security & Regression Coverage
- Every remediated bug or CWE vulnerability must be accompanied by a regression test that reproduces the failure on unpatched code and passes on patched code.
