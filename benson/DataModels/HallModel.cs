using System.Text.Json;
using System.Text.Json.Serialization;

public class HallsModel
{
    [JsonPropertyName("hallID")]
    public int hallID { get; set; }
    [JsonPropertyName("hallName")]
    public string hallName { get; set; }

    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("active")]
    public bool active { get; set; }

    [JsonConstructor]

    public HallsModel(int HallID, string HallName, string Type, bool Active)
    {
        hallID = HallID;
        hallName = HallName;
        type = Type;
        active = Active;
    }


}