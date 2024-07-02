using System.Dynamic;
using System.Globalization;
using System.Text.Json;
using System;

public class PerformanceLogic
{

    private List<PerformancesModel> _performances = new List<PerformancesModel>();

    public PerformanceLogic()
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

    public int GetNewIdRev(PerformancesModel perf)
    {
        int currentId = 0;
        System.Console.WriteLine(perf.reviews);
        if (perf.reviews.Count == 0)    
        {
            return 1;
        }
        int highestidperf = perf.reviews.Max(e => e.id);
        return highestidperf + 1;
       
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

    public List<ReviewsModel> GetReviews(int id){
        PerformancesModel perf = GetPerfById(id);
        return perf.reviews;
    }

    
    // //Overloaded Addmaterials to handle List inputs
    // void AddMaterials(List<Dictionary<string, object>> materials)
    // {
    //     Console.Clear();
    //     Console.WriteLine($"{Color.Yellow}Add Materials for the Performance (type '{Color.Italic}done{Color.FontReset}' when finished):{Color.Reset}");

    //     while (true)
    //     {
    //         Console.WriteLine($"{Color.Italic}Type 'done' when finished{Color.FontReset}");
    //         Console.WriteLine("Material: ");
    //         string material = Console.ReadLine();

    //         if (material.ToLower() == "done")
    //         {
    //             break;
    //         }

    //         if (!string.IsNullOrEmpty(material) && !material.Contains(" "))
    //         {
    //             Console.Write("Quantity: ");
    //             int quantity;
    //             while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
    //             {
    //                 Console.WriteLine($"{Color.Red}Please enter a valid positive integer for quantity.{Color.Reset}");
    //                 Console.Write("Quantity: ");
    //             }

    //             bool found = false;
    //             for (int i = 0; i < materials.Count; i++)
    //             {
    //                 if (materials[i]["material"].ToString().ToLower() == material.ToLower())
    //                 {
    //                     int existingQuantity;
    //                     if (materials[i]["quantity"] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
    //                     {
    //                         existingQuantity = jsonElement.GetInt32();
    //                     }
    //                     else
    //                     {
    //                         existingQuantity = Convert.ToInt32(materials[i]["quantity"]);
    //                     }

    //                     materials[i]["quantity"] = existingQuantity + quantity;
    //                     found = true;
    //                     break;
    //                 }
    //             }

    //             if (!found)
    //             {
    //                 Dictionary<string, object> materialEntry = new Dictionary<string, object>
    //             {
    //                 { "material", material },
    //                 { "quantity", quantity }
    //             };
    //                 materials.Add(materialEntry);
    //             }
    //             DisplayMaterials(materials, -1); // Update display after each addition.
    //         }
    //         else
    //         {
    //             Console.WriteLine($"{Color.Red}Please enter a valid material name without spaces.{Color.Reset}");
    //         }
    //     }
    // }
    
    // Overloaded Display function for EditMaterials that takes a selected material option.
    
    

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