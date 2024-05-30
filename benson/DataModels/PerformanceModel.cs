using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PerformanceModel {

    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("description")]
    public string description { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime startDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime endDate { get; set; }

    [JsonPropertyName("hallId")]
    public int hallId { get; set; }

    [JsonPropertyName("employees")]
    public List<string> employees { get; set; }

    [JsonPropertyName("ticketsAvailable")]
    public List<Dictionary<string, object>> ticketsAvailable { get; set; }

    [JsonPropertyName("listmaterials")]
    public  List<Dictionary<string, object>> listmaterials { get; set; }

    [JsonPropertyName("active")]
    public bool active { get; set; }

    public PerformanceModel(int id, string name, string description, DateTime startDate, DateTime endDate, int hallId, List<Dictionary<string, object>> listmaterials, List<Dictionary<string, object>> ticketsAvailable, bool active) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.startDate = startDate;
        this.endDate = endDate;
        this.hallId = hallId;
        this.listmaterials = listmaterials ?? new List<Dictionary<string, object>>();
        this.active = active;
        this.employees = new List<string>();
        this.ticketsAvailable = ticketsAvailable;
        
    }

}
