
static class LocationFront
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void InsertForm()
    {
        Console.Clear();
        Console.WriteLine("Press Q to Quit");
        Console.WriteLine("Location Name: ");
        bool valid = false;
        string inputname = "";
        while (valid == false)
        {
            inputname = Console.ReadLine().ToLower();
            if (inputname == "Q")
            {
                return;
            }
            
            else if (inputname != "")
            {
                valid = true;
            }
            else{
                Console.WriteLine("Name is empty. Try again!");
            }
        }
        
        
        Console.WriteLine("Type : Small/Medium/Large");
        valid = false;
        string inputtype = "";
        while (valid == false)
        {
            inputtype = Console.ReadLine().ToLower();
            if (inputtype == "Q")
            {
                return;
            }
            else if (inputtype == "small" || inputtype == "medium" || inputtype == "large")
            {
                valid = true;
                Console.WriteLine("Location Added");
            }
            else{
                Console.WriteLine("Type is Empty or not valid. Choose from Small/Medium/Large");
                
            }
        }
        var loc = new LocationLogic();
        loc.insertLocation(inputname, inputtype);
        Menu.Start();

    }
    static public void Delete()
    {
        

    }
}