using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TicketLogic
{
    private PerformanceLogic performanceLogic = new PerformanceLogic();
    private List<PerformanceModel> Performances;

    private List<HallModel> _halls {get;}

    private List<TicketModel> Tickets;
    public TicketLogic()
    {
        Performances = PerformanceAccess.LoadAll();
    }

    public void ShowAvailablePerformances()
    {
        performanceLogic.DisplayTable();
    }

        public void PrintPerformanceById(int id) {

        HallLogic hallLogic = new HallLogic();

        Console.WriteLine("Table of all performances:\n");
        
        Console.WriteLine("{0,-6}{1,-22}{2,-21}{3, -21}{4, -20}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        foreach (PerformanceModel performance in Performances)
        {
            if (performance.id == id)
            {
                Console.WriteLine("{0,-6}{1,-22}{2,-21}{3, -21}{4, -20}{5, -5}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), performance.active);
            }
        
       }

        Console.WriteLine("");

        return;
    }


    public void GenerateTicket(int id, string seat)
    {
        HallLogic hallLogic = new HallLogic();
        PerformanceModel performance = Performances.FirstOrDefault(p => p.id == id);
        if (performance != null)
        {
            string performanceTitle = performance.name;
            string performanceDate = performance.startDate.ToShortDateString();
            string StartTime = performance.startDate.ToShortTimeString();
            string EndTime = performance.startDate.ToShortTimeString();
            int hallid = performance.hallId;
            string Location = hallLogic.GetHallNameById(hallid);

            // Create a new ticket model
            TicketModel ticket = new TicketModel(seat, performanceTitle,Location, performanceDate, StartTime + "-" +EndTime, id, 40);

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
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("|              Ticket Details             |");
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine($"| Ticket ID: {ticket.PerformanceId,-28} |");
        Console.WriteLine($"| Title: {ticket.Title,-32} |");
        Console.WriteLine($"| Date: {ticket.Date,-33} |");
        Console.WriteLine($"| Time: {ticket.Time,-33} |"); // Added line for time
        Console.WriteLine($"| Location: {ticket.Location,-29} |"); // Added line for location
        Console.WriteLine($"| Seat: {ticket.Seat,-33} |");
        Console.WriteLine($"| Price: {ticket.Price,-32} |");    
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine(); 
    }
}






}
