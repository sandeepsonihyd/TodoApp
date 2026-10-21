import { useState } from 'react';

function TodoForm({ editingTodo, onSave, onCancel, serverError }) {
  const [title, setTitle] = useState(editingTodo ? editingTodo.Title : '');
  const [description, setDescription] = useState(
    editingTodo && editingTodo.Description ? editingTodo.Description : ''
  );
  const [category, setCategory] = useState(editingTodo?.Category || '');
  const [errors, setErrors] = useState([]);

  function handleSubmit(event) {
    event.preventDefault();
    const newErrors = [];
    const trimmedTitle = title.trim();

    if (trimmedTitle.length === 0) {
      newErrors.push('Title is required.');
    }
    if (trimmedTitle.length > 200) {
      newErrors.push('Title must be 200 characters or fewer.');
    }
    if (description.length > 1000) {
      newErrors.push('Description must be 1000 characters or fewer.');
    }
    if (category.length > 50) {
      newErrors.push('Category must be 50 characters or fewer.');
    }

    setErrors(newErrors);
    if (newErrors.length > 0) {
      return;
    }

    onSave({
      Title: trimmedTitle,
      Description: description || null,
      Category: category.trim() || null,
    });
    if (!editingTodo) {
      setTitle('');
      setDescription('');
      setCategory('');
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>{editingTodo ? 'Edit todo' : 'Add todo'}</h2>
      <div>
        <label htmlFor="title">Title (required, max 200)</label>
        <br />
        <input
          id="title"
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="description">Description (max 1000)</label>
        <br />
        <textarea
          id="description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="category">Category (max 50)</label>
        <br />
        <input
          id="category"
          type="text"
          maxLength={50}
          value={category}
          onChange={(e) => setCategory(e.target.value)}
        />
      </div>
      {errors.map((error) => (
        <p key={error} role="alert" style={{ color: 'red' }}>
          {error}
        </p>
      ))}
      {serverError && (
        <p role="alert" style={{ color: 'red' }}>
          {serverError}
        </p>
      )}
      <button type="submit">{editingTodo ? 'Save' : 'Add'}</button>
      {editingTodo && (
        <button type="button" onClick={onCancel}>
          Cancel
        </button>
      )}
    </form>
  );
}

export default TodoForm;
