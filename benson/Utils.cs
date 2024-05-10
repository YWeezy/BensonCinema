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
    public static DateTime CurrentDate = DateTime.Now;

    public static string GetDate(int weekLimit)
    {
        Console.WriteLine();
        do
        {
            Console.Clear();
            Console.WriteLine(CurrentDate.ToString("dd-MM-yyyy"));
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    CurrentDate = CurrentDate.AddDays(1);
                    if (CurrentDate > DateTime.Now.AddDays(weekLimit * 7))
                    {
                        CurrentDate = DateTime.Now.AddDays(weekLimit * 7);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    CurrentDate = CurrentDate.AddMonths(1);
                    if (CurrentDate > DateTime.Now.AddDays(weekLimit * 7))
                    {
                        CurrentDate = DateTime.Now.AddDays(weekLimit * 7);
                    }

                    break;
                case ConsoleKey.DownArrow:
                    CurrentDate = CurrentDate.AddDays(-1);
                    if (CurrentDate < DateTime.Now)
                    {
                        CurrentDate = DateTime.Now;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    CurrentDate = CurrentDate.AddMonths(-1);
                    if (CurrentDate < DateTime.Now)
                    {
                        CurrentDate = DateTime.Now;
                    }

                    break;


                case ConsoleKey.Enter:
                    return CurrentDate.ToString("dd-MM-yyyy");

                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;

        } while (true);
        return DateTime.Now.ToString("dd-MM-yyyy");
    }
    public static string GetTime()
    {
        Console.WriteLine();
        do
        {
            Console.Clear();
            Console.WriteLine(CurrentDate.ToString("HH:mm"));
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    CurrentDate = CurrentDate.AddHours(1);
                    break;

                case ConsoleKey.LeftArrow:
                    CurrentDate = CurrentDate.AddMinutes(-1);
                    break;

                case ConsoleKey.RightArrow:
                    CurrentDate = CurrentDate.AddMinutes(1);
                    break;

                case ConsoleKey.DownArrow:
                    CurrentDate = CurrentDate.AddHours(-1);
                    break;
                case ConsoleKey.Enter:
                    return CurrentDate.ToString("HH:mm");

                default:
                    break;
            }

            // Break the loop if user selects an action
            if (key == ConsoleKey.Enter)
                break;

        } while (true);
        return DateTime.Now.ToString("HH:mm");
    }
}



