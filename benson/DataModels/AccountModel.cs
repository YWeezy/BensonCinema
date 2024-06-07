using System;
using System.Text.Json.Serialization;
using System.Text.Json;

public class AccountsModel
{
    [JsonPropertyName("Id")]
    public string Id { get; set; }

    [JsonPropertyName("EmailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("FullName")]
    public string FullName { get; set; }

    [JsonPropertyName("Password")]
    public string Password { get; set; }

    [JsonPropertyName("Role")]
    public UserRole Role { get; set; }

    [JsonPropertyName("Tickets")]
    public List<TicketModel> Tickets { get; set; } = new List<TicketModel>();

    public AccountsModel(string emailAddress, string fullName, string password, UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid().ToString();
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Role = role;
    }

    // public void LoadTickets()
    // {
    //     var tickets = TicketsAccess.LoadAll();
    //     foreach (TicketModel ticket in tickets)
    //     {
    //         if (ticket.RelationId == Id)
    //         {
    //             Tickets.Add(ticket);
    //         }
    //     }
    // }

    // public void SaveTickets()
    // {
    //     List<TicketModel> tickets = TicketsAccess.LoadAll();
    //     foreach (TicketModel ticket in Tickets)
    //     {
    //         if (ticket.RelationId == Id)
    //         {
    //             tickets.Add(ticket);
    //         }
    //     }
    //     TicketsAccess.WriteAll(tickets);
    // }

    public void AddTicket(TicketModel ticket)
    {
        Tickets.Add(ticket);
        // SaveTickets();
    }

    public void RemoveTicket(TicketModel ticket)
    {
        Tickets.Remove(ticket);
        // SaveTickets();
    }

}

public enum UserRole
{
    User,
    Employee,
    ContentManager
}
