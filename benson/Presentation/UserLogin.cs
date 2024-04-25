static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine().Trim().ToLower();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine().Trim().ToLower();

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
            }
        }
        else
        {
            Console.WriteLine("Email or Password cannot be empty");
        }
        Thread.Sleep(2000);
        // Console.Clear();
        Start(); // If login fails, try again
    }
}
