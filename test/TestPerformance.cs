using System.Reflection;
namespace test;

[TestClass]
public class TestPerformance
{
    public int idToAddAndToRemove;
    [TestMethod]
    public void TestGetPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        
        // Act
        List<PerformancesModel> performances = performanceLogic.GetPerformances();

        // Assert
        Assert.AreEqual(1, performances.Count);
    }

    [TestMethod]
    public void TestGetNewId()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        
        // Act
        int newId = performanceLogic.GetNewId();

        // Assert
        Assert.AreEqual(7, newId);
    }

    [TestMethod]
    public void TestUpdateList()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        idToAddAndToRemove = performanceLogic.GetNewId();
        
        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> listOfDicts2 = new();
        

        PerformancesModel performanceToAdd = new PerformancesModel(idToAddAndToRemove, "Demo A", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true);
        
        // Act
        performanceLogic.UpdateList(performanceToAdd);

        // Assert
        Assert.IsTrue(performanceLogic.GetPerformances().Contains(performanceToAdd));
    }

    [TestMethod]
    public void TestGetTotalPerformances()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        
        // Act
        int totalPerformances = performanceLogic.GetTotalPerformances();

        // Assert
        Assert.AreEqual(2, totalPerformances);
    }

    [TestMethod]
    public void TestGetPerfById()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        int id = 7;
        // Act
        PerformancesModel performance = performanceLogic.GetPerfById(id);

        // Assert
        Assert.AreEqual(id, performance.id);
    }

    [TestMethod]
    public void TestDeletePerformance()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        int idToDelete = 7; // ID of the performance to delete
        
        // Act
        bool isDeleted = performanceLogic.DeletePerformance(idToDelete);

        // Assert
        Assert.IsTrue(isDeleted);
        Assert.IsNull(performanceLogic.GetPerfById(idToDelete));
    }


}