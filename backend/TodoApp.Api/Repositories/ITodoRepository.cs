using TodoApp.Api.Models;

namespace TodoApp.Api.Repositories;

public interface ITodoRepository
{
    IReadOnlyList<Todo> GetAll();
    IReadOnlyList<Todo> Query(bool? completed, string sort);
    Todo? GetById(int id);
    Todo Add(Todo todo);
    bool Update(Todo todo);
    bool Delete(int id);
}
