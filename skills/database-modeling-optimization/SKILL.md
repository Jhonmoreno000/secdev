---
name: database-modeling-optimization
category: Database & Data Engineering
source: public-catalog/postgres-sql-performance
tokens_cost: 150
description: Relational schema design (3NF), indexing strategies (B-Tree/GIN), parameterized queries, N+1 elimination, and ACID safety.
---
# Skill: Database Modeling & Query Optimization

## 1. Schema Design & Integrity
- **Normalization:** Default to 3NF; denormalize only with measured read-latency evidence.
- **Keys & Constraints:** Always define explicit `PRIMARY KEY`, `FOREIGN KEY ... ON DELETE`, `NOT NULL`, and `CHECK` constraints at the database engine level.
- **Timestamps & Auditing:** Store all timestamps in UTC (`TIMESTAMPTZ`). Include `created_at` and `updated_at`.

## 2. Performance & Security
- **Zero SQL Injection (CWE-89):** Strictly use parameterized queries / prepared statements (`$1`, `@param`, `?`). Never interpolate user strings into SQL.
- **Indexing:** Create composite B-Tree indexes matching `WHERE` equality columns first, followed by range/sort (`ORDER BY`) columns.
- **Query Hygiene:** Never use `SELECT *` in production code. Paginate large result sets using keyset/cursor pagination (`WHERE id > @last_id LIMIT @page_size`) instead of high `OFFSET`.
