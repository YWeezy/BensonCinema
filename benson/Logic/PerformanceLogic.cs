public class PerformanceLogic
{

    private List<PerformanceModel> _performances = new List<PerformanceModel>();
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/performances.json"));

    public PerformanceLogic()
    {
        _performances = DataAccess<PerformanceModel>.LoadAll(path);
    }

    public List<PerformanceModel> GetPerformances()
    {
        return _performances;
    }

    public int GetTotalPerformances()
    {
        return _performances.Count;
    }

    public void UpdateList(PerformanceModel perf)
    {
        //Find if there is already an model with the same id
        int index = _performances.FindIndex(s => s.id == perf.id);

        if (index != -1)
        {
            //update existing model
            _performances[index] = perf;
        }
        else
        {
            //add new model
            _performances.Add(perf);
        }
        DataAccess<PerformanceModel>.WriteAll(_performances, path);

    }

    public PerformanceModel GetPerfById(int id)
    {
        PerformanceModel? performance = _performances.FirstOrDefault(h => h.id == id);
        return performance != null ? performance : null;
    }

    public int GetNewId()
    {
        int currentId = _performances.Last().id;
        return currentId + 1;
    }

    public void DisplayTable()
    {

        HallLogic hallLogic = new HallLogic();

        Console.WriteLine("Table of all Performances:\n");

        Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -15}{6, -20}", "\u001b[34mID    ", "Name", "Start", "End", "Hall", "Active", "Employees\u001b[0m");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
        foreach (PerformanceModel performance in _performances)
        {
            string actstr = performance.active ? "Active" : "Inactive";
            string employeeString = "";
            foreach (string employee in performance.employees)
            {
                employeeString += employee;
                if (performance.employees.IndexOf(employee) == performance.employees.Count - 1) 
                {
                    
                }
                else
                {
                    employeeString += ", ";
                }
            }
            
            Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -15}{6, -20}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), actstr, employeeString);
        }
        Console.WriteLine("");

        return;
    }

    public bool DeletePerformance(int id)
    {
        PerformanceModel perfToRemove = _performances.Find(p => p.id == id);
        if (perfToRemove != null)
        {
            _performances.Remove(perfToRemove);
            DataAccess<PerformanceModel>.WriteAll(_performances, path);
            return true;
        }
        return false;
    }

}