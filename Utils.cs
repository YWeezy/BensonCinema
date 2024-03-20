
using System.Text.Json;

static class Utils
{
    public static bool userIsLoggedIn = false;
    public static void PrettyWrite(object obj, string fileName)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(fileName, jsonString);
    }

    public static T PrettyRead<T>(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    public static void AddToListAndWriteJson<T>(string fileName, T newItem)
    {
        
        // Read the existing JSON data
        T data = PrettyRead<T>(fileName);

        // Assuming the JSON file contains a list, add newItem to it
        if (data is System.Collections.IList list)
        {
            list.Add(newItem);
        }
        else
        {
            throw new InvalidOperationException("Data in the JSON file is not a list");
        }

        // Write the updated data back to the JSON file
        PrettyWrite(data, fileName);
    }
}
