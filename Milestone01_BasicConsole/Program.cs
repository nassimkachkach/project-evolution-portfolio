// Milestone 01 - Basic Console Application (Day 1)
// A very simple to-do list. Each task is just a piece of text (a string)
// and all tasks are kept in a List while the program is running.

List<string> tasks = new List<string>();
bool running = true;

while (running)
{
    ShowMenu();
    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ListTasks();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Unknown option, please try again.");
            break;
    }
}

Console.WriteLine("Goodbye!");

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("===== TASK TRACKER =====");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("0. Exit");
}

void AddTask()
{
    Console.Write("Task title: ");
    string title = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("The title cannot be empty.");
        return;
    }

    tasks.Add(title.Trim());
    Console.WriteLine("Task added.");
}

void ListTasks()
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("You have no tasks yet.");
        return;
    }

    for (int i = 0; i < tasks.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {tasks[i]}");
    }
}
