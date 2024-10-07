namespace KanbanDesk_Task1;

public class Task
{
    private readonly Dictionary<int, string> _statusDictionary = new()
    {
        { 0, "To Do" },
        { 1, "In Progress" },
        { 2, "Done" },
    };
    
    public int Id { get; private set; } 
    
    public int Status { get; private set; }
    public string Content { get; private set; }

    public Task(int id, int status, string content)
    {
        Id = id;
        Status = status;
        Content = content;
    }

    public void ChangeStatus(int status)
    {
        Status = status;
    }

    private string GetStatus()
    {
        return _statusDictionary[Status];
    }
    
    public override string ToString()
    {
        return $"Id: {Id}\n Status: {GetStatus()}\n Content: {Content}";
    }
}