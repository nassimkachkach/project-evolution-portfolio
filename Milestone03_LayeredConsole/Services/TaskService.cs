using Milestone03_LayeredConsole.Data;
using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Services;

// Business rules for tasks. Every method works only with the tasks
// of the user who is currently logged in (owner-specific data).
public class TaskService
{
    private readonly ITaskRepository _tasks;
    private readonly AuthService _auth;

    public TaskService(ITaskRepository tasks, AuthService auth)
    {
        _tasks = tasks;
        _auth = auth;
    }

    public List<TaskItem> GetMyTasks()
    {
        return _tasks.GetByUser(CurrentUserId())
            .OrderBy(t => t.IsDone)
            .ThenByDescending(t => t.Priority)
            .ToList();
    }

    public List<TaskItem> Search(string text)
    {
        return GetMyTasks()
            .Where(t => t.Title.Contains(text, StringComparison.OrdinalIgnoreCase)
                     || t.Description.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public TaskItem Add(string title, string description, Priority priority)
    {
        title = title.Trim();
        description = description.Trim();

        if (title.Length < 3 || title.Length > 100)
        {
            throw new ValidationException("Title must be between 3 and 100 characters.");
        }

        if (description.Length > 500)
        {
            throw new ValidationException("Description can have at most 500 characters.");
        }

        if (!Enum.IsDefined(priority))
        {
            throw new ValidationException("Priority must be Low, Medium or High.");
        }

        TaskItem task = new TaskItem
        {
            UserId = CurrentUserId(),
            Title = title,
            Description = description,
            Priority = priority,
            IsDone = false,
            CreatedAt = DateTime.Now
        };

        _tasks.Add(task);
        return task;
    }

    public void MarkAsDone(int id)
    {
        TaskItem task = GetMyTask(id);
        task.IsDone = true;
        _tasks.Update(task);
    }

    public void Delete(int id)
    {
        TaskItem task = GetMyTask(id);
        _tasks.Delete(task.Id);
    }

    // Returns the task only if it belongs to the current user.
    private TaskItem GetMyTask(int id)
    {
        TaskItem? task = _tasks.GetById(id);

        if (task == null || task.UserId != CurrentUserId())
        {
            throw new ValidationException($"Task #{id} was not found.");
        }

        return task;
    }

    private int CurrentUserId()
    {
        if (_auth.CurrentUser == null)
        {
            throw new ValidationException("You must be logged in.");
        }

        return _auth.CurrentUser.Id;
    }
}
