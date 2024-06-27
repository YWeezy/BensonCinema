using System.Text.Json;
using System.Text.Json.Serialization;

public class MaterialsModel
{
    
    [JsonPropertyName("material")]
    public string material { get; set; }

    [JsonPropertyName("quantity")]
    public int quantity { get; set; }

    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("occupation")]
    public List<Dictionary<string, object>> occupation { get; set; }

    [JsonConstructor]

    public MaterialsModel(string Material, int Quantity, string Type, List<Dictionary<string, object>> Occupation = null)
    {
        material = Material;
        quantity = Quantity;
        type = Type;
        occupation = Occupation;
    }


}