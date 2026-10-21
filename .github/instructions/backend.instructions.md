---
applyTo: "backend/**"
---
# Backend Instructions
 
## Stack
C# 14, .NET 10, ASP.NET Core Web API. No ORM in Version 1.
 
## Rules
- Every endpoint is defined in a controller class. Controllers call
  services only.
- Services contain the business rules and never reference ASP.NET Core
  types.
- The repository holds the in-memory list. It is the only class that
  knows how todos are stored.
- Register the service and repository through dependency injection.
  In Version 1 the repository is a singleton so the list persists, and
  the service is scoped.
- Request and response bodies are DTO classes. Do not return raw
  dictionaries or anonymous objects from an endpoint.
- Ids are generated in the repository, not in the controller or service.
- Use `DateTime.UtcNow` for DateCreated. Never `DateTime.Now`.
 
## Do NOT
- Inject the repository directly into a controller.
- Use static mutable state outside the repository class.
- Add try/catch blocks around normal control flow. Let ASP.NET Core
  handle what it handles.
