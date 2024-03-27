using System.Data.Common;
using System.Text.Json.Serialization;

class PerformanceModel {

    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime startDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime endDate { get; set; }

    [JsonPropertyName("locationId")]
    public int locationId { get; set; }

    public PerformanceModel(int id, string name, DateTime startDate, DateTime endDate, int locationId) {
        this.id = id;
        this.name = name;
        this.startDate = startDate;
        this.endDate = endDate;
        this.locationId = locationId;
    }

}