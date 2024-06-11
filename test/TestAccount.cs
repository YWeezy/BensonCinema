using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace test;
using System.Text.Json;

[TestClass]
public class UnitTest1
{
    
    
    
    [TestMethod]
    public void TestAccountEncryption()
    {
        string password = "ThisIsNotSafe";
        string hashedPassword = Utils.Encrypt(password);
        Assert.AreNotEqual(password, hashedPassword);
    }

    [TestMethod]
    public void TestAccountCreation()
    {
        AccountsLogic logic = new AccountsLogic();
        AccountsModel user = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        logic.UpdateList(user);
        Assert.AreEqual(user.EmailAddress, "achraf.aarab@benson.com");
        Assert.AreEqual(user.FullName, "Achraf Aarab");
        Assert.AreEqual(user.Role, UserRole.User);
    }
    [TestMethod]

    public void TestLogin()
    {
        AccountsLogic logic = new AccountsLogic();
        
        AccountsModel userresult = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        AccountsModel user = logic.CheckLogin("achraf.aarab@benson.com","achraf");
        AccountsModel userfail = logic.CheckLogin("achraf.aarab@benson.com","abahraf");
        Console.WriteLine(user);
        Console.WriteLine(userresult);
        Assert.AreEqual(user.EmailAddress, "achraf.aarab@benson.com");
        Assert.AreEqual(user.FullName, "Achraf Aarab");
        Assert.IsNull(userfail);
        //CLEAN
        var emptyArray = new object[0]; // or use new string[0] if it's a string array
        var jsonString = JsonSerializer.Serialize(emptyArray);
        File.WriteAllText("../../../TestDataSources/accounts.json", jsonString);
    }
    
}