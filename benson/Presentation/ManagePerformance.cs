using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
public class ManagePerformance : IScreen
{
    void IScreen.Start()
    {
        ManagePerformance.Start();
    }
    static public void Start()
    {
        PerformanceLogic logic = new PerformanceLogic();
        bool loop = true;
        int selectedOption = 1;
        int totalOptions = 4;

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

            if (key == ConsoleKey.Enter)
                break;
        }
    }

    static public void Update(PerformanceLogic logic, int selectedPerformanceIndex = -1)
    {
        bool editing = false;
        PerformancesModel selectedPerformance = null;
        bool active = true;
        if (selectedPerformanceIndex != -1)
        {
            editing = true;
            selectedPerformance = logic.GetPerformances()[selectedPerformanceIndex];
            active = selectedPerformance.active;
        }
        HallLogic hallLogic = new HallLogic();
        string performanceName = null;
        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> innerListOfDicts = new();
        string description = null;
        string performanceStart;
        string performanceEnd;
        bool performanceStartValid = false;
        bool performanceEndValid = false;
        int hallId = 0;
        DateTime performanceStartDT = DateTime.MinValue;
        DateTime performanceEndDT = DateTime.MinValue;

        Console.Clear();
        while (string.IsNullOrEmpty(performanceName))
        {
            Console.WriteLine($"{Color.Yellow}Performance name:{Color.Reset}");
            performanceName = editing ? ConsoleInput.EditLine(selectedPerformance.name) : Console.ReadLine();

            if (string.IsNullOrEmpty(performanceName))
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please provide a Performance name.{Color.Reset}");
                Thread.Sleep(2000);

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
                Thread.Sleep(2000);
            }
        }

        // starttime
        Console.Clear();
        while (!performanceStartValid)
        {
            Console.WriteLine($"{Color.Yellow}Select the performance start date and time:{Color.Reset}");
            if (editing)
            {
                performanceStart = DateSelector.GetDate(10, true, selectedPerformance.startDate.Date) + " " + DateSelector.GetTime(true, selectedPerformance.startDate);
            }
            else
            {
                performanceStart = DateSelector.GetDate(10, true) + " " + DateSelector.GetTime(true);
            }



            if (DateTime.TryParseExact(performanceStart, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceStartDT))
            {
                performanceStartValid = true;
            }
            else
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid date and time format (DD-MM-YYYY HH:MM).{Color.Reset}");
                Thread.Sleep(2000);
            }
        }


        // endtime
        Console.Clear();
        while (!performanceEndValid)
        {
            Console.WriteLine($"{Color.Yellow}Select the performance end date and time:{Color.Reset}");

            if (editing)
            {
                performanceEnd = DateSelector.GetDate(10, false, selectedPerformance.endDate.Date) + " " + DateSelector.GetTime(false, selectedPerformance.endDate);
            }
            else
            {
                performanceEnd = DateSelector.GetDate(10, false) + " " + DateSelector.GetTime(false);
            }

            if (DateTime.TryParseExact(performanceEnd, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out performanceEndDT))
            {
                if (performanceEndDT < performanceStartDT)
                {
                    Console.WriteLine($"{Color.Red}You can't enter a date and time that is before the starttime of the Performance.{Color.Reset}");
                    Thread.Sleep(2000);

                }
                else if (performanceEndDT > DateTime.Now.AddMonths(6))
                {
                    Console.WriteLine($"{Color.Red}You can't enter a date and time that is more than 6 months ahead of the starttime.{Color.Reset}");
                    Thread.Sleep(2000);

                }
                else
                {
                    performanceEndValid = true;
                }
            }
            else
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid date and time format (DD-MM-YYYY HH:MM).{Color.Reset}");
                Thread.Sleep(2000);

            }
        }

        Console.Clear();
        while (hallId == 0)
        {
            try
            {
                bool performanceHallValid = false;

                while (!performanceHallValid)
                {

                    hallLogic.DisplayTable(true);

                    Console.WriteLine($"{Color.Yellow}In which hall do you want the Performance to take place? Enter the Hall ID:{Color.Reset}");
                    hallId = editing ? Convert.ToInt32(ConsoleInput.EditLine(selectedPerformance.hallId)) : Convert.ToInt32(Console.ReadLine());
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/halls.json"));
                    List<HallsModel> locs = DataAccess<HallsModel>.LoadAll();

                    bool idExists = locs.Any(loc => loc.hallID == hallId);

                    if (idExists)
                    {
                        performanceHallValid = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{Color.Red}A Hall with ID {hallId} does not exist.{Color.Reset}");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a valid Hall ID.");
            }
        }
        //Material adding.
        Console.Clear();
        //Boolean for while loop
        bool material = false;
        //Lists for handeling materials
        List<Dictionary<string, object>> materials = new();
        List<Dictionary<string, object>> edited_material = new();
        if (editing)
        {
            material = true;
            edited_material = logic.EditMaterials(selectedPerformance.listmaterials);
        }
        while (!material)
        {
            materials = logic.AddMaterials();
            material = true;
        }

        //TicketTypes
        Dictionary<string, object> dictTickets = new Dictionary<string, object>();
        if (!editing)
        {

            Console.Clear();
            bool donett = false;
            int maxSeats = hallLogic.GetSeatsCount(hallId);
            while (donett == false)
            {
                int Amount;
                string Name;
                double Price = 0.0;

                Console.WriteLine($"{Color.Yellow}Add a ticket type with amount of tickets, the name of the ticket and the price.{Color.Reset}");

                while (true)
                {


                    Console.WriteLine($"Amount: (Max {maxSeats})");
                    string readAmmount = Console.ReadLine();

                    if (int.TryParse(readAmmount, out int AmountV) && AmountV > 0 && AmountV <= maxSeats)
                    {
                        maxSeats -= AmountV;
                        Amount = AmountV;
                        break;

                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a valid Number.");
                    }
                }
                while (true)
                {
                    Console.WriteLine("Name:");
                    string readName = Console.ReadLine();
                    if (readName.Length < 35)
                    {
                        Name = readName;
                        break;

                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Character of name must be below 35.");
                    }
                }
                bool PriceIsValid = false;
                while (!PriceIsValid)
                {
                    Console.WriteLine("Price:");
                    string readPrice = Console.ReadLine();
                    var regex = new Regex(@"^\d+\.\d{2}?$"); // ^\d+(\.|\,)\d{2}?$ use this incase your dec separator can be comma or decimal.
                    if (regex.IsMatch(readPrice))
                    {
                        Price = Convert.ToDouble(readPrice);
                        PriceIsValid = true;

                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please provide a price with 2 Decimals");
                    }
                }
                Dictionary<string, object> ticketTypeAdd = new Dictionary<string, object>();
                ticketTypeAdd["amount"] = Amount;
                ticketTypeAdd["name"] = Name;
                ticketTypeAdd["price"] = Price;
                innerListOfDicts.Add(ticketTypeAdd);

                if (maxSeats == 0)
                {
                    donett = true;
                }
                else
                {
                    Console.WriteLine($"{Color.Yellow}Would you like to add another ticket type? (Y/N){Color.Reset}");
                    string inputadd = Console.ReadLine();
                    if (inputadd.ToLower() != "y")
                    {
                        donett = true;
                    }
                    Console.Clear();
                }



            }

            dictTickets["ticketTypes"] = innerListOfDicts;
        }

        if (editing == true)
        {
            Console.Clear();
            Console.WriteLine($"Current active state: {(selectedPerformance.active ? $"{Color.Green}Active{Color.Reset}" : $"{Color.Red}Inactive{Color.Reset}")}\n\n{Color.Yellow}Do you want to switch to {(selectedPerformance.active ? $"{Color.Red}Inactive{Color.Yellow}" : $"{Color.Green}Active{Color.Yellow}")}? (Y/N){Color.Reset}");

            if (Console.ReadLine().ToLower() == "y")
            {
                active = !active;
            }

            HallLogic Hlogic = new HallLogic();
            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {Color.Reset}{performanceName}");
            Console.WriteLine($"{Color.Blue}Description: {Color.Reset}{description}");
            Console.WriteLine($"{Color.Blue}Start: {Color.Reset}{performanceStartDT}");
            Console.WriteLine($"{Color.Blue}End: {Color.Reset}{performanceEndDT}");
            Console.WriteLine($"{Color.Blue}Hall: {Color.Reset}{Hlogic.GetHallNameById(hallId)}");
            Console.WriteLine($"{Color.Blue}Active: {Color.Reset}{active}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to make these changes? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                selectedPerformance.name = performanceName;
                selectedPerformance.description = description;
                selectedPerformance.startDate = performanceStartDT;
                selectedPerformance.endDate = performanceEndDT;
                selectedPerformance.hallId = hallId;
                selectedPerformance.listmaterials = edited_material;
                selectedPerformance.active = active;
                logic.UpdateList(selectedPerformance);
                Console.Clear();
                Console.WriteLine($"{Color.Green}The Performance was successfully edited.{Color.Reset}\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{Color.Red}The Performance was not edited.{Color.Reset}\n");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {Color.Reset}{performanceName}");
            Console.WriteLine($"{Color.Blue}Description: {Color.Reset}{description}");
            Console.WriteLine($"{Color.Blue}Start: {Color.Reset}{performanceStartDT}");
            Console.WriteLine($"{Color.Blue}End: {Color.Reset}{performanceEndDT}");
            Console.WriteLine($"{Color.Blue}Hall: {Color.Reset}{hallId}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to add this Performance? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                int newId = logic.GetNewId();
                HallLogic hlogic = new HallLogic();
                int[,] emptyseats = hlogic.GetSeatsOfHall(hallId);
                Dictionary<string, object> dictSeats = new Dictionary<string, object>();
                dictSeats["seats"] = ConvertInt2DArrayToIntJArray(emptyseats);
                listOfDicts.Add(dictSeats);
                listOfDicts.Add(dictTickets);
                PerformancesModel performance = new PerformancesModel(newId, performanceName, description, performanceStartDT, performanceEndDT, hallId, materials, listOfDicts, true);
                logic.UpdateList(performance);
                Console.Clear();
                Console.WriteLine($"{Color.Green}The Performance was succesfully added.{Color.Reset}\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{Color.Red}The Performance was not added.{Color.Reset}\n");
            }
        }
    }

    static public void Delete(PerformanceLogic logic)
    {
        Console.WriteLine($"Enter the ID of the Performance you want to {Color.Red}delete{Color.Reset}: ");
        if (int.TryParse(Console.ReadLine(), out int idToDelete))
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

    public static int[][] ConvertInt2DArrayToIntJArray(int[,] intarray)
    {
        int rows = intarray.GetLength(0);
        int cols = intarray.GetLength(1);
        int[][] intArray = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            intArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                intArray[i][j] = intarray[i, j] != 0 ? 9 : 0;
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
        foreach (PerformancesModel performance in logic.GetPerformances())
        {
            Console.Write(index == selectedPerformanceIndex ? $"{Color.Green} >>" : $"{Color.Reset}   ");
            string actstr = performance.active ? "Active" : "Inactive";

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
