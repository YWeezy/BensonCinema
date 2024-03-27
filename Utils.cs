using System;
using System.Text;
using System.Security.Cryptography;


static class Utils
{
    public static bool userIsLoggedIn = false;

    public static bool userIsEmployee = false;




    // generate a encryp and decrypt method for the password

    public static string passPhrase = "IwantToLearnC#SoMuch!";

    public static string Encrypt(string plainText, string passPhrase)
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

    public static string Decrypt(string encryptedText, string passPhrase)
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

