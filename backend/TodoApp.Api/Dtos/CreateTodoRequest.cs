using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Dtos;

/// <summary>Contains the fields accepted when creating a todo.</summary>
public class CreateTodoRequest
{
    /// <summary>Gets or sets the required todo title, limited to 200 characters.</summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>Gets or sets the optional description, limited to 1000 characters.</summary>
    [MaxLength(1000)]
    public string? Description { get; set; }
}
