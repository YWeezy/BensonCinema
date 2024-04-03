using System;
using System.Data;
static class ManageHall
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start() {
        
        HallLogic logichall = new HallLogic();
        Console.Clear();
        bool loop = true;
        int selectedOption = 1; // Default selected option
        int totalOptions = 4; // Total number of options
        while (loop)
        {
            
            DisplayMenu(selectedOption);

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == 1 ? totalOptions : selectedOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == totalOptions ? 1 : selectedOption + 1;
                    break;
                case ConsoleKey.Enter:
                    PerformAction(selectedOption, logichall);
                    break;
                default:
                    break;
            }
            Console.Clear();

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                
                break;
        }
        
    }

    static public void InsertForm(List<HallModel> halls)
    {
        Console.Clear();
        Console.WriteLine("Press Q to Quit");
        Console.WriteLine("hall Name: ");
        bool valid = false;
        string inputname = "";
        while (valid == false)
        {
            inputname = Console.ReadLine();
            if (inputname == "Q")
            {
                return;
            }
            else if(halls.Any(hall => hall.hallName == inputname)){
                Console.WriteLine($"Hallname with {inputname} already exist.");
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
                Console.WriteLine("hall Added");
            }
            else{
                Console.WriteLine("Type is Empty or not valid. Choose from Small/Medium/Large");
                
            }
        }
        var loc = new HallLogic();
        loc.insertHall(inputname, inputtype);

    }

    

    static public void Delete(HallLogic logic)
    {
        Console.WriteLine("Enter the ID of the hall you want to delete: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.Delete(idToDelete))
            {
                Console.WriteLine($"hall with ID {idToDelete} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"hall with ID {idToDelete} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid ID.");
        }

    }

    static private void PerformAction(int option, HallLogic logic)
    {
        List<HallModel> halls = logic.GetList();
        switch (option)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Table of all halls\n");
                    Console.WriteLine("{0,-6}{1,-15}{2,-10}", "ID", "Name", "Type");
                    Console.WriteLine("-------------------------------");
                    foreach (HallModel hall in halls)
                    {
                        Console.WriteLine("{0,-6}{1,-15}{2,-10}", hall.hallID, hall.hallName, hall.type);
                    }
                    Console.WriteLine("\nPress Enter to return to menu.");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter){
                        Start();
                    }
                
                    
                    break;

                case 2:
                    Console.Clear();
                    ManageHall.InsertForm(halls);
                    Start();
                    break;

                case 3:
                    Console.Clear();
                    Delete(logic);
                    Start();
                    break;
                
                case 4:
                    Menu.Start();
                    break;
                default:
                    break;
            }
    }

    static private void DisplayMenu(int selectedOption)
    {
        Console.WriteLine("What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? ">> 1 - View halls" : "1 - View halls");
        Console.WriteLine(selectedOption == 2 ? ">> 2 - Add a hall" : "2 - Add a hall");
        Console.WriteLine(selectedOption == 3 ? ">> 3 - Delete a hall" : "3 - Delete a hall");
        Console.WriteLine(selectedOption == 4 ? ">> 4 - Quit" : "4 - Quit");
    }
}

    