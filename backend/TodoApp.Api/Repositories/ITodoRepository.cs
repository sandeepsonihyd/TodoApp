using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories;

/// <summary>Defines persistence operations for todos.</summary>
public interface ITodoRepository
{
    /// <summary>Gets all todos as a read-only list.</summary>
    IReadOnlyList<Todo> GetAll();

    /// <summary>Gets todos filtered by completion state and ordered by the requested sort key.</summary>
    /// <param name="completed">Optional completion-state filter.</param>
    /// <param name="sort">Ordering key; <c>created_asc</c> selects oldest first, otherwise newest first.</param>
    IReadOnlyList<Todo> Query(bool? completed, string sort);

    /// <summary>Gets a todo by id, or null when it does not exist.</summary>
    /// <param name="id">The todo identifier.</param>
    Todo? GetById(int id);

    /// <summary>Adds a todo and returns it with its generated identifier.</summary>
    /// <param name="todo">The todo to persist.</param>
    Todo Add(Todo todo);

    /// <summary>Updates an existing todo, returning false when its id is unknown.</summary>
    /// <param name="todo">The updated todo values.</param>
    bool Update(Todo todo);

    /// <summary>Deletes a todo, returning false when its id is unknown.</summary>
    /// <param name="id">The todo identifier.</param>
    bool Delete(int id);
}
