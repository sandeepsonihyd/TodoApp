import { useState } from 'react';

function TodoItem({ todo, onToggle, onEdit, onDelete }) {
  const [isConfirmingDelete, setIsConfirmingDelete] = useState(false);

  const created = new Date(todo.DateCreated).toLocaleString();

  return (
    <li style={{ marginBottom: '1rem' }}>
      <strong style={{ textDecoration: todo.IsCompleted ? 'line-through' : 'none' }}>
        {todo.Title}
      </strong>{' '}
      <span>{todo.IsCompleted ? '[Completed]' : '[Open]'}</span>
      {todo.Description && <div>{todo.Description}</div>}
      {todo.Category && <div>Category: {todo.Category}</div>}
      <small>Created: {created}</small>
      <div>
        <button onClick={() => onToggle(todo)}>
          {todo.IsCompleted ? 'Reopen' : 'Complete'}
        </button>
        <button onClick={() => onEdit(todo)}>Edit</button>
        {isConfirmingDelete ? (
          <span>
            {' '}Delete this todo?{' '}
            <button onClick={() => onDelete(todo.Id)}>Yes</button>
            <button onClick={() => setIsConfirmingDelete(false)}>No</button>
          </span>
        ) : (
          <button onClick={() => setIsConfirmingDelete(true)}>Delete</button>
        )}
      </div>
    </li>
  );
}

export default TodoItem;
