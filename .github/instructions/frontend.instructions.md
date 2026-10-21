---
applyTo: "frontend/**"
---
# Frontend Instructions
 
## Stack
React with Vite (the version the project template installed). JavaScript,
not TypeScript. Function components with hooks.
 
## Rules
- One component per file. The file name matches the component name.
- State lives in the highest component that needs it, and is passed
  down as props. No global state library in Version 1.
- All API calls go through a single module, `src/api/todoApi.js`.
  Components never call fetch directly.
- The backend base URL comes from `import.meta.env.VITE_API_URL`, never
  a hardcoded string in a component.
- Field names from the API are used as-is: Id, Title, Description,
  DateCreated, IsCompleted.
 
## Do NOT
- Add a UI component library or a CSS framework.
- Add routing. Version 1 is a single page.
- Use class components.
