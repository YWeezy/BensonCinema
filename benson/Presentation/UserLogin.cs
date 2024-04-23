static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine().Trim().ToLower();
        Console.WriteLine("Please enter your password");
        string password = Utils.Encrypt(Console.ReadLine().Trim().ToLower());

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            if (password != null)
            {
                AccountModel acc = accountsLogic.CheckLogin(email, password);
                if (acc != null)
                {

                    // Set logged-in user
                    Utils.LoggedInUser = acc;
                    Console.WriteLine("Login Successful! " + Utils.LoggedInUser.FullName);
                    Thread.Sleep(2000);
                    Menu.Start();
                    return;
                }
                else
                {
                    Console.WriteLine("No account found with that Email and Password");

                }
            }
        }
        else
        {
            Console.WriteLine("Email or Password cannot be empty");
        }

        Start(); // If login fails, try again
    }
}
