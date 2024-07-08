using System.ComponentModel.DataAnnotations;
using System.Numerics;


public class ManageMaterials
{
    private MaterialsLogic logic = new(); 
    public void Start()
    {
        
        List<MaterialsModel> materials = logic.GetList();
        //Separate AddMaterial function to handel a list with materials.
        int selectedMaterialIndex = materials.Count > 0 ? 0 : -1; // Start with the first material selected, or -1 if the list is empty.

        DisplayMaterials(materials, selectedMaterialIndex);

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            //Case functions for the Controls.
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedMaterialIndex > 0)
                    {
                        selectedMaterialIndex--;
                    }
                    else if (materials.Count > 0)
                    {
                        selectedMaterialIndex = materials.Count - 1; // Wrap to the last item.
                    }
                    DisplayMaterials(materials, selectedMaterialIndex);
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedMaterialIndex < materials.Count - 1)
                    {
                        selectedMaterialIndex++;
                    }
                    else if (materials.Count > 0)
                    {
                        selectedMaterialIndex = 0; // Wrap to the first item.
                    }
                    DisplayMaterials(materials, selectedMaterialIndex);
                    break;

                case ConsoleKey.Backspace:
                    if (selectedMaterialIndex >= 0 && selectedMaterialIndex < materials.Count)
                    {
                        //Removal of a Material.
                        Console.WriteLine($"\n{Color.Yellow}Are you sure you want to delete this material? (Y/N){Color.Reset}");
                        string confirmation = Console.ReadLine();

                        if (confirmation.ToLower() == "y")
                        {
                            logic.delete(selectedMaterialIndex);
                        }else
                        {
                            Console.Clear();
                            Console.WriteLine($"{Color.Red}❌ The material was not deleted.{Color.Reset}\n");
                        }
                        Start();
                    }
                    break;

                case ConsoleKey.Enter:
                    // Add new material.
                    AddMaterials();
                    DisplayMaterials(materials, 0);
                    break;

                case ConsoleKey.Escape:
                    Menu.Start();
                    break;

                case ConsoleKey.V:MaterialsModel model = logic.GetMaterial(materials[selectedMaterialIndex].material, materials[selectedMaterialIndex].type);
                    DisplayOccupation(model);
                    break;

                case ConsoleKey.P:
                    PlanMaterials(materials[selectedMaterialIndex]);
                    DisplayMaterials(materials, selectedMaterialIndex);
                    break;

                default:
                    break;
            }
        }
    }


    public void AddMaterials()
    {
        // List of dictionaries to store materials.

        //Dialog.
        Console.Clear();

        Console.WriteLine($"{Color.FontReset}{Color.Yellow}Add materials{Color.Reset}");
        while (true)
        {
            Console.WriteLine($"{Color.Yellow}Material: {Color.Reset}");
            //User input for material.
            string material = Console.ReadLine();

            

            if (!string.IsNullOrEmpty(material))
            {
                Console.WriteLine($"{Color.Yellow}Quantity: {Color.Reset}");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                //Check to have no negative quantities. 
                {
                    Console.WriteLine($"{Color.Red}Please enter a valid positive integer for quantity.{Color.Reset}");
                    Console.WriteLine($"{Color.Yellow}Quantity: {Color.Reset}");
                }
                string type;
                while (true)
                {
                    Console.WriteLine($"{Color.Yellow}type: (Puppeteers/Requisites/Decor){Color.Reset}");
                    type = Console.ReadLine();

                    if (type.ToLower() == "puppeteers" || type.ToLower() == "requisites" || type.ToLower() == "decor")
                    {
                        type = char.ToUpper(type[0]) + type.Substring(1); // first letter uppercase
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{Color.Red}Invalid input. Please provide a correct material type.{Color.Reset}");
                    }
                }
                MaterialsModel add = new(material, quantity, type);
                logic.insertMaterial(add);
                return;
            }
        }
    }


    private void DisplayMaterials(List<MaterialsModel> materials)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Materials List:{Color.Reset}\n");
        Console.WriteLine("{0,-20}{1,-20}{2,-20}", "Material", "Quantity", "Type");
        Console.WriteLine(new string('-', 60));
        

        foreach (var material in materials)
        {
            Console.WriteLine("{0,-20}{1,-10}{2,-20}", material.material, material.quantity.ToString(), material.type);
        }

        Console.WriteLine();
    }


    void DisplayMaterials(List<MaterialsModel> materials, int selectedIndex = -1)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Existing Materials:{Color.Reset}\n");
        Console.WriteLine($"{Color.Italic}{Color.Blue}Controls: {Color.Red}ESC{Color.Blue} to stop editing Materials, {Color.Red}V{Color.Blue} to view its schedule, {Color.Red}P{Color.Blue} to plan a schedule for the material, {Color.Red}Backspace{Color.Blue} to delete the Material and {Color.Red}Enter{Color.Blue} to add more Materials{Color.Reset}{Color.FontReset}");
        Console.WriteLine("{0,-20}{1,-20}{2,-20}", "Material", "Quantity", "Type");
        Console.WriteLine(new string('-', 60));

        for (int i = 0; i < materials.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.Write($"{Color.Green}>> ");
            }
            else
            {
                Console.Write("   ");
            }

            Console.WriteLine("{0,-20}{1,-20}{2,-20}", materials[i].material, materials[i].quantity.ToString(), materials[i].type);
            Console.Write($"{Color.Reset}");
        }

        if (materials.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No materials available.{Color.Reset}");
        }
    }


    void DisplayOccupation( MaterialsModel material){
        Console.Clear();
        if (material.occupation.Count == 0 || material.occupation == null){
            Console.WriteLine($"{Color.Red}This material has no upcoming schedule{Color.Reset}");
            Console.WriteLine($"{Color.Yellow}Press any key to return{Color.Reset}\n");
            Console.ReadKey(true);
            Start();
        }
        Console.WriteLine($"{Color.Yellow}Schedule for '{material.material}':{Color.Reset}\n");
        Console.WriteLine("{0,-30}{1,-30}{2,-20}", "Quantity", "Date", "Hall");
        Console.WriteLine(new string('-', 80));
        

        foreach (var schedule in material.occupation)
        {
            DateTime start = DateTime.Parse(schedule["start"].ToString());
            DateTime end = DateTime.Parse(schedule["end"].ToString());
            Console.WriteLine("{0,-30}{1,-30}{2,-20}", schedule["quantity"].ToString(), $"{start.ToString("MM-dd-yyyy HH:mm")} - {end.ToString("HH:mm")}", schedule["hallName"]);
        }
        Console.WriteLine($"{Color.Yellow}Press any key to return{Color.Reset}\n");
        Console.ReadKey(true);
        Start();
    } 


    public bool IsMaterialScheduledForAnotherHall(MaterialsModel material, DateTime performanceStartTime, string targetHall)
    {
        if (!string.IsNullOrEmpty(material.currentHall) && material.currentHall != targetHall)
        {
            foreach (var schedule in material.occupation)
            {
                if (schedule["start"].ToString() == performanceStartTime.ToString())
                {
                    Console.WriteLine($"{Color.Red}Material is already scheduled for another hall at this time.{Color.Reset}");
                    return true;
                }
            }
        }
        return false;
    }


    public void PlanMaterials(MaterialsModel material)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Plan Materials{Color.Reset}\n");

        if (logic.GetList().Count == 0)
        {
            Console.WriteLine($"{Color.Red}No materials available to schedule.{Color.Reset}");
            return;
        }

        Console.WriteLine($"Selected material: {material.material}");

        int quantity = 0;
        bool validQuantity = false;

        while (!validQuantity)
        {
            Console.WriteLine($"{Color.Yellow}Enter the quantity needed for the performance:{Color.Reset}");

            try
            {
                quantity = Int32.Parse(Console.ReadLine());

                if (quantity <= 0)
                {
                    Console.WriteLine($"{Color.Red}Quantity must be a positive integer.{Color.Reset}");
                }
                else
                {
                    validQuantity = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"{Color.Red}Invalid input. Please enter a valid integer.{Color.Reset}");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"{Color.Red}Input value is too large or too small for an Int32.{Color.Reset}");
            }
        }


        string type = null;

        PerformanceLogic performanceLogic = new PerformanceLogic();
        int selectedPerformanceIndex = 0;
        List<PerformancesModel> performances = performanceLogic.GetPerformances();
        ManagePerformance.DisplayPerformances(performanceLogic, selectedPerformanceIndex);

        DateTime performanceDateTime = DateTime.MinValue;
        string hallName = ""; 

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedPerformanceIndex > 0)
                    {
                        selectedPerformanceIndex--;
                    }
                    else if (performances.Count > 0)
                    {
                        selectedPerformanceIndex = performances.Count - 1; 
                    }
                    ManagePerformance.DisplayPerformances(performanceLogic, selectedPerformanceIndex);
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedPerformanceIndex < performances.Count - 1)
                    {
                        selectedPerformanceIndex++;
                    }
                    else if (performances.Count > 0)
                    {
                        selectedPerformanceIndex = 0;
                    }
                    ManagePerformance.DisplayPerformances(performanceLogic, selectedPerformanceIndex);
                    break;

                case ConsoleKey.Enter:
                    PerformancesModel selectedPerformance = performances[selectedPerformanceIndex];
                    performanceDateTime = selectedPerformance.startDate; // Set performanceDateTime
                    hallName = new HallLogic().GetHallNameById(selectedPerformance.hallId); // Get hall name based on hallId
                    break;

                case ConsoleKey.Escape:
                    return; 
            }


            if (performanceDateTime != DateTime.MinValue)
                break;
        }

        if (IsMaterialScheduledForAnotherHall(material, performanceDateTime, hallName))
        {
            Console.WriteLine($"{Color.Red}Material is already scheduled for another hall at this date.{Color.Reset}");
            Console.WriteLine($"{Color.Yellow}Press any key to return to the main menu.{Color.Reset}\n");
            Console.ReadKey(true);
            DisplayMaterials(logic.GetList(), logic.GetList().IndexOf(material));
            return;
        }

        material.occupation.Add(new Dictionary<string, object>
        {
            { "quantity", quantity },
            { "start", performanceDateTime },
            { "end", performanceDateTime.AddHours(2) },
            { "hallName", hallName }
        });

        int selectedMaterialIndex = logic.GetList().FindIndex(m => m.material == material.material);

        logic.updateMaterial(selectedMaterialIndex, material.material, material.quantity, hallName, type, material.occupation);

        Console.WriteLine($"{Color.Green}Material scheduled successfully!{Color.Reset}");
        Console.WriteLine($"{Color.Yellow}Press any key to return to the main menu.{Color.Reset}\n");
        Console.ReadKey(true);
        DisplayMaterials(logic.GetList(), selectedMaterialIndex);
    }


    private string SelectMaterialType()
    {
        string[] materialTypes = { "Puppeteers", "Requisites", "Decor" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Select material type:{Color.Reset}\n");

            for (int i = 0; i < materialTypes.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.Write($"{Color.Green}>> ");
                }
                else
                {
                    Console.Write($"{Color.Reset}   ");
                }
                Console.WriteLine(materialTypes[i]);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + materialTypes.Length) % materialTypes.Length;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % materialTypes.Length;
                    break;

                case ConsoleKey.Enter:
                    string selectedType = materialTypes[selectedIndex];
                    Console.WriteLine($"\nSelected type: {selectedType}\n");
                    return selectedType;

                default:
                    Console.WriteLine($"{Color.Red}Invalid input. Please use arrow keys to select and Enter to confirm.{Color.Reset}");
                    break;
            }
        }
    }
}
