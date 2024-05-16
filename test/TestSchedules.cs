using System.Reflection;

namespace test
{ 

[TestClass]
    public class TestEmployeeSchedule
    {
        public static string schedulePath;

        //[TestMethod]
        //public void TestIsValidDate_ValidDate_ReturnsTrue()
        //{
        //    string validDate = "01-01-2024";
        //    bool result = EmployeeSchedule.IsValidDate(validDate);
        //    Assert.IsTrue(result);
        //}
    	//
        //[TestMethod]
        //public void TestIsValidDate_InvalidDate_ReturnsFalse()
        //{
        //    string invalidDate = "32-01-2024";
        //    bool result = EmployeeSchedule.IsValidDate(invalidDate);
        //    Assert.IsFalse(result);
        //}

        //[TestMethod]
        //public void TestGetUpdateSchedules()
        //{
        //// Arrange
        //ScheduleLogic scheduleLogic = new ScheduleLogic(schedulePath);
        //int newId = scheduleLogic.getID();
//
        //List<Dictionary<string, object>> listOfDicts = new();
//
        //ScheduleModel updateToAdd = new ScheduleModel(newId, "John", "01-01-2024", "8", "08:00", "16:00", new PerformanceModel(1, "Demo A", "Test description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 1, listOfDicts, true), true);
        //
        //// Act
        //scheduleLogic.UpdateList(updateToAdd);
//
        //// Assert
        //Assert.IsTrue(scheduleLogic.GetSchedules().Contains(updateToAdd));
        //}
    }
}