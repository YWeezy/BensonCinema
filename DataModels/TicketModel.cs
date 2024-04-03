using System;
using System.Text.Json.Serialization;

public class TicketModel
{
    [JsonPropertyName("PerformanceId")]
    public int PerformanceId { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Date")]
    public string Date { get; set; }

    [JsonPropertyName("Seat")]
    public string Seat { get; set; }

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    [JsonPropertyName("RelationId")]
    public string RelationId { get; set; }

    public TicketModel(){}

    public TicketModel(string seat, string title, string date, int id, decimal price)
    {
        Seat = seat;
        Title = title;
        Date = date;
        PerformanceId = id;
        Price = price;
        RelationId = Utils.LoggedInUser.Id;
    }
}
