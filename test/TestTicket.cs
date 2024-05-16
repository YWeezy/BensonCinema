using System.Reflection;
namespace test;

[TestClass]
public class TestTicket
{
    string ticketPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/tickets.json");

    // W.I.P. tickets worden nog geupdate dus dit is nog niet werkend.

    // [TestMethod]
    // public void TestGenerateTicket()
    // {
    //     // Arrange
    //     TicketLogic ticketLogic = new TicketLogic(ticketPath);
    //     string seat = "A1";
    //     int performanceId = 1;

    //     // Act
    //     ticketLogic.GenerateTicket(performanceId, seat);

    //     // Assert
    //     bool ticketExists = ticketLogic.GetList().Any(t => t.Seat == seat && t.PerformanceId == performanceId);
    //     Assert.IsTrue(ticketExists);
    // }

}