using Newtonsoft.Json;

namespace KanbanDesk_Task1;

public class DataBaseControl : IDBManager
{
    public void RegisterUser()
    {
        string login;

        Console.WriteLine("<————————————————————>");
        Console.WriteLine("Enter employee`s login:");
        
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
        
        if (!CheckUserLogin(login))
        {
            Console.WriteLine("Enter employee`s password:");

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
        
            Console.WriteLine("Admin rights (y/n):");

            bool isAdmin = ConsoleAnswer();

            User user = new User(isAdmin, login, password);
            
            SaveUser(user);
            Console.WriteLine("<————————————————————>");
            Console.WriteLine("New user registered");
        }
        else
        {
            Console.WriteLine("<————————————————————>");
            Console.WriteLine("Login is not unique!\n Try again.");
            RegisterUser();
        }
    }
    
    public void SaveUser(User user)
    {
        List<User> users = FetchUsersFromJson();

        users.Add(user);

        string addedUser = JsonConvert.SerializeObject(users);

        File.WriteAllText(GetDBPath(), addedUser);
    }

    private void UploadUsersToJson(List<User> users)
    {
        string updatedUsers = JsonConvert.SerializeObject(users);

        File.WriteAllText(GetDBPath(), updatedUsers);
    }

    public bool CheckUserData(User user)
    {
        List<User> users = FetchUsersFromJson();
        
        User userLoginToCheck = users.Find(u => u.Login == user.Login);
        User userPasswordToCheck = users.Find(u => u.Password == user.Password);

        if (userLoginToCheck != null && userPasswordToCheck != null)
        {
            if (userLoginToCheck.Login == user.Login && userPasswordToCheck.Password == user.Password)
            {
                return true;
            }
            
            Console.WriteLine("Login or Password is incorrect");
            return false;
        }
        
        Console.WriteLine("User not found.\nTry again:");
        return false;
    }

    public bool CheckUserLogin(string login)
    {
        List<User> users = FetchUsersFromJson();
        
        User userLoginToCheck = users.Find(u => u.Login == login);
        
        if (userLoginToCheck != null)
        {
            return true;
        }

        return false;
    }

    public User FindUserByLogin(string login)
    {
        List<User> users = FetchUsersFromJson();
        
        User userLoginToFind = users.Find(u => u.Login == login);
        
        return userLoginToFind;
    }

    public bool IsAdmin(string login)
    {
        List<User> users = FetchUsersFromJson();
        
        User userToCheck = users.Find(u => u.Login == login)!;

        if (userToCheck != null)
        {
            return userToCheck.IsAdmin;
        }
        return false;
    }

    public bool DeleteUser(string login)
    {
        List<User> users = FetchUsersFromJson();

        User user = users.FirstOrDefault(u => u.Login == login);

        if (user != null)
        {
            users.Remove(user);
            UploadUsersToJson(users);
            return true;
        }
        
        return false;
    }

    public List<User> FetchUsersFromJson()
    {
        string json = File.ReadAllText(GetDBPath());

        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

        return users;
    }
    
    public bool ConsoleAnswer()
    {
        string answer;

        do
        {
            answer = Console.ReadLine();
            if (answer != "y" && answer != "n") Console.WriteLine("Invalid input");
        } while (answer != "y" && answer != "n");

        return answer switch
        {
            "y" => true,
            "n" => false,
        };
    }

    private string GetDBPath()
    {
        return Environment.CurrentDirectory + "\\DataBase.json";
    }
}