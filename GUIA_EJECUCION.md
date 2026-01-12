# Pure Plate - Guía de Ejecución Completa

## 🎯 Resumen

Has adaptado el frontend generado por Lovable a **HTML, CSS y JavaScript puro**, sin dependencias externas. Ahora el sistema está completamente integrado con los microservicios.

## 📋 Pasos para Ejecutar

### 1️⃣ Iniciar la API C# (.NET)

```bash
cd /home/federico/Documentos/DeliveryApi/src/Api
dotnet run
```

La API debería estar disponible en: `http://localhost:5127`

**Verify la salida en consola, debería mostrar:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5127
```

### 2️⃣ Iniciar el Frontend

**Opción A: Con Live Server en VS Code (Recomendado)**

1. Abre el proyecto en VS Code
2. Navega a `/home/federico/Documentos/DeliveryApi/front/pure-plate-project/index.html`
3. Haz clic derecho → "Open with Live Server"
4. Se abrirá automáticamente en `http://localhost:5500`

**Opción B: Con línea de comandos**

```bash
cd /home/federico/Documentos/DeliveryApi/front/pure-plate-project
npx live-server
```

**Opción C: Con Python**

```bash
cd /home/federico/Documentos/DeliveryApi/front/pure-plate-project
python3 -m http.server 8000
```

Luego accede a: `http://localhost:8000`

## ✅ Verificar que Funciona

1. El navegador debería cargar la página con el título "Pure Plate - Gestión de Menú"
2. Debería aparecer un spinner de carga
3. Los platos deberían cargarse desde la API
4. Puedes crear un nuevo plato con el botón "+ Nuevo Plato"
5. Puedes editar platos existentes

## 📁 Archivos Modificados

### Frontend
- **index.html** - Reemplazado completamente con HTML puro
- **public/script.js** - Integración con la API REST
- **public/styles.css** - Estilos mejorados y completos

### Backend
- **Program.cs** - Se agregó configuración de CORS para permitir requests desde el frontend

## 🔌 Integración API

El frontend se conecta con los siguientes endpoints:

```javascript
GET  http://localhost:5127/api/dish           // Obtener todos los platos
POST http://localhost:5127/api/dish           // Crear un plato
PUT  http://localhost:5127/api/dish/{id}      // Actualizar un plato
```

## 🛠️ Stack Tecnológico

### Backend
- .NET 8
- C# 
- Entity Framework Core
- SQL Server

### Frontend
- HTML5
- CSS3 (con variables CSS)
- JavaScript Vanilla (sin frameworks)
- Fetch API para llamadas HTTP

## 🎨 Características de la UI

✅ Búsqueda por nombre en tiempo real
✅ Filtrado por categoría
✅ Ordenamiento por precio
✅ Crear nuevos platos
✅ Editar platos existentes
✅ Validación de formularios
✅ Notificaciones toast
✅ Diseño responsivo (mobile-friendly)
✅ Spinner de carga
✅ Manejo de errores

## 📱 Responsive Design

La aplicación se adapta a:
- 📺 Pantallas de escritorio (1400px+)
- 💻 Tablets (768px - 1399px)
- 📱 Móviles (< 768px)

## 🔒 CORS Habilitado

Se ha configurado CORS en el backend para permitir solicitudes desde:
- `http://localhost:5500` (Live Server default)
- `http://localhost:8000` (Python http.server)
- `http://localhost:3000` (Otros servidores locales)

## ⚠️ Solución de Problemas

### "Network error" al crear/editar platos
**Solución:** Asegúrate de que:
1. El servidor C# esté ejecutándose en el puerto 5127
2. CORS esté habilitado en Program.cs (ya está agregado)
3. Mira la consola del navegador (F12) para detalles del error

### Platos no se cargan al abrir
**Solución:**
1. Abre la consola del navegador (F12)
2. Busca errores en la pestaña Console
3. Revisa la pestaña Network para ver si la API responde
4. Verifica que el servidor C# esté ejecutándose

### Error CORS
**Solución:** El archivo Program.cs ya incluye la configuración CORS necesaria. Si persiste:
1. Reinicia el servidor C#
2. Limpia la caché del navegador (Ctrl+Shift+Delete)

## 📝 Estructura de Datos Esperada

El endpoint GET debe devolver un array de objetos con esta estructura:

```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Salmón Teriyaki",
    "category": "Platos Fuertes",
    "price": 35.50,
    "description": "Filete de salmón glaseado con salsa teriyaki casera..."
  }
]
```

## 🚀 Pasos Recomendados para Desarrollo

1. **Terminal 1:** Ejecuta el backend C#
2. **Terminal 2:** Ejecuta el frontend con Live Server
3. **Browser:** Abre las DevTools (F12) para debugging
4. **Modifica:** Haz cambios en script.js o styles.css
5. **Live Server** recargará automáticamente

## 📚 Archivos Clave

```
DeliveryApi/
├── src/
│   └── Api/
│       ├── Program.cs          ← CORS habilitado aquí
│       ├── Controller/
│       │   └── DishController.cs
│       └── appsettings.json
└── front/
    └── pure-plate-project/
        ├── index.html          ← Frontend principal
        ├── public/
        │   ├── script.js       ← Lógica JavaScript
        │   └── styles.css      ← Estilos CSS
        └── INSTRUCCIONES.md    ← Documentación adicional
```

---

**¡Ahora tu aplicación está lista para usar! 🎉**

Cualquier duda, revisa la consola del navegador (F12) para mensajes de error detallados.
