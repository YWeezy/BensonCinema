using System.Reflection;
namespace test;

[TestClass]
public class TestHalls
{
    string hallPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/halls.json");

    [TestMethod]
    public void TestGetList()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        
        // Act
        List<HallModel> halls = hallLogic.GetList();

        // Assert
        Assert.AreEqual(0, halls.Count);
    }

    [TestMethod]
    public void GetTotalHalls()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        
        // Act
        int totalHalls = hallLogic.GetTotalHalls();

        // Assert
        Assert.AreEqual(0, totalHalls);
    }

    [TestMethod]
    public void TestGetNewId()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        
        // Act
        int newId = hallLogic.GetNewId();

        // Assert
        Assert.AreEqual(1, newId);
    }

    [TestMethod]
    public void TestUpdateList()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        int newId = hallLogic.GetNewId();

        HallModel hallToAdd = new HallModel(newId, "Hall A", "Medium", true);
        
        // Act
        hallLogic.UpdateList(hallToAdd);

        // Assert
        Assert.IsTrue(hallLogic.GetList().Contains(hallToAdd));
    }

    [TestMethod]
    public void TestDeletePerformance()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        int idToDelete = 1;
        
        // Act
        bool isDeleted = hallLogic.Delete(idToDelete);

        // Assert
        Assert.IsTrue(isDeleted);
    }

    [TestMethod]
    public void TestGetHallNamebyId()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic(hallPath);
        int id = 1;
        
        // Act
        string hall = hallLogic.getHallNamebyId(id);

        // Assert
        Assert.AreNotEqual("Hall A", hall);
    }


}