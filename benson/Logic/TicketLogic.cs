using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TicketLogic
{
    private List<TicketModel> _tickets;
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/tickets.json"));

    public TicketLogic(bool test = false)
    {
        _tickets = DataAccess<TicketModel>.LoadAll();
        
        
    }

    // public void ShowAvailablePerformances()
    // {
    //     performanceLogic.DisplayTable();
    // }

    // public void PrintPerformanceById(int id)
    // {

    //     HallLogic hallLogic = new HallLogic();

    //     Console.WriteLine($"{Color.Yellow}Table of all Performances:{Color.Reset}\n");

    //     Console.WriteLine("{0,-6}{1,-22}{2,-21}{3, -21}{4, -20}{5, -5}", "ID", "Name", "Start", "End", "Hall", "Active");
    //     Console.WriteLine("------------------------------------------------------------------------------------------------");
    //     foreach (PerformancesModel performance in Performances)
    //     {
    //         if (performance.id == id)
    //         {
    //             Console.WriteLine("{0,-6}{1,-22}{2,-21}{3, -21}{4, -20}{5, -5}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), performance.active);
    //         }

    //     }

    //     Console.WriteLine("");

    //     return;
    // }

    // public List<TicketsModel> GetList()
    // {
    //     return _tickets;
    // }

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
            string formattedTotalPrice = (price / 100.0).ToString("F2");
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
        Console.WriteLine($"{Color.Yellow}Table of Your Reservations:{Color.Reset}");
        Console.WriteLine();
        Console.WriteLine($"{Color.Yellow}Ticket ID              Title                 Date        Time                  Location              Seat                        Price               {Color.Reset}");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");

        // Print ticket details
        foreach (var ticket in userTickets)
        {
            Console.WriteLine($"{ticket.PerformanceId,-23}{ticket.Title,-22}{ticket.Date,-12}{ticket.Time,-22}{ticket.Location,-22}Row {ticket.Row}/Seat {ticket.Seat,-17}$ {ticket.Price,-12:F2}");
        }
    }
}
