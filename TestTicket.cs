using System.Reflection;
namespace test;

[TestClass]
public class TestPerformance
{
    public static string ticketPath;

    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        ticketPath= Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/tickets.json");
  }

    [TestMethod]
    public void TestGenerateTicket()
    {
        // Arrange
        TicketLogic ticketLogic = new TicketLogic();
        string seat = "A1";
        int performanceId = 1;

        // Act
        ticketLogic.GenerateTicket(performanceId, seat);

        // Assert
        var tickets = DataAccess<TicketModel>.LoadAll(ticketPath);
        bool ticketExists = tickets.Any(t => t.Seat == seat && t.PerformanceId == performanceId);
        Assert.IsTrue(ticketExists);
    }

}