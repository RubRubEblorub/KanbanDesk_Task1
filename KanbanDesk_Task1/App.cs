using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace KanbanDesk_Task1;

public class App
{
    private const string FileName = "DataBase.json";
    private static readonly string _saveFilePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + FileName;
    
    private static void CreateAdmin()
    { 
        User admin = new User(true, "admin", "123");
        
        List<User> users = [admin];
        
        var json = JsonConvert.SerializeObject(users);
        
        File.WriteAllText(_saveFilePath, json);
        
        Console.WriteLine("First program startup –> admin user was created.\nLogin: admin\nPassword: 123");
    }
    
    private static void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddTransient<IDBManager, DataBaseControl>()
            .AddTransient<Sign>()
            .AddTransient<Kanban>();
        
        var serviceProvider = services.BuildServiceProvider();
        Kanban kanban = new Kanban(serviceProvider.GetService<IDBManager>(), serviceProvider.GetService<Sign>());
        
        if (!File.Exists(_saveFilePath))
        {
            File.Create(_saveFilePath).Dispose();
            CreateAdmin();
        }
        
        User user = serviceProvider.GetService<Sign>().SignProccess();
        serviceProvider.GetService<Kanban>().Commands(user);
    }
}