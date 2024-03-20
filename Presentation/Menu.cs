using System;
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
        string[] choices = new[]{
                    "1. [Login]",
                    "2. [Register]",
                    "3. [Employee]",
                };

        Console.Clear();
        Console.CursorVisible = false;



        var option = 0;
        var decorator = "✅ \u001b[32m";
        ConsoleKeyInfo key;
        bool isSelected = false;

        while (!isSelected)
        {
            Console.Clear();

            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║           Doll           ║");
            Console.WriteLine("║          House!          ║");
            Console.WriteLine("╠══════════════════════════╣");
            Console.WriteLine("║   Please select an option║");
            Console.WriteLine("║      to continue:        ║");
            Console.WriteLine("║                          ║");

            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"{(i == option ? decorator : "   ")}{choices[i]}\u001b[0m");
            }

            Console.WriteLine("║                          ║");
            Console.WriteLine("╚══════════════════════════╝");

            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    option = option == 0 ? choices.Length - 1 : option - 1;
                    break;

                case ConsoleKey.DownArrow:
                    option = option == choices.Length - 1 ? 0 : option + 1;
                    break;

                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }

        Console.WriteLine($"Selected: {choices[option]}");
        // Perform action based on selected option
        switch (option)
        {
            case 0:
                UserLogin.Start();
                break;

            case 1:
                UserRegister.Start();
                break;

            case 2:
                // Perform action for option 3
                break;

            default:
                break;
        }






    }
}