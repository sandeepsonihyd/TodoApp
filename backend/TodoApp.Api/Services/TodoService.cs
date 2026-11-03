using TodoApp.Api.Dtos;
using TodoApp.Api.Models;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Services;

/// <summary>Applies todo business rules and maps repository entities to API responses.</summary>
public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    /// <summary>Creates the service with its repository dependency.</summary>
    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    /// <summary>Returns all todos ordered newest first, breaking date ties by descending id.</summary>
    public IReadOnlyList<TodoResponse> GetAll()
    {
        return _repository.GetAll()
            .OrderByDescending(t => t.DateCreated)
            .ThenByDescending(t => t.Id)
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>Returns completed todos ordered newest first.</summary>
    public IReadOnlyList<TodoResponse> GetCompleted()
    {
        return _repository.GetAll()
            .Where(t => t.IsCompleted)
            .OrderByDescending(t => t.DateCreated)
            .ThenByDescending(t => t.Id)
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>Returns a mapped todo, or null when the id does not exist.</summary>
    public TodoResponse? GetById(int id)
    {
        var todo = _repository.GetById(id);
        return todo == null ? null : ToResponse(todo);
    }

    /// <summary>Validates a request, assigns creation values, and persists the new todo.</summary>
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

    /// <summary>Validates and applies request fields while preserving the stored id and creation date.</summary>
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

    /// <summary>Deletes a todo and reports whether it was found.</summary>
    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    /// <summary>Trims a title and enforces the non-empty, 200-character limit.</summary>
    /// <param name="title">The untrusted title to validate.</param>
    /// <returns>The trimmed title.</returns>
    /// <exception cref="ArgumentException">The title is blank or exceeds the length limit.</exception>
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

    /// <summary>Enforces the 1000-character description limit.</summary>
    /// <param name="description">The optional description to validate.</param>
    /// <exception cref="ArgumentException">The description exceeds the length limit.</exception>
    private static void ValidateDescription(string? description)
    {
        if (description != null && description.Length > 1000)
        {
            throw new ArgumentException("Description must be 1000 characters or fewer.");
        }
    }

    /// <summary>Copies the public todo fields into an API response DTO.</summary>
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

    /// <summary>Gets todos from the repository using the requested filter and ordering.</summary>
    public IReadOnlyList<TodoResponse> Query(bool? completed, string sort)
    {
        return _repository.Query(completed, sort).Select(ToResponse).ToList();
    }

    /// <summary>Deletes each completed todo and returns the number successfully removed.</summary>
    public int ClearCompleted()
    {
        // Materialize ids first because each deletion changes the underlying collection.
        var completedIds = _repository.GetAll()
            .Where(t => t.IsCompleted)
            .Select(t => t.Id)
            .ToList();

        var deleted = 0;
        foreach (var id in completedIds)
        {
            if (_repository.Delete(id))
            {
                deleted++;
            }
        }
        return deleted;
    }

    /// <summary>Calculates total, completed, and pending counts from the current todos.</summary>
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
