using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories;

/// <summary>Persists todos with Entity Framework Core.</summary>
public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    /// <summary>Creates the repository with its database context.</summary>
    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    /// <summary>Loads all todos without tracking them for changes.</summary>
    public IReadOnlyList<Todo> GetAll()
    {
        return _context.Todos.AsNoTracking().ToList();
    }

    /// <summary>Loads todos with an optional completion filter and deterministic date/id ordering.</summary>
    public IReadOnlyList<Todo> Query(bool? completed, string sort)
    {
        IQueryable<Todo> query = _context.Todos.AsNoTracking();

        if (completed.HasValue)
        {
            query = query.Where(t => t.IsCompleted == completed.Value);
        }

        query = sort == "created_asc"
            ? query.OrderBy(t => t.DateCreated).ThenBy(t => t.Id)
            : query.OrderByDescending(t => t.DateCreated).ThenByDescending(t => t.Id);

        return query.ToList();
    }

    /// <summary>Loads one todo without tracking it, or returns null when it is absent.</summary>
    public Todo? GetById(int id)
    {
        return _context.Todos.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    /// <summary>Persists a new todo and returns it with its database-generated id.</summary>
    public Todo Add(Todo todo)
    {
        // Let SQLite generate the identifier, even if the caller supplied a value.
        todo.Id = 0;
        _context.Todos.Add(todo);
        _context.SaveChanges();
        _context.Entry(todo).State = EntityState.Detached;
        return todo;
    }

    /// <summary>Copies the supplied values to a stored todo and saves the changes.</summary>
    public bool Update(Todo todo)
    {
        var existing = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Title = todo.Title;
        existing.Description = todo.Description;
        existing.DateCreated = todo.DateCreated;
        existing.IsCompleted = todo.IsCompleted;
        _context.SaveChanges();
        return true;
    }

    /// <summary>Removes the todo with the given id when it exists.</summary>
    public bool Delete(int id)
    {
        var existing = _context.Todos.FirstOrDefault(t => t.Id == id);
        if (existing == null)
        {
            return false;
        }

        _context.Todos.Remove(existing);
        _context.SaveChanges();
        return true;
    }
}
