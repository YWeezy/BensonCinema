using System.Collections.ObjectModel;
using System.Text.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

public  class EmployeeSchedule
{

    public static void Schedule()
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

    static void PerformAction(int option, ScheduleLogic shell)
    {
        string path = "DataSources/schedule.json";
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
                EditSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Schedule();
                break;
            case 4:
                Console.Clear();
                RemoveSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Schedule();
                break;
            case 5:
                Console.Clear();
                Menu.Start();
                break;
            default:
                break;
        }
    }

    static void DisplayMenu(int selectedOption)
    {
        string color = "\u001b[32m";
        string neutral = "\u001b[0m";
        Console.WriteLine($"{neutral}What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? color + ">> View Schedules\u001b[0m" : "   View Schedules ");
        Console.WriteLine(selectedOption == 2 ? color + ">> Add a Schedule \u001b[0m" : "   Add a Schedule");
        Console.WriteLine(selectedOption == 3 ? color + ">> Edit a Schedule \u001b[0m" : "   Edit a Schedule");
        Console.WriteLine(selectedOption == 4 ? color + ">> Remove a Schedule \u001b[0m" : "   Remove a Schedule");
        Console.WriteLine(selectedOption == 5 ? color + ">> Back to main menu \u001b[0m" : "   Back to main menu");


    }



    static void ShowSchedule(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            List<ScheduleModel> schedules = JsonSerializer.Deserialize<List<ScheduleModel>>(json);

            Console.WriteLine("Schedules for this week:\n");
            Console.WriteLine("\u001b[34m Worker        Date        Total Hours   Start Time  End Time  \u001b[0m");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var schedule in schedules)
            {
                Console.WriteLine($" {schedule.Worker,-12}  {schedule.Date,-9}  {schedule.TotalHours,-12}  {schedule.StartTime,-10}  {schedule.EndTime,-8} ");
            }
        }


        catch (FileNotFoundException)
        {
            Console.WriteLine("Json not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }


    static void AddSchedule(string path)
    {
        bool isStartTimeValid, isEndTimeValid;

        Console.WriteLine("Please choose the employee you want to add a schedule for:");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        string date;
        do
        {
            Console.WriteLine("\u001b[0mEnter date: (DD-MM-YYYY for the schedule(within 1-2 weeks from today))");
            date = Console.ReadLine();
        } while (!IsValidDate(date));
        TimeSpan newEndTime, newStartTime;
        string startTimeInput, endTimeInput;
        do 
        {
         


            Console.WriteLine("Enter Start Time: (HH:MM)");
            startTimeInput = Console.ReadLine();

            Console.WriteLine("Enter End Time: (HH:MM)");
            endTimeInput = Console.ReadLine();

            isStartTimeValid = TimeSpan.TryParse(startTimeInput, out newStartTime);
            isEndTimeValid = TimeSpan.TryParse(endTimeInput, out newEndTime);

            if (!isStartTimeValid)
            {
                Console.WriteLine("Invalid Start Time format.");
            }

            if (!isEndTimeValid)
            {
                Console.WriteLine("Invalid End Time format");
            }
            if (isStartTimeValid && isEndTimeValid)
            {
                if (newStartTime >= newEndTime)     
                {
                    Console.WriteLine("Start Time must be before End Time.");
                    isEndTimeValid = false;
                    isStartTimeValid = false;
                }
                else if (IsScheduleOverlap(selectedEmployee, date, newStartTime, newEndTime))
                {
                    Console.WriteLine("This schedule overlaps with an existing schedule. Please choose a different time slot.");
                    isStartTimeValid = false;
                    isEndTimeValid = false;
                }
            }
        } while (!isStartTimeValid || !isEndTimeValid);

        Console.Clear();
        TimeSpan totalHours = newEndTime.Subtract(newStartTime);
        Console.WriteLine($"Total working hours for this date: {totalHours} (HH-MM-SS)");

        PerformanceLogic logic = new PerformanceLogic();
        PerformanceModel selectedChoicePerf = ChoicePerf(logic, startTimeInput, endTimeInput, date);
        if (selectedChoicePerf == null){

        }
        else{
            selectedChoicePerf.employees.Add(selectedEmployee);
            logic.UpdateList(selectedChoicePerf);
        }
        
        

        string scheduleID = Guid.NewGuid().ToString();

        Console.WriteLine("The data you just entered has been saved.");

        ScheduleModel newSchedule = new ScheduleModel(scheduleID, selectedEmployee, date, totalHours.ToString(), startTimeInput, endTimeInput, selectedChoicePerf, true);

        ScheduleLogic scheduleLogicUp = new ScheduleLogic();
        scheduleLogicUp.UpdateList(newSchedule);
    }

    static void RemoveSchedule(string path)
    {
        string wrong = "\u001b[31m";
        string neutral = "\u001b[0m";

        Console.WriteLine($"{neutral}Please choose the employee you want to remove a schedule for:");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        ScheduleLogic scheduleLogic = new ScheduleLogic();
        List<ScheduleModel> schedules = scheduleLogic.GetSchedules(selectedEmployee);

        if (schedules.Count == 0)
        {
            Console.WriteLine($"{wrong}No existing schedules found for {selectedEmployee}");
            return;
        }

        int selectedScheduleIndex = 0;
        bool scheduleSelected = false;

        do
        {
            Console.Clear();
            Console.WriteLine($"Select a schedule to remove for {selectedEmployee}.");
            for (int i = 0; i < schedules.Count; i++)
            {
                if (i == selectedScheduleIndex)
                    Console.WriteLine($">> {i + 1}. Date: {schedules[i].Date}  -  Starttime:{schedules[i].StartTime} to Endtime:{schedules[i].EndTime}");
                else
                    Console.WriteLine($"   {i + 1}. Date: {schedules[i].Date}  -  Starttime:{schedules[i].StartTime} to Endtime:{schedules[i].EndTime}");
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedScheduleIndex = selectedScheduleIndex == 0 ? schedules.Count - 1 : selectedScheduleIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedScheduleIndex = selectedScheduleIndex == schedules.Count - 1 ? 0 : selectedScheduleIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    scheduleSelected = true;
                    break;
                default:
                    break;
            }

        } while (!scheduleSelected);

        ScheduleModel selectedSchedule = schedules[selectedScheduleIndex];

        scheduleLogic.RemoveSchedule(selectedSchedule.ID);
    }
    static void EditSchedule(string path)
    {
        string red = "\u001b[31m";
        string neutral = "\u001b[0m"; 
        Console.WriteLine($"{neutral}Please choose the employee whose schedule you want to edit:");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        ScheduleLogic scheduleLogic = new ScheduleLogic();
        List<ScheduleModel> schedules = scheduleLogic.GetSchedules(selectedEmployee);

        if (schedules.Count == 0)
        {
            Console.WriteLine($"{red}No existing schedules found for {selectedEmployee}");
            return;
        }

        int selectedScheduleIndex = 0;
        bool scheduleSelected = false;

        do
        {
            Console.Clear();
            string color = "\u001b[32m";
            string noncolor = "\u001b[0m";
            Console.WriteLine($"Select a schedule to edit for {selectedEmployee}.");
            for (int i = 0; i < schedules.Count; i++)
            {
                if (i == selectedScheduleIndex)
                    Console.WriteLine($"{color}>> {i + 1}. Date: {schedules[i].Date}  -  Starttime:{schedules[i].StartTime} to Endtime:{schedules[i].EndTime}\u001b[0m");
                else
                    Console.WriteLine($"{noncolor}   {i + 1}. Date: {schedules[i].Date}  -  Starttime:{schedules[i].StartTime} to Endtime:{schedules[i].EndTime}");
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedScheduleIndex = selectedScheduleIndex == 0 ? schedules.Count - 1 : selectedScheduleIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedScheduleIndex = selectedScheduleIndex == schedules.Count - 1 ? 0 : selectedScheduleIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    scheduleSelected = true;
                    break;
                default:
                    break;
            }

        } while (!scheduleSelected);

        ScheduleModel selectedSchedule = schedules[selectedScheduleIndex];

        Console.WriteLine("Enter new details");

        string date;
        do
        {
            Console.WriteLine("Enter date: (DD-MM-YYYY for the schedule(within 1-2 weeks from today))");
            date = ConsoleInput.EditLine(selectedSchedule.Date);
        } while (!IsValidDate(date));

        Console.WriteLine("Enter start time: (HH:MM)");
        string startTime = ConsoleInput.EditLine(selectedSchedule.StartTime);

        Console.WriteLine("Enter end time: (HH:MM)");
        string endTime = ConsoleInput.EditLine(selectedSchedule.EndTime);

        TimeSpan totalHours = TimeSpan.Parse(endTime).Subtract(TimeSpan.Parse(startTime));
        Console.WriteLine($"Total working hours for this date: {totalHours} (HH-MM-SS)");

        selectedSchedule.Date = date;
        selectedSchedule.StartTime = startTime;
        selectedSchedule.EndTime = endTime;
        selectedSchedule.TotalHours = totalHours.ToString();

        scheduleLogic.UpdateList(selectedSchedule);

    }

    static bool IsValidDate(string inputdate)
    {
        if (DateTime.TryParseExact(inputdate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            DateTime today = DateTime.Today;
            DateTime oneWeekLater = today.AddDays(7);
            DateTime twoWeeksLater = today.AddDays(14);

            if (parsedDate >= oneWeekLater && parsedDate <= twoWeeksLater)
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

            List<string> employees = accounts
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

    static private void DisplayPerformances(IEnumerable<PerformanceModel> scheduledPerf, int selectedPerformanceIndex)
    {
        Console.Clear();
        int index = 0;
        Console.WriteLine("\u001b[0m Select which performance to add to the schedule.");
        foreach (PerformanceModel performance in scheduledPerf)
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write("\u001b[32m >>");
            }
            else
            {
                Console.Write("\u001b[0m   ");
            }

    
            
    
            
            Console.WriteLine("   {0,-6}{1,-22}", performance.id, performance.name);

            index++;
        }
        Console.WriteLine("\u001b[0m Press ESC for no performance");
    }
        
    static public PerformanceModel ChoicePerf(PerformanceLogic logic, string startTime, string endTime, string date){
        
        int selectedPerformanceIndex = 0;

        PerformanceModel selectedPerf;
        TimeSpan startTimeS = TimeSpan.Parse(startTime);
        TimeSpan endTimeS = TimeSpan.Parse(endTime);
        DateTime datedt = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        DateTime startDatetime = datedt.Add(startTimeS);

        DateTime endDatetime = datedt.Add(endTimeS);
        Console.WriteLine(startDatetime);
        Console.WriteLine(endDatetime);
        List<PerformanceModel> allPerf = logic.GetPerformances();
        IEnumerable<PerformanceModel> scheduledPerf = allPerf.Where(el => el.startDate >= startDatetime && el.endDate <= endDatetime && el.active == true);
        int totalPerformances = scheduledPerf.Count();
        if (totalPerformances > 0){
            while (true)
            {   
                
                DisplayPerformances(scheduledPerf, selectedPerformanceIndex);
                
                
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
                        List<PerformanceModel> scheduledPerfList = scheduledPerf.ToList();
                        selectedPerf = logic.GetPerfById(scheduledPerfList[selectedPerformanceIndex].id);
                        return selectedPerf;
                    case ConsoleKey.Escape:
                        return null;
                    default:
                        break;
                }
            }
        }else{
            return null;
        }
        
    
    }

    static bool IsScheduleOverlap(string employee, string date, TimeSpan newStartTime, TimeSpan newEndTime)
    {
        ScheduleLogic scheduleLogic = new ScheduleLogic();
        List<ScheduleModel> schedules = scheduleLogic.GetSchedules(employee);

        DateTime newDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        foreach (var schedule in schedules)
        {
            TimeSpan existingStartTime = TimeSpan.Parse(schedule.StartTime);
            TimeSpan existingEndTime = TimeSpan.Parse(schedule.EndTime);

            DateTime existingDate = DateTime.ParseExact(schedule.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (existingDate == newDate &&
            ((newStartTime >= existingStartTime && newStartTime < existingEndTime) ||
            (newEndTime > existingStartTime && newEndTime <= existingEndTime) ||
            (newStartTime <= existingStartTime && newEndTime >= existingEndTime)))
            {
                return true;
            }
        }
        return false;
    }
}