import { useEffect, useState } from 'react';
import * as todoApi from './api/todoApi';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';

function App() {
  const [todos, setTodos] = useState([]);
  const [editingTodo, setEditingTodo] = useState(null);
  const [message, setMessage] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  async function loadTodos() {
    try {
      const data = await todoApi.getAll();
      setTodos(data);
    } catch (error) {
      setMessage({ type: 'error', text: error.message });
    }
    setIsLoading(false);
  }

  useEffect(() => {
    loadTodos();
  }, []);

  async function handleSave(todoData) {
    try {
      if (editingTodo) {
        await todoApi.update(editingTodo.Id, {
          ...todoData,
          IsCompleted: editingTodo.IsCompleted,
        });
        setEditingTodo(null);
        setMessage({ type: 'success', text: 'Todo updated.' });
      } else {
        await todoApi.create(todoData);
        setMessage({ type: 'success', text: 'Todo added.' });
      }
    } catch (error) {
      setMessage({ type: 'error', text: error.message });
    }
    await loadTodos();
  }

  async function handleToggle(todo) {
    try {
      await todoApi.update(todo.Id, {
        Title: todo.Title,
        Description: todo.Description,
        IsCompleted: !todo.IsCompleted,
      });
      setMessage({
        type: 'success',
        text: todo.IsCompleted ? 'Todo reopened.' : 'Todo completed.',
      });
    } catch (error) {
      setMessage({ type: 'error', text: error.message });
    }
    await loadTodos();
  }

  async function handleDelete(id) {
    try {
      await todoApi.remove(id);
      setMessage({ type: 'success', text: 'Todo deleted.' });
    } catch (error) {
      setMessage({ type: 'error', text: error.message });
    }
    await loadTodos();
  }

  return (
    <div style={{ maxWidth: '600px', margin: '0 auto', padding: '1rem', textAlign: 'left' }}>
      <h1>Todo App</h1>
      <p>
        <em>Note: todos are not saved permanently. They are lost when the server restarts.</em>
      </p>

      {message && (
        <p role="alert" style={{ color: message.type === 'error' ? 'red' : 'green' }}>
          {message.text}
        </p>
      )}

      <TodoForm
        key={editingTodo ? editingTodo.Id : 'new'}
        editingTodo={editingTodo}
        onSave={handleSave}
        onCancel={() => setEditingTodo(null)}
      />

      {isLoading ? (
        <p>Loading...</p>
      ) : (
        <TodoList
          todos={todos}
          onToggle={handleToggle}
          onEdit={setEditingTodo}
          onDelete={handleDelete}
        />
      )}
    </div>
  );
}

export default App;
