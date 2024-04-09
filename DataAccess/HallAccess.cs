using System.Text.Json;
static class HallAccess
{
    static private readonly string path = "./DataSources/halls.json";
    public static List<HallModel> Hallget(){
        List<HallModel> locs = new List <HallModel>();
        try
        {
            // Read the JSON file
            string content = File.ReadAllText(path);
            var options = new JsonSerializerOptions { IncludeFields = true };
            // Deserialize the JSON array into a list of Hall objects
            locs = JsonSerializer.Deserialize<List<HallModel>>(content, options);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The file was not found. Please make sure the file exists.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON format. Please make sure the JSON file is formatted correctly.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
        return locs;
    }
    public static void WriteAll(List<HallModel> Halls)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(Halls, options);
        File.WriteAllText(path, json);
    }
}
