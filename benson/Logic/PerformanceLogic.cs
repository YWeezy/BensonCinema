using System.Globalization;

public class PerformanceLogic
{

    private List<PerformanceModel> _performances = new List<PerformanceModel>();
    public string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/performances.json"));

    public PerformanceLogic(string? newPath = null)
    {
        if (newPath != null) {
            path = newPath;
        } 
        _performances = DataAccess<PerformanceModel>.LoadAll(path);
    }

    public List<PerformanceModel> GetPerformances(string from = "01-01-0001", string to = "31-12-9999")
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

    public List<PerformanceModel> GetActivePerformances()
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

    public void UpdateList(PerformanceModel perf)
    {
        if (_performances == null || _performances.Count == 0)
        {
            // If _performances is null or empty, add the performance directly
            _performances = new List<PerformanceModel>() { perf };
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
        
        DataAccess<PerformanceModel>.WriteAll(_performances, path);
    }


    public PerformanceModel GetPerfById(int id)
    {
        PerformanceModel? performance = _performances.FirstOrDefault(h => h.id == id);
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
        foreach (PerformanceModel performance in _performances)
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

            Console.WriteLine("{0,-6}{1,-22}{2,-26}{3, -26}{4, -20}{5, -15}{6, -20}", performance.id, performance.name, performance.startDate, performance.endDate, hallLogic.GetHallNameById(performance.hallId), actstr, employeeString);
        }
        Console.WriteLine("");

        return;
    }

    public bool DeletePerformance(int id)
    {
        PerformanceModel perfToRemove = _performances.Find(p => p.id == id);
        if (perfToRemove != null)
        {
            _performances.Remove(perfToRemove);
            DataAccess<PerformanceModel>.WriteAll(_performances, path);
            return true;
        }
        return false;
    }

    public List<Dictionary<string, object>> AddMaterials()
    {
        // List of dictionaries to store materials
        List<Dictionary<string, object>> materials = new List<Dictionary<string, object>>();

        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Materials List {Color.Red}Example{Color.Reset}{Color.Italic}:\n");
        Console.WriteLine("{0,-20}{1,-10}", "Material", "Quantity");
        Console.WriteLine(new string('-', 30));
        Console.WriteLine("{0,-20}{1,-10}", "Stoelen", "10");
        Console.WriteLine("{0,-20}{1,-10}\n", "Achrafen", "100");

        Console.WriteLine($"{Color.FontReset}{Color.Yellow}Add materials for the performance (type '{Color.Italic}done{Color.FontReset}' when finished):{Color.Reset}");
        while (true)
        {
            Console.WriteLine($"{Color.Italic}Type 'done' when finished{Color.FontReset}");
            Console.WriteLine("Material: ");
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
                {
                    Console.WriteLine($"{Color.Red}Please enter a valid positive integer for quantity.{Color.Reset}");
                    Console.Write("Quantity: ");
                }

                Dictionary<string, object> materialEntry = new Dictionary<string, object>();
                materialEntry["material"] = material;
                materialEntry["quantity"] = quantity;
                materials.Add(materialEntry);

                DisplayMaterials(materials);
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


}