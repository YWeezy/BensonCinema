///namespace test
///{ 
///
///[TestClass]
///    public class TestEmployeeSchedule
///    {
///        public static string path;
///
///        [AssemblyInitialize]
///        public static void Initialize(TestContext context)
///        {
///            string excecutingAssemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
///            path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(excecutingAssemblyLocation), "../../../TestDataSources/schedule.json" );
///
///        }
///
///        [TestMethod]
///        public void TestIsValidDate_ValidDate_ReturnsTrue()
///        {
///            string validDate = "01-01-2024";
///            bool result = EmployeeSchedule.IsValidDate(validDate);
///            Assert.IsTrue(result);
///        }
///
///        [TestMethod]
///        public void TestIsValidDate_InvalidDate_ReturnsFalse()
///        {
///            string invalidDate = "32-01-2024";
///            bool result = EmployeeSchedule.IsValidDate(invalidDate);
///            Assert.IsFalse(result);
///        }
///    }
///}