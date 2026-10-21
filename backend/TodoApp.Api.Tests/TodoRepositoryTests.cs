using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Models;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Tests;

public class TodoRepositoryTests
{
    [Fact]
    public void Query_WithCategory_ReturnsOnlyExactMatches()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new TodoDbContext(options);
        context.Database.EnsureCreated();
        context.Todos.AddRange(
            new Todo { Title = "Work task", Category = "Work" },
            new Todo { Title = "Lowercase task", Category = "work" },
            new Todo { Title = "Related task", Category = "Workplace" },
            new Todo { Title = "Uncategorized task" });
        context.SaveChanges();

        var repository = new TodoRepository(context);

        var result = repository.Query(null, "Work", "created_desc");

        Assert.Equal(new[] { "Work task" }, result.Select(todo => todo.Title));
    }
}
