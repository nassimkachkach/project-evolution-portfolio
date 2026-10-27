using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Data;

public class JsonUserRepository : IUserRepository
{
    private readonly string _filePath;
    private readonly List<User> _users;

    public JsonUserRepository(string filePath)
    {
        _filePath = filePath;
        _users = JsonFile.Load<User>(filePath);
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public void Add(User user)
    {
        user.Id = _users.Count == 0 ? 1 : _users.Max(u => u.Id) + 1;
        _users.Add(user);
        JsonFile.Save(_filePath, _users);
    }
}
