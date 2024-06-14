using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestReserve
{   
    private List<TicketModel> _tickets;

    [TestCleanup]
    // Clean up (remove added halls and performances)
    public void TestCleanup()
    {
        // Arrange
        var loadedTickets = DataAccess<TicketModel>.LoadAll();
        // Act
        loadedTickets.Clear();
        // Write the empty list back to the JSON file
        DataAccess<TicketModel>.WriteAll(loadedTickets);

        // Clean up halls and performances
        int performanceId = 4;
        int hallId = 9;
        HallLogic hallLogic = new HallLogic();
        PerformanceLogic performanceLogic = new PerformanceLogic();
        hallLogic.Delete(hallId);
        performanceLogic.DeletePerformance(performanceId);
    }
    
    [TestMethod]
    public void GenerateTicketsTest()
    {
        // Arrange
        var ticketLogic = new TicketLogic();
        HallLogic hallLogic = new HallLogic();
        PerformanceLogic performanceLogic = new PerformanceLogic();

        int performanceId = 4;
        int hallId = 9;

        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> listOfDicts2 = new();
        HallsModel hallToAdd3 = new HallsModel(9, "Hall C", "Large", true);
        
        // Act
        hallLogic.UpdateList(hallToAdd3);
        
        // Update performance information
        PerformancesModel performanceToAdd = new PerformancesModel(4, "TEST", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), hallId, listOfDicts2, listOfDicts, true);
        performanceLogic.UpdateList(performanceToAdd);
        
        // Generate three tickets for the same performance
        ticketLogic.GenerateTicket(performanceId, "A1", "Row1", 10);
        ticketLogic.GenerateTicket(performanceId, "A2", "Row1", 10);
        ticketLogic.GenerateTicket(performanceId, "A3", "Row1", 10);

        // Load all tickets again after generation
        var loadedTickets = DataAccess<TicketModel>.LoadAll();
        // Count tickets with the specified performance ID
        var ticketsWithSameId = loadedTickets.Count(t => t.PerformanceId == performanceId);

        // Assert
        Assert.AreEqual(3, ticketsWithSameId);
    }
}
