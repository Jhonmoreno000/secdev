---
name: api-security-rest-graphql
category: Seguridad de APIs y Servicios Web
source: owasp-api-security-top-10
tokens_cost: 165
description: Blindaje de interfaces de programacion REST y GraphQL segun OWASP API Top 10, autorizacion BOLA, limitacion de tasa y esquemas estrictos.
---

# Directivas de Seguridad para APIs REST y GraphQL (OWASP API Top 10)

1. Mitigacion de BOLA (Broken Object Level Authorization - API1:2023):
   - Nunca confiar ciegamente en IDs enviados por el cliente (`/api/pedidos/{id}`).
   - Toda consulta a la base de datos DEBE validar la relacion de pertenencia: `WHERE id = :id AND usuario_id = :auth_user_id`.

2. Limitacion de Tasa y Prevencion de Agotamiento de Recursos (API4:2023):
   - Configurar limites estrictos de tasa por IP y por Token (algoritmo Token Bucket).
   - Limitar el tamano maximo del cuerpo de la peticion (`MaxRequestBodySize = 2MB`).
   - Paginacion obligatoria con limite maximo (Keyset pagination, `limit <= 100`).

3. Respuestas de Error Estandarizadas (RFC 7807 Problem Details):
   - Nunca exponer trazas de pila (stack traces), rutas internas del servidor ni versiones de frameworks en las respuestas de error HTTP.
   - Devolver respuestas uniformes con estructura `application/problem+json` (type, title, status, detail, instance).
