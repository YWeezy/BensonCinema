using System;

static class Menu
{


    enum MenuOption
    {
        Login = 1,
        Register,
        Employee,
        Exit
    }

    public static void Start()
    {
        if (Utils.userIsLoggedIn)
        {
            if (Utils.userIsEmployee)
                ShowEmployeeMenu();
            else
                ShowUserMenu();
        }
        else
        {
            ShowDefaultMenu();
        }
    }

    static void ShowDefaultMenu()
    {
        MenuOption option = MenuOption.Login;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║           Doll           ║");
            Console.WriteLine("║          House!          ║");
            Console.WriteLine("╠══════════════════════════╣");
            Console.WriteLine("║   Please select an option║");
            Console.WriteLine("║      to continue:        ║");
            Console.WriteLine("║                          ║");

            foreach (MenuOption menuOption in Enum.GetValues(typeof(MenuOption)))
            {
                Console.WriteLine($"{(menuOption == option ? "✅ \u001b[32m" : "   ")}{(int)menuOption}. [{menuOption}]");
            }

            Console.WriteLine("║                          ║");
            Console.WriteLine("╚══════════════════════════╝");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    option = option == MenuOption.Login ? MenuOption.Exit : option - 1;
                    break;

                case ConsoleKey.DownArrow:
                    option = option == MenuOption.Exit ? MenuOption.Login : option + 1;
                    break;

                case ConsoleKey.Enter:
                    PerformAction(option);
                    return;

                default:
                    break;
            }
        }
    }

    static void ShowUserMenu()
    {
        // Implement user menu view
        // This could be different from the default menu
        Console.WriteLine("User menu view");
    }

    static void ShowEmployeeMenu()
    {
        // Implement employee menu view
        // This could be different from the default menu
        Console.WriteLine("Employee menu view");
    }

    static void PerformAction(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.Login:
                UserLogin.Start();
                break;

            case MenuOption.Register:
                UserRegister.Start();
                break;

            case MenuOption.Employee:
                // Perform action for option 3
                break;

            case MenuOption.Exit:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}
