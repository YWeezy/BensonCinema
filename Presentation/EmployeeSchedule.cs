

static class EmployeeSchedule
{
  
    public static void Schedule()
    {  
        string path = "/DataSources/schedule.json";
        Console.WriteLine("Welcome to the employee schedule page");

        try 
        {
            string json = File.ReadAllText(path);
            Console.WriteLine(json);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("json not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }
}