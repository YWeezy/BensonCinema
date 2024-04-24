namespace test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestEncryption()
    {
        AccountModel user = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        Assert.AreNotEqual(user.Password, "achraf");
    }

    [TestMethod]
    public void TestAccount()
    {
        AccountModel user = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        Assert.AreEqual(user.EmailAddress, "achraf.aarab@benson.com");
        Assert.AreEqual(user.FullName, "Achraf Aarab");
        Assert.AreEqual(user.Role, UserRole.User);
    }

    [TestMethod]
    public void TestUniqueAccount()
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        AccountModel user = new("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        accountsLogic.UpdateList(user);
        bool isUnique = accountsLogic.CheckIfInDB(user);
        Console.WriteLine(isUnique);
        Assert.AreEqual(isUnique, false);

    }
}