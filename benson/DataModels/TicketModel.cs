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

    public TicketModel(string seat, string row, string type, string title, string location, string date, string time, int performanceId, string price)
    {
        Seat = seat ?? throw new ArgumentNullException(nameof(seat));
        Row = row ?? throw new ArgumentNullException(nameof(row));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Date = date ?? throw new ArgumentNullException(nameof(date));
        Time = time ?? throw new ArgumentNullException(nameof(time));
        PerformanceId = performanceId;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        if (!UnitTestDetector.IsInUnitTest) {
            RelationId = Utils.LoggedInUser.Id;
        }
    }
}
