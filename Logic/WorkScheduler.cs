using System.Collections.Generic;


public class WorkScheduler
{

    private List<Worker> workers;

    public WorkScheduler()
    {
        workers = new List<Worker>();

    }

    public void AddWorker(Worker worker)
    {
        workers.Add(worker);

    }

    public void SaveToFile(string fileName)
    {  
        Utils.PrettyWrite(workers, fileName);
    }

    public void LoadFromFile(string filename)
    {
        Utils.PrettyRead(workers, filename);
    }
}

