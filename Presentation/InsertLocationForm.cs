static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Ask()
    {
        Console.WriteLine("Press Q to Quit");
        Console.WriteLine("LocationName: ");
Type
        string inputname = Console.ReadLine();
        if (inputname == "Q")
        {
            Menu.Start();
            return;
        }
        string inputType = Console.ReadLine();
        if (inputType == "Q")
        {
            Menu.Start();
            return;
        }
        Location.insertLocation(inputname,inputType);
        

    }
}