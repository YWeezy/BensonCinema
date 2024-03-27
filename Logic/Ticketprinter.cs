using System;
using System.IO;
using System.Linq;
using System.Text.Json;


public class TicketPrinter
{
    public void PrintTicketDetails()
    {
        Console.WriteLine("Enter user name:");
        string userName = Console.ReadLine();

        string JSONfile = "./DataSources/Reservations.json";
        string jsonData = File.ReadAllText(JSONfile);
        Ticket[] tickets = JsonSerializer.Deserialize<Ticket[]>(jsonData);

        Ticket ticket = tickets.FirstOrDefault(t => t.name == userName);

        if (ticket != null)
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
