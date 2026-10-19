using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly List<Todo> _todos = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public IReadOnlyList<Todo> GetAll()
    {
        lock (_lock)
        {
            return _todos.Select(Copy).ToList();
        }
    }

    public Todo? GetById(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            return todo == null ? null : Copy(todo);
        }
    }

    public Todo Add(Todo todo)
    {
        lock (_lock)
        {
            todo.Id = _nextId;
            _nextId++;
            _todos.Add(Copy(todo));
            return Copy(todo);
        }
    }

    public bool Update(Todo todo)
    {
        lock (_lock)
        {
            var index = _todos.FindIndex(t => t.Id == todo.Id);
            if (index == -1)
            {
                return false;
            }

            _todos[index] = Copy(todo);
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return false;
            }

            _todos.Remove(todo);
            return true;
        }
    }

    private static Todo Copy(Todo todo)
    {
        return new Todo
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            DateCreated = todo.DateCreated,
            IsCompleted = todo.IsCompleted
        };
    }
}
