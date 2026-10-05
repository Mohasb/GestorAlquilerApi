# GestorAlquilerApi

API REST de **[MHCars](https://github.com/Mohasb/MHCars-React)**, la web de alquiler y venta de coches con showroom 3D de mi proyecto final de **Desarrollo de Aplicaciones Web (DAW)**. Desarrollada en solitario. **Nota del proyecto: 10.**

[Frontend (React)](https://github.com/Mohasb/MHCars-React) · [Demo en vivo de la web](https://mohasb.github.io/MHCars-React/) · [Ver en mi portfolio](https://mohasb.github.io/#proyecto-daw)

## Tecnologías

| Parte | Tecnología |
|---|---|
| Framework | ASP.NET Core Web API (.NET 7) |
| Datos | Entity Framework Core con migraciones, SQLite |
| Seguridad | JWT con roles, contraseñas cifradas con BCrypt |
| Mapeo | AutoMapper (entidades ↔ DTO) |
| Documentación | Swagger (Swashbuckle), con autenticación JWT |

## Qué hace

- **Sucursales, coches, clientes, reservas y planificación:** operaciones CRUD completas (`/api/Branches`, `/api/Cars`, `/api/Clients`, `/api/Reservations`, `/api/Planning`). Las de gestión están restringidas al rol administrador.
- **Disponibilidad:** coches libres por sucursal, fechas y edad del conductor (`/api/Custom/getCarsAvailables/...`), con devolución en otra sucursal.
- **Login** con JWT, reservas de cada cliente y cambio de contraseña (`/api/Custom`).

Arquitectura por capas:
- `BussinessLogicLayer`: controladores, servicios, DTO, interfaces y modelos.
- `DataAccessLayer`: contexto de Entity Framework, repositorios y migraciones.

## Ejecutar en local

Necesita el SDK de .NET 7.

```bash
cd GestorAlquilerApi
dotnet restore
dotnet ef database update   # crea la base de datos SQLite con las migraciones
dotnet run
```

En modo desarrollo, la documentación interactiva queda en `/swagger`, en la dirección que muestra la consola al arrancar.

La configuración está en `appsettings.json` (cadena de conexión y emisor, audiencia y clave del JWT). Para un uso real, cambia la clave del JWT y guárdala fuera del repositorio.

## Autor

**Muhammad Hicho Haidor**, desarrollador full stack.
[Portfolio](https://mohasb.github.io) · [LinkedIn](https://www.linkedin.com/in/mhichohaidor)
