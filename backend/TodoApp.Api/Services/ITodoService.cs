using TodoApp.Api.Dtos;

namespace TodoApp.Api.Services;

/// <summary>Defines todo use cases and application-level validation.</summary>
public interface ITodoService
{
    /// <summary>Gets all todos ordered newest first.</summary>
    IReadOnlyList<TodoResponse> GetAll();

    /// <summary>Gets completed todos ordered newest first.</summary>
    IReadOnlyList<TodoResponse> GetCompleted();

    /// <summary>Gets todos using the optional completion filter and sort key.</summary>
    /// <param name="completed">Optional completion-state filter.</param>
    /// <param name="sort">Sort key; <c>created_asc</c> selects oldest first.</param>
    IReadOnlyList<TodoResponse> Query(bool? completed, string sort);

    /// <summary>Deletes all completed todos and returns the number removed.</summary>
    int ClearCompleted();

    /// <summary>Gets a todo by id, or null when it does not exist.</summary>
    /// <param name="id">The todo identifier.</param>
    TodoResponse? GetById(int id);

    /// <summary>Validates and creates a todo from the supplied request.</summary>
    /// <param name="request">The title and optional description.</param>
    TodoResponse Create(CreateTodoRequest request);

    /// <summary>Validates and updates a todo, or returns null when its id is unknown.</summary>
    /// <param name="id">The todo identifier.</param>
    /// <param name="request">The replacement todo fields.</param>
    TodoResponse? Update(int id, UpdateTodoRequest request);

    /// <summary>Deletes a todo and reports whether it existed.</summary>
    /// <param name="id">The todo identifier.</param>
    bool Delete(int id);

    /// <summary>Gets total, completed, and pending todo counts.</summary>
    TodoStatsResponse GetStats();
}
