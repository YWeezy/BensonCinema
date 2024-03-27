using System.Text.Json.Serialization;

class AccountModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }


    public AccountModel(string emailAddress, string fullName, string password, bool isAdmin = false)
    {
        Guid myuuid = Guid.NewGuid();
        Id = myuuid.ToString();
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsAdmin = isAdmin;
    }
}



