---
name: backend-clean-architecture
category: Backend & Distributed Systems
source: public-catalog/clean-hexagonal-architecture
tokens_cost: 160
description: Clean/Hexagonal Architecture, SOLID principles, REST/gRPC API contracts, RFC 7807 error handling, and concurrency safety.
---
# Skill: Backend & Clean Architecture

## 1. Architectural Boundaries (Hexagonal / Clean)
- **Domain Layer:** Zero external framework dependencies. Pure business entities, value objects, and domain invariants.
- **Application Layer:** Use-cases, command/query handlers (CQRS), and port interfaces (`IRepository`, `IGateway`).
- **Infrastructure Layer:** Adapters for databases, HTTP clients, message brokers, and OS file systems.

## 2. API Design & Resilience
- **Contracts:** Strict schema validation at ingress boundaries. Return deterministic status codes and `RFC 7807 Problem Details` (`type`, `title`, `status`, `detail`, `instance`) on errors.
- **Resilience:** Enforce explicit timeouts, bounded retries with exponential backoff + jitter, and idempotency keys on state-mutating endpoints.
- **Concurrency:** Prefer immutable data structures, cancellation tokens (`CancellationToken` / `context.Context`), and bounded channels/queues to prevent thread starvation.
