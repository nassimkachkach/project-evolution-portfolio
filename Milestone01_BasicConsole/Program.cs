using System.Text.Json;

// Milestone 01 - Basic Console Application
// A very simple to-do list. Each task is just a piece of text (a string)
// and all tasks are kept in a List. The list is saved to a JSON file,
// so the tasks are still there the next time the program starts.

const string FileName = "tasks.json";

List<string> tasks = LoadTasks();
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
        case "3":
            FindTask();
            break;
        case "4":
            DeleteTask();
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
    Console.WriteLine("3. Find task");
    Console.WriteLine("4. Delete task");
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
    SaveTasks();
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

void FindTask()
{
    Console.Write("Search text: ");
    string text = Console.ReadLine() ?? "";
    int found = 0;

    for (int i = 0; i < tasks.Count; i++)
    {
        if (tasks[i].Contains(text, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
            found++;
        }
    }

    if (found == 0)
    {
        Console.WriteLine("No matching tasks.");
    }
}

void DeleteTask()
{
    ListTasks();
    Console.Write("Number of the task to delete: ");

    if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > tasks.Count)
    {
        Console.WriteLine("Invalid number.");
        return;
    }

    string removed = tasks[number - 1];
    tasks.RemoveAt(number - 1);
    SaveTasks();
    Console.WriteLine($"Deleted: {removed}");
}

List<string> LoadTasks()
{
    if (!File.Exists(FileName))
    {
        return new List<string>();
    }

    string json = File.ReadAllText(FileName);
    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
}

void SaveTasks()
{
    string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(FileName, json);
}
