using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> innerListOfDicts = new();
        string description = null;
        bool performanceStartValid = false;
        bool performanceEndValid = false;
        int hallId = 0;
        DateTime performanceStartDT = DateTime.MinValue;
        DateTime performanceEndDT = DateTime.MinValue;

        Console.Clear();
        // name
        while (string.IsNullOrEmpty(performanceName))
        {
            Console.WriteLine($"{Color.Yellow}Performance name:{Color.Reset}");
            performanceName = editing ? ConsoleInput.EditLine(selectedPerformance.name) : Console.ReadLine();


            if (string.IsNullOrEmpty(performanceName))
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please provide a Performance name.{Color.Reset}");
            }
        }

        //description
        while (string.IsNullOrEmpty(description))
        {
            Console.WriteLine($"{Color.Yellow}Performance description:{Color.Reset}");
            description = editing ? ConsoleInput.EditLine(selectedPerformance.description) : Console.ReadLine();


            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please provide a Performance description.{Color.Reset}");
            }
        }

        Console.Clear();
        // startDate
        while (performanceStartValid == false)
        {
            Console.WriteLine($"{Color.Yellow}When does it start? (DD-MM-YYYY HH:MM):{Color.Reset}");
            string performanceStart = editing ? ConsoleInput.EditLine(selectedPerformance.startDate.ToString("dd-MM-yyyy HH:mm")) : Console.ReadLine();

            if (DateTime.TryParseExact(performanceStart, "d-M-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceStartDT))
            {
                if (editing == false)
                {
                    if (performanceStartDT < DateTime.Now) {
                        Console.WriteLine($"{Color.Red}You can't enter a date and time that is in the past.{Color.Reset}");
                    }
                    else {
                        performanceStartValid = true;
                    }
                } else {
                    performanceStartValid = true;
                }
            }
            else {
                Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid date and time format (DD-MM-YYYY HH:MM).{Color.Reset}");
            }
        }

        Console.Clear();
        // endDate
        while (performanceEndValid == false)
        {
            Console.WriteLine($"{Color.Yellow}When does it end? (DD-MM-YYYY HH:MM):{Color.Reset}");
            string performanceEnd = editing ? ConsoleInput.EditLine(selectedPerformance.endDate.ToString("dd-MM-yyyy HH:mm")) : Console.ReadLine();

            if (DateTime.TryParseExact(performanceEnd, "d-M-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceEndDT))
            {
                if (performanceEndDT < performanceStartDT) {
                    Console.WriteLine($"{Color.Red}You can't enter a date and time that is before the starttime of the Performance.{Color.Reset}");
                }
                else if (performanceEndDT > DateTime.Now.AddMonths(6)) {
                    Console.WriteLine($"{Color.Red}You can't enter a date and time that is more than 6 months ahead of the starttime.{Color.Reset}");
                }
                else {
                    performanceEndValid = true;
                }
            }
            else {
                Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please enter a valid date and time format (DD-MM-YYYY HH:MM).");
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

                    Console.WriteLine($"{Color.Yellow}In which hall do you want the Performance to take place? Enter the Hall ID:{Color.Reset}");
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
                        Console.WriteLine($"{Color.Red}A Hall with ID {hallId} does not exist.{Color.Reset}");
                    }
                }

            }
            catch (System.Exception)
            {
                Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a valid Hall ID.");
            }
        }
        //ticketTypes
        Console.Clear();
        bool donett = false;
        while(donett == false){
            int Amount;
            string Name;
            double Price = 0.0;
            Console.WriteLine($"{Color.Yellow}Add a ticket type with amount of tickets, the name of the ticket and the price.{Color.Reset}");
            while (true){
                Console.WriteLine("Amount:");
                string readAmmount = Console.ReadLine();
                if(int.TryParse(readAmmount, out int AmountV) && AmountV > 0){
                    Amount = AmountV;
                    break;
                    
                }else{
                    Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a valid Number.");
                }
            }
            while (true){
                Console.WriteLine("Name:");
                string readName = Console.ReadLine();
                if(readName.Length < 35){
                    Name = readName;
                    break;
                    
                }else{
                    Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Character of name must be below 35.");
                }
            }
            bool PriceIsValid = false;
            while (!PriceIsValid){
                Console.WriteLine("Price:");
                string readPrice = Console.ReadLine();
                var regex = new Regex(@"^\d+\.\d{2}?$"); // ^\d+(\.|\,)\d{2}?$ use this incase your dec separator can be comma or decimal.
                if (regex.IsMatch(readPrice)){
                    Price = Convert.ToDouble(readPrice);
                    PriceIsValid = true;
                    
                }else{
                    Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a price with 2 Decimals");
                }
            }
            Dictionary<string, object> ticketTypeAdd = new Dictionary<string, object>();
            ticketTypeAdd["amount"] = Amount;
            ticketTypeAdd["name"] = Name;
            ticketTypeAdd["price"] = Price;
            innerListOfDicts.Add(ticketTypeAdd);    

            Console.WriteLine($"{Color.Yellow}Would you like to add another ticket type? (Y/N){Color.Reset}");
            string inputadd = Console.ReadLine();
            if (inputadd.ToLower() != "y"){
                donett = true;
            }

            
        }
        Dictionary<string, object> dict2 = new Dictionary<string, object>();
        dict2["ticketTypes"] = innerListOfDicts;
        

        if (editing == true)
        {

            Console.Clear();
            // active
            if (selectedPerformance.active == false) {
                Console.WriteLine($"Current active state: {Color.Red}Inactive{Color.Reset}\n\n{Color.Yellow}Do you want to switch to {Color.Red}Active{Color.Yellow}? (Y/N){Color.Reset}");
            } else {
                Console.WriteLine($"Current active state: {Color.Green}Active{Color.Reset}\n\n{Color.Yellow}Do you want to switch to {Color.Red}Inactive{Color.Yellow}? (Y/N){Color.Reset}");
            }

            if (Console.ReadLine().ToLower() == "y") {
                active = !active;
            }

            HallLogic Hlogic = new HallLogic();
            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {Color.Reset}{performanceName}");
            Console.WriteLine($"{Color.Blue}Start: {Color.Reset}{performanceStartDT}");
            Console.WriteLine($"{Color.Blue}End: {Color.Reset}{performanceEndDT}");
            Console.WriteLine($"{Color.Blue}Hall: {Color.Reset}{Hlogic.getHallNamebyId(hallId)}");
            Console.WriteLine($"{Color.Blue}Active: {Color.Reset}{active}{Color.Reset}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to make these changes? (Y/N){Color.Reset}");
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
                    Console.WriteLine($"{Color.Green}The Performance was successfully edited.{Color.Reset}\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Color.Red}The Performance was not edited.{Color.Reset}\n");
                    break;
            }

        } else {

            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {Color.Reset}{performanceName}");
            Console.WriteLine($"{Color.Blue}Start: {Color.Reset}{performanceStartDT}");
            Console.WriteLine($"{Color.Blue}End: {Color.Reset}{performanceEndDT}");
            Console.WriteLine($"{Color.Blue}Hall: {Color.Reset}{hallId}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to add this Performance? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    int newId = logic.GetNewId();
                    HallLogic hlogic = new HallLogic();
                    bool[,] emptyseats = hlogic.GetSeatsOfHall(hallId);
                    Dictionary<string, object> dict1 = new Dictionary<string, object>();
                    
                    



                    for (int row = 0; (row < emptyseats.GetLength(0)); row++)
                    {
                        for (int col = 0; (col < emptyseats.GetLength(1)); col++)
                        {
                            emptyseats[row, col] = false;
                        }
                    }
                    dict1["seats"] = ConvertBoolArrayToIntArray(emptyseats);
                    listOfDicts.Add(dict1);
                    listOfDicts.Add(dict2);
                    Console.WriteLine("Boolean Array:");
                    for (int i = 0; i < emptyseats.GetLength(0); i++)
                    {
                        for (int j = 0; j < emptyseats.GetLength(1); j++)
                        {
                            Console.Write(emptyseats[i, j]);
                            Console.Write(" "); // Add space between elements
                        }
                        Console.WriteLine(); // Move to the next row
                    }
                    Console.ReadLine();
                    PerformanceModel performance = new PerformanceModel(newId, performanceName, description, performanceStartDT, performanceEndDT, hallId, listOfDicts, true);
                    logic.UpdateList(performance);
                    Console.Clear();
                    Console.WriteLine($"{Color.Green}The Performance was succesfully added.{Color.Reset}\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Color.Red}The Performance was not added.{Color.Reset}\n");
                    break;
            }
        }
    }

    static public void Delete(PerformanceLogic logic)
    {
        Console.WriteLine($"Enter the ID of the Performance you want to {Color.Red}delete{Color.Reset}: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.DeletePerformance(idToDelete))
            {
                Console.WriteLine($"{Color.Green}Performance with ID {idToDelete} deleted successfully.{Color.Reset}");
            }
            else
            {
                Console.WriteLine($"{Color.Red}Performance with ID {idToDelete} not found.{Color.Reset}");
            }
        }
        else
        {
            Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid ID.{Color.Reset}");
        }

    }

    static private int[][] ConvertBoolArrayToIntArray(bool[,] boolArray)
    {
        int rows = boolArray.GetLength(0);
        int cols = boolArray.GetLength(1);
        int[][] intArray = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            intArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                intArray[i][j] = boolArray[i, j] ? 1 : 0;
            }
        }
        return intArray;
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
        Console.WriteLine($"{Color.Yellow}Please select a Performance to edit:{Color.Reset}\n");

        Console.WriteLine("      {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
        Console.WriteLine("      ------------------------------------------------------------------------------------------------------------");

        int index = 0;
        foreach (PerformanceModel performance in logic.GetPerformances())
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write($"{Color.Green} >>");
            }
            else
            {
                Console.Write($"{Color.Reset}   ");
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
        Console.WriteLine($"{Color.Yellow}What do you want to do?{Color.Reset}\n");

        Console.WriteLine(selectedOption == 1 ? $"{Color.Green}>> View Performances{Color.Reset}" : "   View Performances");
        Console.WriteLine(selectedOption == 2 ? $"{Color.Green}>> Add a Performance{Color.Reset}" : "   Add a Performance");
        Console.WriteLine(selectedOption == 3 ? $"{Color.Green}>> Edit a Performance{Color.Reset}" : "   Edit a Performance");
        Console.WriteLine(selectedOption == 4 ? $"{Color.Green}>> Back to main menu{Color.Reset}" : "   Back to main menu");
    }
}