using Milestone03_LayeredConsole.Data;
using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Services;

// Handles register, login, logout and remembers who the current user is.
public class AuthService
{
    private readonly IUserRepository _users;

    public AuthService(IUserRepository users)
    {
        _users = users;
    }

    public User? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;

    public User Register(string username, string password)
    {
        username = username.Trim();

        if (username.Length < 3 || username.Length > 20 || !username.All(char.IsLetterOrDigit))
        {
            throw new ValidationException("Username must be 3-20 letters or digits.");
        }

        if (password.Length < 6)
        {
            throw new ValidationException("Password must be at least 6 characters.");
        }

        if (_users.GetByUsername(username) != null)
        {
            throw new ValidationException("This username is already taken.");
        }

        User user = new User
        {
            Username = username,
            PasswordHash = PasswordHasher.Hash(password),
            CreatedAt = DateTime.Now
        };

        _users.Add(user);
        return user;
    }

    public User Login(string username, string password)
    {
        User? user = _users.GetByUsername(username.Trim());

        if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            throw new ValidationException("Wrong username or password.");
        }

        CurrentUser = user;
        return user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
