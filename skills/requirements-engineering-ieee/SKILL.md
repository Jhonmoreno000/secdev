---
name: requirements-engineering-ieee
category: Requirements & Systems Analysis
source: public-catalog/iso-ieee-29148-ears
tokens_cost: 140
description: ISO/IEC/IEEE 29148 & IEEE 830 requirements specification using EARS syntax and testable Gherkin acceptance criteria.
---
# Skill: Requirements Engineering (ISO/IEC/IEEE 29148 & EARS)

## 1. EARS (Easy Approach to Requirements Syntax)
Write unambiguous, testable requirements using deterministic patterns:
- **Ubiquitous:** `The <system> shall <response>.`
- **Event-Driven:** `WHEN <trigger>, the <system> shall <response>.`
- **State-Driven:** `WHILE <in state>, the <system> shall <response>.`
- **Unwanted Behavior:** `IF <unwanted condition>, THEN the <system> shall <mitigation response>.`

## 2. Verification & Acceptance Criteria (Gherkin)
Every functional requirement must include measurable NFR targets (latency, throughput, memory ceiling) and deterministic scenarios:
- `GIVEN <precondition / initial state>`
- `WHEN <actor action / event>`
- `THEN <verifiable outcome / invariant>`
