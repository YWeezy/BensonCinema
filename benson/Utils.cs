using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Utils
{
    //read the passPhrase from the .env file
    private static string passPhrase = Environment.GetEnvironmentVariable("PASS_PHRASE") ?? "password";


    public static AccountModel LoggedInUser { get; set; } = null;

    public static string getPassword()
    {
        return passPhrase;
    }
    public static string Encrypt(string plainText)
    {
        byte[] salt = Encoding.ASCII.GetBytes(passPhrase);
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passPhrase, salt);
        RijndaelManaged rm = new RijndaelManaged();
        rm.Key = key.GetBytes(rm.KeySize / 8);
        rm.IV = key.GetBytes(rm.BlockSize / 8);
        ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherTextBytes;
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                cs.Close();
            }
            cipherTextBytes = ms.ToArray();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }


    public static string Decrypt(string encryptedText)
    {
        byte[] salt = Encoding.ASCII.GetBytes(passPhrase);
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passPhrase, salt);
        RijndaelManaged rm = new RijndaelManaged();
        rm.Key = key.GetBytes(rm.KeySize / 8);
        rm.IV = key.GetBytes(rm.BlockSize / 8);
        ICryptoTransform decryptor = rm.CreateDecryptor(rm.Key, rm.IV);
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = 0;
        using (MemoryStream ms = new MemoryStream(cipherTextBytes))
        {
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                decryptedByteCount = cs.Read(plainTextBytes, 0, plainTextBytes.Length);
            }
        }
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}

public class Color
{
    public static string Reset = "\u001B[0m";
    public static string Black = "\u001B[30m";
    public static string Red = "\u001B[31m";
    public static string Green = "\u001B[32m";
    public static string Yellow = "\u001B[33m";
    public static string Blue = "\u001B[34m";
    public static string Purple = "\u001B[35m";
    public static string Cyan = "\u001B[36m";
    public static string White = "\u001B[37m";
    public static string Blink = "\u001B[5m";
}


public class ConsoleInput
{
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


public static class DateSelector
{
    public static DateTime CurrentDate { get; private set; } = DateTime.Now;

    public static string GetDate(int weekLimit, bool isStartDate, DateTime? optionalDate = null)
    {
        CurrentDate = optionalDate ?? DateTime.Now; // Update the class-level property instead of defining a new local variable
        while (true)
        {
            Console.Clear();
            string message = isStartDate ? $"{Color.Yellow}Select a Start date? (< - month > + month ^ + day ):{Color.Reset}" : $"{Color.Yellow}Select a End date? (< - month > + month ^ + day ):{Color.Reset}";
            Console.WriteLine(message);
            Console.WriteLine(CurrentDate.ToString("dd-MM-yyyy"));
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    AdjustDate(1, 0, weekLimit);
                    break;
                case ConsoleKey.RightArrow:
                    AdjustDate(0, 1, weekLimit);
                    break;
                case ConsoleKey.DownArrow:
                    AdjustDate(-1, 0, weekLimit);
                    break;
                case ConsoleKey.LeftArrow:
                    AdjustDate(0, -1, weekLimit);
                    break;
                case ConsoleKey.Enter:
                    return CurrentDate.ToString("dd-MM-yyyy");

                default:
                    break;
            }
        }
    }

    public static string GetTime(bool isStartDate, DateTime? optionalTime = null)
    {
        DateTime CurrentTime = optionalTime ?? DateTime.Now.AddHours(1).Date.AddHours(DateTime.Now.Hour + 1);
        while (true)
        {
            Console.Clear();
            string message = isStartDate ? $"{Color.Yellow}Enter Start Time (< - 15 minutes > + 15 minutes ^ + hour ):{Color.Reset}" : $"{Color.Yellow}Enter End Time? (< - 15 minutes > + 15 minutes ^ + hour ):{Color.Reset}";
            Console.WriteLine(message);
            Console.WriteLine(CurrentTime.ToString("HH:mm"));
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    CurrentTime = CurrentTime.AddHours(1);
                    break;
                case ConsoleKey.LeftArrow:
                    CurrentTime = CurrentTime.AddMinutes(-15);
                    break;
                case ConsoleKey.RightArrow:
                    CurrentTime = CurrentTime.AddMinutes(15);
                    break;
                case ConsoleKey.DownArrow:
                    CurrentTime = CurrentTime.AddHours(-1);
                    break;
                case ConsoleKey.Enter:
                    var formattedTime = CurrentTime.ToString("HH mm");
                    string[] hoursAndMinutes = formattedTime.Split(" ");
                    return $"{hoursAndMinutes[0]}:{hoursAndMinutes[1]}";




                default:
                    break;
            }
        }
    }


    private static void AdjustDate(int dayIncrement, int monthIncrement, int weekLimit)
    {
        if (dayIncrement != 0)
        {
            CurrentDate = CurrentDate.AddDays(dayIncrement);
        }
        if (monthIncrement != 0)
        {
            CurrentDate = CurrentDate.AddMonths(monthIncrement);
        }

        DateTime maxDate = DateTime.Now.AddDays(weekLimit * 7);
        if (CurrentDate > maxDate)
        {
            CurrentDate = maxDate;
        }
        if (CurrentDate < DateTime.Now)
        {
            CurrentDate = DateTime.Now;
        }
    }
}




