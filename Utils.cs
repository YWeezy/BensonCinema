static class Utils
{
    public static void PrettyWrite(object obj, string fileName)
    {
        var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, _options);
        File.WriteAllText(fileName, jsonString);
    }
}