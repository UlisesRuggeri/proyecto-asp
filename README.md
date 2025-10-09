# EsteroidesToDo

> Plataforma para la gestión empresarial centrada en procesos de contratación y una arquitectura pensada para proyectos y tareas.

**EsteroidesToDo** facilita la publicación de vacantes, la postulación de usuarios y la gestión de respuestas por parte de las empresas. La arquitectura está diseñada para soportar multiempresa, control de acceso y versionado temporal de datos mediante SQL Server System-Versioning.

---

## Módulos y alcance actual

* **Módulo implementado**

  * **Vacantes & Postulaciones**: las empresas pueden publicar vacantes; los usuarios pueden postular; las empresas pueden aceptar o dejar pendientes/ignorar postulaciones. Este módulo está probado y operando en el backend.

* **Diseñado para soportar** (arquitectura / esquema)

  * Multiempresa (cada empresa separa sus datos y procesos).
  * Gestión de Proyectos y Tareas — entidades y relaciones definidas en el esquema; interfaz y lógica por implementar.
---

## Tech stack

* Backend: .NET 8 (C#)
* ORM: Entity Framework Core
* Base de datos: SQL Server (con System-Versioned tables)
* Autenticación: Claims & JWT (estructura preparada)
* Herramientas: EF Core Migrations, dotnet CLI

---

## Requisitos previos

* .NET 8 SDK
* SQL Server con permisos para crear bases y habilitar System Versioning
* Git

---

## Clonar el repositorio y levantar el backend (rápido)

```bash
# cloná el repo
git clone https://github.com/UlisesRuggeri/EsteroidesToDo.git
cd EsteroidesToDo

# restaurar y construir
dotnet restore
dotnet build

# aplicar migraciones 
cd src/Api
dotnet ef database update

# ejecutar la API
dotnet run --project EsteroidesToDo.Api
```

> El flujo de Vacantes está disponible a través de los endpoints del módulo de contratación. Revisá la colección de Postman / OpenAPI para ver rutas, modelos y ejemplos.

---

## Configuración mínima (appsettings.json - plantilla)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EsteroidesToDoDb;User Id=sa;Password=Your_password123;"
  },
  "Jwt": {
    "Key": "REEMPLAZAR_POR_SECRETO_LARGO",
    "Issuer": "EsteroidesToDo",
    "Audience": "EsteroidesToDoUsers",
    "ExpiryMinutes": 43200
  }
}
```

Video demo
[![Demo](https://img.youtube.com/vi/rgyWH1IVY1A/0.jpg)](https://www.youtube.com/watch?v=rgyWH1IVY1A)


---




