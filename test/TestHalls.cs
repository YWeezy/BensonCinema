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
        HallsModel hallToAdd = new HallsModel(1, "Hall A", "Medium", true);
        HallsModel hallToAdd2 = new HallsModel(2, "Hall B", "Large", true);
        HallsModel hallToAdd3 = new HallsModel(3, "Hall C", "Large", true);
        // Act
        hallLogic.UpdateList(hallToAdd);
        hallLogic.UpdateList(hallToAdd2);

        // Assert
        Assert.IsTrue(hallLogic.GetList().Contains(hallToAdd));
        Assert.IsTrue(hallLogic.GetList().Contains(hallToAdd2));
        Assert.IsFalse(hallLogic.GetList().Contains(hallToAdd3));
    }

    [TestMethod]
    public void TestEditUpdateList()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();

        HallsModel editedHallToAdd = new HallsModel(1, "Hall ACE", "Medium", true);
        
        // Act
        hallLogic.UpdateList(editedHallToAdd);

        // Assert
        Assert.IsTrue(hallLogic.GetList().Contains(editedHallToAdd));
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
        Assert.AreEqual("Hall ACE", hall);
    }

    [TestMethod]
    public void TestDeletePerformance()
    {
        // Arrange
        HallLogic hallLogic = new HallLogic();
        int idToDelete = 1;
        int idToDelete2 = 2;
        int idToDelete3 = 3;
        // Act
        bool isDeleted = hallLogic.Delete(idToDelete);
        bool isDeleted2 = hallLogic.Delete(idToDelete2);
        bool isDeleted3 = hallLogic.Delete(idToDelete3);

        // Assert
        Assert.IsTrue(isDeleted);
        Assert.IsTrue(isDeleted2);
        Assert.IsFalse(isDeleted3);
    }

    
    


}