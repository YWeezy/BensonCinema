using System;
using System.Drawing;

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
    private static void ShowContentManagerMenu()
    {
        ContentManagerOption selectedOption = ContentManagerOption.Performances;

        do
        {
            Console.Clear();
            DisplayMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == ContentManagerOption.Performances ? ContentManagerOption.Exit : (ContentManagerOption)((int)selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == ContentManagerOption.Exit ? ContentManagerOption.Performances : (ContentManagerOption)((int)selectedOption + 1);
                    break;
                case ConsoleKey.Enter:
                    PerformContentManagerAction(selectedOption);
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
        UserOption selectedOption = UserOption.ViewPerformances;

        do
        {
            Console.Clear();
            DisplayUserMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == UserOption.ViewPerformances ? UserOption.Exit : (UserOption)((int)selectedOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == UserOption.Exit ? UserOption.ViewPerformances : (UserOption)((int)selectedOption + 1);
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


    private static void PerformContentManagerAction(ContentManagerOption option)
    {
        switch (option)
        {
            case ContentManagerOption.Performances:
                ManagePerformance.Start();
                break;
            case ContentManagerOption.Halls:
                ManageHall.Start();
                break;
            case ContentManagerOption.Schedule:
                EmployeeSchedule.Schedule();
                break;
            case ContentManagerOption.ExportData:
                ExportData.Start();
                break;
            case ContentManagerOption.Exit:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }

    private static void PerformUserAction(UserOption option)
    {
        TicketPresentation reserver = new TicketPresentation();
        TicketLogic ticketer = new TicketLogic();
        switch (option)
        {
            case UserOption.ViewPerformances:
                ViewPerformances.Start();
                Console.WriteLine("Press Enter to go back.");
                // Wait for the user to press enter
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                ShowUserDefaultMenu();
                break;
            case UserOption.Reserve:
                reserver.ReserveTicket();
                Console.WriteLine("Press Enter to go back.");
                // Wait for the user to press enter
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                ShowUserDefaultMenu();
                break;
            case UserOption.Reservations:
                ticketer.loadMytickets(Utils.LoggedInUser.Id);
                Console.WriteLine("Press Enter to go back.");
                // Wait for the user to press enter
                while (Console.ReadKey().Key != ConsoleKey.Enter) {Console.Clear();}
                ShowUserDefaultMenu();
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
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Welcome to the user menu!{Color.Reset}\n", Utils.LoggedInUser.FullName);

        foreach (UserOption option in Enum.GetValues(typeof(UserOption)))
        {
            string displayText = option == UserOption.ViewPerformances ? "View Performances" : option.ToString(); // custom text for View Performances
            Console.Write(option == selectedOption ? $"{Color.Green}>> " : "   ");
            Console.WriteLine($"{displayText}{Color.Reset}");
        }
    }

    private static void DisplayMenu(MenuOption selectedOption)
    {
        Console.WriteLine($"{Color.Yellow}Welcome to the Dollhouse theater!{Color.Reset}\n");

        foreach (MenuOption option in Enum.GetValues(typeof(MenuOption)))
        {
            Console.Write(option == selectedOption ? $"{Color.Green}>> " : "   ");
            Console.WriteLine($"{option}{Color.Reset}");
        }
    }

    private static void DisplayMenu(ContentManagerOption selectedOption)
    {
        Console.WriteLine($"{Color.Yellow}Welcome to the Content Manager menu!{Color.Reset}\n");

        foreach (ContentManagerOption option in Enum.GetValues(typeof(ContentManagerOption)))
        {
            string displayText = option == ContentManagerOption.ExportData ? "Export data" : option.ToString(); // custom text for ExportData
            Console.Write(option == selectedOption ? $"{Color.Green}>> " : "   ");
            Console.WriteLine($"{displayText}{Color.Reset}");
        }
    }

    private static void ShowEmployeeMenu()
    {
        EmployeeSchedule.EmployeeMenu();
    }


    enum MenuOption
    {
        Login = 1,
        Register,
        Exit
    }
    enum UserOption
    {
        ViewPerformances = 1,
        Reserve,
        Reservations,
        Exit
    }

    enum ContentManagerOption
    {
        Performances = 1,
        Halls,
        Schedule,
        ExportData,
        Exit
    }
}