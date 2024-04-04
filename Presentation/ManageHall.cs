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

    static public void Edit(HallLogic logic) {
        Console.Clear();
        
        int selectedHallIndex = 0;
        int totalHalls = logic.GetTotalHalls();
        
        while (true)
        {
            DisplayHalls(logic, selectedHallIndex);
            
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedHallIndex = selectedHallIndex == 0 ? totalHalls - 1 : selectedHallIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedHallIndex = selectedHallIndex == totalHalls - 1 ? 0 : selectedHallIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    EditHall(logic, selectedHallIndex);
                    return;
                case ConsoleKey.Escape:
                    Start();
                    return;
                default:
                    break;
            }
        }
    }

    static private void DisplayHalls(HallLogic logic, int selectedHallIndex)
    {
        Console.Clear();
        Console.WriteLine("Please select a hall to edit:\n");

        Console.WriteLine("      {0,-18}{1,-10}", "Name", "Type");
        Console.WriteLine("      --------------------------------------------------------------------------------------");
        
        int index = 0;
        foreach (HallModel hall in logic.GetList())
        {
            if (index == selectedHallIndex)
            {
                Console.Write(">> ");
            }
            else
            {
                Console.Write("   ");
            }
            
            Console.WriteLine("   {0,-18}{1,-10}", hall.hallName, hall.type);

            index++;
        }
    }

    static private void EditHall(HallLogic logic, int selectedHallIndex)
    {
        HallModel selectedHall = logic.GetList()[selectedHallIndex];

        string hallName = null;
        string type = null;
        bool active = selectedHall.active;

        Console.Clear();
        // name
        while (string.IsNullOrEmpty(hallName))
        {
            Console.WriteLine($"\nCurrent hall name: {selectedHall.hallName}\n\nEnter a new name, or leave it blank to keep it.");
            hallName = Console.ReadLine();

            if (string.IsNullOrEmpty(hallName))
            {
                hallName = selectedHall.hallName;
            }
        }

        Console.Clear();
        //type
        while (string.IsNullOrEmpty(type))
        {   
            Console.WriteLine($"\nCurrent type: {selectedHall.type}\n\nEnter a Small/Medium/Large, or leave it blank to keep it.");
            bool isValid = false;
            while (isValid == false)
            {
                
                type = Console.ReadLine().ToLower();

                if (string.IsNullOrEmpty(type))
                {
                    type = selectedHall.type;
                    isValid = true;
                } else if (type == "small" || type == "medium" || type == "large"){
                    isValid = true;
                } else{
                    Console.WriteLine("Type is not valid. Choose from Small/Medium/Large");
                }
            }
            
        }
        
        Console.Clear();
        // active
        if (selectedHall.active == false)
        {
            Console.WriteLine($"\nCurrent active state: {selectedHall.active}\n\nDo you want to switch to active? (Y/N)");
        }
        else
        {
            Console.WriteLine($"\nCurrent active state: {selectedHall.active}\n\nDo you want to switch to inactive? (Y/N)");
        }

        if (Console.ReadLine().ToLower() == "y")
        {
            active = !active;
        }

        Console.Clear();
        Console.WriteLine($"Name: {hallName}");
        Console.WriteLine($"Type: {type}");
        Console.WriteLine($"Active: {active}");

        Console.WriteLine("\nAre you sure you want to make these changes? (Y/N)");
        string confirmation = Console.ReadLine();

        switch (confirmation.ToLower())
        {
            case "y":
                selectedHall.hallName = hallName;
                selectedHall.type = type;
                selectedHall.active = active;
                logic.UpdateList(selectedHall);
                Console.Clear();
                Console.WriteLine("The hall was successfully edited.\n");
                break;
            default:
                Console.Clear();
                Console.WriteLine("The hall was not edited.\n");
                break;
        }
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
                    
                    Console.WriteLine("{0,-15}{1,-10}{2,-15}", "Name", "Type", "Active");
                    Console.WriteLine("-----------------------------------");
                    foreach (HallModel hall in halls)
                    {
                        string actstr;
                        if (hall.active)
                        {
                            actstr = "active";
                        }else{
                            actstr = "inactive";
                        }
                        Console.WriteLine("{0,-15}{1,-10}{2,-15}", hall.hallName, hall.type, actstr);
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
                    Edit(logic);
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
        Console.WriteLine(selectedOption == 3 ? ">> 3 - Edit a hall" : "3 - Edit a hall");
        Console.WriteLine(selectedOption == 4 ? ">> 4 - Back to main menu" : "4 - Back to main menu");
    }
}

    