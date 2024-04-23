using System;

static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private string secretEmployeePassword = "123";
    static private string secretContentManagerPassword = "123";
    static private int milisecondTimeOut = 2000;


    public static void Start()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the register page");
        Console.WriteLine("Choose your role (User /Employee/ Content manager):");

        UserRole role = GetSelectedUserRole();
        AskUserInfo(role);
    }

    private static UserRole GetSelectedUserRole()
    {
        UserRole selectedRole = UserRole.User;

        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Choose your role (Use arrow keys to select, Enter to confirm):");
            DisplayUserRole(selectedRole);

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedRole = selectedRole == UserRole.User ? UserRole.ContentManager : (UserRole)((int)selectedRole - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedRole = selectedRole == UserRole.ContentManager ? UserRole.User : (UserRole)((int)selectedRole + 1);
                    break;
                case ConsoleKey.Enter:
                    return selectedRole;
            }

        } while (key != ConsoleKey.Enter);

        return selectedRole;
    }

    private static void DisplayUserRole(UserRole selectedRole)
    {
        Console.WriteLine(selectedRole == UserRole.User ? ">> User" : "   User");
        Console.WriteLine(selectedRole == UserRole.Employee ? ">> Employee" : "   Employee");
        Console.WriteLine(selectedRole == UserRole.ContentManager ? ">> Content Manager" : "   Content Manager");
    }

    private static void AskUserInfo(UserRole role)
    {
        string email;
        string name;
        string password;

        Console.Clear();
        if (role == UserRole.Employee || role == UserRole.ContentManager)
        {
            Console.WriteLine("Please enter the secret registration password:");
            string secretPassword = Console.ReadLine().Trim();
            if ((role == UserRole.Employee && secretPassword != secretEmployeePassword) ||
                (role == UserRole.ContentManager && secretPassword != secretContentManagerPassword))
            {
                Console.WriteLine("Incorrect secret password. Registration failed.");
                Thread.Sleep(milisecondTimeOut);
                Start();
            }
        }
        Console.WriteLine("Please enter your email address:");
        email = Console.ReadLine().Trim().ToLower();
        if (!IsValidEmail(email))
        {
            Console.WriteLine("Invalid email format. Registration failed.");
            Thread.Sleep(milisecondTimeOut);
            Start();
        }

        Console.WriteLine("Please enter your name:");
        name = Console.ReadLine().Trim();
        if (!IsValidName(name))
        {
            Console.WriteLine("Invalid name format. Registration failed.");
            Thread.Sleep(milisecondTimeOut);

            Start();
        }



        Console.WriteLine("Please enter your password:");
        password = Console.ReadLine().Trim().ToLower();

        if (password == null)
        {
            Console.WriteLine("Error encrypting password. Registration failed.");
            Thread.Sleep(milisecondTimeOut);

            Start();
        }

        try
        {
            AccountModel user = new AccountModel(email, name, password, role);
            accountsLogic.UpdateList(user);

            // Set logged-in user
            Utils.LoggedInUser = user;
            Console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
            Console.WriteLine("Registration failed.");
        }
        finally
        {
            Thread.Sleep(milisecondTimeOut);
            Menu.Start();
        }
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }
}
