using System;

static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private string secretEmployeePassword = "123Employee#";
    static private string secretContentManagerPassword = "123Manager#";
    static private int milisecondTimeOut = 2000;


    public static void Start()
    {
        Console.Clear();
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
            Console.WriteLine($"{Color.Yellow}Welcome to the Register page!{Color.Reset}");
            Console.WriteLine($"\n{Color.Yellow}Choose your role:{Color.Reset}\n");
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
        Console.WriteLine(selectedRole == UserRole.User ? $"{Color.Green}>> User{Color.Reset}" : "   User");
        Console.WriteLine(selectedRole == UserRole.Employee ? $"{Color.Green}>> Employee{Color.Reset}" : "   Employee");
        Console.WriteLine(selectedRole == UserRole.ContentManager ? $"{Color.Green}>> Content Manager{Color.Reset}" : "   Content Manager");
    }

    private static void AskUserInfo(UserRole role)
    {
        string email;
        string name;
        string password;

        Console.Clear();
        if (role == UserRole.Employee || role == UserRole.ContentManager)
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Please enter the secret registration password:{Color.Reset}");
            string secretPassword = Console.ReadLine().Trim();
            if ((role == UserRole.Employee && secretPassword != secretEmployeePassword) ||
                (role == UserRole.ContentManager && secretPassword != secretContentManagerPassword))
            {
                Console.WriteLine("Incorrect secret password. Registration failed.");
                Thread.Sleep(milisecondTimeOut);
                Start();
            }
        }

        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please enter your email address:{Color.Reset}");
        email = Console.ReadLine().Trim().ToLower();
        if (!IsValidEmail(email))
        {
            Console.WriteLine($"\n{Color.Red}Invalid email format. Registration failed.{Color.Reset}");
            Thread.Sleep(milisecondTimeOut);
            Start();
        }

        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please enter your name:{Color.Reset}");
        name = Console.ReadLine().Trim();
        if (!IsValidName(name))
        {
            Console.WriteLine($"\n{Color.Red}Invalid name format. Registration failed.{Color.Reset}");
            Thread.Sleep(milisecondTimeOut);

            Start();
        }


        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please enter your password:{Color.Reset}");
        password = Utils.Encrypt(Console.ReadLine().Trim().ToLower());

        if (password == null)
        {
            Console.WriteLine($"\n{Color.Red}Error encrypting password. Registration failed.{Color.Reset}");
            Thread.Sleep(milisecondTimeOut);

            Start();
        }

        try
        {
            Console.Clear();
            AccountsModel user = new AccountsModel(email, name, password, role);
            accountsLogic.UpdateList(user);

            // Set logged-in user
            Utils.LoggedInUser = user;
            Console.WriteLine($"{Color.Green}Registration successful!{Color.Reset}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n{Color.Red}Error occurred: {ex.Message}");
            Console.WriteLine($"Registration failed.{Color.Reset}");
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
