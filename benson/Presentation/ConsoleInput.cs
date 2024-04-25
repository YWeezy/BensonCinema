public class ConsoleInput {
    public static T EditLine<T>(T initialValue)
    {
        string? value = initialValue.ToString();
        int cursorPosition = value.Length;
        Console.Write(value);
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Backspace && cursorPosition > 0)
            {
                value = value.Remove(cursorPosition - 1, 1);
                cursorPosition--;
                UpdateConsole(value, cursorPosition);
            }
            else if (keyInfo.Key == ConsoleKey.Delete && cursorPosition < value.Length)
            {
                value = value.Remove(cursorPosition, 1);
                UpdateConsole(value, cursorPosition);
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow && cursorPosition > 0)
            {
                cursorPosition--;
                UpdateConsole(value, cursorPosition);
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow && cursorPosition < value.Length)
            {
                cursorPosition++;
                UpdateConsole(value, cursorPosition);
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                value = value.Insert(cursorPosition, keyInfo.KeyChar.ToString());
                cursorPosition++;
                UpdateConsole(value, cursorPosition);
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return (T)Convert.ChangeType(value, typeof(T));
    }

    private static void UpdateConsole(string value, int cursorPosition)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(value);
        Console.SetCursorPosition(cursorPosition, Console.CursorTop);
    }
}