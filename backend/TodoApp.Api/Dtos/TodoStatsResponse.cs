namespace TodoApp.Api.Dtos;

/// <summary>Contains aggregate counts for the current todo collection.</summary>
public class TodoStatsResponse
{
    /// <summary>Gets or sets the total number of todos.</summary>
    public int Total { get; set; }

    /// <summary>Gets or sets the number of completed todos.</summary>
    public int Completed { get; set; }

    /// <summary>Gets or sets the number of todos that are not completed.</summary>
    public int Pending { get; set; }
}


