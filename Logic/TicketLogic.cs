using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TicketLogic
{
    private PerformanceLogic performanceLogic = new PerformanceLogic();
    private List<PerformanceModel> Performances;

    private List<TicketModel> Tickets;
    public TicketLogic()
    {
        Performances = PerformanceAccess.LoadAll();
    }

    public void ShowAvailablePerformances()
    {
        performanceLogic.DisplayTable();
    }

    public void PrintPerformanceById(int id)
    {
        PerformanceModel performance = Performances.FirstOrDefault(p => p.id == id);
        Console.WriteLine($"Name: {performance.name}");
        if (performance != null)
        {
            Console.WriteLine($"Performance ID: {performance.id}");
            Console.WriteLine($"Name: {performance.name}");
            Console.WriteLine($"Start Date: {performance.startDate}");
            Console.WriteLine($"End Date: {performance.endDate}");
            Console.WriteLine($"Hall ID: {performance.hallId}");
        }
        else
        {
            Console.WriteLine("Performance not found.");
        }
    }

    public void GenerateTicket(int id, string seat)
    {
        PerformanceModel performance = Performances.FirstOrDefault(p => p.id == id);
        if (performance != null)
        {
            // Assuming you have the performance title and date available
            string performanceTitle = performance.name;
            string performanceDate = performance.startDate.ToShortDateString();
            // Create a new ticket model
            TicketModel ticket = new TicketModel(seat, performanceTitle, performanceDate, id, 40);

            // Write the ticket to the data source
            List<TicketModel> tickets = TicketsAccess.LoadAll();
            tickets.Add(ticket);
            TicketsAccess.WriteAll(tickets);
        }
        else
        {
            Console.WriteLine("Performance not found.");
        }
    }

    public void loadMytickets(string id)
    {
        // Load all tickets from the data source
        List<TicketModel> allTickets = TicketsAccess.LoadAll();
        
        // Filter tickets based on the provided user ID
        List<TicketModel> userTickets = allTickets.Where(t => t.RelationId == id).ToList();
        
        foreach (var ticket in userTickets)
        {
            Console.WriteLine($"Ticket ID: {ticket.PerformanceId}");
            Console.WriteLine($"Title: {ticket.Title}");
            Console.WriteLine($"Date: {ticket.Date}");
            Console.WriteLine($"Seat: {ticket.Seat}");
            Console.WriteLine($"Price: {ticket.Price}");
            Console.WriteLine($"Relation ID: {ticket.RelationId}");
            Console.WriteLine(); 
        }
    }

}
