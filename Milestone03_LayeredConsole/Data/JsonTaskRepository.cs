using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Data;

public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private readonly List<TaskItem> _tasks;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
        _tasks = JsonFile.Load<TaskItem>(filePath);
    }

    public List<TaskItem> GetByUser(int userId)
    {
        return _tasks.Where(t => t.UserId == userId).ToList();
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public void Add(TaskItem task)
    {
        task.Id = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        _tasks.Add(task);
        JsonFile.Save(_filePath, _tasks);
    }

    public void Update(TaskItem task)
    {
        // The object in the list is the same object we changed, so we only need to save.
        JsonFile.Save(_filePath, _tasks);
    }

    public void Delete(int id)
    {
        _tasks.RemoveAll(t => t.Id == id);
        JsonFile.Save(_filePath, _tasks);
    }
}
