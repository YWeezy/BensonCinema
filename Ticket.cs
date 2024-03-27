using System.Data;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;



public class Ticket
{
    public string name { set; get; }
    public int ticketID  {get; set;}
    public double price { set; get; }
    public int seat { set; get; }

    [JsonIgnore] // Ignore this property during serialization
    public static int NextTicketID { get; set; } = 1;

    public Ticket() { }

    public Ticket(string user, double price, int seat)
    {
        this.name = user;
        this.price = price;
        this.seat = seat;
        ticketID = NextTicketID++; // Assign the next available ticket ID
    }

    public static void Reserve(Ticket ticket)
    {
        string JSONfile = "Reservations.json";
        string Updatejson = JsonSerializer.Serialize(ticket);
        File.WriteAllText(JSONfile, Updatejson);

    }
}