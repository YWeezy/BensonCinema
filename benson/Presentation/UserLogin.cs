using System;
using System.IO;
using System.Threading;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    private static string filePath = "./../benson/DataSources/IsLoggedIn.txt";

    public static void Start()
    {
        Console.Clear();
        string email = GetLoggedInEmail();

        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine($"{Color.Yellow}Welcome to the Login page!{Color.Reset}");
            Console.WriteLine($"{Color.Yellow}Please enter your email address:{Color.Reset}");
            email = Console.ReadLine().Trim().ToLower();
        }
        else
        {
            Console.WriteLine($"{Color.Yellow}Welcome back! Please enter your password for {email}:{Color.Reset}");
        }

        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please enter your password:{Color.Reset}");
        string password = Utils.Encrypt(Console.ReadLine().Trim().ToLower());

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            if (password != null)
            {
                AccountsModel acc = accountsLogic.CheckLogin(email, password);
                if (acc != null)
                {
                    // Set logged-in user
                    Utils.LoggedInUser = acc;
                    Console.WriteLine($"\n{Color.Green}Logged in successfully, {Utils.LoggedInUser.FullName}!{Color.Reset}");

                    // Write the email to the file
                    File.WriteAllText(filePath, acc.EmailAddress);

                    Thread.Sleep(2000);
                    Menu.Start();
                    return;
                }
                else
                {
                    Console.WriteLine($"\n{Color.Red}Invalid email or password. Please try again.{Color.Reset}");
                }
            }
        }
        else
        {
            Console.WriteLine($"\n{Color.Red}Email or Password cannot be empty.{Color.Reset}");
        }

        Thread.Sleep(2000);
        Start(); // If login fails, try again
    }

    private static string GetLoggedInEmail()
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath).Trim().ToLower();
        }
        return null;
    }
}
