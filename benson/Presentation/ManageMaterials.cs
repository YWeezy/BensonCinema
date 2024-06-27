using System.ComponentModel.DataAnnotations;

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
        Console.WriteLine($"{Color.Yellow}Materials List {Color.Red}Example{Color.Reset}{Color.Italic}:\n");
        Console.WriteLine("{0,-20}{1,-10}{2,-10}", "Material", "Quantity", "Type");
        Console.WriteLine(new string('-', 40));

        Console.WriteLine($"{Color.FontReset}{Color.Yellow}Add materials (type '{Color.Italic}done{Color.FontReset}' when finished):{Color.Reset}");
        while (true)
        {
            Console.WriteLine("Material: ");
            //User input for material.
            string material = Console.ReadLine();

            if (material.ToLower() == "done")
            {
                return ;
            }

            if (!string.IsNullOrEmpty(material))
            {
                Console.Write("Quantity: ");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                //Check to have no negative quantities. 
                {
                    Console.WriteLine($"{Color.Red}Please enter a valid positive integer for quantity.{Color.Reset}");
                    Console.Write("Quantity: ");
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
            System.Console.WriteLine(material.type);
            Console.WriteLine("{0,-20}{1,-10}{2,-20}", material.material, material.quantity.ToString(), material.type);
        }

        Console.WriteLine();
    }

    void DisplayMaterials(List<MaterialsModel> materials, int selectedIndex = -1)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Existing Materials:{Color.Reset}\n");
        Console.WriteLine($"{Color.Italic}{Color.Blue}Controls: {Color.Red}ESC{Color.Blue} to stop editing Materials, {Color.Red}Backspace{Color.Blue} to delete the Material and {Color.Red}Enter{Color.Blue} to add more Materials{Color.Reset}{Color.FontReset}");
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
                Console.Write("");
            }

            Console.WriteLine("{0,-20}{1,-20}{2,-20}", materials[i].material, materials[i].quantity.ToString(), materials[i].type);
            Console.Write($"{Color.Reset}");
        }

        if (materials.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No materials available.{Color.Reset}");
        }
    }
}