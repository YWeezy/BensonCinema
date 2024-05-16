using System.Reflection;
namespace test
{ 

[TestClass]
    public class TestEmployeeSchedule
    {
        public static string path;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../TestDataSources/schedule.json" );

        }

        [TestMethod]
        public void TestIsValidDate_ValidDate_ReturnsTrue()
        {
            string validDate = "01-01-2024";
            bool result = EmployeeSchedule.IsValidDate(validDate);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsValidDate_InvalidDate_ReturnsFalse()
        {
            string invalidDate = "32-01-2024";
            bool result = EmployeeSchedule.IsValidDate(invalidDate);
            Assert.IsFalse(result);
        }
    }
}