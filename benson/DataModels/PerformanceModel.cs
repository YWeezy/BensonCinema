using System.Data.Common;
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
    public List<(List<(int, string, double)>, bool[, ])> ticketsAvailable { get; set; }

    [JsonPropertyName("active")]
    public bool active { get; set; }

    public PerformanceModel(int id, string name, string description, DateTime startDate, DateTime endDate, int hallId, List<(int, string, double)> typeTickets, bool active) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.startDate = startDate;
        this.endDate = endDate;
        this.hallId = hallId;
        this.active = active;
        this.employees = new List<string>();
        HallLogic logic = new HallLogic();
        bool[,] emptyseats = logic.GetSeatsOfHall(hallId);
        for (int row = 0; (row < emptyseats.GetLength(0)); row++)
        {
            for (int col = 0; (col < emptyseats.GetLength(1)); col++)
            {
                emptyseats[row, col] = true;
            }
        }

        this.ticketsAvailable = new List<(List<(int, string, double)>, bool[, ])>{(typeTickets, emptyseats)};
    }

}