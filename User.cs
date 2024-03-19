using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class User
{
    public string Email { get; set; }
    public static int ID { get; private set; }
    private string Password;
    public string Name { get; set; }

    public bool IsEmployee;

    public User(string email, string name, string password, bool isEmployee = false)
    {
        ID++;
        this.Email = email;
        this.Name = name;
        this.Password = password;
        IsEmployee = isEmployee;
    }
}

