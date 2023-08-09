using ConsoleAppWithSqliteDb.Data.Db;
using ConsoleAppWithSqliteDb.Data.Entities.Models;

await using var context = new AppDbContext();
await context.Database.EnsureCreatedAsync();

ConsoleKey keyPressed;

while (true)
{
    Console.WriteLine("\n----------Menu---------");
    Console.WriteLine("1. Get all tasks");
    Console.WriteLine("2. Create new task");
    Console.WriteLine("Anything else to quit");
    Console.WriteLine("-----------------------");

    keyPressed = Console.ReadKey().Key;

    switch (keyPressed)
    {
        case ConsoleKey.D1:
            Console.WriteLine("\nList of all tasks:");
            var tasksList = context.TodoItems;

            var taskCounter = 1;
            foreach (var task in tasksList)
            {
                var isTaskDoneString = task.IsTaskDone ? "Done" : "In progress";
                Console.WriteLine($"{taskCounter}. {task.TaskName} - {task.Description}\n" +
                                  $"{isTaskDoneString}, priority: {task.TaskPriority}\n" +
                                  $"Planned finish date: {task.PlannedFinishDate.ToString("f")!}\n");
                taskCounter++;
            }
            break;

        case ConsoleKey.D2:
            Console.WriteLine("\nAdd new task:");

            var newTask = new TodoItemModel();

            Console.Write("Name: ");
            newTask.TaskName = Console.ReadLine();

            Console.Write("Description: ");
            newTask.Description = Console.ReadLine();

            Console.WriteLine("Select priority:");
            var priorityCount = 1;

            foreach (var priority in Enum.GetValues<Priority>())
            {
                Console.WriteLine($"{priorityCount}. {priority}");
                priorityCount++;
            }
            
            Console.Write("> ");

            try
            {
                var optionSelected = Int32.Parse(Console.ReadLine()!);

                if (optionSelected > priorityCount || optionSelected <= 0)
                    throw new Exception("Option selected is out of menu");

                newTask.TaskPriority = Enum.GetValues<Priority>()[optionSelected-1];

                Console.WriteLine($"\nHow many hours you want to set as final goal to do?: ");
                Console.Write("> ");

                var hoursInserted = Int32.Parse(Console.ReadLine()!);

                if (hoursInserted <= 0)
                    throw new Exception("Hours number cant be lower than 1");

                newTask.PlannedFinishDate = DateTime.Now.AddHours(hoursInserted);

                await context.TodoItems.AddAsync(newTask);
                await context.SaveChangesAsync();

                Console.WriteLine("Successfully added new task");
            }
            catch (Exception e)
            {
#if DEBUG
    Console.WriteLine(e);
#else
    Console.WriteLine(e.Message);
#endif
                Console.WriteLine();
            }
            break;

        default:
            goto quit;
    }
}

quit:
Console.WriteLine("Thank you for using this app. See you later!");
Console.WriteLine("Click anything to continue...");
Console.ReadKey();