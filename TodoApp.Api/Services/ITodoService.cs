using TodoApp.Api.Dtos;

namespace TodoApp.Api.Services;

public interface ITodoService
{
    IReadOnlyList<TodoResponse> GetAll();
    TodoResponse? GetById(int id);
    TodoResponse Create(CreateTodoRequest request);
    TodoResponse? Update(int id, UpdateTodoRequest request);
    bool Delete(int id);
}
