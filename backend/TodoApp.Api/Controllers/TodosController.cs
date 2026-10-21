using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Dtos;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<TodoResponse>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<TodoResponse> GetById(int id)
    {
        var todo = _service.GetById(id);
        if (todo == null)
        {
            return NotFound(Problem404(id));
        }
        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoResponse> Create(CreateTodoRequest request)
    {
        try
        {
            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails { Status = 400, Title = "Bad Request", Detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public ActionResult<TodoResponse> Update(int id, UpdateTodoRequest request)
    {
        try
        {
            var updated = _service.Update(id, request);
            if (updated == null)
            {
                return NotFound(Problem404(id));
            }
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails { Status = 400, Title = "Bad Request", Detail = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id))
        {
            return NotFound(Problem404(id));
        }
        return NoContent();
    }

    private static ProblemDetails Problem404(int id)
    {
        return new ProblemDetails { Status = 404, Title = "Not Found", Detail = $"Todo {id} was not found." };
    }

    // GET /todos/stats - return total count, completed count, and pending count
    // Delegate to _service.GetStats(), which returns a TodoStatsResponse
    [HttpGet("stats")]
    public ActionResult<TodoStatsResponse> GetStats()
    {
        return Ok(_service.GetStats());
    }
}
