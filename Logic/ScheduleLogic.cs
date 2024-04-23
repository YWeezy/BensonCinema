

public class ScheduleLogic
{
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/schedule.json"));
    private List<ScheduleModel> _schedules = new List<ScheduleModel>();

    public ScheduleLogic()
    {

        _schedules = DataAccess<ScheduleModel>.LoadAll(path);
    }

    public void UpdateList(ScheduleModel schedule)
    {

        int index = _schedules.FindIndex(s => s.ID == schedule.ID);

        if (index != -1)
        {

            _schedules[index] = schedule;
            Console.WriteLine("Schedule updated succesfully.");
        }
        else
        {

            _schedules.Add(schedule);
            Console.WriteLine("New schedule added succesfully.");
        }
        DataAccess<ScheduleModel>.WriteAll(_schedules, path);
    }

    public List<ScheduleModel> GetSchedules(string employeeName)
    {
        return _schedules.Where(s => s.Worker == employeeName).ToList();
    }

    public void RemoveSchedule(string scheduleID)
    {
        int index = _schedules.FindIndex(s => s.ID == scheduleID);
        if (index != -1)

        {
            string removedEmployee = _schedules[index].Worker;
            _schedules.RemoveAt(index);
            DataAccess<ScheduleModel>.WriteAll(_schedules, path);
            Console.WriteLine($"Schedule for {removedEmployee} removed succesfully.");
        }
        else
        {
            string employeeUnknown = _schedules[index].Worker;
            Console.WriteLine($"Schedule for {employeeUnknown} not found.");
        }
    }

}

