using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Todo> GetAll()
    {
        return _context.Todos.AsNoTracking().ToList();
    }

    public Todo? GetById(int id)
    {
        return _context.Todos.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    public Todo Add(Todo todo)
    {
        todo.Id = 0;
        _context.Todos.Add(todo);
        _context.SaveChanges();
        _context.Entry(todo).State = EntityState.Detached;
        return todo;
    }

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
