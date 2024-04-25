using System.Drawing;

public class EmployeeSelector
{
    private readonly List<string> options;
    private int selectedIndex;

    public EmployeeSelector(List<string> options)
    {
        this.options = options;
        selectedIndex = 0;
    }

    public int SelectedIndex
    {
        get { return selectedIndex; }
    }

    public void Run()
    {

        Console.CursorVisible = false;

        DisplayOptions();

        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                MoveSelectionUp();
                break;
                case ConsoleKey.DownArrow:
                MoveSelectionDown();
                break;
            
            }
            
            DisplayOptions();
        } while (key.Key != ConsoleKey.Enter);

        Console.CursorVisible = true;
    }
    private void DisplayOptions()
    {
        Console.Clear();
        string color = "\u001b[32m";
        string neutral = " \u001b[0m";
        Console.WriteLine($"{neutral}Please select the employee you want to modify the schedule for:");

        for (int i = 0; i < options.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.Write($"{ color }>>  ");
            }
            else 
            {
                Console.Write($"{neutral}   ");
            }
            Console.WriteLine(options[i]);      
        }
    }
    private void MoveSelectionUp()
    {
        selectedIndex = Math.Max(0,selectedIndex -1);
    }
    private void MoveSelectionDown()
    {
        selectedIndex = Math.Min(options.Count - 1, selectedIndex +1);
    }
}