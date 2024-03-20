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
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine().Trim();
        Console.WriteLine("Please enter your name");
        string name = Console.ReadLine().Trim();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        try
        {
            AccountModel user = new AccountModel(email, name, password, isAdmin);
            accountsLogic.UpdateList(user);
            Console.WriteLine("Account created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }
}
