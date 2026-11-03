---
applyTo: "frontend/**"
---
# Frontend (React 19 + Vite, JavaScript)

## Structure
- src/App.jsx - root component. Owns todos, editingTodo, message, isLoading, filter and sort, and coordinates all API calls.
- src/components/ - TodoForm, TodoList, TodoItem. Receive data and callbacks via props.
- src/api/todoApi.js - the ONLY module that calls `fetch`. Exports getAll, getById, create, update, remove, clearCompleted. Turns non-2xx responses into `Error` with a readable message (ProblemDetails errors/detail/title; 404 -> "Todo not found.").
- Import it as `import * as todoApi from './api/todoApi'`.

## Conventions
- JavaScript only (.js/.jsx). No TypeScript.
- Functional components with hooks only; one default-exported component per file; PascalCase file names.
- camelCase for variables and functions; 2-space indent, single quotes, semicolons.
- API fields are PascalCase and used as-is (todo.Title, todo.IsCompleted, todo.Category). Never rename or camel-case them.
- Base URL comes from `import.meta.env.VITE_API_URL`, falling back to http://localhost:8000. No hard-coded URLs elsewhere.

## UI rules
- Render user text as plain text. Never use `dangerouslySetInnerHTML`.
- Everything is keyboard operable; labels are tied to inputs (`htmlFor`/`id`).
- Messages use `role="alert"`. Completed state uses a text marker, not color alone.
- Client validation mirrors the backend: Title required and at most 200 after trim; Description at most 1000.
- Show DateCreated in local time (`new Date(...).toLocaleString()`).
- On 404 show the message and reload the list.

## Quality
- Lint: `npm run lint`. Tests: `npm test` (Vitest + Testing Library, jsdom). Build: `npm run build`. Run from frontend/.
- ESLint flat config with react-hooks and react-refresh rules; keep it clean.

## Do NOT
- Call `fetch` from a component.
- Add routing, state-management or UI libraries unless asked, and state why for any new dependency.
