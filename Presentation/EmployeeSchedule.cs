using System.Text.Json;

static class EmployeeSchedule
{
  
    public static void Schedule()
    {   
        {
        ScheduleLogic shell = new ScheduleLogic();
        Console.Clear();
        bool loop = true;
        int selectedOption = 1; // Default selected option
        int totalOptions = 5; // Total number of options

        while (loop)
            {
            Console.Clear();
            DisplayMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
                {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == 1 ? totalOptions : selectedOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == totalOptions ? 1 : selectedOption + 1;
                    break;
                case ConsoleKey.Enter:
                    PerformAction(selectedOption, shell);
                    break;
                default:
                    break;
                }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;
            }
        }
    }
        
    static void PerformAction(int option, ScheduleLogic shell)
    {string path = "DataSources/schedule.json";
        switch (option)
        {
            case 1:
                Console.Clear();
                ShowSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Schedule();
                break;
            case 2:
                Console.Clear();
                AddSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Schedule();
                break;
            case 3:
                Console.Clear();
                RemoveSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Schedule();
                break;     
            case 4:
                Console.Clear();
                Menu.Start();
                break;
            default:
                break;
        }
    }

    static void DisplayMenu(int selectedOption)
    {   string color = "\u001b[32m";
        Console.WriteLine("What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? color + ">> 1 - View schedules\u001b[0m" : " 1 - View schedules ");
        Console.WriteLine(selectedOption == 2 ? color + ">> 2 - Add a new schedule \u001b[0m" : " 2 - Add a new schedule");
        Console.WriteLine(selectedOption == 3 ? color + ">> 3 - Remove a schedule \u001b[0m" : " 3 - Remove a schedule");
        Console.WriteLine(selectedOption == 4 ? color + ">> 4 - Go back to the previous menu \u001b[0m" : " 4 - Go back to the previous menu");
        Console.WriteLine(selectedOption == 5 ? color + ">> 5 - Close application \u001b[0m" : " 5 - Close application");
        

    }

        

    static void ShowSchedule(string path)
    {
        try 
            {
                string json = File.ReadAllText(path);
                List<ScheduleModel> schedules = JsonSerializer.Deserialize<List<ScheduleModel>>(json);
                
                Console.WriteLine("Schedule for this week:");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("| Worker       | Date       | Total Hours  | Start Time | End Time |");
                Console.WriteLine("--------------------------------------------------------------------------------");
            
                foreach (var schedule in schedules)
                {
                    Console.WriteLine($"| {schedule.Worker,-12} | {schedule.Date,-9} | {schedule.TotalHours,-12} | {schedule.StartTime,-10} | {schedule.EndTime,-8} |");
                }
                Console.WriteLine("--------------------------------------------------------------------------------");
            }
            
            
            catch (FileNotFoundException)
            {
                Console.WriteLine("json not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }
    }
    

    static void AddSchedule(string path)
    {
        Console.WriteLine("Please choose the employee you want to add a schedule for");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        string date;
        do
        { Console.WriteLine("Enter date: (DD-MM-YYYY for the schedule(within 1-2 weeks from today))");
        date = Console.ReadLine();
        } while (!IsValidDate(date));

        Console.WriteLine("Enter start time: (HH:MM)");
        string startTime = Console.ReadLine();

        Console.WriteLine("Enter end time: (HH:MM)");
        string endTime = Console.ReadLine();
    
        TimeSpan totalHours = TimeSpan.Parse(endTime).Subtract(TimeSpan.Parse(startTime));
        Console.WriteLine($"Total working hours for this date: {totalHours} (HH-MM-SS)");
        

        Console.WriteLine("The data you just entered has been saved.");

        ScheduleModel newSchedule = new ScheduleModel( selectedEmployee, date, totalHours.ToString(), startTime, endTime);

        ScheduleLogic scheduleLogicUp = new ScheduleLogic();
        scheduleLogicUp.UpdateList(newSchedule);
    }

    static void RemoveSchedule(string path)
    {
        Console.WriteLine("Please choose the employee you want to add a schedule for");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        ScheduleLogic scheduleLogicRE = new ScheduleLogic();
        scheduleLogicRE.RemoveSchedule(selectedEmployee);
    }
    static bool IsValidDate(string inputdate)
    {
        if (DateTime.TryParseExact(inputdate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            DateTime today = DateTime.Today;
            DateTime oneWeekLater = today.AddDays(7);
            DateTime twoWeeksLater = today.AddDays(14);

            if (parsedDate>= oneWeekLater && parsedDate <= twoWeeksLater)
             return true;
        }
        Console.WriteLine("Please enter a valid date within 1-2 weeks from today.");
        return false;
    }
    public static string SelectEmployee()
    {
        string path = "DataSources/accounts.json";
        try 
        {
            string json = File.ReadAllText(path);
            List<AccountModel> accounts = JsonSerializer.Deserialize<List<AccountModel>>(json);

            List<string> employees =  accounts
            .Where(account => account.Role == UserRole.Employee)
            .Select(account => account.FullName)
            .ToList();

            var selector = new EmployeeSelector(employees);
            selector.Run();

            return employees[selector.SelectedIndex];
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
            return null;
        }
    }
}
