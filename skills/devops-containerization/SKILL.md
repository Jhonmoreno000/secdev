---
name: devops-containerization
category: Ingenieria de Plataforma y Despliegue
source: directivas-estandares/cncf-devops
tokens_cost: 160
description: Contenedorizacion segura, compilaciones multi-etapa Docker, ejecucion rootless y manifiestos Kubernetes con politicas de recursos y seguridad.
---

# Directivas de Contenedorizacion Segura & Kubernetes (DevOps)

1. Compilacion Multi-Etapa (Multi-Stage Builds):
   - Etapa de construccion (builder): Contiene SDK, herramientas de compilacion y dependencias.
   - Etapa final de ejecucion (runtime): Utiliza imagenes minimas (distroless o alpine) sin herramientas de compilacion ni shells innecesarios.

2. Principio de Minimo Privilegio (Rootless Containers):
   - Nunca ejecutar como root (USER ID 0).
   - Crear y asignar usuario y grupo no privilegiado explicito: `USER 10001:10001`.
   - Montar el sistema de archivos raiz en modo solo lectura (`readOnlyRootFilesystem: true`).

3. Gobernanza de Recursos en Kubernetes:
   - Toda definicion de Pod o Deployment DEBE declarar explicitamente `resources.requests` y `resources.limits` para CPU y Memoria (evita degradacion por OOMKilled).
   - Configurar sondas `livenessProbe` y `readinessProbe` con umbrales deterministas.
