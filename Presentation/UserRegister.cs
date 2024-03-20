static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        // ask user for his, email, name, password and if he is an employee or not
        Console.WriteLine("Welcome to the register page");

        Console.WriteLine("Are you an employee? (yes/no)");
        string isAdmin = Console.ReadLine().Trim().ToLower();
        if (isAdmin == "yes")
        {
            // ask the employee for a master password so he can register as an employee
            Console.WriteLine("Please enter the master password");
            string masterPassword = Console.ReadLine();

            if (masterPassword == "admin")
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


        void AskUserInfo(bool isAdmin)
        {
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            AccountModel user = new(email, name, password, isAdmin);
            accountsLogic.UpdateList(user);
        }
        // Menu.Start();
    }
}