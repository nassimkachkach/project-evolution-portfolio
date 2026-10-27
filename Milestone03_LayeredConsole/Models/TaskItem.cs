namespace Milestone03_LayeredConsole.Models;

// In the layered version the model is a simple data class.
// The rules (validation, who owns what) now live in the service layer.
public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }          // the owner of this task
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public Priority Priority { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }

    public override string ToString()
    {
        string status = IsDone ? "[x]" : "[ ]";
        return $"{status} #{Id} {Title} ({Priority}) - created {CreatedAt:yyyy-MM-dd}";
    }
}
