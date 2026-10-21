using TodoApp.Api.Dtos;

namespace TodoApp.Api.Services;

public interface ITodoService
{
    IReadOnlyList<TodoResponse> GetAll();
    IReadOnlyList<TodoResponse> GetCompleted();
    IReadOnlyList<TodoResponse> Query(bool? completed, string? category, string sort);
    int ClearCompleted();
    TodoResponse? GetById(int id);
    TodoResponse Create(CreateTodoRequest request);
    TodoResponse? Update(int id, UpdateTodoRequest request);
    bool Delete(int id);
    TodoStatsResponse GetStats();
}
