# Product Requirements Document: Todo Application

## 1. Overview

The Todo application lets one person record, review, update, and remove personal tasks through a simple web interface. It is intentionally small. Its purpose is to deliver a reliable core task list that can be extended in later versions.

**Goal:** Let a user manage a list of tasks and track which ones are done.

**Success criteria for Version 1:** A user can create, view, edit, complete, and delete a todo. Every action gives clear feedback and takes effect immediately.

## 2. Target User

A single individual who wants a quick way to track personal tasks.

- Uses the application on their own, with no sign-in.
- Has no technical knowledge.
- Expects to manage tasks within seconds of opening the application.

## 3. In Scope for Version 1

- Create a todo with a title and an optional description.
- View the full list of todos.
- View the details of a single todo.
- Edit a todo's title and description.
- Mark a todo as completed, and mark it as not completed again.
- Delete a todo.
- Todos are kept only while the server is running.

## 4. Out of Scope for Version 1

- Permanent storage. Todos are lost when the server restarts.
- Categories, tags, or labels.
- Due dates, reminders, and priorities.
- Search, sorting options, and filtering.
- User accounts, login, and sharing.
- Multiple lists.
- Import and export.
- Offline use.

## 5. Functional Requirements

Each todo has these fields:

| Field | Description |
|---|---|
| Id | Unique identifier, assigned automatically and never changed |
| Title | Short text, required |
| Description | Longer text, optional |
| DateCreated | Date and time of creation, assigned automatically and never changed |
| IsCompleted | Yes or no. New todos start as not completed |

**FR-1 Create todo.** The user can create a todo by entering a title and, optionally, a description. The system assigns the Id and DateCreated and sets IsCompleted to no.

**FR-2 Title validation.** A todo cannot be saved without a title. The title must not be empty or only spaces. The title is limited to 100 characters, and the description to 500 characters. When validation fails, the user sees a clear message and nothing is saved.

**FR-3 List todos.** The user can see all todos in one list. Each item shows its title, its completion status, and its creation date.

**FR-4 Order of list.** Todos are listed with the newest first.

**FR-5 View todo.** The user can open one todo to see all its fields.

**FR-6 Edit todo.** The user can change a todo's title and description. The same validation rules as FR-2 apply. Id and DateCreated cannot be changed.

**FR-7 Complete todo.** The user can mark a todo as completed. Completed todos are visually distinct from open ones.

**FR-8 Reopen todo.** The user can mark a completed todo as not completed.

**FR-9 Delete todo.** The user can delete a todo. The system asks for confirmation first. After confirmation, the todo is removed permanently.

**FR-10 Empty state.** When no todos exist, the list shows a message that invites the user to create the first todo.

**FR-11 Not found.** If the user requests a todo that does not exist, for example one already deleted, the system shows a clear "not found" message and does not fail.

**FR-12 Feedback.** After each create, edit, complete, reopen, or delete action, the user sees confirmation that it succeeded or a message that explains why it did not.

## 6. Non-Functional Requirements

- **Performance:** Any action completes and shows its result within 1 second for up to 500 todos.
- **Usability:** A first-time user can create a todo without instructions. Every action is reachable from the main list screen within two interactions.
- **Reliability:** Invalid input never causes a crash or corrupts existing todos.
- **Data lifetime:** Todos exist only while the server runs. The user is told that data is not saved permanently.
- **Consistency:** Ids are unique and never reused during a server session.
- **Compatibility:** The interface works in current versions of mainstream desktop and mobile browsers.
- **Accessibility:** All actions can be used with a keyboard. Text has sufficient contrast. Completion status is not shown by color alone.
- **Security:** User-entered text is always displayed as plain text and is never interpreted as instructions.

## 7. Roadmap

**Version 1 (current):** In-memory todo list with the fields Id, Title, Description, DateCreated, and IsCompleted. Todos are lost on server restart.

**Version 2 (future):** Same functionality as Version 1. Todos are stored in a SQLite database and survive a server restart. Users should notice no change in behavior, except that their data persists.

**Version 3 (future):** A Category field is added to each todo. Details such as whether the field is required, how categories are chosen, and whether they can be used for filtering will be defined in a separate specification.

## 8. Assumptions

- One person uses the application, and no two people work on the same list.
- The server stays running during normal use, so losing data on restart is acceptable in Version 1.
- Times are shown in the user's local time.
- A list of up to 500 todos is sufficient.
- The interface is in English only.
- Completed todos stay in the list until the user deletes them.
- Version 2 does not need to migrate Version 1 data, because Version 1 data does not survive a restart.