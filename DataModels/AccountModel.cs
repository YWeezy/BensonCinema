using System;
using System.Text.Json.Serialization;

class AccountModel
{
    public string Id { get; set; }
    public string EmailAddress { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }

    public AccountModel(string emailAddress, string fullName, string password, UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid().ToString();
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Role = role;
    }
}

enum UserRole
{
    User,
    Employee,
    ContentManager
}
