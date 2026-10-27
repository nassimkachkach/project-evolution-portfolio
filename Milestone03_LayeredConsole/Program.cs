using Milestone03_LayeredConsole.Data;
using Milestone03_LayeredConsole.Services;

// Milestone 03 - Layered Console Application (Day 5)
// Today only the data layer and the service layer are ready.
// This small check creates the objects to show that the layers fit together.
// The console menus (presentation layer) are added next.

IUserRepository userRepository = new JsonUserRepository("users.json");
ITaskRepository taskRepository = new JsonTaskRepository("tasks.json");

AuthService authService = new AuthService(userRepository);
TaskService taskService = new TaskService(taskRepository, authService);

Console.WriteLine("Layers created. Logged in: " + authService.IsLoggedIn);
