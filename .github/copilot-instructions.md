# Copilot Instructions - TodoApp
 
## Project
A single-user Todo application. No authentication. Built in versions:
- Version 1 (complete): in-memory storage, no database
- Version 2 (complete) : SQLite via Entity Framework Core with migrations
- Version 3 (current) : Category field added to todos
 
## Source of truth
docs/PRD.md and docs/TRD.md. Consult them before proposing structural changes. If code and docs disagree, flag it instead of silently choosing one.

Stack-specific rules live in `.github/instructions/backend.instructions.md` and `.github/instructions/frontend.instructions.md`. This file holds only what is common to both.

## Repository layout
Monorepo with one Visual Studio solution, TodoApp.slnx, at the root.
- backend/TodoApp.Api - ASP.NET Core Web API (.NET 10, C#)
- backend/TodoApp.Tests - xUnit tests
- frontend - React + Vite (JavaScript)
- docs/ - PRD.md and TRD.md

New projects are added through Visual Studio (Solution Explorer > Add > New Project). Never hand-edit TodoApp.slnx.

## Shared API contract
- Todo fields, with these exact names and casing in the API and the frontend: Id, Title, Description, DateCreated, IsCompleted, Category.
- JSON property names are PascalCase (PropertyNamingPolicy = null).
- Routes have no /api prefix: /todos, /todos/{id}.
- Limits: Title is required, trimmed, at most 200 characters. Description is at most 1000. Keep backend and frontend validation in sync.
- DateCreated is UTC (ISO 8601). The UI shows it in local time.
- Status codes: 200 read/update, 201 create, 204 delete, 400 invalid input, 404 unknown id.
- Errors are ProblemDetails JSON with a human-readable message.
- Dev URLs: API http://localhost:8000, frontend http://localhost:5173. `VITE_API_URL` overrides the API URL.

## Do
- Keep code simple and readable. Prefer clarity over cleverness.
- When the API contract changes, update backend, frontend and docs together.
- Run the relevant build, lint and tests before finishing.

## Do NOT
- Add authentication, authorization, or user accounts.
- Use TypeScript. This project is JavaScript.
- Add new dependencies without stating why.
- Hand-edit TodoApp.slnx.
