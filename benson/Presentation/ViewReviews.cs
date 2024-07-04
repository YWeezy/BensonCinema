using System.ComponentModel;


class ViewReviews
{
    PerformancesModel performance;
    PerformanceLogic perfLogic;

    public ViewReviews(int id)
    {
        perfLogic = new PerformanceLogic();
        performance = perfLogic.GetPerfById(id);
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Reviews of {performance.name}{Color.Reset}");
            Console.WriteLine($"{Color.Italic}{Color.Blue}Controls: {Color.Red}ESC{Color.Blue} to go back to reservations, {Color.Red}A{Color.Blue} to add a review{Color.Reset}");
            Console.WriteLine("{0,-10}{1,-80}", "Rating", "Description");
            Console.WriteLine(new string('-', 90));
            List<ReviewsModel> listR = perfLogic.GetReviews(performance.id);
            if (listR.Count > 0)
            {
                foreach (var item in listR)
                {
                    Console.WriteLine("{0,-10}{1,-80}", item.rating, item.description);
                }
            }
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key == ConsoleKey.A)
            {
                AddReview();
            }
        }
    }

    public void AddReview()
    {
        while (true)
        {
            Console.WriteLine($"{Color.Yellow}Rating: {Color.Reset}");
            int rating;
            while (!int.TryParse(Console.ReadLine(), out rating) || rating < 0 || rating > 10)
            // Check Valid number
            {
                Console.WriteLine($"{Color.Red}Please enter a valid number between 0 and 10.{Color.Reset}");
                Console.Write($"{Color.Yellow}Rating: {Color.Reset}");
            }
            string description;
            Console.WriteLine($"{Color.Yellow}Description (Max 80 Char){Color.Reset}");
            do
            {
                description = Console.ReadLine();
                if (description.Length > 80)
                {
                    Console.WriteLine("Max characters exceeded");
                }
            } while (description.Length > 80);

            Console.WriteLine($"\n{Color.Yellow}Are you sure you want to add this Review? (Y/N){Color.Reset}");
            string confirmation = Console.ReadLine();
            if (confirmation.ToLower() == "y")
            {
                ReviewsModel add = new ReviewsModel(perfLogic.GetNewIdRev(performance), rating, description, string.Empty);
                
                performance.reviews.Add(add);
                perfLogic.UpdateList(performance);

                // dict.Add("id", performance.reviews.Max(p => p["id"]);

                // performance.reviews.Add({"id"})
                
                Console.WriteLine($"{Color.Green}✔️ Review added successfully.{Color.Reset}");
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"{Color.Red}❌ Review was not added.{Color.Reset}\n");
                break;
            }
        }
    }
}
