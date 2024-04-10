class PerformanceLogic {

    private List<PerformanceModel> _performances = new List<PerformanceModel>();

    public PerformanceLogic() {
        _performances = PerformanceAccess.LoadAll();
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
        PerformanceAccess.WriteAll(_performances);

    }

    public int GetNewId() {
        int currentId = _performances.Last().id;
        return currentId + 1;
    }

    public void DisplayTable() {

        HallLogic hallLogic = new HallLogic();

        Console.WriteLine("Table of all Performances:\n");
        
        Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------");
        foreach (PerformanceModel performance in _performances)
        {   
            string actstr;
            if (performance.active)
            {
                actstr = "Active";
            }else{
                actstr = "Inactive";
            }
            Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -5}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), actstr);
        }
        Console.WriteLine("");

        return;
    }

    public bool DeletePerformance(int id) {
        PerformanceModel perfToRemove = _performances.Find(p => p.id == id);
        if (perfToRemove != null)
        {
            _performances.Remove(perfToRemove);
            PerformanceAccess.WriteAll(_performances);
            return true;
        }
        return false;
    }

}