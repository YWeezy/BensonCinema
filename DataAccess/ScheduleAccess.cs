using System.Text.Json;

static class ScheduleAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/schedule.json"));


    public static List<ScheduleModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ScheduleModel>>(json);
    }


    public static void WriteAll(List<ScheduleModel> schedule)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(schedule, options);
        File.WriteAllText(path, json);
    }

    public static void AddSchedule()
    {
        List<ScheduleModel> schedule = LoadAll();

        Console.WriteLine("enter worker ID:");
        string workerId = Console.ReadLine();

        Console.WriteLine("Enter start time:");
        string startTime = Console.ReadLine();

        Console.WriteLine("Enter end time:");
        string endTime = Console.ReadLine();



    }


}