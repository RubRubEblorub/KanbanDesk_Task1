using KanbanDesk_Task1;
using Microsoft.Extensions.DependencyInjection;

public class App
{
    private static void Main(string[] args)
    {
        string _saveFilePath = Environment.CurrentDirectory;
        string _fileName = "DataBase.json";

        if (!File.Exists(_saveFilePath))
        {
            File.Create(_saveFilePath).Dispose();
        }
        
        var services = new ServiceCollection()
            .AddTransient<IDBManager, DataBaseControl>()
            .AddTransient<Sign>()
            .AddTransient<Kanban>();
        
        var serviceProvider = services.BuildServiceProvider();
        Kanban kanban = new Kanban(serviceProvider.GetService<IDBManager>(), serviceProvider.GetService<Sign>());
        
        User user = serviceProvider.GetService<Sign>().SignProccess();
        serviceProvider.GetService<Kanban>().Commands(user);
    }
}