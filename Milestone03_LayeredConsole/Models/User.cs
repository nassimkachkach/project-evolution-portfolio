namespace Milestone03_LayeredConsole.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";

    // We never store the real password, only a hash of it.
    public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
