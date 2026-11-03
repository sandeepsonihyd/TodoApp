using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;

namespace TodoApp.Api.Data;

/// <summary>Provides Entity Framework Core access to persisted todo data.</summary>
public class TodoDbContext : DbContext
{
    /// <summary>Initializes the context with the configured database options.</summary>
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    /// <summary>Gets the database set containing todos.</summary>
    public DbSet<Todo> Todos => Set<Todo>();
}
