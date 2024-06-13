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
        
        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> listOfDicts2 = new();
        

        PerformancesModel performanceToAdd = new PerformancesModel(1, "Demo A", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true);
        PerformancesModel performanceToAdd2 = new PerformancesModel(2, "Demo B", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true);
        PerformancesModel performanceToAdd3 = new PerformancesModel(3, "Demo B", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true);
        // Act
        performanceLogic.UpdateList(performanceToAdd);
        performanceLogic.UpdateList(performanceToAdd2);

        // Assert
        Assert.IsTrue(performanceLogic.GetPerformances().Contains(performanceToAdd));
        Assert.IsTrue(performanceLogic.GetPerformances().Contains(performanceToAdd2));
        Assert.IsFalse(performanceLogic.GetPerformances().Contains(performanceToAdd3));
    }

    [TestMethod]
    public void TestEditUpdateList()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        
        List<Dictionary<string, object>> listOfDicts = new();
        List<Dictionary<string, object>> listOfDicts2 = new();
        

        PerformancesModel performanceToAdd = new PerformancesModel(1, "Demo ACED", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true);
        
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
        Assert.AreEqual(3, totalPerformances);
    }

    [TestMethod]
    public void TestGetPerfById()
    {
        // Arrange
        PerformanceLogic performanceLogic = new PerformanceLogic();
        int id = 1;
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
        int idToDelete = 1;
        int idToDelete2 = 2;
        int idToDelete3 = 3;
        // Act
        bool isDeleted = performanceLogic.DeletePerformance(idToDelete);
        bool isDeleted2 = performanceLogic.DeletePerformance(idToDelete2);
        bool isDeleted3 = performanceLogic.DeletePerformance(idToDelete3);

        // Assert
        Assert.IsTrue(isDeleted);
        Assert.IsTrue(isDeleted2);
        Assert.IsFalse(isDeleted3);
    }


}