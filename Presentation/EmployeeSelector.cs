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

        Console.WriteLine("Please select the employee you want to Add/Remove the schedule from.");

        for (int i = 0; i < options.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(">>");
            }
            else 
            {
                Console.Write("   ");
            }
            Console.WriteLine(options[i]);
            Console.ResetColor();        
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