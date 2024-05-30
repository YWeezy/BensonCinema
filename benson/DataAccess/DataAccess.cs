using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class DataAccess<T>
{
    public static List<T> LoadAll(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        catch (FileNotFoundException)
        {
            Console.WriteLine($"The JSON file can not be found. Please provide the right path to JSON file.");
            Thread.Sleep(2000);

        }
        catch (JsonException)
        {
            Console.WriteLine($"The JSON format is not valid. Please enter a path where the JSON file is valid.");
            Thread.Sleep(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong with loading the database:{ex.Message}");
            Thread.Sleep(2000);
        }
        return null;

    }

    public static void WriteAll(List<T> accounts, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(filePath, json);
    }
}
