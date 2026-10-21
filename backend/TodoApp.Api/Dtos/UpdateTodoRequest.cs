using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Dtos;

public class UpdateTodoRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public bool? IsCompleted { get; set; }
}
