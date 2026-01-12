# 🏗️ Arquitectura del Sistema

## Diagrama General

```
┌─────────────────────────────────────────────────────────────────┐
│                         NAVEGADOR WEB                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │          FRONTEND - Pure Plate (HTML/CSS/JS)            │  │
│  ├──────────────────────────────────────────────────────────┤  │
│  │                                                          │  │
│  │  index.html              script.js         styles.css    │  │
│  │  ├─ Header              ├─ API Config      ├─ Variables  │  │
│  │  ├─ Filters             ├─ State Mgmt      ├─ Layout     │  │
│  │  ├─ Dishes Grid         ├─ Event Listen    ├─ Components │  │
│  │  ├─ Modal Form          ├─ Validation      ├─ Responsive │  │
│  │  └─ Toast               ├─ Fetch Calls     └─ Animations │  │
│  │                         └─ DOM Render                    │  │
│  │                                                          │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
│  [LOCALHOST:5500 - Live Server]                               │
└─────────────────────────────────────────────────────────────────┘
                               ↕ HTTP
                          (Fetch API)
                               ↕
┌─────────────────────────────────────────────────────────────────┐
│                      BACKEND - .NET 8                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌────────────────────────────────────────────────────────┐   │
│  │                 Program.cs                             │   │
│  │  ├─ CORS Policy (AllowFrontend)                       │   │
│  │  ├─ Dependency Injection                              │   │
│  │  └─ Middleware Configuration                          │   │
│  └────────────────────────────────────────────────────────┘   │
│                                                                 │
│  ┌────────────────────────────────────────────────────────┐   │
│  │              DishController                            │   │
│  │  ├─ GET  /api/dish         → GetAllDishes()          │   │
│  │  ├─ POST /api/dish         → CreateDish()            │   │
│  │  └─ PUT  /api/dish/{id}    → UpdateDish()            │   │
│  └────────────────────────────────────────────────────────┘   │
│                                                                 │
│  ┌────────────────────────────────────────────────────────┐   │
│  │              APPLICATION LAYER                         │   │
│  │  ├─ IDishService (Interface)                          │   │
│  │  ├─ DishService (Implementation)                      │   │
│  │  ├─ createDishRequest (DTO)                           │   │
│  │  └─ updateDishRequest (DTO)                           │   │
│  └────────────────────────────────────────────────────────┘   │
│                                                                 │
│  ┌────────────────────────────────────────────────────────┐   │
│  │           INFRASTRUCTURE LAYER                         │   │
│  │  ├─ DishCommand (Write Operations)                    │   │
│  │  ├─ DishQuery (Read Operations)                       │   │
│  │  └─ AppDbContext (EF Core)                            │   │
│  └────────────────────────────────────────────────────────┘   │
│                                                                 │
│  ┌────────────────────────────────────────────────────────┐   │
│  │              DOMAIN LAYER                              │   │
│  │  └─ Dish Entity                                        │   │
│  └────────────────────────────────────────────────────────┘   │
│                                                                 │
│  [LOCALHOST:5127 - HTTPS]                                     │
└─────────────────────────────────────────────────────────────────┘
                               ↕ EF Core
                          (SQL Server)
                               ↕
┌─────────────────────────────────────────────────────────────────┐
│                      SQL SERVER DATABASE                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────────────────┐                     │
│  │        Dishes Table                  │                     │
│  ├──────────────────────────────────────┤                     │
│  │ Id (GUID)                            │                     │
│  │ Name (VARCHAR)                       │                     │
│  │ Category (VARCHAR)                   │                     │
│  │ Price (DECIMAL)                      │                     │
│  │ Description (VARCHAR)                │                     │
│  │ CreatedAt (DATETIME)                 │                     │
│  │ UpdatedAt (DATETIME)                 │                     │
│  └──────────────────────────────────────┘                     │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

---

## Flujo de Datos - Crear Plato

```
1. USER INTERACTION
   └─ Click "+ Nuevo Plato"

2. FRONTEND (JavaScript)
   └─ openCreateModal()
      └─ Modal abierto

3. USER ENTERS DATA
   └─ Completa formulario

4. USER SUBMITS
   └─ Click "Guardar"

5. VALIDATION (Frontend)
   ├─ validateForm()
   ├─ Checks:
   │  ├─ Name not empty (max 100 chars)
   │  ├─ Category selected
   │  ├─ Price > 0 (max 9999.99)
   │  └─ Description not empty (max 500 chars)
   └─ If invalid → Show errors → Return

6. API CALL
   └─ fetch(API_URL, {
      ├─ method: 'POST'
      ├─ headers: 'application/json'
      └─ body: {name, category, price, description}
      })

7. NETWORK TRANSMISSION
   └─ HTTP POST → localhost:5127/api/dish

8. BACKEND PROCESSING
   ├─ DishController.CreateDish()
   ├─ DishService.createDish()
   ├─ IDishCommand.CreateDish()
   │  └─ DishCommand.CreateDish()
   │     └─ AppDbContext.Dishes.Add()
   └─ SaveChanges()

9. DATABASE OPERATION
   └─ INSERT INTO Dishes (Name, Category, Price, Description)
      VALUES (@Name, @Category, @Price, @Description)

10. RESPONSE
    ├─ Status: 200 OK
    └─ Body: (vacío o JSON del nuevo objeto)

11. FRONTEND UPDATE
    ├─ fetchDishes() (recargar lista)
    ├─ renderDishes() (actualizar UI)
    └─ closeModalHandler() (cerrar modal)

12. USER NOTIFICATION
    └─ Toast verde: "Plato creado exitosamente"
```

---

## Estado de la Aplicación (Frontend)

```javascript
// Global State
{
  dishes: [              // Array de platos desde la API
    {
      id: "uuid",
      name: string,
      category: string,
      price: number,
      description: string
    }
  ],
  editingDishId: null,   // ID del plato en edición (null si es nuevo)
  isLoading: boolean     // Spinner visible durante API calls
}

// Form State
{
  dishIdInput: string,           // ID (solo si editando)
  dishNameInput: string,         // Nombre del plato
  dishCategoryInput: string,     // Categoría seleccionada
  dishPriceInput: string,        // Precio
  dishDescriptionInput: string   // Descripción
}

// UI State
{
  modal: {
    visible: boolean,    // Modal abierto/cerrado
    title: string        // "Crear Nuevo Plato" o "Editar Plato"
  },
  profileMenu: {
    visible: boolean     // Menú de perfil abierto/cerrado
  },
  toast: {
    visible: boolean,    // Notificación visible
    message: string,
    type: 'success' | 'error' | 'warning'
  }
}
```

---

## Componentes del Frontend

```
┌─ Header
│  ├─ Navbar
│  │  ├─ Logo (Pure Plate)
│  │  └─ Actions
│  │     ├─ + Nuevo Plato (btn)
│  │     └─ Perfil (btn)
│  │        └─ Menu
│  │           ├─ Configuración
│  │           └─ Cerrar sesión
│  │
│  └─ Filters Section
│     ├─ Search Input
│     ├─ Category Select
│     └─ Price Sort Select
│
├─ Main Content
│  ├─ Loading Spinner (si loading)
│  │
│  ├─ Dishes Grid (si hay platos)
│  │  └─ Dish Card (x N)
│  │     ├─ Image (emoji)
│  │     ├─ Header
│  │     │  ├─ Name
│  │     │  └─ Price
│  │     ├─ Category Badge
│  │     ├─ Description
│  │     └─ Actions
│  │        └─ Edit Button
│  │
│  └─ Empty State (si sin platos)
│     ├─ Icon
│     ├─ Title
│     └─ Message
│
├─ Modal (oculto por defecto)
│  ├─ Header
│  │  ├─ Title
│  │  └─ Close Button (X)
│  │
│  ├─ Form
│  │  ├─ Name Input + Error
│  │  ├─ Category Select + Error
│  │  ├─ Price Input + Error
│  │  ├─ Description Textarea + Error
│  │  │
│  │  └─ Actions
│  │     ├─ Save Button
│  │     └─ Cancel Button
│  │
│  └─ Backdrop (fondo gris semi-transparente)
│
└─ Toast Notification (esquina inferior derecha)
   ├─ Message
   └─ Auto-hide después de 3s
```

---

## Flujo de Eventos

```
BUSCAR
  └─ Input#searchInput event:input
     └─ renderDishes()

FILTRAR
  └─ Select#categoryFilter event:change
     └─ renderDishes()

ORDENAR
  └─ Select#priceSort event:change
     └─ renderDishes()

CREAR PLATO
  └─ Button#createDishBtn event:click
     └─ openCreateModal()

EDITAR PLATO
  └─ Button.edit-btn event:click
     └─ openEditModal(dishId)

SUBMIT FORM
  └─ Form#dishForm event:submit
     └─ validateForm()
        └─ createDish() o updateDish()

CERRAR MODAL
  ├─ Button#closeModal event:click
  ├─ Button#cancelBtn event:click
  ├─ Div.modal-backdrop event:click
  └─ Document event:keydown (ESC)
     └─ closeModalHandler()

PROFILE MENU
  ├─ Button#profileBtn event:click
  │  └─ toggleProfileMenu()
  └─ Document event:click (fuera)
     └─ closeProfileMenu()
```

---

## Métodos Principales (JavaScript)

```javascript
// API Calls
fetchDishes()          // GET todos los platos
createDish(data)       // POST nuevo plato
updateDish(id, data)   // PUT actualizar plato

// Rendering
renderDishes()         // Renderizar grid de platos
createDishCard(dish)   // HTML de un card

// Modal
openCreateModal()      // Abrir modal para crear
openEditModal(id)      // Abrir modal para editar
closeModalHandler()    // Cerrar modal
resetForm()            // Limpiar formulario

// Validation
validateForm()         // Validar todos los campos
clearErrors()          // Limpiar mensajes de error

// Form Handling
handleFormSubmit(e)    // Submit del formulario

// UI Updates
showToast(msg, type)   // Mostrar notificación
showLoadingSpinner(bool) // Mostrar/ocultar spinner
toggleProfileMenu()    // Toggle menú de perfil

// Utilities
escapeHtml(text)       // Escapar caracteres HTML
```

---

## Métodos Principales (C# Backend)

```csharp
// Controller
DishController.GetAllDishes()      // GET /api/dish
DishController.CreateDish(request) // POST /api/dish
DishController.UpdateDish(id, req) // PUT /api/dish/{id}

// Service
IDishService.GetAllDishes()        // Obtener todos
IDishService.createDish(request)   // Crear
IDishService.updateDish(id, req)   // Actualizar

// Repository Commands
IDishCommand.CreateDish(dish)      // Crear en DB
IDishCommand.UpdateDish(dish)      // Actualizar en DB

// Repository Queries
IDishQuery.GetAllDishes()          // Leer desde DB
IDishQuery.GetById(id)             // Leer por ID
```

---

## Integración Frontend-Backend

```
┌─────────────────────────────────────┐
│      Frontend (JavaScript)          │
│  const data = {                     │
│    name: "Salmón",                  │
│    category: "Platos Fuertes",      │
│    price: 35.50,                    │
│    description: "..."               │
│  }                                  │
└─────────────────────────────────────┘
            ↓ JSON
        HTTP POST
   /api/dish
            ↓
┌─────────────────────────────────────┐
│    Backend (C# .NET 8)              │
│  [FromBody] createDishRequest req   │
│  {                                  │
│    Name: "Salmón",                  │
│    Category: "Platos Fuertes",      │
│    Price: 35.50,                    │
│    Description: "..."               │
│  }                                  │
└─────────────────────────────────────┘
            ↓
     Entity Framework
     INSERT INTO Dishes
            ↓
      SQL Server DB
```

---

## Patrones Utilizados

| Patrón | Ubicación | Descripción |
|--------|-----------|-------------|
| MVC | Backend | Model-View-Controller |
| CQRS | Backend | Command Query Responsibility Segregation |
| Dependency Injection | Backend | inyección de dependencias |
| Repository | Backend | Acceso a datos |
| Event-Driven | Frontend | Escuchadores de eventos |
| State Management | Frontend | Variables globales + closures |
| REST API | Comunicación | APIs RESTful |

---

## Escalabilidad Futura

```
Actual (Monolítico simplificado)
Frontend (HTML/CSS/JS) ←→ Backend Único (.NET)

Potencial (Microservicios)
Frontend ←→ API Gateway
             ├─ Dishes Microservice
             ├─ Orders Microservice
             ├─ Users Microservice
             └─ Payments Microservice
             
Almacenamiento
├─ Dishes DB (SQL)
├─ Orders DB (NoSQL)
└─ Cache (Redis)

Deployment
├─ Frontend (Vercel, Netlify)
├─ Backend (Docker, Kubernetes)
└─ Database (Cloud DB)
```

---

Este es un sistema simple pero escalable listo para producción. 🚀
