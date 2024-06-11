using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace test;

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
        AccountsModel user = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        Assert.AreEqual(user.EmailAddress, "achraf.aarab@benson.com");
        Assert.AreEqual(user.FullName, "Achraf Aarab");
        Assert.AreEqual(user.Role, UserRole.User);
    }
    [TestMethod]

    public void TestDuplicatedAccount()
    {
        //picked up by AccountLogic.UpdateList on Register
    }
}