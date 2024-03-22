using System.Text.Json;
using System.Text.Json.Serialization;

class LocationModel
{
    public int locationID { get; set; }
	public string locationName { get; set; }
    public string type { get; set; }
    [JsonConstructor]
    public LocationModel(int LocationID, string LocationName, string Type){
        locationID = LocationID;
        locationName = LocationName;
        type = Type;
    }

    
}