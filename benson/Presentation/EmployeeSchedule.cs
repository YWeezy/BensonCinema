using System.Collections.ObjectModel;
using System.Text.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

public class EmployeeSchedule
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
        Console.WriteLine($"{Color.Yellow}What do you want to do?{Color.Reset}\n");

        Console.WriteLine(selectedOption == 1 ? $"{Color.Green}>> View Schedules{Color.Reset}" : "   View Schedules ");
        Console.WriteLine(selectedOption == 2 ? $"{Color.Green}>> Add a Schedule{Color.Reset}" : "   Add a Schedule");
        Console.WriteLine(selectedOption == 3 ? $"{Color.Green}>> Edit a Schedule{Color.Reset}" : "   Edit a Schedule");
        Console.WriteLine(selectedOption == 4 ? $"{Color.Green}>> Remove a Schedule{Color.Reset}" : "   Remove a Schedule");
        Console.WriteLine(selectedOption == 5 ? $"{Color.Green}>> Back to main menu{Color.Reset}" : "   Back to main menu");
    }

    public static void EmployeeMenu()
    {
        ScheduleLogic shell = new ScheduleLogic();
        bool loop = true;
        int selectedOption = 1; // Default selected option
        int totalOptions = 2; // Total number of options

        while (loop)
        {
            Console.Clear();
            DisplayEmployeeMenu(selectedOption);

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
                    PerformEmployeeAction(selectedOption, shell);
                    break;
                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;
        }
    }

    static void PerformEmployeeAction(int option, ScheduleLogic shell)
    {
        string path = "DataSources/schedule.json";
        switch (option)
        {
            case 1:
                Console.Clear();
                ShowSchedule(path);
                Console.WriteLine("Press Enter to return to the menu.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                EmployeeMenu();
                break;
            case 2:
                Console.WriteLine("Bye! Come back soon.");
                Thread.Sleep(2000);
                try
                {
                    string filePath = "./../benson/DataSources/IsLoggedIn.txt";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                }
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }


    static void DisplayEmployeeMenu(int selectedOption)
    {
        Console.WriteLine($"{Color.Yellow}What do you want to do?{Color.Reset}\n");

        Console.WriteLine(selectedOption == 1 ? $"{Color.Green}>> View the Work Schedule{Color.Reset}" : "   View the Work Schedule");
        Console.WriteLine(selectedOption == 2 ? $"{Color.Green}>> Exit{Color.Reset}" : "   Exit");
    }


    static void ShowSchedule(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            List<ScheduleModel> schedules = JsonSerializer.Deserialize<List<ScheduleModel>>(json);

            Console.WriteLine($"{Color.Yellow}Schedules for this week:\n{Color.Blue}");
            Console.WriteLine($" Worker            Date        Duration   Start Time  End Time  Active{Color.Reset}");
            Console.WriteLine("----------------------------------------------------------------------");

            foreach (var schedule in schedules)
            {
                string actstr = schedule.Active ? "Active" : "Inactive";
                Console.WriteLine($" {schedule.Worker,-16}  {schedule.Date,-9}  { schedule.TotalHours.ToString().Substring(0, schedule.TotalHours.ToString().Length - 3),-9}  {schedule.StartTime,-10}  {schedule.EndTime,-9} {actstr,-12}");
            }
        }


        catch (FileNotFoundException)
        {
            Console.WriteLine($"{Color.Red}Json not found.{Color.Reset}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{Color.Red}An error occured: {e.Message}{Color.Reset}");
        }
    }



    static void AddSchedule(string path)
    {
        bool isStartTimeValid, isEndTimeValid;

        Console.WriteLine($"{Color.Yellow}Please choose the employee you want to add a schedule for:{Color.Reset}");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        string date;
        do
        {
            Console.WriteLine($"{Color.Yellow}Enter date for the schedule{Color.Reset}");
            date = DateSelector.GetDate(10, true);
            IsValidDate(date);
        } while (!IsValidDate(date));
        TimeSpan newEndTime, newStartTime;
        string startTimeInput, endTimeInput;
        do
        {



            Console.WriteLine($"{Color.Yellow}Enter Start Time: (HH:MM){Color.Reset}");
            startTimeInput = DateSelector.GetTime(true);

            Console.WriteLine($"{Color.Yellow}Enter End Time: (HH:MM){Color.Reset}");
            endTimeInput = DateSelector.GetTime(false);

            isStartTimeValid = TimeSpan.TryParse(startTimeInput, out newStartTime);
            isEndTimeValid = TimeSpan.TryParse(endTimeInput, out newEndTime);

            if (!isStartTimeValid)
            {
                Console.WriteLine($"{Color.Red}Invalid Start Time format.{Color.Reset}");
                Thread.Sleep(2000);

            }

            if (!isEndTimeValid)
            {
                Console.WriteLine($"{Color.Red}Invalid End Time format.{Color.Yellow}");
                Thread.Sleep(2000);

            }
            if (isStartTimeValid && isEndTimeValid)
            {
                if (newStartTime >= newEndTime)
                {
                    Console.WriteLine($"{Color.Red}Start Time must be before End Time.{Color.Yellow}");
                    isEndTimeValid = false;
                    isStartTimeValid = false;
                    Thread.Sleep(2000);

                }
                else if (IsScheduleOverlap(selectedEmployee, date, newStartTime, newEndTime))
                {
                    Console.WriteLine($"{Color.Red}This schedule overlaps with an existing schedule. Please choose a different time slot.{Color.Yellow}");
                    isStartTimeValid = false;
                    isEndTimeValid = false;
                    Thread.Sleep(2000);

                }
            }
        } while (!isStartTimeValid || !isEndTimeValid);

        Console.Clear();
        TimeSpan totalHours = newEndTime.Subtract(newStartTime);
        Console.WriteLine($"{Color.Blue}Total working hours for this date: {totalHours} (HH-MM-SS){Color.Reset}");

        PerformanceLogic logic = new PerformanceLogic();
        PerformanceModel selectedChoicePerf = ChoicePerf(logic, startTimeInput, endTimeInput, date);
        if (selectedChoicePerf == null)
        {

        }
        else
        {
            selectedChoicePerf.employees.Add(selectedEmployee);
            logic.UpdateList(selectedChoicePerf);
        }



        string scheduleID = Guid.NewGuid().ToString();

        Console.WriteLine($"{Color.Green}The data you just entered has been saved.{Color.Reset}");

        ScheduleModel newSchedule = new ScheduleModel(scheduleID, selectedEmployee, date, totalHours.ToString(), startTimeInput, endTimeInput, selectedChoicePerf, true);

        ScheduleLogic scheduleLogicUp = new ScheduleLogic();
        scheduleLogicUp.UpdateList(newSchedule);
    }

    static void RemoveSchedule(string path)
    {

        Console.WriteLine($"{Color.Yellow}Please choose the employee you want to remove a schedule for:{Color.Reset}");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        ScheduleLogic scheduleLogic = new ScheduleLogic();
        List<ScheduleModel> schedules = scheduleLogic.GetSchedules(selectedEmployee);

        if (schedules.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No existing schedules found for {selectedEmployee}{Color.Reset}");
            Thread.Sleep(2000);
            return;
        }

        int selectedScheduleIndex = 0;
        bool scheduleSelected = false;

        do
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Select a schedule to remove for {selectedEmployee}.{Color.Reset}");
            for (int i = 0; i < schedules.Count; i++)
            {
                if (i == selectedScheduleIndex)
                    Console.WriteLine($"{Color.Green}>> {i + 1}. Date: {schedules[i].Date}  -  {schedules[i].StartTime} to {schedules[i].EndTime}{Color.Reset}");
                else
                    Console.WriteLine($"   {i + 1}. Date: {schedules[i].Date}  -  {schedules[i].StartTime} to {schedules[i].EndTime}");
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
        Console.WriteLine($"{Color.Yellow}Please choose the employee whose schedule you want to edit:{Color.Reset}");
        string selectedEmployee = SelectEmployee();
        if (selectedEmployee == null)
            return;

        ScheduleLogic scheduleLogic = new ScheduleLogic();
        List<ScheduleModel> schedules = scheduleLogic.GetSchedules(selectedEmployee);

        if (schedules.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No existing schedules found for {selectedEmployee}{Color.Reset}");
            return;
        }

        int selectedScheduleIndex = 0;
        bool scheduleSelected = false;

        do
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Select a schedule to edit for {selectedEmployee}.{Color.Reset}");
            for (int i = 0; i < schedules.Count; i++)
            {
                if (i == selectedScheduleIndex)
                    Console.WriteLine($"{Color.Green}>> {i + 1}. Date: {schedules[i].Date}  -  {schedules[i].StartTime} to {schedules[i].EndTime}{Color.Reset}");
                else
                    Console.WriteLine($"{Color.Reset}   {i + 1}. Date: {schedules[i].Date}  -  {schedules[i].StartTime} to {schedules[i].EndTime}");
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


        Console.WriteLine($"{Color.Yellow}Enter new details.{Color.Reset}");


        string date;
        do
        {
            Console.WriteLine($"{Color.Yellow}Enter date:{Color.Reset}");
            date = DateSelector.GetDate(10, true, DateTime.Parse(selectedSchedule.Date).Date);
            IsValidDate(date);
        } while (!IsValidDate(date));
        TimeSpan newEndTime, newStartTime;
        string startTimeInput, endTimeInput;

        Console.WriteLine($"{Color.Yellow}Enter Start Time: (HH:MM){Color.Reset}");
        startTimeInput = DateSelector.GetTime(true, DateTime.Parse(selectedSchedule.Date).Date);

        Console.WriteLine($"{Color.Yellow}Enter End Time: (HH:MM){Color.Reset}");
        endTimeInput = DateSelector.GetTime(false, DateTime.Parse(selectedSchedule.Date).Date);

        TimeSpan totalHours = TimeSpan.Parse(endTimeInput).Subtract(TimeSpan.Parse(startTimeInput));
        Console.WriteLine($"{Color.Blue}Total working hours for this date: {totalHours} (HH-MM-SS){Color.Reset}");

        selectedSchedule.Date = date;
        selectedSchedule.StartTime = startTimeInput;
        selectedSchedule.EndTime = endTimeInput;
        selectedSchedule.TotalHours = totalHours.ToString();

        string IsActive;
        while (true)
        {
            Console.WriteLine($"{Color.Yellow}Is the schedule active? (Y/N){Color.Reset}");
            IsActive = Console.ReadLine().Trim().ToUpper();
            if (IsActive == "Y" || IsActive == "N")
            {
                break;
            }
        }
        selectedSchedule.Active = (IsActive == "Y") ? true : false;

        scheduleLogic.UpdateList(selectedSchedule);

    }

    public static bool IsValidDate(string inputdate)
    {
        if (DateTime.TryParseExact(inputdate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            return true;
        }
        else
        {
            Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid date and time format (DD-MM-YYYY HH:MM).{Color.Reset}");
            Thread.Sleep(2000);

            return false;
        }

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
            Console.WriteLine($"{Color.Red}File not found.{Color.Reset}");
            return null;
        }
    }

    static private void DisplayPerformances(IEnumerable<PerformanceModel> scheduledPerf, int selectedPerformanceIndex)
    {
        Console.Clear();
        int index = 0;
        Console.WriteLine($"{Color.Yellow}Select which performance to add to the schedule.{Color.Reset}");
        foreach (PerformanceModel performance in scheduledPerf)
        {
            if (index == selectedPerformanceIndex)
            {
                Console.Write($"{Color.Green}>> ");
            }
            else
            {
                Console.Write($"{Color.Red}   ");
            }


            Console.WriteLine("   {0,-6}{1,-22}", performance.id, performance.name);

            index++;
        }
        Console.WriteLine($"{Color.Reset}Press ESC for no performance");
    }

    static public PerformanceModel ChoicePerf(PerformanceLogic logic, string startTime, string endTime, string date)
    {
        int selectedPerformanceIndex = 0;
        PerformanceModel selectedPerf = null;

        TimeSpan startTimeS = TimeSpan.Parse(startTime);
        TimeSpan endTimeS = TimeSpan.Parse(endTime);
        DateTime datedt = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        DateTime startDatetime = datedt.Add(startTimeS);
        DateTime endDatetime = datedt.Add(endTimeS);

        List<PerformanceModel> allPerf = logic.GetPerformances();

        IEnumerable<PerformanceModel> scheduledPerf = allPerf.Where(el => el.startDate <= endDatetime && el.endDate >= startDatetime && el.active);

        // Output or process the scheduled performances for debugging
        foreach (var perf in allPerf)
        {
            Console.WriteLine($"Performance: {perf.name}, Start: {perf.startDate}, End: {perf.endDate}, Active: {perf.active}");
            Console.WriteLine(perf.startDate <= endDatetime);
            Console.WriteLine(perf.endDate >= startDatetime);
        }

        int totalPerformances = scheduledPerf.Count();
        if (totalPerformances > 0)
        {
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
        }
        else
        {
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

            if (existingDate == newDate && schedule.Active == true &&
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