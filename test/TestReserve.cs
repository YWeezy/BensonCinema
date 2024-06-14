using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AccountsModelTests
{
    [TestMethod]
    public void AccountsWithSameRole()
    {
        // Arrange
        var userAccount = new AccountsModel("user@example.com", "User Name", "password123", UserRole.User);
        var employeeAccount = new AccountsModel("employee@example.com", "Employee Name", "password123", UserRole.Employee);
        var contentManagerAccount = new AccountsModel("manager@example.com", "Manager Name", "password123", UserRole.ContentManager);

        var accounts = new List<AccountsModel> { userAccount, employeeAccount, contentManagerAccount };

        // Act & Assert
        Assert.AreEqual(UserRole.User, userAccount.Role);
        Assert.AreEqual(UserRole.Employee, employeeAccount.Role);
        Assert.AreEqual(UserRole.ContentManager, contentManagerAccount.Role);
    }
}
