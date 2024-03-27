using System.Text.Json.Serialization;
using System.Text.Json;

class AccountModel 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    public static int TicketID { set; get; }

    public AccountModel(int id, string emailAddress, string password, string fullName, int ticketID)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        TicketID =  ticketID;
    }
    public void PrintTicketDetails(int ticketID)
    {
        string JSONfile = "Reservations.json";
        string jsonData = File.ReadAllText(JSONfile);
        Ticket ticket = JsonSerializer.Deserialize<Ticket>(jsonData);
        
        if (ticket.ticketID == ticketID)
        {
            Console.WriteLine($"Ticket ID: {ticket.ticketID}");
            Console.WriteLine($"User: {ticket.name}");
            Console.WriteLine($"Price: {ticket.price}");
            Console.WriteLine($"Seat: {ticket.seat}");
        }
        else
        {
            Console.WriteLine("Ticket not found.");
        }
    }
}

    






