

public class ScheduleLogic
{

    private List<ScheduleModel> _schedules = new List<ScheduleModel>();
     
    public ScheduleLogic()
    {
        _schedules = ScheduleAccess.LoadAll();
    }

    public void UpdateList(ScheduleModel schedule)
    {
 
        int index = _schedules.FindIndex(s => s.WorkerId == schedule.WorkerId);

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
}
