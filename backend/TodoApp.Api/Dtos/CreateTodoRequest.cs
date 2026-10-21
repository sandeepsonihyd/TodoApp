using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Dtos;

public class CreateTodoRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }
}
