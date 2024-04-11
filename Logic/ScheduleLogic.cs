

public class ScheduleLogic
{

    private List<ScheduleModel> _schedules = new List<ScheduleModel>();
     
    public ScheduleLogic()
    {
        _schedules = ScheduleAccess.LoadAll();
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
        ScheduleAccess.WriteAll(_schedules);
    }
    
    public List<ScheduleModel> GetSchedules(string employeeName)
    {
        return _schedules.Where(s => s.Worker == employeeName).ToList();
    }

    public void RemoveSchedule(string selectedEmployee)
    {
        int index = _schedules.FindIndex(s => s.ID == selectedEmployee);
        if (index != -1)

        {
            _schedules.RemoveAt(index);
            ScheduleAccess.WriteAll(_schedules);
            Console.WriteLine($"Schedule for {selectedEmployee} removed succesfully.");
        }
        else 
        {
            Console.WriteLine($"Schedule for {selectedEmployee} not found.");
        }
    }

}

