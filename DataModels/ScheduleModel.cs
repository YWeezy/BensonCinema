using System.Text.Json.Serialization;

public class ScheduleModel
{
    [JsonPropertyName("worker")]
    public string Worker { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("totalHours")]
    public string TotalHours { get; set;}

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }



    public ScheduleModel( string worker, string date, string totalHours, string startTime, string endTime)
    {
        this.Worker = worker;
        this.Date = date;
        this.TotalHours = totalHours;
        this.StartTime = startTime;
        this.EndTime = endTime;
    }
}
