using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

static class Utils
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
