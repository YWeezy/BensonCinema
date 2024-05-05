static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Welcome to the Login page!{Color.Reset}");
        Console.WriteLine($"{Color.Yellow}Please enter your email address:{Color.Reset}");
        string email = Console.ReadLine().Trim().ToLower();

        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please enter your password:{Color.Reset}");
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
                    Console.WriteLine($"\n{Color.Green}Logged in successfully, {Utils.LoggedInUser.FullName}!{Color.Reset}");
                    Thread.Sleep(2000);
                    Menu.Start();
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine($"\n{Color.Red}Email or Password cannot be empty.{Color.Reset}");
        }
        Thread.Sleep(2000);
        // Console.Clear();
        Start(); // If login fails, try again
    }
}
