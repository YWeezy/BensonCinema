using System.Reflection;

namespace test
{ 

[TestClass]
    public class TestEmployeeSchedule
    {
        
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

        [TestMethod]
        public void TestGetUpdateSchedules()
        {
            // Arrange
            ScheduleLogic scheduleLogic = new ScheduleLogic();
            string newId = "dccbbb63-2839-42d6-8300-92fd02801442"; // hardcoded value for testing purposes

            List<Dictionary<string, object>> listOfDicts = new();
            List<Dictionary<string, object>> listOfDicts2 = new();

            SchedulesModel updateToAdd = new SchedulesModel(newId, "John", "01-01-2024", "8", "08:00", "16:00", new PerformancesModel(1, "Demo A", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts2, listOfDicts, true), true);
            
            // Act
            bool add = scheduleLogic.UpdateList(updateToAdd);

            // Assert
            Assert.IsTrue(add);
        }

        [TestMethod]
        public void TestRemoveSchedule()
        {
            // Arrange
            ScheduleLogic scheduleLogic = new ScheduleLogic();
            string idToRemove = "dccbbb63-2839-42d6-8300-92fd02801442"; // hardcoded value for testing purposes
            string idToRemoveThatDoesntExist = "2c458eff-a8f9-45f7-a7e5-a6b4aa0aaac5"; // hardcoded value for testing purposes
            
            // Act
            bool removed = scheduleLogic.RemoveSchedule(idToRemove);
            bool doesntExist = scheduleLogic.RemoveSchedule(idToRemoveThatDoesntExist);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(doesntExist);
        }
    }
}