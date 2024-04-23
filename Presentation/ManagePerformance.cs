using System.Globalization;
using System.Runtime.CompilerServices;

static class ManagePerformance
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        PerformanceLogic logic = new PerformanceLogic();
        Console.Clear();
        bool loop = true;
        int selectedOption = 1; // Default selected option
        int totalOptions = 4; // Total number of options
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
                    PerformAction(selectedOption, logic);
                    break;
                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;
        }
    }

    static public void Update(PerformanceLogic logic, int selectedPerformanceIndex = -1)
    {
        bool editing = false;
        PerformanceModel selectedPerformance = null;
        bool active = true;
        if (selectedPerformanceIndex != -1)
        {
            editing = true;
            selectedPerformance = logic.GetPerformances()[selectedPerformanceIndex];
            active = selectedPerformance.active;
        }

        string performanceName = null;
        bool performanceStartValid = false;
        bool performanceEndValid = false;
        int hallId = 0;
        DateTime performanceStartDT = DateTime.MinValue;
        DateTime performanceEndDT = DateTime.MinValue;

        Console.Clear();
        // name
        while (string.IsNullOrEmpty(performanceName))
        {
            Console.WriteLine("\nPerformance name: ");
            performanceName = editing ? ConsoleInput.EditLine(selectedPerformance.name) : Console.ReadLine();


            if (string.IsNullOrEmpty(performanceName))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please provide a Performance name.\u001b[0m");
            }
        }

        Console.Clear();
        // startDate
        while (performanceStartValid == false)
        {
            Console.WriteLine("\nWhen does it start? (DD-MM-YYYY HH:MM): ");
            string performanceStart = editing ? ConsoleInput.EditLine(selectedPerformance.startDate.ToString("dd-MM-yyyy HH:mm")) : Console.ReadLine();

            if (DateTime.TryParseExact(performanceStart, "d-M-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceStartDT))
            {
                if (editing == false)
                {
                    if (performanceStartDT < DateTime.Now)
                    {
                        Console.WriteLine("\u001b[31mYou can't enter a date and time that is in the past.\u001b[0m");
                    }
                    else
                    {
                        Console.WriteLine($"\u001b[31mYou entered: {performanceStartDT}\u001b[0m");
                        performanceStartValid = true;
                    }
                } else {
                    performanceStartValid = true;
                }
            }
            else
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid date and time format (DD-MM-YYYY HH:MM).\u001b[0m");
            }
        }

        Console.Clear();
        // endDate
        while (performanceEndValid == false)
        {
            Console.WriteLine("\nWhen does it end? (DD-MM-YYYY HH:MM): ");
            string performanceEnd = editing ? ConsoleInput.EditLine(selectedPerformance.endDate.ToString("dd-MM-yyyy HH:mm")) : Console.ReadLine();

            if (DateTime.TryParseExact(performanceEnd, "d-M-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceEndDT))
            {
                if (performanceEndDT < performanceStartDT)
                {
                    Console.WriteLine("\u001b[31mYou can't enter a date and time that is before the starttime of the Performance.\u001b[0m");
                }
                else if (performanceEndDT > DateTime.Now.AddMonths(6))
                {
                    Console.WriteLine("\u001b[31mYou can't enter a date and time that is more than 6 months ahead of the starttime.\u001b[0m");
                }
                else
                {
                    Console.WriteLine("\u001b[32mYou entered: " + performanceEndDT);
                    performanceEndValid = true;
                }
            }
            else
            {
                Console.WriteLine("\u001b[31mInvalid input.\u001b[0m Please enter a valid date and time format (DD-MM-YYYY HH:MM).");
            }
        }

        Console.Clear();
        // hallid
        while (hallId == 0)
        {
            try
            {
                bool performanceHallValid = false;

                while (performanceHallValid == false)
                {
                    HallLogic hallLogic = new HallLogic();
                    hallLogic.DisplayTable(true);

                    Console.WriteLine("\nHall ID: ");
                    hallId = editing ? Convert.ToInt32(ConsoleInput.EditLine(selectedPerformance.hallId)) : Convert.ToInt32(Console.ReadLine());
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/halls.json"));
                    List<HallModel> locs = DataAccess<HallModel>.LoadAll(path);

                    bool idExists = locs.Any(loc => loc.hallID == hallId);

                    if (idExists)
                    {
                        performanceHallValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"\u001b[31mA Hall with ID {hallId} does not exist.\u001b[0m");
                    }
                }

            }
            catch (System.Exception)
            {
                Console.WriteLine("\u001b[31mInvalid input.\u001b[0m Please provide a valid Hall ID.");
            }
        }

        if (editing == true)
        {

            Console.Clear();
            // active
            if (selectedPerformance.active == false)
            {
                Console.WriteLine($"\nCurrent active state: \u001b[31mInactive\u001b[0m\n\nDo you want to switch to \u001b[31mActive\u001b[0m? (Y/N)");
            }
            else
            {
                Console.WriteLine($"\nCurrent active state: \u001b[32mActive\u001b[0m\n\nDo you want to switch to \u001b[31mInactive\u001b[0m? (Y/N)");
            }

            if (Console.ReadLine().ToLower() == "y")
            {
                active = !active;
            }

            HallLogic Hlogic = new HallLogic();
            Console.Clear();
            Console.WriteLine($"\u001b[34mName: {performanceName}");
            Console.WriteLine($"Start: {performanceStartDT}");
            Console.WriteLine($"End: {performanceEndDT}");
            Console.WriteLine($"Hall: {Hlogic.getHallNamebyId(hallId)}");
            Console.WriteLine($"Active: {active}\u001b[0m");

            Console.WriteLine("\nAre you sure you want to make these changes? (Y/N)");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    selectedPerformance.name = performanceName;
                    selectedPerformance.startDate = performanceStartDT;
                    selectedPerformance.endDate = performanceEndDT;
                    selectedPerformance.hallId = hallId;
                    selectedPerformance.active = active;
                    logic.UpdateList(selectedPerformance);
                    Console.Clear();
                    Console.WriteLine("\u001b[32mThe Performance was successfully edited.\u001b[0m\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\u001b[31mThe Performance was not edited.\u001b[0m\n");
                    break;
            }

        }
        else
        {

            Console.Clear();
            Console.WriteLine($"Name: {performanceName}");
            Console.WriteLine($"Start: {performanceStartDT}");
            Console.WriteLine($"End: {performanceEndDT}");
            Console.WriteLine($"Hall: {hallId}");

            Console.WriteLine("\n\u001b[32mAre you sure you want to add this Performance?\u001b[0m (Y/N)");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    int newId = logic.GetNewId();
                    PerformanceModel performance = new PerformanceModel(newId, performanceName, performanceStartDT, performanceEndDT, hallId, true);
                    logic.UpdateList(performance);
                    Console.Clear();
                    Console.WriteLine("\u001b[32mThe Performance was succesfully added.\u001b[0m\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\u001b[31mThe Performance was not added.\u001b[0m\n");
                    break;
            }
        }
    }

    static public void Delete(PerformanceLogic logic)
    {
        Console.WriteLine("Enter the ID of the Performance you want to \u001b[31mdelete\u001b[0m: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.DeletePerformance(idToDelete))
            {
                Console.WriteLine($"\u001b[32mPerformance with ID {idToDelete} deleted successfully.\u001b[0m");
            }
            else
            {
                Console.WriteLine($"\u001b[31mPerformance with ID {idToDelete} not found.\u001b[0m");
            }
        }
        else
        {
            Console.WriteLine("\u001b[31mInvalid input. Please enter a valid ID.\u001b[0m");
        }

    }

    static public void Edit(PerformanceLogic logic)
    {
        Console.Clear();

        int selectedPerformanceIndex = 0;
        int totalPerformances = logic.GetTotalPerformances();

        while (true)
        {
            DisplayPerformances(logic, selectedPerformanceIndex);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedPerformanceIndex = selectedPerformanceIndex == 0 ? totalPerformances - 1 : selectedPerformanceIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedPerformanceIndex = selectedPerformanceIndex == totalPerformances - 1 ? 0 : selectedPerformanceIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    Update(logic, selectedPerformanceIndex);
                    return;
                case ConsoleKey.Escape:
                    Start();
                    return;
                default:
                    break;
            }
        }
    }

    static private void DisplayPerformances(PerformanceLogic logic, int selectedPerformanceIndex)
    {
        Console.Clear();
        HallLogic hallLogic = new HallLogic();
        Console.WriteLine("\u001b[0mPlease select a Performance to edit:\n");

        Console.WriteLine("      {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
        Console.WriteLine("      ------------------------------------------------------------------------------------------------------------");

        int index = 0;
        foreach (PerformanceModel performance in logic.GetPerformances())
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write("\u001b[32m >>");
            }
            else
            {
                Console.Write("\u001b[0m   ");
            }

            string actstr;
            if (performance.active)
            {
                actstr = "Active";
            }
            else
            {
                actstr = "Inactive";
            }

            Console.WriteLine("   {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -5}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), actstr);

            index++;
        }
    }


    static private void PerformAction(int option, PerformanceLogic logic)
    {
        switch (option)
        {
            case 1:
                Console.Clear();
                logic.DisplayTable();
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Start();
                break;
            case 2:
                Console.Clear();
                Update(logic);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Start();
                break;
            case 3:
                Console.Clear();
                Edit(logic);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Start();
                break;
            case 4:
                Menu.Start();
                break;
            default:
                break;
        }
    }

    static private void DisplayMenu(int selectedOption)
    {
        string color = "\u001b[32m";
        Console.WriteLine("What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? color + ">> View Performances\u001b[0m" : "   View Performances");
        Console.WriteLine(selectedOption == 2 ? color + ">> Add a Performance\u001b[0m" : "   Add a Performance");
        Console.WriteLine(selectedOption == 3 ? color + ">> Edit a Performance\u001b[0m" : "   Edit a Performance");
        Console.WriteLine(selectedOption == 4 ? color + ">> Back to main menu\u001b[0m" : "   Back to main menu");
    }
}