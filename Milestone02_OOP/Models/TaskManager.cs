namespace Milestone02_OOP.Models;

// TaskManager owns the list of tasks and contains all the rules for working with it.
// The list itself is private, so nobody outside can add or remove tasks directly.
// Every change is saved through the JsonTaskStorage object.
public class TaskManager
{
    private readonly JsonTaskStorage _storage;
    private readonly List<TaskItem> _tasks;

    public TaskManager(JsonTaskStorage storage)
    {
        _storage = storage;
        _tasks = storage.Load();
    }

    public IReadOnlyList<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem Add(string title, Priority priority)
    {
        int nextId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        TaskItem task = new TaskItem(nextId, title, priority);
        _tasks.Add(task);
        _storage.Save(_tasks);
        return task;
    }

    public List<TaskItem> Find(string text)
    {
        return _tasks
            .Where(t => t.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public bool MarkAsDone(int id)
    {
        TaskItem? task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return false;
        }

        task.MarkAsDone();
        _storage.Save(_tasks);
        return true;
    }

    public bool Delete(int id)
    {
        TaskItem? task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return false;
        }

        _tasks.Remove(task);
        _storage.Save(_tasks);
        return true;
    }
}
