using System;
using System.Text.Json.Serialization;

public class TicketModel
{
    [JsonPropertyName("PerformanceId")]
    public int PerformanceId { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Location")]
    public string Location { get; set; }

    [JsonPropertyName("Date")]
    public string Date { get; set; }

    [JsonPropertyName("Time")]
    public string Time { get; set; }

    [JsonPropertyName("Seat")]
    public string Seat { get; set; }

    [JsonPropertyName("Row")]
    public string Row { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("Price")]
    public string Price { get; set; }

    [JsonPropertyName("RelationId")]
    public string RelationId { get; set; }

    public TicketModel() { }

    public TicketModel(string seat, string row, string type, string title, string location, string date, string time, int id, string price)
    {
        Seat = seat;
        Row = row;
        Type = type;
        Title = title;
        Location = location;
        Date = date;
        Time = time;
        PerformanceId = id;
        Price = price;
        RelationId = Utils.LoggedInUser.Id;
    }
}
