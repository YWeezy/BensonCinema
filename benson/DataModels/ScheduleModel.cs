using System.Text.Json.Serialization;

public class ScheduleModel
{
    [JsonPropertyName("id")]
    public string ID { get; set; }

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

    [JsonPropertyName("performance")]
    public PerformanceModel Performance { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }



    public ScheduleModel( string id, string worker, string date, string totalHours, string startTime, string endTime, PerformanceModel performance, bool active)
    {   this.ID = id;
        this.Worker = worker;
        this.Date = date;
        this.TotalHours = totalHours;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Performance = performance;
        this.Active = active;
    }
}
