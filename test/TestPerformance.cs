using System.Reflection;
namespace test;

[TestClass]
public class TestPerformance
{

    public static string path;

    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/performances.json");
    }

    [TestMethod]
    public void TestGetPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
        
        // Act
        List<PerformanceModel> performances = performanceLogic.GetPerformances();

        // Assert
        Assert.AreEqual(0, performances.Count);
    }

    [TestMethod]
    public void TestGetNewId()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
        
        // Act
        int newId = performanceLogic.GetNewId();

        // Assert
        Assert.AreEqual(1, newId);
    }

    [TestMethod]
    public void TestUpdateList()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
        int newId = performanceLogic.GetNewId();

        PerformanceModel performanceToAdd = new PerformanceModel(newId, "Demo A", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, true);
        
        // Act
        performanceLogic.UpdateList(performanceToAdd);

        // Assert
        Assert.IsTrue(performanceLogic.GetPerformances().Contains(performanceToAdd));
    }

    [TestMethod]
    public void TestGetTotalPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
        
        // Act
        int totalPerformances = performanceLogic.GetTotalPerformances();

        // Assert
        Assert.AreEqual(1, totalPerformances);
    }

    [TestMethod]
    public void TestGetPerfById()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
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
        PerformanceLogic performanceLogic = new PerformanceLogic(path);
        int idToDelete = 1; // ID of the performance to delete
        
        // Act
        bool isDeleted = performanceLogic.DeletePerformance(idToDelete);

        // Assert
        Assert.IsTrue(isDeleted);
        Assert.IsNull(performanceLogic.GetPerfById(idToDelete));
    }


}