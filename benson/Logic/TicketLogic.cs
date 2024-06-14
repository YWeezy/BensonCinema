using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TicketLogic
{
    private List<TicketModel> _tickets;

    public TicketLogic()
    {
        _tickets = DataAccess<TicketModel>.LoadAll();
        
        
    }

    public void GenerateTicket(int id, string seat, string row, int price)
    {
        HallLogic hallLogic = new HallLogic();
        PerformanceLogic PLogic = new PerformanceLogic();
        PerformancesModel performance = PLogic.GetPerfById(id);
        if (performance != null)
        {
            string performanceTitle = performance.name;
            string performanceDate = performance.startDate.ToShortDateString();
            string StartTime = performance.startDate.ToShortTimeString();
            string EndTime = performance.startDate.ToShortTimeString();
            int hallid = performance.hallId;
            string Location = hallLogic.GetHallNameById(hallid);
            string formattedTotalPrice = price.ToString("F2");
            // Create a new ticket model
            TicketModel ticket = new TicketModel(seat, row, "regular", performanceTitle, Location, performanceDate, StartTime + "-" + EndTime, id, formattedTotalPrice);

            // Write the ticket to the data source
            List<TicketModel> tickets = DataAccess<TicketModel>.LoadAll();
            tickets.Add(ticket);
            DataAccess<TicketModel>.WriteAll(tickets);
        }
        else
        {
            Console.WriteLine("Performance not found.");
        }
    }
    public void loadMytickets(string id)
    {
        Console.Clear();
        // Load all tickets from the data source
        List<TicketModel> allTickets = DataAccess<TicketModel>.LoadAll();

        // Filter tickets based on the provided user ID
        List<TicketModel> userTickets = allTickets.Where(t => t.RelationId == id).ToList();
        Console.WriteLine($"{Color.Yellow}Table of your reservations:{Color.Reset}");
        Console.WriteLine();
        Console.WriteLine($"{Color.Blue}Ticket ID    Title                 Date        Time             Location           Seat                Price{Color.Reset}");
        Console.WriteLine($"----------------------------------------------------------------------------------------------------------------");

        // Print ticket details
        foreach (var ticket in userTickets)
        {
            Console.WriteLine($"{ticket.PerformanceId,-13}{ticket.Title,-22}{ticket.Date,-12}{ticket.Time,-17}{ticket.Location,-19}Row {ticket.Row}/Seat {ticket.Seat,-9}€{Convert.ToDouble(ticket.Price) / 100,-12:F2}");
        }
    }
}
