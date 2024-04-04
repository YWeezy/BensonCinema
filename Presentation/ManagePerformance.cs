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

    static public void Insert(PerformanceLogic logic)
    {

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
            performanceName = Console.ReadLine();

            if (string.IsNullOrEmpty(performanceName))
            {
                Console.WriteLine("Invalid input. Please provide a performance name.");
            }
        }

        Console.Clear();
        // startDate
        while (performanceStartValid == false)
        {
            Console.WriteLine("\nWhen does it start? (YYYY-MM-DD HH:MM:SS): ");
            string performanceStart = Console.ReadLine();

            if (DateTime.TryParse(performanceStart, out performanceStartDT))
            {
                if (performanceStartDT < DateTime.Now) {
                    Console.WriteLine("You can't enter a date and time that is in the past.");
                } else {
                    Console.WriteLine("You entered: " + performanceStartDT);
                    performanceStartValid = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
            }
        }

        Console.Clear();
        // endDate
        while (performanceEndValid == false)
        {
            Console.WriteLine("\nWhen does it end? (YYYY-MM-DD HH:MM:SS): ");
            string performanceEnd = Console.ReadLine();

            if (DateTime.TryParse(performanceEnd, out performanceEndDT))
            {
                if (performanceEndDT < performanceStartDT) {
                    Console.WriteLine("You can't enter a date and time that is before the starttime of the performance.");
                } else if (performanceEndDT > DateTime.Now.AddMonths(6)) {
                    Console.WriteLine("You can't enter a date and time that is more than 6 months ahead of the starttime.");
                } else {
                    Console.WriteLine("You entered: " + performanceEndDT);
                    performanceEndValid = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
            }
        }

        Console.Clear();
        // hallid
        while (hallId == 0)
        {
            try
            {
                bool performanceHallValid = false;

                while (performanceHallValid == false) {
                    Console.WriteLine("\nHall ID: ");
                    hallId = Convert.ToInt32(Console.ReadLine());

                    List<HallModel> locs = HallAccess.Hallget();

                    bool idExists = locs.Any(loc => loc.hallID == hallId);

                    if (idExists)
                    {
                        performanceHallValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"A hall with ID {hallId} does not exist.");
                    }
                }

            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid input. Please provide a valid hall ID.");
            }
        }

        Console.Clear();
        Console.WriteLine($"Name: {performanceName}");
        Console.WriteLine($"Start: {performanceStartDT}");
        Console.WriteLine($"End: {performanceEndDT}");
        Console.WriteLine($"Hall: {hallId}");

        Console.WriteLine("\nAre you sure you want to add this performance? (Y/N)");
        string confirmation = Console.ReadLine();

        switch (confirmation.ToLower())
        {
            case "y":
                int newId = logic.GetNewId();
                PerformanceModel performance = new PerformanceModel(newId, performanceName, performanceStartDT, performanceEndDT, hallId, true);
                logic.UpdateList(performance);
                Console.Clear();
                Console.WriteLine("The performance was succesfully added.\n");
                break;
            default:
                Console.Clear();
                Console.WriteLine("The performance was not added.\n");
                break;
        }
    }

    static public void Delete(PerformanceLogic logic)
    {
        Console.WriteLine("Enter the ID of the performance you want to delete: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.DeletePerformance(idToDelete))
            {
                Console.WriteLine($"Performance with ID {idToDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Performance with ID {idToDelete} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid ID.");
        }

    }

    static public void Edit(PerformanceLogic logic) {
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
                    EditPerformance(logic, selectedPerformanceIndex);
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
        Console.WriteLine("Please select a performance to edit:\n");

        Console.WriteLine("      {0,-6}{1,-22}{2,-21}{3, -21}{4, -10}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
        Console.WriteLine("      --------------------------------------------------------------------------------------");
        
        int index = 0;
        foreach (PerformanceModel performance in logic.GetPerformances())
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write(">> ");
            }
            else
            {
                Console.Write("   ");
            }
            
            Console.WriteLine("   {0,-6}{1,-22}{2,-21}{3, -21}{4, -10}{5, -5}", performance.id, performance.name, performance.startDate, performance.endDate, performance.hallId, performance.active);

            index++;
        }
    }

    static private void EditPerformance(PerformanceLogic logic, int selectedPerformanceIndex)
    {
        PerformanceModel selectedPerformance = logic.GetPerformances()[selectedPerformanceIndex];

        string performanceName = null;
        bool performanceStartValid = false;
        bool performanceEndValid = false;
        int hallId = 0;
        DateTime performanceStartDT = DateTime.MinValue;
        DateTime performanceEndDT = DateTime.MinValue;
        bool active = selectedPerformance.active;

        Console.Clear();
        // name
        while (string.IsNullOrEmpty(performanceName))
        {
            Console.WriteLine($"\nCurrent performance name: {selectedPerformance.name}\n\nEnter a new name, or leave it blank to keep it.");
            performanceName = Console.ReadLine();

            if (string.IsNullOrEmpty(performanceName))
            {
                performanceName = selectedPerformance.name;
            }
        }

        Console.Clear();
        // startDate
        while (performanceStartValid == false)
        {
            Console.WriteLine($"\nCurrent start time: {selectedPerformance.startDate}: \n\nEnter a new date & time, or leave it blank to keep it.");
            string performanceStart = Console.ReadLine();

            if (string.IsNullOrEmpty(performanceStart))
            {
                performanceStartDT = selectedPerformance.startDate;
                performanceStartValid = true;
            }
            else if (DateTime.TryParse(performanceStart, out performanceStartDT))
            {
                if (performanceStartDT < DateTime.Now)
                {
                    Console.WriteLine("You can't enter a date and time that is in the past.");
                }
                else
                {
                    Console.WriteLine("You entered: " + performanceStartDT);
                    performanceStartValid = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
            }
        }

        Console.Clear();
        // endDate
        while (performanceEndValid == false)
        {
            Console.WriteLine($"\nCurrent end time: {selectedPerformance.endDate}: \n\nEnter a new date & time, or leave it blank to keep it.");
            string performanceEnd = Console.ReadLine();

            if (string.IsNullOrEmpty(performanceEnd))
            {
                performanceEndDT = selectedPerformance.endDate;
                performanceEndValid = true;
            }
            else if (DateTime.TryParse(performanceEnd, out performanceEndDT))
            {
                if (performanceEndDT < performanceStartDT)
                {
                    Console.WriteLine("You can't enter a date and time that is before the start time of the performance.");
                }
                else if (performanceEndDT > DateTime.Now.AddMonths(6))
                {
                    Console.WriteLine("You can't enter a date and time that is more than 6 months ahead of the start time.");
                }
                else
                {
                    Console.WriteLine("You entered: " + performanceEndDT);
                    performanceEndValid = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
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
                    Console.WriteLine($"\nCurrent hall ID: {selectedPerformance.hallId}\n\nEnter a new ID, or leave it blank to keep it.");
                    string hallInput = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(hallInput))
                    {
                        hallId = selectedPerformance.hallId;
                        break;
                    }

                    hallId = Convert.ToInt32(hallInput);

                    List<HallModel> locs = HallAccess.Hallget();

                    bool idExists = locs.Any(loc => loc.hallID == hallId);

                    if (idExists)
                    {
                        performanceHallValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"A hall with ID {hallId} does not exist.");
                    }
                }
            }
            catch (System.Exception)
            {
                hallId = selectedPerformance.hallId;
            }
        }

        Console.Clear();
        // active
        if (selectedPerformance.active == false)
        {
            Console.WriteLine($"\nCurrent active state: {selectedPerformance.active}\n\nDo you want to switch to active? (Y/N)");
        }
        else
        {
            Console.WriteLine($"\nCurrent active state: {selectedPerformance.active}\n\nDo you want to switch to inactive? (Y/N)");
        }

        if (Console.ReadLine().ToLower() == "y")
        {
            active = !active;
        }

        Console.Clear();
        Console.WriteLine($"Name: {performanceName}");
        Console.WriteLine($"Start: {performanceStartDT}");
        Console.WriteLine($"End: {performanceEndDT}");
        Console.WriteLine($"Hall: {hallId}");
        Console.WriteLine($"Active: {active}");

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
                Console.WriteLine("The performance was successfully edited.\n");
                break;
            default:
                Console.Clear();
                Console.WriteLine("The performance was not edited.\n");
                break;
        }
    }


    static private void PerformAction(int option, PerformanceLogic logic)
    {
        switch (option)
        {
            case 1:
                Console.Clear();
                logic.GetList();
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Start();
                break;
            case 2:
                Console.Clear();
                Insert(logic);
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
        Console.WriteLine("What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? ">> View performances" : "   View performances");
        Console.WriteLine(selectedOption == 2 ? ">> Add a performance" : "   Add a performance");
        Console.WriteLine(selectedOption == 3 ? ">> Edit a performance" : "   Edit a performance");
        Console.WriteLine(selectedOption == 4 ? ">> Back to main menu" : "   Back to main menu");
    }
}