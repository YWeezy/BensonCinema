static class Utils
{
    public static void PrettyWrite(object obj, string fileName)
    {
        var options = new JsonSerializerOptions(_options)
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(fileName, jsonString);
    }
}