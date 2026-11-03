using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Dtos;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers;

/// <summary>Maps todo HTTP requests to service operations and HTTP responses.</summary>
[ApiController]
[Route("todos")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    /// <summary>Creates the controller with its todo service dependency.</summary>
    public TodosController(ITodoService service)
    {
        _service = service;
    }

    /// <summary>Returns todos matching the optional completion filter and sort order.</summary>
    /// <param name="completed">When supplied, limits results to the requested completion state.</param>
    /// <param name="sort">Sort key; defaults to newest-created first.</param>
    [HttpGet]
    public ActionResult<IReadOnlyList<TodoResponse>> GetAll([FromQuery] bool? completed, [FromQuery] string? sort)
    {
        return Ok(_service.Query(completed, sort ?? "created_desc"));
    }

    /// <summary>Deletes all completed todos and returns the number removed.</summary>
    [HttpDelete("completed")]
    public ActionResult<int> ClearCompleted()
    {
        return Ok(_service.ClearCompleted());
    }

    /// <summary>Returns one todo, or a not-found problem when its id does not exist.</summary>
    /// <param name="id">The todo identifier.</param>
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

    /// <summary>Creates a todo and returns its resource location.</summary>
    /// <param name="request">The title and optional description for the new todo.</param>
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

    /// <summary>Updates a todo, returning not found or a validation problem when appropriate.</summary>
    /// <param name="id">The todo identifier.</param>
    /// <param name="request">The replacement todo fields.</param>
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

    /// <summary>Deletes a todo, or returns not found when its id does not exist.</summary>
    /// <param name="id">The todo identifier.</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id))
        {
            return NotFound(Problem404(id));
        }
        return NoContent();
    }

    /// <summary>Builds the standard problem response for an unknown todo id.</summary>
    /// <param name="id">The identifier that could not be found.</param>
    /// <returns>A 404 problem with a human-readable detail.</returns>
    private static ProblemDetails Problem404(int id)
    {
        return new ProblemDetails { Status = 404, Title = "Not Found", Detail = $"Todo {id} was not found." };
    }

    /// <summary>Returns the total, completed, and pending todo counts.</summary>
    [HttpGet("stats")]
    public ActionResult<TodoStatsResponse> GetStats()
    {
        return Ok(_service.GetStats());
    }
}
