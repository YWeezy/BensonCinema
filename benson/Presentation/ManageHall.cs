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
            Console.WriteLine($"{Color.Yellow}Hall name:{Color.Reset}");
            hallName = editing ? ConsoleInput.EditLine(selectedHall.hallName) : Console.ReadLine();


            if (string.IsNullOrEmpty(hallName))
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please provide a Hall name.{Color.Reset}");
            }
        }

        Console.Clear();
        // type
        while (true)
        {
            Console.WriteLine($"{Color.Yellow}Hall type: (Small/Medium/Large){Color.Reset}");
            type = editing ? ConsoleInput.EditLine(selectedHall.type) : Console.ReadLine();


            if (type.ToLower() == "small" || type.ToLower() == "medium" || type.ToLower() == "large") {
                type = char.ToUpper(type[0]) + type.Substring(1); // first letter uppercase
                break;
            } else {
                Console.WriteLine($"{Color.Red}Invalid input. Please provide a correct Hall type.{Color.Reset}");
            }
        }

        

        if (editing == true)
        {

            Console.Clear();
            // active
            if (selectedHall.active == false) {
                Console.WriteLine($"Current active state: {Color.Red}Inactive{Color.Reset}\n\n{Color.Yellow}Do you want to switch to {Color.Red}Active{Color.Yellow}? (Y/N){Color.Reset}");
            } else {
                Console.WriteLine($"Current active state: {Color.Green}Active{Color.Reset}\n\n{Color.Yellow}Do you want to switch to {Color.Red}Inactive{Color.Yellow}? (Y/N){Color.Reset}");
            }

            if (Console.ReadLine().ToLower() == "y")
            {
                active = !active;
            }

            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {hallName}");
            Console.WriteLine($"{Color.Blue}Type: {type}");
            Console.WriteLine($"Active: {active}{Color.Reset}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to make these changes? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    selectedHall.hallName = hallName;
                    selectedHall.type = type;
                    selectedHall.active = active;
                    logic.UpdateList(selectedHall);
                    Console.Clear();
                    Console.WriteLine($"{Color.Green}The Hall was successfully edited.{Color.Reset}\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Color.Red}The Hall was not edited.{Color.Reset}\n");
                    break;
            }

        }
        else
        {

            Console.Clear();
            Console.WriteLine($"{Color.Blue}Name: {hallName}");
            Console.WriteLine($"{Color.Blue}Type: {type}");

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to add this Hall? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();

            switch (confirmation.ToLower())
            {
                case "y":
                    int newId = logic.GetNewId();
                    HallModel hall = new HallModel(newId, hallName, type, true);
                    logic.UpdateList(hall);
                    Console.Clear();
                    Console.WriteLine($"{Color.Green}The Hall was succesfully added.{Color.Reset}\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Color.Red}The Hall was not added.{Color.Reset}\n");
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
        Console.WriteLine($"{Color.Yellow}Please select a Hall to edit:{Color.Reset}\n");

        Console.WriteLine("      {0,-15}{1,-10}{2,-15}", "Name", "Type", "Active");
        Console.WriteLine("      -------------------------------------");
        
        int index = 0;
        foreach (HallModel hall in logic.GetList())
        {
            if (index == selectedHallIndex)
            {
                Console.Write($"{Color.Green}>> ");
            }
            else
            {
                Console.Write($"{Color.Reset}   ");
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
                Console.WriteLine($"{Color.Green}Hall with ID {idToDelete} deleted successfully.{Color.Reset}");
            }
            else
            {
                Console.WriteLine($"{Color.Red}Hall with ID {idToDelete} not found.{Color.Reset}");
            }
        }
        else
        {
            Console.WriteLine($"{Color.Red}Invalid input.{Color.Reset} Please enter a valid ID.");
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
        Console.WriteLine($"{Color.Yellow}What do you want to do?{Color.Reset}\n");

        Console.WriteLine(selectedOption == 1 ? $"{Color.Green}>> View Halls{Color.Reset}" : "   View Halls");
        Console.WriteLine(selectedOption == 2 ? $"{Color.Green}>> Add a Hall{Color.Reset}" : "   Add a Hall");
        Console.WriteLine(selectedOption == 3 ? $"{Color.Green}>> Edit a Hall{Color.Reset}" : "   Edit a Hall");
        Console.WriteLine(selectedOption == 4 ? $"{Color.Green}>> Back to main menu{Color.Reset}" : "   Back to main menu");
    }
}