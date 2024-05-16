using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TicketLogic
{
    private PerformanceLogic performanceLogic = new PerformanceLogic();
    private List<PerformanceModel> Performances;

    private List<HallModel> _halls { get; }

    private List<TicketModel> _tickets;
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/tickets.json"));
    public TicketLogic()
    {

        _tickets = DataAccess<TicketModel>.LoadAll(path);
    }

    public void ShowAvailablePerformances()
    {
        performanceLogic.DisplayTable();
    }

    public void PrintPerformanceById(int id)
    {

        HallLogic hallLogic = new HallLogic();

        Console.WriteLine($"{Color.Yellow}Table of all Performances:{Color.Reset}\n");

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
            TicketModel ticket = new TicketModel(seat, "regular", performanceTitle, Location, performanceDate, StartTime + "-" + EndTime, id, 40);

            // Write the ticket to the data source
            List<TicketModel> tickets = DataAccess<TicketModel>.LoadAll(path);
            tickets.Add(ticket);
            DataAccess<TicketModel>.WriteAll(tickets, path);
        }
        else
        {
            Console.WriteLine("Performance not found.");
        }
    }
    public void loadMytickets(string id)
    {
        // Load all tickets from the data source
        List<TicketModel> allTickets = DataAccess<TicketModel>.LoadAll(path);

        // Filter tickets based on the provided user ID
        List<TicketModel> userTickets = allTickets.Where(t => t.RelationId == id).ToList();

        Console.WriteLine("Ticket ID              Title                 Date        Time                  Location              Seat             Price               ");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");

        // Print ticket details
        foreach (var ticket in userTickets)
        {
            Console.WriteLine($"{ticket.PerformanceId,-23}{ticket.Title,-22}{ticket.Date,-12}{ticket.Time,-22}{ticket.Location,-22}{ticket.Seat,-17}$ {ticket.Price,-12:F2}");
        }
    }
}
