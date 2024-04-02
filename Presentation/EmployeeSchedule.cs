using System.Runtime.InteropServices;

static class EmployeeSchedule
{
  
    public static void Schedule()
    {   
        string path = "DataSources/schedule.json";
        ScheduleLogic shell = new ScheduleLogic();
        Console.Clear();
        bool loop = true;
        while (loop) {
            Console.WriteLine("What do you want to do?\n");
            
            Console.WriteLine("1 - View schedules");
            Console.WriteLine("2 - Add schedules");
            Console.WriteLine("3 - Exit\n");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowSchedule(path);
                    break;

                case "2":
                    Console.Clear();
                    AddSchedule(path);
                    break;

                default:
                    loop = false;
                    break;
            }
        }

    static void ShowSchedule(string path)
    {
        try 
            {
                string json = File.ReadAllText(path);
                Console.WriteLine(json);
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
        Console.WriteLine("enter worker ID:");
        string workerId = Console.ReadLine();

        Console.WriteLine("Enter Full name:");
        string fullName = Console.ReadLine();

        Console.WriteLine("Enter position:");
        string position = Console.ReadLine();

        Console.WriteLine("Enter date: (DD-MM-YYYY)");
        string date = Console.ReadLine();

        Console.WriteLine("Enter total working hours for this date:");
        string TotalHours = Console.ReadLine();

        Console.WriteLine("Enter start time: (HH:MM)");
        string startTime = Console.ReadLine();

        Console.WriteLine("Enter end time: (HH:MM)");
        string endTime = Console.ReadLine();

        ScheduleModel newSchedule = new ScheduleModel(workerId, fullName, position, date, TotalHours, startTime, endTime);

        ScheduleLogic scheduleLogicUp = new ScheduleLogic();
        scheduleLogicUp.UpdateList(newSchedule);
        }



    }
}
