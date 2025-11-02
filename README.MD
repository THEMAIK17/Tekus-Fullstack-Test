#  Tekus Technical Test: Fullstack Provider Management Solution

This repository contains the solution for the Fullstack Developer technical test. The architecture focuses on Clean Architecture (DDD) and SOLID principles, delivering a robust and highly maintainable codebase.

---
## 1.  Project Summary 

The solution successfully delivers a **100% complete and tested 'Vertical Slice'** for the **Provider entity**. This demonstrates the full cycle: SQL -> DDD Layers -> Unit Tests -> API REST -> React Frontend.

---
## 2. Backend Architecture: DDD & SOLID

The solution is split into 5 projects, ensuring strict separation of concerns (Clean Architecture):

* **Tekus.Domain :** Contains Entities (Provider, Service, etc.) and Contracts (Interfaces like `IProviderRepository`, `IUnitOfWork`). It has ZERO external dependencies.
* **Tekus.Application :** Contains the Business Logic (`ProviderService`) and Validations. It depends solely on Domain Interfaces.
* **Tekus.Infraestructure :** Implements the contracts using Entity Framework Core and SQL Server. It handles data access and implements the Unit of Work.
* **Tekus.API :** Contains Controllers (`ProvidersController`) and the configuration of the Dependency Injection (DI) container.
* **Tekus.Tests :** Uses **xUnit** and **Moq** to test `ProviderService` business logic in complete isolation.

### Key Architectural Highlights

* **Dependency Inversion (DIP):** Achieved by injecting interfaces (`IProviderService`, `IProviderRepository`) into constructors instead of concrete classes.
* **Paging & Searching:** Dynamically implemented in the `ProviderRepository` using `IQueryable` for highly efficient SQL queries (a core requirement).
* **Security:** API access is enforced via an **API Key Middleware**, checking the required `X-API-Key` header on every request.

---
## 3. Database Schema 

The database model uses a normalized, 4-table schema to handle dynamic custom fields and many-to-many service/country relationships.

* **Diagram File:** `/database/Diagrama-TekusBD.jpg`

---
## 4. Execution Guide

### 4.1. Setup (Shared)
1.  **Database:** Execute the SQL scripts in the `/database` folder: **`01-Schema.sql`** (Schema) then **`02-Data.sql`** (Initial Data).
2.  **Configuration:** The sensitive connection string and **API Key** are stored in **User Secrets**.
    * **Required API Key Header:** `X-API-Key: TEKUS-2025-FULLSTACK-TEST`

### 4.2. Backend (Tekus.API)
1.  Set `Tekus.API` as the startup project in Rider.
2.  Run the application. The API runs on `http://localhost:5262` .

### 4.3. Frontend (client-react)
1.  Navigate to the `/client-react` folder in VS Code.
2.  Run `npm install`.
3.  Ensure the **`.env`** file exists with `VITE_API_KEY="TEKUS-2025-FULLSTACK-TEST"`.
4.  Run `npm run dev`. The client runs on `http://localhost:5173`.

---
## 5. Functionalities Not Implemented (Time Analysis)

Due to the extreme time constraints , these items were prioritized for documentation. The implementation pattern is guaranteed by the current architecture:

* **CRUD for Services:** The implementation would follow the **exact same DDD/Service/Repository pattern** as `Provider`.
* **Dashboard/Indicators :** Would be implemented in a dedicated `DashboardController` using complex EF Core queries with `GROUP BY` to calculate metrics (ej, Service Count per Country).
* **External Country API:** Would be integrated via **`HttpClientFactory`** within the `Tekus.Infrastructure` layer.
* **Omitted UI Functionality (Edit/Delete):** While the **ProvidersController** fully implements the `PUT` (Update) and `DELETE` (Remove) endpoints, the respective forms and UI buttons were omitted in the React frontend. The architectural capability to complete the full CRUD cycle is demonstrated and only requires the final React presentation layer.