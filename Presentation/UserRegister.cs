using System;

static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the register page");

        Console.WriteLine("Are you an employee? (yes/no)");
        string isAdmin = Console.ReadLine().Trim();

        if (isAdmin.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Please enter the master password");
            string masterPassword = Console.ReadLine();

            if (masterPassword.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                AskUserInfo(true);
            }
            else
            {
                Console.WriteLine("You are not an employee!");
            }
        }
        else
        {
            AskUserInfo(false);
        }
    }

    private static void AskUserInfo(bool isAdmin)
    {
        string email;
        string name;
        string password;

        while (true)
        {
            Console.WriteLine("Please enter your email address");
            email = Console.ReadLine().Trim().ToLower();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format. Please try again.");
                continue;
            }

            Console.WriteLine("Please enter your name");
            name = Console.ReadLine().Trim();
            if (!IsValidName(name))
            {
                Console.WriteLine("Invalid name format. Please try again.");
                continue;
            }

            Console.WriteLine("Please enter your password");
            password = Utils.Encrypt(Console.ReadLine().Trim().ToLower(), Utils.passPhrase);

            try
            {
                AccountModel user = new AccountModel(email, name, password, isAdmin);
                accountsLogic.UpdateList(user);
                break; // Exit the loop if everything is successful

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                break; // Exit the loop if an error occurs
            }
        }
        Menu.Start();
    }

    private static bool IsValidEmail(string email)
    {

        return email.Contains("@") && email.Contains(".");
    }

    private static bool IsValidName(string name)
    {

        return !string.IsNullOrWhiteSpace(name);
    }
}
