using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace test;
using System.Text.Json;

[TestClass]
public class TestMaterial
{
    
    
    
    [TestMethod]
    public void TestAddingMaterial()
    {
        MaterialsLogic logic = new MaterialsLogic();
        MaterialsModel add = new("Wood", 20, "Decor");
        MaterialsModel add2 = new("Wood", 20, "Decor");
        MaterialsModel add3 = new("Person1", 4, "Puppeteers");

        //Act
        logic.insertMaterial(add);
        logic.insertMaterial(add2);
        logic.insertMaterial(add3);
        List<MaterialsModel> list = logic.GetList();

        //Assert
        MaterialsModel result = new("Wood", 40, "Decor");
        Assert.IsTrue(list.Contains(add3));
        MaterialsModel added = list.FirstOrDefault(h => h.material == "Wood");
        System.Console.WriteLine(added.quantity);
        System.Console.WriteLine(added.material);
        System.Console.WriteLine(added.type);
        Assert.AreEqual(result.quantity, added.quantity);
    }

    [TestMethod]
    public void Clean()
    {
        MaterialsLogic logic = new MaterialsLogic();
        List<MaterialsModel> list = logic.GetList();
        for (int i = 0; i < 2; i++)
        {
            logic.delete(0);
        }
    }

   
    
}