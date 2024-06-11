using System.Reflection;
namespace test;

[TestClass]
public class TestHalls
{
    

    [TestMethod]
    public void TestGetList()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
        
        // Act
        List<HallsModel> halls = hallLogic.GetList();

        // Assert
        Assert.AreEqual(0, halls.Count);
    }

    [TestMethod]
    public void GetTotalHalls()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
        
        // Act
        int totalHalls = hallLogic.GetTotalHalls();

        // Assert
        Assert.AreEqual(0, totalHalls);
    }

    [TestMethod]
    public void TestGetNewId()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
        
        // Act
        int newId = hallLogic.GetNewId();

        // Assert
        Assert.AreEqual(1, newId);
    }

    [TestMethod]
    public void TestUpdateList()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
        int newId = hallLogic.GetNewId();

        HallsModel hallToAdd = new HallsModel(newId, "Hall A", "Medium", true);
        
        // Act
        hallLogic.UpdateList(hallToAdd);

        // Assert
        Assert.IsTrue(hallLogic.GetList().Contains(hallToAdd));
    }

    [TestMethod]
    public void TestDeletePerformance()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
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
        HallLogic hallLogic = new HallLogic();
        int id = 1;
        
        // Act
        string hall = hallLogic.getHallNamebyId(id);

        // Assert
        Assert.AreNotEqual("Hall A", hall);
    }


}