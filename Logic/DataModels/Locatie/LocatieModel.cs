static class LocatieModel
{
    static private readonly string path = "././DataSources/locaties.json";

    public
    public static List<> LocatieAvailable(){
        string text = File.ReadAllText(@"./person.json");
        var person = JsonSerializer.Deserialize<Person>(text);

        return List
    }
}