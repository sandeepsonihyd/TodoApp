using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [MaxLength(50)]
    public string? Category { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsCompleted { get; set; }
}
