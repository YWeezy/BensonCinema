using System;

static class Menu
{
    public static void Start()
    {
        if (Utils.LoggedInUser != null)
        {
            if (Utils.LoggedInUser.Role == UserRole.Employee)
                ShowEmployeeMenu();
            else if (Utils.LoggedInUser.Role == UserRole.ContentManager)
                ShowContentManagerMenu();
            else
                ShowUserDefaultMenu();
        }
        else
        {
            ShowDefaultMenu();
        }
    }

    private static void ShowDefaultMenu()
    {
        MenuOption selectedOption = MenuOption.Login;

        do
        {
            Console.Clear();
            DisplayMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == MenuOption.Login ? MenuOption.Exit : (MenuOption)((int)selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == MenuOption.Exit ? MenuOption.Login : (MenuOption)((int)selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    PerformAction(selectedOption);
                    break;
                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;

        } while (true);
    }

    private static void ShowUserDefaultMenu()
    {
        UserOption selectedOption = UserOption.Reserve;

        do
        {
            Console.Clear();
            DisplayUserMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == UserOption.Reserve ? UserOption.Exit : (UserOption)((int)selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == UserOption.Exit ? UserOption.Reserve : (UserOption)((int)selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    PerformUserAction(selectedOption);
                    break;
                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;

        } while (true);
    }


    private static void DisplayMenu(MenuOption selectedOption)
    {
        Console.WriteLine("Welcome to the application!");

        foreach (MenuOption option in Enum.GetValues(typeof(MenuOption)))
        {
            Console.Write(option == selectedOption ? ">> " : "   ");
            Console.WriteLine($"{(int)option}. {option}");
        }
    }
    
    private static void PerformUserAction(UserOption option)
    {
        switch (option)
        {
            case UserOption.Reserve:
                UserLogin.Start();
                break;
            case UserOption.Reservations:
                UserRegister.Start();
                break;
            case UserOption.Exit:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }    

    private static void PerformAction(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.Login:
                UserLogin.Start();
                break;
            case MenuOption.Register:
                UserRegister.Start();
                break;
            case MenuOption.Exit:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }


    private static void DisplayUserMenu(UserOption selectedOption)
    {
        Console.WriteLine("Welcome", Utils.LoggedInUser.FullName);

        foreach (UserOption option in Enum.GetValues(typeof(UserOption)))
        {
            Console.Write(option == selectedOption ? ">> " : "   ");
            Console.WriteLine($"{(int)option}. {option}");
        }
    }

    private static void ShowEmployeeMenu()
    {
        // Display employee menu
        Console.WriteLine("Employee Menu:");
        Console.WriteLine("1. View Tasks");
        Console.WriteLine("2. Assign Task");
        Console.WriteLine("3. View Employees");
        Console.WriteLine("4. Logout");
        Console.Write("Please select an option: ");
    }

    private static void ShowContentManagerMenu()
    {
        // Display content manager menu
        Console.WriteLine("Content Manager Menu:");
        Console.WriteLine("1. Manage Content");
        Console.WriteLine("2. View Analytics");
        Console.WriteLine("3. Logout");
        Console.Write("Please select an option: ");
    }

    enum MenuOption
    {
        Login = 1,
        Register,
        Exit
    }
        enum UserOption
    {
        Reserve = 1,
        Reservations,
        Exit
    }

}
