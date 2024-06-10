using System.Text.Json.Serialization;

public class TicketType {
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }
}