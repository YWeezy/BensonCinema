using System;
using System.Collections.Generic;


public class ManageReviews
{
    private PerformanceLogic perfLogic;
    private PerformancesModel selectedPerformance;

    public ManageReviews()
    {
        perfLogic = new PerformanceLogic();
    }

    public static void Start()
    {
        ManageReviews manager = new ManageReviews();
        manager.DisplayPerformancesMenu();
    }

    private object DisplayPerformancesMenu()
    {
        Console.Clear();
        List<PerformancesModel> performances = perfLogic.GetPerformances();
        int selectedOption = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Color.Yellow}Select a Performance to Manage Reviews:{Color.Reset}");
            Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return to Performance Selection.{Color.Reset}");
            Console.WriteLine();
            for (int i = 0; i < performances.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Color.Green}>> {performances[i].name} {Color.Reset}");
                    Console.ResetColor();   
                }
                else
                {
                    Console.WriteLine($"   {performances[i].name}");
                }
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption == 0) ? performances.Count - 1 : selectedOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption == performances.Count - 1) ? 0 : selectedOption + 1;
                    break;
                case ConsoleKey.Enter:
                    selectedPerformance = performances[selectedOption];
                    ManageSelectedPerformance();
                    break;
                case ConsoleKey.Escape:
                    Menu.Start(); // Allow user to exit by pressing Escape
                    break;
                default:
                    break;
            }
        }
    }

    private void ManageSelectedPerformance()
    {
        if (selectedPerformance == null)
        {
            Console.WriteLine($"{Color.Red}No performance selected. Exiting...{Color.Reset}");
            return;
        }

        bool loop = true;
        int selectedOption = 1; // Default selected option
        int totalOptions = 3; // Total number of options

        while (loop)
        {
            Console.Clear();
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
                    PerformAction(selectedOption);
                    break;
                case ConsoleKey.Escape:
                    loop = false; // Exit the loop and return to performance selection
                    break;
                default:
                    break;
            }
        }
    }

    private void DisplayMenu(int selectedOption)
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Managing Reviews for: {selectedPerformance.name}{Color.Reset}");
        Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return to Performance Selection.{Color.Reset}");
        Console.WriteLine();
        Console.WriteLine($"{Color.Cyan}Select an option:{Color.Reset}");
        Console.WriteLine(selectedOption == 1 ? $"{Color.Green}>> View Reviews{Color.Reset}" : $"   View Reviews");
        Console.WriteLine(selectedOption == 2 ? $"{Color.Green}>> Delete Review{Color.Reset}" : $"   Delete Review");
        Console.WriteLine(selectedOption == 3 ? $"{Color.Green}>> Reply to Review{Color.Reset}" : $"   Reply to Review");
        
    }

    private void PerformAction(int selectedOption)
    {
        switch (selectedOption)
        {
            case 1:
                ViewReviews();
                break;
            case 2:
                DeleteReview();
                break;
            case 3:
                ReplyToReview();
                break;
        }
    }

    private void ViewReviews()
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Reviews for {Color.Reset}{Color.Blue}{selectedPerformance.name}:{Color.Reset}");
        Console.WriteLine();
        List<ReviewsModel> reviews = selectedPerformance.reviews;

        if (reviews.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No reviews available.{Color.Reset}");
        }
        else
        {
            foreach (var review in reviews)
            {
                Console.WriteLine($"{Color.Yellow}Rating: {review.rating} {Color.Reset}, {Color.Blue}Description: {review.description} {Color.Reset}, {Color.Cyan}Reply: {review.reply}{Color.Reset}");
            }
        }

        Console.WriteLine($"{Color.Yellow}Press any key to return to the menu...{Color.Reset}");
        Console.ReadKey(true); // Wait for key press to continue
    }

    private void DeleteReview()
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Select a review to delete for: {Color.Reset} {Color.Blue} {selectedPerformance.name}:{Color.Reset}");
        Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return.{Color.Reset}");
        Console.WriteLine();

        List<ReviewsModel> reviews = selectedPerformance.reviews;

        if (reviews.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No reviews available.{Color.Reset}");
        }
        else
        {
            int selectedOption = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{Color.Cyan}Select a review to delete:{Color.Reset}");
                Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return.{Color.Reset}");
                Console.WriteLine();

                for (int i = 0; i < reviews.Count; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.WriteLine($"{Color.Green}>>  {Color.Reset}{Color.Yellow}Rating: {reviews[i].rating}{Color.Reset}, {Color.Blue}Description: {reviews[i].description}{Color.Reset}");
                    }
                    else
                    {
                        Console.WriteLine($"    Rating: {reviews[i].rating}, Description: {reviews[i].description}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? reviews.Count - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == reviews.Count - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine($"{Color.Yellow}\nAre you sure you want to delete this review? (Y/N){Color.Reset}");
                        string confirmation = Console.ReadLine();
                        if (confirmation.ToLower() == "y")
                        {
                            selectedPerformance.reviews.RemoveAt(selectedOption);
                            perfLogic.UpdateList(selectedPerformance);
                            Console.WriteLine($"{Color.Green}Review deleted.{Color.Reset}");
                        }
                        else
                        {
                            Console.WriteLine($"{Color.Red}Review not deleted.{Color.Reset}");
                        }
                        return;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }

        Console.WriteLine($"{Color.Yellow}Press any key to return to the menu...{Color.Reset}");
        Console.ReadKey(true); // Wait for key press to continue
    }

    private void ReplyToReview()
    {
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Select a review to reply to for:{Color.Reset} {Color.Blue} {selectedPerformance.name}:{Color.Reset}");
        Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return.{Color.Reset}");
        Console.WriteLine();
        List<ReviewsModel> reviews = selectedPerformance.reviews;

        if (reviews.Count == 0)
        {
            Console.WriteLine($"{Color.Red}No reviews available.{Color.Reset}");
        }
        else
        {
            int selectedOption = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{Color.Cyan}Select a review to reply to:{Color.Reset}");
                Console.WriteLine($"{Color.Italic}{Color.Red}Press ESC to return.{Color.Reset}");
                Console.WriteLine();
                for (int i = 0; i < reviews.Count; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.WriteLine($"{Color.Green}>>  {Color.Reset}{Color.Yellow}Rating: {reviews[i].rating}{Color.Reset}, {Color.Blue}Description: {reviews[i].description}{Color.Reset}");
                    }
                    else
                    {
                        Console.WriteLine($"    Rating: {reviews[i].rating}, Description: {reviews[i].description}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? reviews.Count - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == reviews.Count - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine($"{Color.Cyan}\nEnter your reply:{Color.Reset}");
                        string reply = Console.ReadLine();
                        reviews[selectedOption].reply = reply;
                        perfLogic.UpdateList(selectedPerformance);
                        Console.WriteLine($"{Color.Green}Reply added.{Color.Reset}");
                        return;
                    case ConsoleKey.Escape:
                        return;    
                    default:
                        break;
                }
            }
        }

        Console.WriteLine($"{Color.Yellow}Press any key to return to the menu...{Color.Reset}");
        Console.ReadKey(true); // Wait for key press to continue
    }
}
