using System.Text.Json;
static class LocationAccess
{
    static private readonly string path = "./DataSources/locations.json";
    public static List<LocationModel> Locationget(){
        List<LocationModel> locs = new List <LocationModel>();
        try
        {
            // Read the JSON file
            string content = File.ReadAllText(path);
            var options = new JsonSerializerOptions { IncludeFields = true };
            // Deserialize the JSON array into a list of Location objects
            locs = JsonSerializer.Deserialize<List<LocationModel>>(content, options);
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
    public static void WriteAll(List<LocationModel> locations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(locations, options);
        File.WriteAllText(path, json);
    }
}
