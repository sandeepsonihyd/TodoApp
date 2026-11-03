---
applyTo: "backend/**"
---
# Backend (C# / ASP.NET Core, .NET 10)

## Layers (each layer calls only the one directly below)
- Controllers/ - routing, model binding and status-code mapping only. Use DTOs. No business logic, no data access, never inject `ITodoRepository` or use the `Todo` entity.
- Services/ - business rules: trim and validate, set `DateCreated = DateTime.UtcNow`, map `Todo` to `TodoResponse`. Throw `ArgumentException` for invalid input. Never touch HTTP types.
- Repositories/ - the only code that uses `TodoDbContext`. No validation, no DTOs, no HTTP types.
- Data/ (DbContext), Models/ (entities), Dtos/ (requests and responses), Migrations/.
- When storage changes, only the repository layer changes.

## Conventions
- PascalCase for types, methods and properties; camelCase for locals and parameters; `_camelCase` for private fields.
- 4-space indent, file-scoped namespaces, nullable reference types on, constructor injection.
- Each service and repository has an interface and is registered as Scoped in Program.cs.
- Keep `PropertyNamingPolicy = null` in Program.cs.
- Use `DateTime.UtcNow`, never `DateTime.Now`.

## Controllers
- `[ApiController]` and `[Route("todos")]`.
- Service returns null/false -> `NotFound(ProblemDetails)`; `ArgumentException` -> `BadRequest(ProblemDetails)`; create -> `CreatedAtAction`; delete -> `NoContent`.
- Request DTOs carry `[Required]` and `[MaxLength]`; the service re-validates the same rules.

## Data and migrations
- Every model change needs an EF Core migration: `dotnet ef migrations add <Name>` from backend/TodoApp.Api. Never hand-edit the snapshot or an applied migration.
- Use `AsNoTracking` for reads. `Update` and `Delete` return `bool` (false = id not found).
- Adding a field (e.g. Category): update Todo, DTOs, `ToResponse`, repository, migration, tests and the frontend together.

## Testing
- backend/TodoApp.Tests uses xUnit, Moq for unit tests, and `TodoApiFactory` (WebApplicationFactory with a temp SQLite file) for API tests.
- Cover each status code (200/201/204/400/404) for any endpoint you add or change.
- Run `dotnet build` and `dotnet test` from backend/.

## Do NOT
- Put data access in a controller or a service, or business logic in a controller.
- Add NuGet packages without stating why.
