# Copilot Instructions - TodoApp
 
## Project
A single-user Todo application. No authentication. Built in versions:
- Version 1 (complete): in-memory storage, no database
- Version 2 (complete) : SQLite via Entity Framework Core with migrations
- Version 3 (current) : Category field added to todos
 
## Repository layout
Monorepo with one Visual Studio solution, TodoApp.slnx, at the root.
- backend/TodoApp.Api - C# ASP.NET Core Web API
- frontend - React with Vite (JavaScript)
- docs/ - PRD.md and TRD.md, which are the source of truth
New projects are added through Visual Studio (Solution Explorer >
Add > New Project). Never hand-edit TodoApp.slnx.
 
## Architecture
Three backend layers, strictly separated:
- controllers - HTTP concerns only. No business logic. No data access.
- services - business rules. Calls the repository. Never touches HTTP.
- repository - the only code that stores or retrieves data.
When storage changes, only the repository layer changes.
 
## Conventions
- C#: PascalCase for types, methods and properties; camelCase for
  locals and parameters. 4-space indent. Nullable reference types on.
- JavaScript: camelCase, 2-space indent, functional components only.
- Todo fields are Id, Title, Description, DateCreated, IsCompleted.
  Use these names exactly, in both the API and the frontend.
- JSON property names are PascalCase (PropertyNamingPolicy = null).
- API routes have no /api prefix: /todos, /todos/{id}.
 
## Do
- Keep code simple and readable. Prefer clarity over cleverness.
- Return correct HTTP status codes: 201 on create, 204 on delete,
  404 when a todo id does not exist.
- Consult docs/TRD.md before proposing structural changes.
 
## Do NOT
- Add authentication, authorization, or user accounts.
- Add a Category field. That is Version 3.
- Add new dependencies without stating why.
- Put data access code in a controller or a service.
- Use TypeScript. This project is JavaScript.
