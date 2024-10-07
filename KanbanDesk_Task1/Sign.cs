namespace KanbanDesk_Task1;

public class Sign
{
    private readonly IDBManager _database;
    
    public Sign(IDBManager database)
    {
        _database = database;
    }
    
    public User SignProccess()
    {
        User currentUser;

        do
        {
            Console.WriteLine("<————————————————————>");
            Console.WriteLine("Enter your login:");

            string login;
            do
            {
                login = Console.ReadLine();

                if (login == string.Empty)
                {
                    Console.WriteLine("Error: Field cannot be empty");
                    Console.WriteLine("Enter your login:");
                }
            }
            while (login == string.Empty);
            
            Console.WriteLine("Enter your password:");

            string password;
            do
            {
                password = Console.ReadLine();

                if (password == string.Empty)
                {
                    Console.WriteLine("Error: Field cannot be empty");
                    Console.WriteLine("Enter your password:");
                }
            }
            while (password == string.Empty);

            currentUser = new User(_database.IsAdmin(login), login, password);
            
        } while (!_database.CheckUserData(currentUser));
        
        Console.WriteLine("<————————————————————>");
        Console.WriteLine("Sign In successfully");
        
        return currentUser;
    }
}