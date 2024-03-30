static class UserRegister
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the register page");
        Console.WriteLine("Choose your role (user/employee/content manager):");
        string roleInput = Console.ReadLine().Trim().ToLower();

        UserRole role;
        switch (roleInput)
        {
            case "employee":
                role = UserRole.Employee;
                break;
            case "content manager":
                role = UserRole.ContentManager;
                break;
            case "user":
            default:
                role = UserRole.User;
                break;
        }

        AskUserInfo(role);
    }

    private static void AskUserInfo(UserRole role)
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
            password = Utils.Encrypt(Console.ReadLine().Trim().ToLower());

            if (password == null)
            {
                Console.WriteLine("Error encrypting password. Please try again.");
                continue;
            }

            try
            {
                AccountModel user = new AccountModel(email, name, password, role);
                accountsLogic.UpdateList(user);

                // Set logged-in user
                Utils.LoggedInUser = user;
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                break;
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
