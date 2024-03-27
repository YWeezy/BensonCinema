
using System.Text.Json;

static class Utils
{
    public static bool userIsLoggedIn = false;

    public static bool userIsEmployee = false;
    public static void PrettyWrite(object obj, string fileName)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

    

    
}
