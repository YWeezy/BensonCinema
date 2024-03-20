static class Menu
{
    //checks if user is logged in
    public static bool userIsLoggedIn = false;

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        // The choices for the user can be admin or bezoeker.
        string[] choices = {
                    "1. [Login]",
                    "2. [Register]",
                    "3. [Employee]",
                };

        Console.WriteLine("╔══════════════════════════╗");
        Console.WriteLine("║           Doll           ║");
        Console.WriteLine("║          House!          ║");
        Console.WriteLine("╠══════════════════════════╣");
        Console.WriteLine("║   Please select an option║");
        Console.WriteLine("║      to continue:        ║");
        Console.WriteLine("║                          ║");

        foreach (string choice in choices)
        {
            Console.WriteLine($"║   {choice,-21}  ║");
        }

        Console.WriteLine("║                          ║");
        Console.WriteLine("╚══════════════════════════╝");

        Console.Write("\nEnter the number corresponding to your choice: ");
        string input = Console.ReadLine();


        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                UserRegister.Start();
                break;

            case "3":
                Console.WriteLine("Choice 2");
                break;

            default:
                Console.WriteLine($"Error: {input} is not valid. Please try again.");
                Start();
                break;


        }



    }
}