---
name: microservices-event-architecture
category: Arquitectura de Microservicios y Sistemas Distribuidos
source: directivas-estandares/distributed-systems
tokens_cost: 160
description: Patrones de sistemas distribuidos, mensajeria asincrona, consumidores idempotentes, Transactional Outbox, patron Saga y tolerancia a fallos.
---

# Directivas de Arquitectura Basada en Eventos y Microservicios

1. Consistencia Eventual y Patron Transactional Outbox:
   - Nunca intentar una transaccion distribuida bidireccional acoplada (2PC).
   - Escribir el evento en una tabla Outbox dentro de la misma transaccion de base de datos que modifica el estado de la entidad de negocio.
   - Un proceso asincrono en segundo plano publica los eventos desde la tabla Outbox hacia el broker de mensajeria con garantia al-menos-una-vez (At-Least-Once).

2. Consumidores Idempotentes (Idempotency Key):
   - Todo consumidor de mensajes DEBE verificar si el identificador unico del mensaje (`message_id` / `event_id`) ya fue procesado antes de ejecutar mutaciones de negocio.
   - Mantener un registro de claves de idempotencia para prevenir duplicaciones ante reintentos de red.

3. Tolerancia a Fallos y Circuit Breaker:
   - Proteger llamadas interservicios mediante patrones de Circuit Breaker (ej. Polly, Resilience4j) con caida elegante (fallback) y reintentos con retraso exponencial y dispersion aleatoria (Exponential Backoff with Jitter).
   - Enrutar mensajes fallidos recurrentes hacia colas de cartas muertas (DLQ / Dead Letter Queue) con telemetria y alertas.
