using Milestone02_OOP.Models;

// Milestone 02 - OOP and Persistence
// The tasks are now objects of the TaskItem class instead of plain strings.
// TaskManager holds the rules and JsonTaskStorage saves the tasks to a file.

TaskManager manager = new TaskManager(new JsonTaskStorage("tasks.json"));
bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("===== TASK TRACKER (OOP) =====");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("3. Find task");
    Console.WriteLine("4. Mark task as done");
    Console.WriteLine("5. Delete task");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    switch (Console.ReadLine())
    {
        case "1":
            AddTask();
            break;
        case "2":
            PrintTasks(manager.GetAll());
            break;
        case "3":
            Console.Write("Search text: ");
            PrintTasks(manager.Find(Console.ReadLine() ?? ""));
            break;
        case "4":
            int doneId = ReadInt("Task id to mark as done: ");
            Console.WriteLine(manager.MarkAsDone(doneId) ? "Task completed." : "Task not found.");
            break;
        case "5":
            int deleteId = ReadInt("Task id to delete: ");
            Console.WriteLine(manager.Delete(deleteId) ? "Task deleted." : "Task not found.");
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

void AddTask()
{
    Console.Write("Task title: ");
    string title = Console.ReadLine() ?? "";
    Priority priority = (Priority)ReadInt("Priority (1 = Low, 2 = Medium, 3 = High): ", 1, 3);

    try
    {
        TaskItem task = manager.Add(title, priority);
        Console.WriteLine($"Added: {task}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void PrintTasks(IEnumerable<TaskItem> tasks)
{
    bool any = false;
    foreach (TaskItem task in tasks)
    {
        Console.WriteLine(task);
        any = true;
    }

    if (!any)
    {
        Console.WriteLine("No tasks to show.");
    }
}

int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
{
    while (true)
    {
        Console.Write(message);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
        {
            return value;
        }

        Console.WriteLine("Please enter a valid number.");
    }
}
