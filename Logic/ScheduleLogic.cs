

public class ScheduleLogic
{

    private List<ScheduleModel> _schedules = new List<ScheduleModel>();
     
    public ScheduleLogic()
    {
        _schedules = ScheduleAccess.LoadAll();
    }

    public void UpdateList(ScheduleModel schedule)
    {
 
        int index = _schedules.FindIndex(s => s.Worker == schedule.Worker);

        if (index != -1)
        {
            
            _schedules[index] = schedule;
        }
        else
        {
            
            _schedules.Add(schedule);
        }
        ScheduleAccess.WriteAll(_schedules);

    }
    public void RemoveSchedule(string selectedEmployee)
    {
        int index = _schedules.FindIndex(s => s.Worker == selectedEmployee);
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

