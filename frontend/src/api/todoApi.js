const BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:8000';

async function handleResponse(response) {
  if (response.ok) {
    if (response.status === 204) {
      return null;
    }
    return response.json();
  }

  if (response.status === 404) {
    throw new Error('Todo not found.');
  }

  let message = 'Something went wrong.';
  try {
    const problem = await response.json();
    if (problem.errors) {
      message = Object.values(problem.errors).flat().join(' ');
    } else if (problem.detail) {
      message = problem.detail;
    } else if (problem.title) {
      message = problem.title;
    }
  } catch {
    // keep the default message
  }
  throw new Error(message);
}

export async function getAll(completed, sort, category) {
  const params = new URLSearchParams();
  if (completed !== undefined && completed !== null) {
    params.set('completed', completed);
  }
  if (sort) {
    params.set('sort', sort);
  }
  if (category) {
    params.set('category', category);
  }
  const query = params.toString();
  const response = await fetch(`${BASE_URL}/todos${query ? `?${query}` : ''}`);
  return handleResponse(response);
}

export async function clearCompleted() {
  const response = await fetch(`${BASE_URL}/todos/completed`, {
    method: 'DELETE',
  });
  return handleResponse(response);
}

export async function getById(id) {
  const response = await fetch(`${BASE_URL}/todos/${id}`);
  return handleResponse(response);
}

export async function create(todo) {
  const response = await fetch(`${BASE_URL}/todos`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(todo),
  });
  return handleResponse(response);
}

export async function update(id, todo) {
  const response = await fetch(`${BASE_URL}/todos/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(todo),
  });
  return handleResponse(response);
}

export async function remove(id) {
  const response = await fetch(`${BASE_URL}/todos/${id}`, {
    method: 'DELETE',
  });
  return handleResponse(response);
}
