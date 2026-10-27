using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Data;

// An interface describes WHAT the data layer can do, not HOW.
// Later (Milestone 04) we write a database version of the same interface.
public interface ITaskRepository
{
    List<TaskItem> GetByUser(int userId);
    TaskItem? GetById(int id);
    void Add(TaskItem task);
    void Update(TaskItem task);
    void Delete(int id);
}
