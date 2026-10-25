using System.Text.Json.Serialization;

namespace Milestone02_OOP.Models;

// A TaskItem is one task in the to-do list.
// Encapsulation: the properties have "private set", so code outside this class
// can read them but can only change them through the methods below.
public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public Priority Priority { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Constructor used when the user creates a new task.
    public TaskItem(int id, string title, Priority priority)
        : this(id, title, priority, false, DateTime.Now)
    {
    }

    // Constructor used when a task is loaded back from the JSON file.
    [JsonConstructor]
    public TaskItem(int id, string title, Priority priority, bool isDone, DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("The title cannot be empty.");
        }

        Id = id;
        Title = title.Trim();
        Priority = priority;
        IsDone = isDone;
        CreatedAt = createdAt;
    }

    public void MarkAsDone()
    {
        IsDone = true;
    }

    public override string ToString()
    {
        string status = IsDone ? "[x]" : "[ ]";
        return $"{status} #{Id} {Title} ({Priority}) - created {CreatedAt:yyyy-MM-dd}";
    }
}
