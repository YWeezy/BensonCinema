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
                    switch (selectedOption)
                    {
                        case ContentManagerOption.Performances:
                            selectedOption = ContentManagerOption.Exit; 
                            break;
                        case ContentManagerOption.schedule:
                            selectedOption = ContentManagerOption.Performances;
                            break;
                        case ContentManagerOption.Exit:
                            selectedOption = ContentManagerOption.schedule;
                            break;
                        default:
                            break;            
                    }
                    break;
                case ConsoleKey.DownArrow:
                    switch (selectedOption)
                    {
                        case ContentManagerOption.Performances:
                            selectedOption = ContentManagerOption.schedule;
                            break;
                        case ContentManagerOption.schedule:
                            selectedOption = ContentManagerOption.Exit;
                            break;
                        case ContentManagerOption.Exit:
                            selectedOption = ContentManagerOption.Performances;
                            break;
                        default:
                            break;
                    }
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


    private static void PerformContentManagerAction(ContentManagerOption option)
    {
        switch (option)
        {
            case ContentManagerOption.Performances:
                ManagePerformance.Start();
                break;
            case ContentManagerOption.Locations:
                ManageHall.Start();
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
            case UserOption.Reserve:
                reserver.ReserveTicket();
                break;
            case UserOption.Reservations:
                ticketer.loadMytickets(Utils.LoggedInUser.Id);
                Console.WriteLine("Press Enter to show the menu.");
                // Wait for the user to press enter
                while (Console.ReadKey().Key != ConsoleKey.Enter) {}
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
        Console.WriteLine("Welcome", Utils.LoggedInUser.FullName);

        foreach (UserOption option in Enum.GetValues(typeof(UserOption)))
        {
            Console.Write(option == selectedOption ? ">> " : "   ");
            Console.WriteLine($"{(int)option}. {option}");
        }
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

    private static void DisplayMenu(ContentManagerOption selectedOption)
    {
        Console.WriteLine("Welcome to the application!");

        foreach (ContentManagerOption option in Enum.GetValues(typeof(ContentManagerOption)))
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

    enum ContentManagerOption
    {
        Performances = 1,
        Locations,
        schedule,
        Exit
    }


}
