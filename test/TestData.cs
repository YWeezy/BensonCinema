using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestData
{   
    private List<TicketModel> _tickets;

    [TestMethod]
    public void TestDataEncryption()
    {   
        // Arrange
        AccountsLogic logic = new AccountsLogic();
        string email = "content@hr.nl";
        string name = "Achraf Aarab";
        string plainTextPassword = "content";
        string hashedPassword = Utils.Encrypt(plainTextPassword);
        UserRole role = UserRole.User;
        AccountsModel user = new AccountsModel(email, name, hashedPassword, role);

        // Update the list 
        logic.UpdateList(user);

        // Act
        var account = logic.GetAllAccounts().Find(a => a.EmailAddress == email);

        // Assert
        Assert.AreNotEqual(plainTextPassword, account.Password);
    }

    [TestMethod]
    public void AccountsWithSameRole()
    {
        // Arrange
        var userAccount = new AccountsModel("user@hr.nl", "User Name", "password123", UserRole.User);
        var employeeAccount = new AccountsModel("employee@hr.nl", "Employee Name", "password123", UserRole.Employee);
        var contentManagerAccount = new AccountsModel("content@hr.nl", "Content Name", "password123", UserRole.ContentManager);

        var accounts = new List<AccountsModel> { userAccount, employeeAccount, contentManagerAccount };

        // Act & Assert
        Assert.AreEqual(UserRole.User, userAccount.Role);
        Assert.AreEqual(UserRole.Employee, employeeAccount.Role);
        Assert.AreEqual(UserRole.ContentManager, contentManagerAccount.Role);
    }
}
