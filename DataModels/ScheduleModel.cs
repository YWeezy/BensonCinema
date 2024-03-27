using System.Text.Json.Serialization;

public class ScheduleModel
{
    [JsonPropertyName("workerId")]
    public int WorkerId { get; set; }
    
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

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



    public ScheduleModel( int workerId, string fullName, string position, string date, string totalHours, string startTime, string endTime)
    {
        this.WorkerId = workerId;
        this.FullName = fullName;
        this.Position = position;
        this.Date = date;
        this.TotalHours = totalHours;
        this.StartTime = startTime;
        this.EndTime = endTime;
    }
}
