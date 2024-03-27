using System.Text.Json.Serialization;

public class Worker
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; }

    [JsonPropertyName("totalHours")]
    public double totalHours { get; set;}

    [JsonPropertyName("startTime")]
    public double startTime { get; set; }

    [JsonPropertyName("endTime")]
    public double endTime { get; set; }



    public Worker(string name, string position, double totalHours, double startTime, double endTime)
    {
        this.Name = name;
        this.Position = position;
        this.totalHours = totalHours;
        this.startTime = startTime;
        this.endTime = endTime;
    }
}
