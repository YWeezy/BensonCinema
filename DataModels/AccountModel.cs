using System.Text.Json.Serialization;

class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }


    private static int nextId = 1;

    public AccountModel(string emailAddress, string fullName, string password, bool isAdmin = false)
    {
        Id = nextId++;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        IsAdmin = isAdmin;
    }
}



