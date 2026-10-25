namespace Milestone02_OOP.Models;

// TaskManager owns the list of tasks and contains all the rules for working with it.
// The list itself is private, so nobody outside can add or remove tasks directly.
public class TaskManager
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>();

    public IReadOnlyList<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem Add(string title, Priority priority)
    {
        int nextId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        TaskItem task = new TaskItem(nextId, title, priority);
        _tasks.Add(task);
        return task;
    }

    public List<TaskItem> Find(string text)
    {
        return _tasks
            .Where(t => t.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public bool Delete(int id)
    {
        TaskItem? task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return false;
        }

        _tasks.Remove(task);
        return true;
    }
}
