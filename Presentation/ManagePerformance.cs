using System.Runtime.CompilerServices;

static class ManagePerformance
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start() {

        PerformanceLogic logic = new PerformanceLogic();

        bool loop = true;
        while (loop) {
            Console.WriteLine("What do you want to do?\n");
            
            Console.WriteLine("1 - View performances");
            Console.WriteLine("2 - Add a performance");
            Console.WriteLine("3 - Delete a performance");
            Console.WriteLine("Q - Exit\n");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine(logic.GetList());
                    break;

                case "2":
                    Insert(logic);
                    break;

                case "3":
                    Delete(logic);
                    break;
                
                default:
                    loop = false;
                    break;
            }
        }
    }

    static public void Insert(PerformanceLogic logic) {

        string performanceName = null;
        bool performanceStartValid = false;
        bool performanceEndValid = false;
        int locationId = 0;
        DateTime performanceStartDT = DateTime.MinValue;
        DateTime performanceEndDT = DateTime.MinValue;

        // name
        while (string.IsNullOrEmpty(performanceName)) {
            Console.WriteLine("\nPerformance name: ");
            performanceName = Console.ReadLine();
            
            if (string.IsNullOrEmpty(performanceName))
            {
                Console.WriteLine("Invalid input. Please provide a performance name.");
            }
        }

        // startDate
        while (performanceStartValid == false) {
            Console.WriteLine("\nWhen does it start? (YYYY-MM-DD HH:MM:SS): ");
            string performanceStart = Console.ReadLine();

            if (DateTime.TryParse(performanceStart, out performanceStartDT))
            {
                Console.WriteLine("You entered: " + performanceStartDT);
                performanceStartValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
            }
        }

        // endDate
        while (performanceEndValid == false) {
            Console.WriteLine("\nWhen does it end? (YYYY-MM-DD HH:MM:SS): ");
            string performanceEnd = Console.ReadLine();

            if (DateTime.TryParse(performanceEnd, out performanceEndDT))
            {
                Console.WriteLine("You entered: " + performanceEndDT);
                performanceEndValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date and time format (YYYY-MM-DD HH:MM:SS).");
            }
        }

        // locationid
        while (locationId == 0) {
            Console.WriteLine("\nLocation ID: ");
            try
            {
                locationId = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid input. Please provide a valid location ID.");
            }
        }
        
        Console.WriteLine("------------------------");
        Console.WriteLine($"Name: {performanceName}");
        Console.WriteLine($"Start: {performanceStartDT}");
        Console.WriteLine($"End: {performanceEndDT}");
        Console.WriteLine($"Location: {locationId}");

        Console.WriteLine("\nAre you sure you want to add this performance? (Y/N)");
        string confirmation = Console.ReadLine();

        switch (confirmation.ToLower())
        {
            case "y":
                int newId = logic.GetNewId();
                Console.WriteLine(newId);
                PerformanceModel performance = new PerformanceModel(newId, performanceName, performanceStartDT, performanceEndDT, locationId);
                logic.UpdateList(performance);
                Console.WriteLine("The performance was succesfully added.");
                break;
            default:
                Console.WriteLine("The performance was not added.");
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
}