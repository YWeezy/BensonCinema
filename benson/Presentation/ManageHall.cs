using System;
using System.Data;
static class ManageHall
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start() {
        
        HallLogic logic = new HallLogic();
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
                    PerformAction(selectedOption, logic);
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

    static public void Update(HallLogic logic, int selectedHallIndex = -1)
    {
        bool editing = false;
        HallModel selectedHall = null;
        bool active = true;
        if (selectedHallIndex != -1)
        {
            editing = true;
            selectedHall = logic.GetList()[selectedHallIndex];
            active = selectedHall.active;
        }

        string hallName = null;
        string type = null;

        Console.Clear();
        // name
        while (string.IsNullOrEmpty(hallName))
        {
            Console.WriteLine("\nHall name: ");
            hallName = editing ? ConsoleInput.EditLine(selectedHall.hallName) : Console.ReadLine();


            if (string.IsNullOrEmpty(hallName))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please provide a Hall name.\u001b[0m");
            }
        }

        Console.Clear();
        // type
        while (true)
        {
            Console.WriteLine("\nHall type: (Small/Medium/Large)");
            type = editing ? ConsoleInput.EditLine(selectedHall.type) : Console.ReadLine();


            if (type.ToLower() == "small" || type.ToLower() == "medium" || type.ToLower() == "large") {
                break;
            } else {
                Console.WriteLine("\u001b[31mInvalid input. Please provide a correct Hall type.\u001b[0m");
            }
        }

        

        if (editing == true)
        {

            Console.Clear();
            // active
            if (selectedHall.active == false)
            {
                Console.WriteLine($"\nCurrent active state: \u001b[31mInactive\u001b[0m\n\nDo you want to switch to \u001b[31mActive\u001b[0m? (Y/N)");
            }
            else
            {
                Console.WriteLine($"\nCurrent active state: \u001b[32mActive\u001b[0m\n\nDo you want to switch to \u001b[31mInactive\u001b[0m? (Y/N)");
            }

            if (Console.ReadLine().ToLower() == "y")
            {
                active = !active;
            }

            Console.Clear();
            Console.WriteLine($"\u001b[34mName: {hallName}");
            Console.WriteLine($"\u001b[34mType: {type}");
            Console.WriteLine($"Active: {active}\u001b[0m");

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
                    Console.WriteLine("\u001b[32mThe Hall was successfully edited.\u001b[0m\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\u001b[31mThe Hall was not edited.\u001b[0m\n");
                    break;
            }

        }
        else
        {

            Console.Clear();
            Console.WriteLine($"\u001b[34mName: {hallName}");
            Console.WriteLine($"\u001b[34mType: {type}");

            Console.WriteLine("\n\u001b[32mAre you sure you want to add this Hall?\u001b[0m (Y/N)");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    int newId = logic.GetNewId();
                    HallModel hall = new HallModel(newId, hallName, type, true);
                    logic.UpdateList(hall);
                    Console.Clear();
                    Console.WriteLine("\u001b[32mThe Hall was succesfully added.\u001b[0m\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\u001b[31mThe Hall was not added.\u001b[0m\n");
                    break;
            }
        }
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
                    Update(logic, selectedHallIndex);
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
        Console.WriteLine("\u001b[32mPlease select a Hall to edit:\u001b[0m\n");

        Console.WriteLine("      {0,-15}{1,-10}{2,-15}", "Name", "Type", "Active");
        Console.WriteLine("      -------------------------------------");
        
        int index = 0;
        foreach (HallModel hall in logic.GetList())
        {
            if (index == selectedHallIndex)
            {
                Console.Write("\u001b[32m>> ");
            }
            else
            {
                Console.Write("\u001b[0m   ");
            }

            string actstr;
            if (hall.active)
            {
                actstr = "Active";
            }else{
                actstr = "Inactive";
            }
            
            Console.WriteLine("   {0,-15}{1,-10}{2,-15}", hall.hallName, hall.type, actstr);

            index++;
        }
    }


    static public void Delete(HallLogic logic)
    {
        Console.WriteLine("Enter the ID of the Hall you want to delete: ");
        int idToDelete;
        if (int.TryParse(Console.ReadLine(), out idToDelete))
        {
            if (logic.Delete(idToDelete))
            {
                Console.WriteLine($"\u001b[32mHall with ID {idToDelete} deleted successfully.\u001b[0m");
            }
            else
            {
                Console.WriteLine($"\u001b[31mHall with ID {idToDelete} not found.\u001b[0m");
            }
        }
        else
        {
            Console.WriteLine("\u001b[31mInvalid input.\u001b[0m Please enter a valid ID.");
        }

    }

    static private void PerformAction(int option, HallLogic logic)
    {
        List<HallModel> halls = logic.GetList();
        switch (option)
            {
                case 1:
                    Console.Clear();
                    logic.DisplayTable();
                    Console.WriteLine("\nPress Enter to return to menu.");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Start();
                    break;
                case 2:
                    Console.Clear();
                    Update(logic);
                    Console.WriteLine("Press Enter to return to the menu.");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Start();
                    break;
                case 3:
                    Console.Clear();
                    Edit(logic);
                    Console.WriteLine("Press Enter to return to the menu.");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Start();
                    break;
                case 4:
                    Menu.Start();
                    Console.WriteLine("Press Enter to return to the menu.");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Start();
                    break;
                default:
                    break;
            }
    }

    static private void DisplayMenu(int selectedOption)
    {
        string color = "\u001b[32m";
        Console.WriteLine("What do you want to do?\n");

        Console.WriteLine(selectedOption == 1 ? color + ">> View Halls\u001b[0m" : "   View Halls");
        Console.WriteLine(selectedOption == 2 ? color + ">> Add a Hall\u001b[0m" : "   Add a Hall");
        Console.WriteLine(selectedOption == 3 ? color + ">> Edit a Hall\u001b[0m" : "   Edit a Hall");
        Console.WriteLine(selectedOption == 4 ? color + ">> Back to main menu\u001b[0m" : "   Back to main menu");
    }
}