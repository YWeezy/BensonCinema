static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        // ask user for his, email, name, password and if he is an employee or not
        Console.WriteLine("Welcome to the register page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your name");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();

        AccountModel user = new(7, email, name, password);
        // add the information to the user.json file
        accountsLogic.UpdateList(user);
    }
}