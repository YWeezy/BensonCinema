using System;
using System.Globalization;

static class ExportData
{
    static public void Start()
    {
        DisplayMenu(new string[] { "Export accounts", "Export halls", "Export performances", "Back to main menu" }); // "Export schedules"
    }

    static public void End()
    {
        Console.WriteLine("Press Enter to return to the menu.");
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        Start();
    }

    static private void DisplayMenu(string[] options)
    {
        Console.Clear();
        int selectedOption = 0;
        int totalOptions = options.Length;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}What do you want to do?{Color.Reset}\n");
            for (int i = 0; i < totalOptions; i++)
            {
                if (i == selectedOption)
                    Console.WriteLine($"{Color.Green}>> {options[i]}{Color.Reset}");
                else
                    Console.WriteLine($"   {options[i]}");
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == 0 ? totalOptions - 1 : selectedOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == totalOptions - 1 ? 0 : selectedOption + 1;
                    break;
                case ConsoleKey.Enter:
                    PerformAction(options[selectedOption]);
                    return;
                default:
                    break;
            }
        }
    }

    static private void PerformAction(string option)
    {
        switch (option)
        {
            // Account options
            case "Export accounts":
                DisplayMenu(new string[] { "Export all accounts", "Export all User accounts", "Export all Employee accounts", "Export all Content Manager accounts", "Back to previous menu" });
                break;

            case "Export all accounts":
                ExportAccounts();
                break;

            case "Export all User accounts":
                ExportAccounts(0);
                break;

            case "Export all Employee accounts":
                ExportAccounts(1);
                break;

            case "Export all Content Manager accounts":
                ExportAccounts(2);
                break;


            // Hall option
            case "Export halls":
                ExportHalls();
                break;

            // Performance options
            case "Export performances":
                DisplayMenu(new string[] { "Export all performances", "Export performances of the last 30 days", "Export specific performances by date", "Back to previous menu" });
                break;

            case "Export all performances":
                ExportPerformances();
                break;

            case "Export performances of the last 30 days":
                ExportPerformances(DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy"), DateTime.Now.ToString("dd-MM-yyyy"));
                break;

            case "Export specific performances by date":
                Console.Clear();
                string startDate;
                string endDate;

                while (true)
                {
                    Console.WriteLine("\nEnter a start date for the performances you want to export. (DD-MM-YYYY): ");
                    startDate = ConsoleInput.EditLine(DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy"));
                    DateTime startDateDT = DateTime.MinValue;

                    if (DateTime.TryParseExact(startDate, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateDT))
                    {
                        Console.WriteLine($"{Color.Green}You entered: {startDateDT}{Color.Reset}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please enter a valid date format (DD-MM-YYYY).");
                    }
                }

                while (true)
                {
                    Console.WriteLine("\nEnter an end date for the performances you want to export. (DD-MM-YYYY): ");
                    endDate = ConsoleInput.EditLine(DateTime.Now.ToString("dd-MM-yyyy"));
                    DateTime endDateDT = DateTime.MinValue;

                    if (DateTime.TryParseExact(endDate, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateDT))
                    {
                        Console.WriteLine($"{Color.Green}You entered: {endDateDT}{Color.Reset}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please enter a valid date format (DD-MM-YYYY).");
                    }
                }

                ExportPerformances(startDate, endDate);
                break;


            // Schedule options
            case "Export schedules":
                End();
                break;

            case "Back to main menu":
                Menu.Start();
                break;
            case "Back to previous menu":
                Start();
                break;
            default:
                break;
        }
    }

    // Methods for exporting
    static private void ExportAccounts(int role = -1)
    {
        Console.Clear();
        Console.WriteLine("Exporting accounts...");

        AccountsLogic accountsLogic = new AccountsLogic();
        List<AccountsModel> accounts = accountsLogic.GetAllAccounts(role);

        string csvFilePath = "Exports/accounts/accounts.csv";

        // Writing data to CSV
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            // Header
            writer.WriteLine("Id,EmailAddress,FullName,Role");

            // Accounts
            foreach (var account in accounts)
            {
                writer.WriteLine($"{account.Id},{account.EmailAddress},{account.FullName},{account.Role}");
            }
        }

        Console.WriteLine($"{Color.Green}Accounts exported to {csvFilePath}{Color.Reset}");

        End();
    }

    static private void ExportHalls(int role = -1)
    {
        Console.Clear();
        Console.WriteLine("Exporting halls...");

        HallLogic hallLogic = new HallLogic();
        List<HallsModel> halls = hallLogic.GetList();

        string csvFilePath = "Exports/halls/halls.csv";

        // Writing data to CSV
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            // Header
            writer.WriteLine("hallID,hallName,type,active");

            // Halls
            foreach (var hall in halls)
            {
                writer.WriteLine($"{hall.hallID},{hall.hallName},{hall.type},{hall.active}");
            }
        }

        Console.WriteLine($"{Color.Green}Halls exported to {csvFilePath}{Color.Reset}");

        End();
    }

    static private void ExportPerformances(string from = "01-01-0001", string to = "31-12-9999")
    {
        Console.Clear();
        Console.WriteLine("Exporting performances...");

        PerformanceLogic performanceLogic = new PerformanceLogic();
        List<PerformancesModel> performances = performanceLogic.GetPerformances(from, to);

        string csvFilePath = "Exports/performances/performances.csv";

        // Writing data to CSV
        using (StreamWriter writer = new StreamWriter(csvFilePath))
        {
            // Header
            writer.WriteLine("id,name,startDate,endDate,hallId,active");

            // Performances
            foreach (var performance in performances)
            {
                writer.WriteLine($"{performance.id},{performance.name},{performance.startDate},{performance.endDate},{performance.hallId},{performance.active}");
            }
        }

        Console.WriteLine($"{Color.Green}Performances exported to {csvFilePath}{Color.Reset}");

        End();
    }

}
