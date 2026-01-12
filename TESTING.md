# 🧪 Testing & Validación

## ✅ Checklist de Funcionalidad

### 1. Carga Inicial
- [ ] La página carga sin errores
- [ ] El spinner aparece mientras carga
- [ ] Los platos se cargan desde la API
- [ ] No hay errores en la consola (F12)

### 2. Búsqueda
- [ ] Puedo buscar "Salmón" y se filtra correctamente
- [ ] Puedo limpiar la búsqueda
- [ ] La búsqueda es case-insensitive (mayúsculas/minúsculas)

### 3. Filtros por Categoría
- [ ] Puedo filtrar por "Entradas"
- [ ] Puedo filtrar por "Platos Fuertes"
- [ ] Puedo filtrar por "Postres"
- [ ] Puedo filtrar por "Bebidas"
- [ ] Puedo limpiar el filtro

### 4. Ordenamiento por Precio
- [ ] El ordenamiento "Menor a mayor" funciona
- [ ] El ordenamiento "Mayor a menor" funciona
- [ ] Puedo combinar búsqueda + filtro + ordenamiento

### 5. Crear Plato
- [ ] El modal se abre con "+ Nuevo Plato"
- [ ] Puedo escribir en todos los campos
- [ ] El botón "Guardar" valida los campos
- [ ] Sin nombre: muestra error "El nombre del plato es obligatorio"
- [ ] Sin categoría: muestra error
- [ ] Precio = 0: muestra error "El precio debe ser mayor a 0"
- [ ] Sin descripción: muestra error
- [ ] Los errores tienen estilo rojo
- [ ] Al guardar: el plato aparece en la lista
- [ ] Toast verde: "Plato creado exitosamente"
- [ ] Modal se cierra automáticamente

### 6. Editar Plato
- [ ] El botón "Editar" en cada plato abre el modal
- [ ] El modal muestra "Editar Plato"
- [ ] Los campos se rellenan con datos actuales
- [ ] Puedo cambiar los valores
- [ ] Al guardar: se actualiza en la lista
- [ ] Toast verde: "Plato actualizado exitosamente"

### 7. Validación de Formularios
- [ ] Nombre máx 100 caracteres
- [ ] Descripción máx 500 caracteres
- [ ] Precio máx $9,999.99
- [ ] Los campos requeridos no aceptan vacío

### 8. Modal
- [ ] El botón "X" cierra el modal
- [ ] El botón "Cancelar" cierra el modal
- [ ] Click en el fondo gris cierra el modal
- [ ] Presionar ESC cierra el modal
- [ ] El modal se centra correctamente

### 9. Notificaciones (Toast)
- [ ] Aparecen en la esquina inferior derecha
- [ ] Tienen el icono y color correcto
- [ ] Desaparecen después de 3 segundos
- [ ] No se solapan múltiples toasts

### 10. Errores de API
- [ ] Si la API no responde: mensaje de error
- [ ] Si hay error en red: toast rojo
- [ ] La UI sigue siendo responsive

### 11. Responsivo
- [ ] En móvil: los filtros se apilan verticalmente
- [ ] En tablet: la grid se ajusta
- [ ] En desktop: funciona perfectamente
- [ ] No hay scroll horizontal innecesario

---

## 🧬 Testing en la Consola (F12)

### Ver estado de dishes
```javascript
console.log(dishes);
```

### Buscar un plato específico
```javascript
dishes.find(d => d.name.includes('Salmón'))
```

### Verificar API URL
```javascript
console.log(API_URL);
```

### Forzar recarga de API
```javascript
fetchDishes();
```

---

## 📡 Testing de API

### Verificar endpoint GET
```bash
curl http://localhost:5127/api/dish
```

### Crear plato desde cURL
```bash
curl -X POST http://localhost:5127/api/dish \
  -H "Content-Type: application/json" \
  -d '{"name":"Test","category":"Entradas","price":25.50,"description":"Test"}'
```

### Actualizar plato desde cURL
```bash
curl -X PUT http://localhost:5127/api/dish/{id} \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated","category":"Entradas","price":30.00,"description":"Updated"}'
```

---

## 🔍 DevTools (F12)

### Tab Console
- Ver cualquier error de JavaScript
- Ejecutar comandos manuales
- Ver logs de fetch

### Tab Network
- Ver todas las llamadas a la API
- Verificar status code (200, 404, 500, etc)
- Ver request/response headers
- Inspeccionar JSON responses

### Tab Elements
- Inspeccionar la estructura HTML
- Ver clases CSS aplicadas
- Modificar estilos en vivo

---

## ⚠️ Errores Comunes

### Error: "Fetch failed"
- ❌ La API no está corriendo
- ✅ Inicia: `dotnet run` en src/Api

### Error: "CORS policy"
- ❌ CORS no está configurado
- ✅ Verifica Program.cs está con los cambios

### Los platos no se cargan
- ❌ La API no devuelve datos
- ✅ Prueba el endpoint con cURL

### El formulario no valida
- ❌ JavaScript no está cargando
- ✅ Abre F12 y revisa Console

---

## 📊 Estadísticas

| Métrica | Valor |
|---------|-------|
| Tamaño index.html | ~5 KB |
| Tamaño script.js | ~15 KB |
| Tamaño styles.css | ~12 KB |
| **Total sin GZIP** | ~32 KB |
| **Con GZIP** | ~8 KB |
| Tiempo de carga | < 1s |
| Dependencias externas | 0 |

---

## ✨ Mejoras Potenciales

Si quieres expandir:

- [ ] Agregar modal de confirmación para delete
- [ ] Agregar categoría personalizada
- [ ] Guardar favoritos en localStorage
- [ ] Exportar menú a PDF
- [ ] Importar platos desde archivo
- [ ] Galería de imágenes
- [ ] Filtro por precio rango
- [ ] Ordenar por nombre, rating, etc
- [ ] Dark mode
- [ ] Multiidioma (i18n)

---

## 📚 Documentación Adicional

Ver también:
- [QUICK_START.md](./QUICK_START.md) - Iniciar rápido
- [GUIA_EJECUCION.md](./GUIA_EJECUCION.md) - Guía completa
- [RESUMEN_CAMBIOS.md](./RESUMEN_CAMBIOS.md) - Cambios realizados
- [front/pure-plate-project/INSTRUCCIONES.md](./front/pure-plate-project/INSTRUCCIONES.md) - Docs frontend

---

¡Ahora tienes todo para validar que funciona correctamente! 🎉
