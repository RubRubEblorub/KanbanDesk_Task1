using KanbanDesk_Task1;

public interface IDBManager
{
    public void RegisterUser();
    
    public void SaveUser(User user);
    
    public bool CheckUserData(User user);
    
    public bool CheckUserLogin(string login);
    
    public bool IsAdmin(string login);
    
    public bool DeleteUser(string login);
    
    public bool ConsoleAnswer();
    
    public User FindUserByLogin(string login);
    
    public List<User> FetchUsersFromJson();
}