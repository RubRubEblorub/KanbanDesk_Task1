namespace KanbanDesk_Task1;

public class Kanban
{
    private readonly Sign _signServiceProvider;
    private readonly IDBManager _managerServiceProvider;
    
    public Kanban(IDBManager managerServices, Sign signServices)
    {
        _managerServiceProvider = managerServices;
        _signServiceProvider = signServices;
    }
    
    public void Commands(User _user)
    {
        User activeUser = _user;

        if (activeUser.IsAdmin)
        {
            Console.WriteLine("<————————————————————>");
            Console.WriteLine("Commands:\n 1 - Register new employee\n 2 - Delete employee\n" +
                              " 3 - View all employees\n 9 - Switch user\n 0 - Exit");

            switch (Console.ReadLine())
            {
                case "1":
                {
                    _managerServiceProvider.RegisterUser();
                    break;
                }
                case "2":
                {
                    Console.WriteLine("<————————————————————>");
                    Console.WriteLine("Enter employee`s login to delete:");

                    string login = Console.ReadLine();

                    if (_managerServiceProvider.DeleteUser(login))
                    {
                        Console.WriteLine("<————————————————————>");
                        Console.WriteLine("User deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("<————————————————————>");
                        Console.WriteLine("User not found");
                    }

                    break;
                }
                case "3":
                {
                    List<User> userList = _managerServiceProvider.FetchUsersFromJson();

                    if (userList.Count > 0)
                    {
                        int index = 1;

                        Console.WriteLine("<————————————————————>");

                        foreach (var user in userList)
                        {
                            Console.WriteLine($"{index}.");
                            Console.WriteLine(user);

                            index++;
                        }

                        index = 1;
                    }

                    Console.WriteLine("<————————————————————>");
                    Console.WriteLine("Select an employee by login: ");

                    string selectedEmployee;

                    do
                    {
                        selectedEmployee = Console.ReadLine();
                        if (!_managerServiceProvider.CheckUserLogin(selectedEmployee))
                        {
                            Console.WriteLine("User not found!\nPlease try again:");
                        }
                    }
                    while (!_managerServiceProvider.CheckUserLogin(selectedEmployee));

                    User userToCheck = _managerServiceProvider.FindUserByLogin(selectedEmployee);

                    Console.WriteLine("<————————————————————>");
                    Console.WriteLine($"Current {userToCheck.Login} tasks` list:");

                    userToCheck.PrintTasks();
                    
                    Console.WriteLine("<————————————————————>");
                    Console.WriteLine("Add new task (y)\nTo main menu (n)");
                    bool answer = _managerServiceProvider.ConsoleAnswer();

                    if (answer)
                    {
                        int taskId = userToCheck.GetLastTaskId() + 1;

                        Console.WriteLine("Enter task description:");
                        string taskText = Console.ReadLine();

                        Task addTask = new Task(taskId, 0, taskText);

                        userToCheck.AddTask(addTask);
                        
                        _managerServiceProvider.DeleteUser(userToCheck.Login);
                        _managerServiceProvider.SaveUser(userToCheck);
                        
                        Console.WriteLine("<————————————————————>");
                        Console.WriteLine("Task successfully added");
                    }
                    break;
                }
                case "9":
                {
                    activeUser = _signServiceProvider.SignProccess();
                    Commands(activeUser);
                    break;
                }
                case "0":
                {
                    Console.WriteLine(" <————— Exiting... —————>");
                    Environment.Exit(0);
                    break;
                }
                default:
                {
                    Console.WriteLine("Please enter a valid command");
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("<————————————————————>");
            Console.WriteLine("Commands:\n 1 - Tasks` list\n 9 - Switch user\n 0 - Exit");
            
            switch (Console.ReadLine())
            {
                case "1":
                {
                    Console.WriteLine("<————————————————————>");
                    Console.WriteLine("Select task: ");
                    
                    User userFetch = _managerServiceProvider.FindUserByLogin(activeUser.Login);
                    
                    userFetch.PrintTasks();
                    
                    string selectedTask = Console.ReadLine();

                    if (int.TryParse(selectedTask, out int taskId))
                    {
                        Task task = userFetch.Tasks.Find(u => u.Id == taskId);

                        if (task != null)
                        {
                            Console.WriteLine("<————————————————————>");
                            Console.WriteLine("Change status:\n Note:\n 0 - To Do\n 1 - In Progress\n 2 - Done\n:");

                            string selectedStatus = Console.ReadLine();
                        
                            if (int.TryParse(selectedStatus, out int status))
                            {
                                if (Math.Abs(status) < 2)
                                {
                                    task.ChangeStatus(status);
                                    _managerServiceProvider.DeleteUser(userFetch.Login);
                                    _managerServiceProvider.SaveUser(userFetch);
                                    
                                    Console.WriteLine("<————————————————————>");
                                    Console.WriteLine("Task status successfully changed.");
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid status id");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter a number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid task id");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number");
                    }
                    
                    break;
                }
                case "9":
                {
                    activeUser = _signServiceProvider.SignProccess();
                    Commands(activeUser);
                    break;
                }
                case "0":
                {
                    Console.WriteLine(" <————— Exiting... —————>");
                    Environment.Exit(0);
                    break;
                }
                default:
                {
                    Console.WriteLine("Please enter a valid command");
                    break;
                }
            }
        }
        Commands(activeUser);
    }
}