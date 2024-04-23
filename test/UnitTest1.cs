namespace test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestEncryption(){
        AccountModel user =  new ("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        Assert.AreNotEqual(user.Password, "achraf");
    }

    [TestMethod]
    public void TestEncryption(){
        AccountModel user =  new ("achraf.aarab@benson.com", "Achraf Aarab", "achraf", UserRole.User);
        Assert.AreNotEqual(user.Password, "achraf");
    }
}