namespace TodoApp.Api.Dtos;

/// <summary>Represents a todo returned by the API.</summary>
public class TodoResponse
{
    /// <summary>Gets or sets the unique todo identifier.</summary>
    public int Id { get; set; }

    /// <summary>Gets or sets the validated todo title.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Gets or sets the optional todo description.</summary>
    public string? Description { get; set; }

    /// <summary>Gets or sets the UTC time when the todo was created.</summary>
    public DateTime DateCreated { get; set; }

    /// <summary>Gets or sets whether the todo is complete.</summary>
    public bool IsCompleted { get; set; }
}
