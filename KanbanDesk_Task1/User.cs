namespace KanbanDesk_Task1;

public class User
{
    public bool IsAdmin { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public List<Task> Tasks { get; private set; }

    public User(bool isAdmin, string login, string password)
    {
        IsAdmin = isAdmin;
        Login = login;
        Password = password;
        Tasks = new List<Task>();
    }

    public void SetAdmin(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void PrintTasks()
    {
        foreach (var task in Tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    public int GetLastTaskId()
    {
        return Tasks.Count == 0 ? 0 : Tasks.Last().Id;
    }

    public override string ToString()
    {
        return $"IsAdmin: {IsAdmin}, Login: {Login}, Password: {Password}\n";
    }
}