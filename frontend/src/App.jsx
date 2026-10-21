import { useEffect, useState } from 'react';
import * as todoApi from './api/todoApi';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';

function App() {
  const [todos, setTodos] = useState([]);
  const [allTodos, setAllTodos] = useState([]);
  const [editingTodo, setEditingTodo] = useState(null);
  const [message, setMessage] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  const [filter, setFilter] = useState('all');
  const [categoryFilter, setCategoryFilter] = useState('');
  const [sort, setSort] = useState('created_desc');

  async function loadTodos(f = filter, s = sort, c = categoryFilter) {
    try {
      const completed = f === 'all' ? undefined : f === 'completed';
      const allData = await todoApi.getAll(completed, s);
      setAllTodos(allData);
      if (c) {
        setTodos(await todoApi.getAll(completed, s, c));
      } else {
        setTodos(allData);
      }
    } catch (error) {
      setMessage({ type: 'error', text: error.message });
    }
    setIsLoading(false);
  }

  async function handleCategoryChange(value) {
    setCategoryFilter(value);
    await loadTodos(filter, sort, value);
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
        Category: todo.Category,
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

  async function handleFilterChange(value) {
    setFilter(value);
    await loadTodos(value, sort);
  }

  async function handleSortChange(value) {
    setSort(value);
    await loadTodos(filter, value);
  }

  async function handleClearCompleted() {
    try {
      const count = await todoApi.clearCompleted();
      setMessage({ type: 'success', text: `Cleared ${count} completed todo(s).` });
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

      <div style={{ display: 'flex', gap: '1rem', margin: '1rem 0', alignItems: 'center' }}>
        <label>
          Show:{' '}
          <select value={filter} onChange={(e) => handleFilterChange(e.target.value)}>
            <option value="all">All</option>
            <option value="active">Active</option>
            <option value="completed">Completed</option>
          </select>
        </label>
        <label>
          Category:{' '}
          <select value={categoryFilter} onChange={(e) => handleCategoryChange(e.target.value)}>
            <option value="">All categories</option>
            {[...new Set(allTodos.map((todo) => todo.Category).filter(Boolean))]
              .sort()
              .map((category) => (
                <option key={category} value={category}>{category}</option>
              ))}
          </select>
        </label>
        <label>
          Sort:{' '}
          <select value={sort} onChange={(e) => handleSortChange(e.target.value)}>
            <option value="created_desc">Newest first</option>
            <option value="created_asc">Oldest first</option>
          </select>
        </label>
        <button type="button" onClick={handleClearCompleted}>Clear all completed</button>
      </div>

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
