using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

static class TicketsAccess
{
    static string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/tickets.json"));

    public static List<TicketModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<TicketModel>>(json);
    }

    public static void WriteAll(List<TicketModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}
