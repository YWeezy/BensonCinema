using System.Text.Json;
static class LocationJsonModel
{
    static private readonly string path = "locations.json";
    public static List<Location> Locationget(){
        List<Location> locs = new List <Location>();
        try
        {
            // Read the JSON file
            string content = File.ReadAllText(path);
            var options = new JsonSerializerOptions { IncludeFields = true };
            // Deserialize the JSON array into a list of Person objects
            locs = JsonSerializer.Deserialize<List<Location>>(content, options);
            // Print out the information of each person
            Console.WriteLine(locs);
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
}
