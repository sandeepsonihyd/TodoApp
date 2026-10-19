# Todo App (Version 1)

Backend: ASP.NET Core Web API (.NET 10). Frontend: React + Vite. Todos are kept in memory and are lost when the backend restarts.

## Run the backend

1. Open a terminal in the backend project folder (the folder with TodoApp.Api.csproj).
2. Run: `dotnet run --launch-profile http`
3. The API runs on http://localhost:8000 and Swagger UI opens at http://localhost:8000/swagger

## Run the frontend

1. Open a second terminal in the `frontend` folder.
2. Run: `npm install`
3. Run: `npm run dev`
4. Open the URL that Vite prints (usually http://localhost:5173).

The API URL comes from `VITE_API_URL` in `frontend/.env.development` (http://localhost:8000).
