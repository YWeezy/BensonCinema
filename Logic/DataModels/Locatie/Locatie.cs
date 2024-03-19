class Locatie
{
    public int locatieID;
	public string locatieNaam;
    public string Type;

    public Locatie(int ID, string naam, string type){
        locatieID = ID;
        locatieNaam = naam;
        Type = type;
    }
}