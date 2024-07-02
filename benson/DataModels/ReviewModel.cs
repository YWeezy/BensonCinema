using System.Text.Json;
using System.Text.Json.Serialization;

public class ReviewsModel
{
    
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("rating")]
    public int rating { get; set; }

    [JsonPropertyName("description")]
    public string description { get; set; }

    [JsonConstructor]

    public ReviewsModel(int id, int rating, string description)
    {
        this.id = id;
        this.rating = rating;
        this.description = description;
    
    }
}
