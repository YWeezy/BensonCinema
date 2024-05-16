using System.Reflection;
namespace test;

[TestClass]
public class TestPerformance
{

    public static string performancePath;
    public static string hallPath;

    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        performancePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/performances.json");
        hallPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/halls.json");
    }

    [TestMethod]
    public void TestGetPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        
        // Act
        List<PerformanceModel> performances = performanceLogic.GetPerformances();

        // Assert
        Assert.AreEqual(0, performances.Count);
    }

    [TestMethod]
    public void TestGetNewId()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        
        // Act
        int newId = performanceLogic.GetNewId();

        // Assert
        Assert.AreEqual(1, newId);
    }

    [TestMethod]
    public void TestUpdateList()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        int newId = performanceLogic.GetNewId();

        List<Dictionary<string, object>> listOfDicts = new();

        PerformanceModel performanceToAdd = new PerformanceModel(newId, "Demo A", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts, true);
        
        // Act
        performanceLogic.UpdateList(performanceToAdd);

        // Assert
        Assert.IsTrue(performanceLogic.GetPerformances().Contains(performanceToAdd));
    }

    [TestMethod]
    public void TestGetTotalPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        
        // Act
        int totalPerformances = performanceLogic.GetTotalPerformances();

        // Assert
        Assert.AreEqual(1, totalPerformances);
    }

    [TestMethod]
    public void TestGetPerfById()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        int id = 1;
        
        // Act
        PerformanceModel performance = performanceLogic.GetPerfById(id);

        // Assert
        Assert.AreEqual(id, performance.id);
    }

    [TestMethod]
    public void TestDeletePerformance()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(performancePath);
        int idToDelete = 1; // ID of the performance to delete
        
        // Act
        bool isDeleted = performanceLogic.DeletePerformance(idToDelete);

        // Assert
        Assert.IsTrue(isDeleted);
        Assert.IsNull(performanceLogic.GetPerfById(idToDelete));
    }


}