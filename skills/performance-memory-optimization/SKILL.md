---
name: performance-memory-optimization
category: Rendimiento y Eficiencia de Recursos
source: directivas-estandares/performance-engineering
tokens_cost: 155
description: Optimizacion de latencia, eliminacion de cuellos de botella de memoria, zero-copy, pooling de objetos y operaciones asincronas no bloqueantes.
---

# Directivas de Rendimiento y Optimizacion de Memoria (ISO/IEC 25010)

1. Semantica Zero-Copy y Reduccion de Asignaciones en el Heap:
   - Utilizar vistas de memoria (`ReadOnlySpan<T>`, slices, buffer pooling) en lugar de duplicar subcadenas o matrices en memoria dinamica.
   - Reutilizar colecciones y buferes mediante patrones de ObjectPool o memoria contigua lineal para minimizar la presion del recolector de basura (GC / Garbage Collector).

2. Localidad de Referencia y Alineacion de Cache:
   - Estructurar los datos en memoria favoreciendo el acceso secuencial en lineas de cache L1/L2/L3 (Estructura de Matrices vs Matriz de Estructuras segun el patron de acceso).
   - Evitar fragmentacion de memoria y punteros dispersos en bucles de procesamiento critico.

3. E/S Asincrona No Bloqueante (Non-Blocking I/O):
   - Nunca bloquear hilos de despacho de interfaz grafica o del pool de hilos mediante llamadas síncronas bloqueantes (`.Result`, `.Wait()`, `sleep`).
   - Propagar tokens de cancelacion (`CancellationToken`) en todas las operaciones asincronas para liberar recursos inmediatamente ante peticiones abortadas.
