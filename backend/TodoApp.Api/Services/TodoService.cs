using TodoApp.Api.Dtos;
using TodoApp.Api.Models;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<TodoResponse> GetAll()
    {
        return _repository.GetAll()
            .OrderByDescending(t => t.DateCreated)
            .ThenByDescending(t => t.Id)
            .Select(ToResponse)
            .ToList();
    }

    public IReadOnlyList<TodoResponse> GetCompleted()
    {
        return _repository.GetAll()
            .Where(t => t.IsCompleted)
            .OrderByDescending(t => t.DateCreated)
            .ThenByDescending(t => t.Id)
            .Select(ToResponse)
            .ToList();
    }

    public TodoResponse? GetById(int id)
    {
        var todo = _repository.GetById(id);
        return todo == null ? null : ToResponse(todo);
    }

    public TodoResponse Create(CreateTodoRequest request)
    {
        var title = ValidateAndTrimTitle(request.Title);
        ValidateDescription(request.Description);

        var todo = new Todo
        {
            Title = title,
            Description = request.Description,
            DateCreated = DateTime.UtcNow,
            IsCompleted = false
        };

        var saved = _repository.Add(todo);
        return ToResponse(saved);
    }

    public TodoResponse? Update(int id, UpdateTodoRequest request)
    {
        var title = ValidateAndTrimTitle(request.Title);
        ValidateDescription(request.Description);

        var existing = _repository.GetById(id);
        if (existing == null)
        {
            return null;
        }

        existing.Title = title;
        existing.Description = request.Description;
        existing.IsCompleted = request.IsCompleted ?? false;

        if (!_repository.Update(existing))
        {
            return null;
        }

        return ToResponse(existing);
    }

    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    private static string ValidateAndTrimTitle(string? title)
    {
        var trimmed = (title ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            throw new ArgumentException("Title is required.");
        }
        if (trimmed.Length > 200)
        {
            throw new ArgumentException("Title must be 200 characters or fewer.");
        }
        return trimmed;
    }

    private static void ValidateDescription(string? description)
    {
        if (description != null && description.Length > 1000)
        {
            throw new ArgumentException("Description must be 1000 characters or fewer.");
        }
    }

    private static TodoResponse ToResponse(Todo todo)
    {
        return new TodoResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            DateCreated = todo.DateCreated,
            IsCompleted = todo.IsCompleted
        };
    }
    // Count the todos: total, completed and pending 
    public TodoStatsResponse GetStats()
    {
        var todos = _repository.GetAll();
        var total = todos.Count;
        var completed = todos.Count(t => t.IsCompleted);
        var pending = total - completed;
        return new TodoStatsResponse
        {
            Total = total,
            Completed = completed,
            Pending = pending
        };
    }
}
