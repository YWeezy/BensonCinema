static class EmployeeSchedule
{
  
    public static void Schedule()
    {  
        string path = "DataSources/schedule.json";
        Console.WriteLine("Welcome to the employee schedule page");

        Console.WriteLine("Do you want to see the schedule? (yes or no)");
        string response = Console.ReadLine().ToLower();

        if (response == "yes")
        {       
                ShowSchedule(path);
        }

        else     
        {
            Console.WriteLine("Do you want to add a new schedule? (yes or no)");
            string addResponse = Console.ReadLine().ToLower();

            if (addResponse == "yes")
            {       
                AddSchedule(path);
            }
            else 
            {
                Console.WriteLine("Okay, see you later");
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

        Console.WriteLine("Enter date:");
        string date = Console.ReadLine();

        Console.WriteLine("Enter total working hours:");
        string TotalHours = Console.ReadLine();

        Console.WriteLine("Enter start time:");
        string startTime = Console.ReadLine();

        Console.WriteLine("Enter end time:");
        string endTime = Console.ReadLine();

        ScheduleModel newSchedule = new ScheduleModel(workerId, fullName, position, date, TotalHours, startTime, endTime);

        ScheduleLogic scheduleLogicUp = new ScheduleLogic();
        scheduleLogicUp.UpdateList(newSchedule);
        }



    }
}