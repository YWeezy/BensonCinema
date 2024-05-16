using System;
using System.Linq;

public class TicketPresentation
{
    private PerformanceLogic performanceLogic = new PerformanceLogic();
    private TicketLogic ticketLogic = new TicketLogic();
    public void ShowAvailablePerformances()
    {
        performanceLogic.DisplayTable();
    }

    public void ReserveTicket()
    {
        ShowAvailablePerformances();

        Console.WriteLine("Enter the ID of the performance you want to reserve a ticket for:");
        if (int.TryParse(Console.ReadLine(), out int performanceId))
        {
            
            if (performanceId != null)
            {
                Console.Clear();
                Console.WriteLine($"Selected Performance:");
                Console.WriteLine("Enter seat:");

                string seat = Console.ReadLine();

                ticketLogic.GenerateTicket(performanceId, seat);

                Console.WriteLine("Ticket reserved successfully!");
                
        }
            else
            {
                Console.WriteLine("Invalid performance ID.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for performance ID.");
        }
    }

}