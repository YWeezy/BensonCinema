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

}