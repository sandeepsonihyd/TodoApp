namespace TodoApp.Api.Models;

/// <summary>Represents a todo stored by the application.</summary>
public class Todo
{
    /// <summary>Gets or sets the database-generated identifier.</summary>
    public int Id { get; set; }

    /// <summary>Gets or sets the todo title.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Gets or sets the optional todo description.</summary>
    public string? Description { get; set; }

    /// <summary>Gets or sets the UTC creation time.</summary>
    public DateTime DateCreated { get; set; }

    /// <summary>Gets or sets whether the todo is complete.</summary>
    public bool IsCompleted { get; set; }
}
