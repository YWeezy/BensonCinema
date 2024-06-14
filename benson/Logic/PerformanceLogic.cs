using System.Dynamic;
using System.Globalization;
using System.Text.Json;
using System;

public class PerformanceLogic
{

    private List<PerformancesModel> _performances = new List<PerformancesModel>();
    public string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/performances.json"));

    public PerformanceLogic(bool test = false)
    {
        _performances = DataAccess<PerformancesModel>.LoadAll();
        
    }

    public List<PerformancesModel> GetPerformances(string from = "01-01-0001", string to = "31-12-9999")
    {
        DateTime fromDate;
        DateTime toDate;

        // parse from date
        if (!DateTime.TryParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
        {
            Console.WriteLine("Invalid from date format. Using default from date.");
            fromDate = DateTime.MinValue;
        }

        // parse to date
        if (!DateTime.TryParseExact(to, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
        {
            Console.WriteLine("Invalid to date format. Using default to date.");
            toDate = DateTime.MaxValue;
        }

        return _performances.Where(performance => performance.startDate >= fromDate && performance.endDate <= toDate).ToList();
    }

    public int[][] GetSeatsById(int id)
    {
        PerformancesModel? performance = _performances.FirstOrDefault(h => h.id == id);
        string seatsStr = Convert.ToString(performance.ticketsAvailable[0]["seats"]);
        int[][] seats = JsonSerializer.Deserialize<int[][]>(seatsStr);
        return seats;
    }

    public List<TicketType> GetTicketTypesById(int id)
    {
        PerformancesModel? performance = _performances.FirstOrDefault(h => h.id == id);
        if (performance == null || performance.ticketsAvailable.Count < 2)
        {
            return new List<TicketType>();
        }
        string ticketTypesStr = performance.ticketsAvailable[1]["ticketTypes"].ToString();
        List<TicketType> ticketTypes = JsonSerializer.Deserialize<List<TicketType>>(ticketTypesStr);
        return ticketTypes;
    }


    public List<PerformancesModel> GetActivePerformances()
    {
        string from = DateTime.Today.ToString("dd-MM-yyyy");
        DateTime fromDate;

        // parse from date
        if (!DateTime.TryParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
        {
            Console.WriteLine("Invalid from date format. Using default from date.");
            fromDate = DateTime.MinValue;
        }

        return _performances.Where(performance => performance.startDate >= fromDate && performance.active == true).ToList();
    }

    public int GetTotalPerformances()
    {
        if (_performances == null)
        {
            Console.WriteLine($"{Color.Red}Error: _performances list is null{Color.Reset}");
            Thread.Sleep(1000);
            return 0;
        }
        return _performances.Count;
    }

    public void UpdateList(PerformancesModel perf)
    {
        if (_performances == null || _performances.Count == 0)
        {
            // If _performances is null or empty, add the performance directly
            _performances = new List<PerformancesModel>() { perf };
        }
        else
        {
            // Find if there is already a model with the same id
            int index = _performances.FindIndex(s => s.id == perf.id);

            if (index != -1)
            {
                // Update existing model
                _performances[index] = perf;
            }
            else
            {
                // Add new model
                _performances.Add(perf);
            }
        }

        DataAccess<PerformancesModel>.WriteAll(_performances);
    }


    public PerformancesModel GetPerfById(int id)
    {
        PerformancesModel? performance = _performances.FirstOrDefault(h => h.id == id);
        return performance != null ? performance : null;
    }

    public int GetNewId()
    {
        if (_performances == null || _performances.Count == 0)
        {
            return 1;
        }

        int currentId = 0;
        foreach (var performance in _performances)
        {
            if (performance.id > currentId)
            {
                currentId = performance.id;
            }
        }
        return currentId + 1;
    }


    public void DisplayTable()
    {

        HallLogic hallLogic = new HallLogic();

        Console.WriteLine($"{Color.Yellow}Table of all Performances:{Color.Reset}\n");

        Console.Write(Color.Blue);
        Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -15}{6, -20}", "ID", "Name", "Start", "End", "Hall", "Active", "Employees");
        Console.WriteLine($"{Color.Reset}-------------------------------------------------------------------------------------------------------------------------------");
        foreach (PerformancesModel performance in _performances)
        {
            string actstr = performance.active ? "Active" : "Inactive";
            string employeeString = "";
            foreach (string employee in performance.employees)
            {
                employeeString += employee;
                if (performance.employees.IndexOf(employee) == performance.employees.Count - 1)
                {

                }
                else
                {
                    employeeString += ", ";
                }
            }

            Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -15}{6, -20}", performance.id, performance.name, performance.startDate.ToString().Substring(0, performance.startDate.ToString().Length - 3), performance.endDate.ToString().Substring(0, performance.endDate.ToString().Length - 3), hallLogic.GetHallNameById(performance.hallId), actstr, employeeString);
        }
        Console.WriteLine("");

        return;
    }

    public bool DeletePerformance(int id)
    {
        PerformancesModel perfToRemove = _performances.Find(p => p.id == id);
        if (perfToRemove != null)
        {
            _performances.Remove(perfToRemove);
            DataAccess<PerformancesModel>.WriteAll(_performances);
            return true;
        }
        return false;
    }

    public List<Dictionary<string, object>> AddMaterials()
    {
        // List of dictionaries to store materials.
        List<Dictionary<string, object>> materials = new List<Dictionary<string, object>>();

        //Dialog.
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Materials List {Color.Red}Example{Color.Reset}{Color.Italic}:\n");
        Console.WriteLine("{0,-20}{1,-10}", "Material", "Quantity");
        Console.WriteLine(new string('-', 30));
        Console.WriteLine("{0,-20}{1,-10}", "Stoelen", "10");
        Console.WriteLine("{0,-20}{1,-10}\n", "Kleding", "10");

        Console.WriteLine($"{Color.FontReset}{Color.Yellow}Add materials for the performance (type '{Color.Italic}done{Color.FontReset}' when finished):{Color.Reset}");
        while (true)
        {
            Console.WriteLine($"{Color.Italic}Type 'done' when finished{Color.FontReset}");
            Console.WriteLine("Material: ");
            //User input for material.
            string material = Console.ReadLine();

            if (material.ToLower() == "done")
            {
                return materials;
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
                bool found = false;
                foreach (var item in materials)
                {
                    if (item["material"].ToString().ToLower() == material.ToLower())
                    {
                        item["quantity"] = (int)item["quantity"] + quantity;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Dictionary<string, object> materialEntry = new Dictionary<string, object>
                {
                    { "material", material },
                    { "quantity", quantity }
                };
                    materials.Add(materialEntry);
                }

                //Display of the user added materials.
                DisplayMaterials(materials);
            }
        }
    }
    //Overloaded Addmaterials to handle List inputs
    void AddMaterials(List<Dictionary<string, object>> materials)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Add Materials for the Performance (type '{Color.Italic}done{Color.FontReset}' when finished):{Color.Reset}");

        while (true)
        {
            Console.WriteLine($"{Color.Italic}Type 'done' when finished{Color.FontReset}");
            Console.WriteLine("Material: ");
            string material = Console.ReadLine();

            if (material.ToLower() == "done")
            {
                break;
            }

            if (!string.IsNullOrEmpty(material) && !material.Contains(" "))
            {
                Console.Write("Quantity: ");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine($"{Color.Red}Please enter a valid positive integer for quantity.{Color.Reset}");
                    Console.Write("Quantity: ");
                }

                bool found = false;
                for (int i = 0; i < materials.Count; i++)
                {
                    if (materials[i]["material"].ToString().ToLower() == material.ToLower())
                    {
                        int existingQuantity;
                        if (materials[i]["quantity"] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
                        {
                            existingQuantity = jsonElement.GetInt32();
                        }
                        else
                        {
                            existingQuantity = Convert.ToInt32(materials[i]["quantity"]);
                        }

                        materials[i]["quantity"] = existingQuantity + quantity;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Dictionary<string, object> materialEntry = new Dictionary<string, object>
                {
                    { "material", material },
                    { "quantity", quantity }
                };
                    materials.Add(materialEntry);
                }
                DisplayMaterials(materials, -1); // Update display after each addition.
            }
            else
            {
                Console.WriteLine($"{Color.Red}Please enter a valid material name without spaces.{Color.Reset}");
            }
        }
    }
    private void DisplayMaterials(List<Dictionary<string, object>> materials)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Materials List:{Color.Reset}\n");
        Console.WriteLine("{0,-20}{1,-10}", "Material", "Quantity");
        Console.WriteLine(new string('-', 30));

        foreach (var material in materials)
        {
            Console.WriteLine("{0,-20}{1,-10}", material["material"], material["quantity"]);
        }

        Console.WriteLine();
    }
    // Overloaded Display function for EditMaterials that takes a selected material option.
    void DisplayMaterials(List<Dictionary<string, object>> materials, int selectedIndex = -1)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Existing Materials:{Color.Reset}\n");
        Console.WriteLine($"{Color.Italic}{Color.Blue}Controls: {Color.Red}ESC{Color.Blue} to stop editing Materials, {Color.Red}Backspace{Color.Blue} to delete the Material and {Color.Red}Enter{Color.Blue} to add more Materials{Color.Reset}{Color.FontReset}");
        Console.WriteLine("{0,-20}{1,-10}", "Material", "Quantity");
        Console.WriteLine(new string('-', 30));

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

            Console.WriteLine("{0,-20}{1,-10}", materials[i]["material"], materials[i]["quantity"]);
            Console.Write($"{Color.Reset}");
        }

        if (materials.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No materials available.{Color.Reset}");
        }
    }
    public List<Dictionary<string, object>> EditMaterials(List<Dictionary<string, object>> materials)
    {
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
                        materials.RemoveAt(selectedMaterialIndex);
                        selectedMaterialIndex = materials.Count > 0 ? Math.Min(selectedMaterialIndex, materials.Count - 1) : -1;
                        DisplayMaterials(materials, selectedMaterialIndex);
                    }
                    break;

                case ConsoleKey.Enter:
                    // Add new material.
                    AddMaterials(materials);
                    DisplayMaterials(materials, selectedMaterialIndex);
                    break;

                case ConsoleKey.Escape:
                    return materials;

                default:
                    break;
            }
        }
    }

    public static int[][] ConvertBoolArrayToIntArray(int[,] intarray)
    {
        int rows = intarray.GetLength(0);
        int cols = intarray.GetLength(1);
        int[][] intArray = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            intArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                intArray[i][j] = intarray[i, j] != 0 ? 9 : 0;
            }
        }
        return intArray;
    }

}