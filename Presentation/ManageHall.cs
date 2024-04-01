
static class ManageHall
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    static public void Start() {

        HallLogic logicloc = new HallLogic();
        Console.Clear();
        bool loop = true;
        while (loop) {
            
            Console.WriteLine("What do you want to do?\n");
            
            Console.WriteLine("1 - View locations");
            Console.WriteLine("2 - Add a location");
            Console.WriteLine("3 - Delete a location");
            Console.WriteLine("Q - Exit\n");

            string? input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    Console.Clear();
                    List<LocationModel> locations = logicloc.GetList();
                    string listOfLocs = "List of locations:\n";
                    listOfLocs += "------------------------\n";

                    foreach (LocationModel location in locations)
                    {
                        listOfLocs += $"ID: {location.locationID}\n";
                        listOfLocs += $"Name: {location.locationName}\n";
                        listOfLocs += $"Type: {location.type}\n";
                        listOfLocs += "------------------------\n";
                    }
                    Console.WriteLine(listOfLocs);
                    break;

                case "2":
                    Console.Clear();
                    ManageHall.InsertForm();
                    
                    break;

                case "3":
                    Console.Clear();
                    Delete(logicloc);
                    
                    break;
                
                default:
                    loop = false;
                    break;
            }
        }
    }

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
            if (inputname == "q")
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
            if (inputtype == "q")
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
        var loc = new HallLogic();
        loc.insertLocation(inputname, inputtype);
        Menu.Start();

    }
    static public void Delete(HallLogic logic)
    {
        Console.WriteLine("Enter the ID of the location you want to delete: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.Delete(idToDelete))
            {
                Console.WriteLine($"Location with ID {idToDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Location with ID {idToDelete} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid ID.");
        }

    }
}