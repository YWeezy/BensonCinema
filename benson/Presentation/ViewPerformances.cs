using System.Globalization;
using System.Runtime.CompilerServices;

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
                    Show(logic, selectedPerformanceIndex);
                    return;
                case ConsoleKey.Escape:
                    Start();
                    return;
                default:
                    break;
            }
        }
    }

    static public void Show(PerformanceLogic logic, int selectedPerformanceIndex)
    {
        PerformanceModel selectedPerformance = logic.GetActivePerformances()[selectedPerformanceIndex];
        HallLogic hallLogic = new HallLogic();

        Console.Clear();

        Console.WriteLine($"{Color.Yellow}{selectedPerformance.name}{Color.Reset}");
        Console.WriteLine($"\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Ut dignissim elit felis, sit amet porta erat pretium eu. Duis eget massa et turpis dictum bibendum at in turpis. Phasellus consequat faucibus ligula, eu hendrerit mi pretium et. Maecenas vehicula elementum luctus. Praesent porttitor augue id ante aliquet, nec bibendum libero aliquam. Praesent interdum at dui et blandit. Cras ut felis non erat tempor dignissim in maximus lorem. Nullam massa arcu, sagittis in convallis at, egestas ac urna. In a tortor rhoncus, interdum sem et, hendrerit enim. Vivamus turpis eros, tempus a condimentum ac, convallis vitae elit. Donec gravida ac metus vel vulputate. Duis nec nibh eget diam tincidunt malesuada.");
        Console.WriteLine($"\n{Color.Yellow}Time: {Color.Green}{selectedPerformance.startDate} {Color.Yellow}<-> {Color.Green}{selectedPerformance.endDate}{Color.Reset}");
        Console.WriteLine($"\n{Color.Yellow}Hall: {Color.Green}{hallLogic.GetHallNameById(selectedPerformance.hallId)}{Color.Reset}");
        Console.WriteLine($"\n{Color.Yellow}Tickets: {Color.Green}(5/60){Color.Reset}");
    }

    static private void DisplayPerformances(PerformanceLogic logic, int selectedPerformanceIndex)
    {
        Console.Clear();
        HallLogic hallLogic = new HallLogic();
        Console.WriteLine($"{Color.Yellow}Please select a Performance:{Color.Reset}\n");

        Console.WriteLine("      {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}", "ID", "Name", "Start", "End", "Hall");
        Console.WriteLine("      ----------------------------------------------------------------------------------------------------");

        int index = 0;
        foreach (PerformanceModel performance in logic.GetActivePerformances())
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write($"{Color.Green} >>");
            }
            else
            {
                Console.Write($"{Color.Reset}   ");
            }
            Console.WriteLine("   {0,-6}{1,-22}{2,-26}{3, -26}{4, -20}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId));

            index++;
        }
    }
}