/// <summary>
/// Detects if we are running inside a unit test.
/// </summary>
public static class UnitTestDetector
{
    static UnitTestDetector()
    {
        string testAssemblyName = "Microsoft.VisualStudio.TestPlatform.TestFramework";
    UnitTestDetector.IsInUnitTest = AppDomain.CurrentDomain.GetAssemblies()
        .Any(a => a.FullName.StartsWith(testAssemblyName));
        
    }

    public static bool IsInUnitTest { get; private set; }
}