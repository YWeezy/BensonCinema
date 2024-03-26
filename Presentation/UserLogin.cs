static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine().Trim();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            AccountModel acc = accountsLogic.CheckLogin(email, password);
            if (acc != null)
            {
                Console.WriteLine("Welcome back " + acc.FullName);
                Console.WriteLine("Your email number is " + acc.EmailAddress);

                //Write some code to go back to the menu
                Utils.userIsLoggedIn = true;
                Menu.Start();
            }
            else
            {
                Console.WriteLine("No account found with that email and password");
            }
        }
        else
        {
            Console.WriteLine("Email or password cannot be empty");
        }
    }
}
