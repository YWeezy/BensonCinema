using System.Text.Json.Serialization;

public class ScheduleModel
{
    [JsonPropertyName("workerNumber")]
    public string WorkerId { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("totalHours")]
    public string TotalHours { get; set;}

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }



    public ScheduleModel( string workerId, string position, string date, string totalHours, string startTime, string endTime)
    {
        this.WorkerId = workerId;
        this.Position = position;
        this.Date = date;
        this.TotalHours = totalHours;
        this.StartTime = startTime;
        this.EndTime = endTime;
    }
}
