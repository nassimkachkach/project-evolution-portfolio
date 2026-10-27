namespace Milestone03_LayeredConsole.Services;

// Thrown by the service layer when the user's input breaks a rule.
// The presentation layer catches it and shows the message.
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
