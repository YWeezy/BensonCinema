class PerformanceLogic {

    private List<PerformanceModel> _performances = new List<PerformanceModel>();

    public PerformanceLogic() {
        _performances = PerformanceAccess.LoadAll();
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

    public string GetList() {

        string listOfPerf = "List of performances:\n";
        listOfPerf += "------------------------\n";

        foreach (PerformanceModel performance in _performances)
        {
            listOfPerf += $"ID: {performance.id}\n";
            listOfPerf += $"Name: {performance.name}\n";
            listOfPerf += $"Start: {performance.startDate}\n";
            listOfPerf += $"End: {performance.endDate}\n";
            listOfPerf += $"Location: {performance.locationId}\n";
            listOfPerf += "------------------------\n";
        }

        return listOfPerf;
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