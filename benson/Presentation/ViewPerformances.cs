using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

static class ViewPerformances
{

    
    static public void Start() {
        PerformanceLogic logic = new PerformanceLogic();
        Edit(logic);
    }


    static public void Edit(PerformanceLogic logic)
    {
        Console.Clear();

        int selectedPerformanceIndex = 0;
        int totalPerformances = logic.GetActivePerformances().Count();

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
                    Show(logic, selectedPerformanceIndex, new string[] { "Buy ticket", "Back to previous menu" });
                    return;
                case ConsoleKey.Escape:
                    return;
                default:
                    break;
            }
        }
    }
    static public void ViewReservations(string id, PerformanceLogic logic)
    {
        Console.Clear();
        TicketLogic tLogic = new TicketLogic();
        List<TicketModel> myTickets = tLogic.loadMyticketsList(id);
        int selectedReservationIndex = 0;
        int totalReservations = myTickets.Count();

        while (true)
        {
            tLogic.loadMytickets(myTickets, selectedReservationIndex);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedReservationIndex = selectedReservationIndex == 0 ? totalReservations - 1 : selectedReservationIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedReservationIndex = selectedReservationIndex == totalReservations - 1 ? 0 : selectedReservationIndex + 1;
                    break;
                case ConsoleKey.R:
                    string[] Time = myTickets[selectedReservationIndex].Time.Split("-");
                    string dtimestr = $"{myTickets[selectedReservationIndex].Date} {Time[1]}";
                    DateTime dtime = DateTime.Parse(dtimestr); 
                    
                    if (DateTime.Now > dtime)
                    {
                        ViewReviews view = new ViewReviews(myTickets[selectedReservationIndex].PerformanceId);
                        view.Start();
                    }else{
                        Console.WriteLine($"{Color.Red}Cant View access reviews before the end of the performance!{Color.Reset}");
                        Thread.Sleep(3000);
                    }
                    


                    continue;
                case ConsoleKey.Escape:
                    return;
                default:
                    break;
            }
        }
    }

    static private void PerformAction(string option, int performanceId = 0)
    {
        switch (option)
        {
            case "Buy ticket":
                ShowSeats showSeats = new ShowSeats(performanceId);
                showSeats.SelectSeats();
                showSeats.SaveSeats(performanceId);
                Start();
                break;
            case "Back to previous menu":
                Start();
                break;
            default:
                break;
        }
    }

    static public void Show(PerformanceLogic logic, int selectedPerformanceIndex, string[] options)
    {
        int selectedOption = 0;
        int totalOptions = options.Length;
        while (true)
        {
            PerformancesModel selectedPerformance = logic.GetActivePerformances()[selectedPerformanceIndex];
            HallLogic hallLogic = new HallLogic();

            Console.Clear();

            Console.WriteLine($"{Color.Yellow}{selectedPerformance.name}{Color.Reset}");
            Console.WriteLine($"\n{selectedPerformance.description}");
            Console.WriteLine($"\n{Color.Yellow}Time: {Color.Green}{selectedPerformance.startDate.ToString().Substring(0, selectedPerformance.startDate.ToString().Length - 3)} {Color.Yellow}<-> {Color.Green}{selectedPerformance.endDate.ToString().Substring(0, selectedPerformance.endDate.ToString().Length - 3)}{Color.Reset}");
            Console.WriteLine($"\n{Color.Yellow}Hall: {Color.Green}{hallLogic.GetHallNameById(selectedPerformance.hallId)}{Color.Reset}");
            Console.WriteLine("");

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
                    PerformAction(options[selectedOption], selectedPerformance.id);
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
        Console.WriteLine($"{Color.Yellow}Please select a Performance:{Color.Reset}\n");

        Console.WriteLine("      {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}", "ID", "Name", "Start", "End", "Hall");
        Console.WriteLine("      ----------------------------------------------------------------------------------------------------");

        int index = 0;
        foreach (PerformancesModel performance in logic.GetActivePerformances())
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write($"{Color.Green} >>");
            }
            else
            {
                Console.Write($"{Color.Reset}   ");
            }
            Console.WriteLine("   {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}", performance.id, performance.name, performance.startDate.ToString("dd-MM-yyyy H:mm"), performance.endDate.ToString("dd-MM-yyyy H:mm"), hallLogic.GetHallNameById(performance.hallId));

            index++;
        }

        Console.WriteLine($"{Color.Cyan}\nPress ESC to go back to the Main Menu{Color.Reset}");
    }
}