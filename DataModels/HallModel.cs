using System.Text.Json;
using System.Text.Json.Serialization;

class HallModel
{
    public int hallID { get; set; }
	public string hallName { get; set; }
    public string type { get; set; }
    [JsonConstructor]
    public HallModel(int HallID, string HallName, string Type){
        hallID = HallID;
        hallName = HallName;
        type = Type;
    } 

    
}