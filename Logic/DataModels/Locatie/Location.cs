using System.Text.Json;
using System.Text.Json.Serialization;

class Location
{
    public int LocationID;
	public string LocationName;
    public string Type;
    [JsonConstructor]
    public Location(int locationID, string locationName, string type){
        LocationID = locationID;
        LocationName = locationName;
        Type = type;
    }

    public static void insertLocation(string name, string type){

    }

}