using System.Reflection;

namespace test
{ 

[TestClass]
    public class TestSeats
    {
        
        [TestMethod]
        public void TestPrintSeatsTrue()
        {
            bool test = true;
            PerformanceLogic PLogic = new PerformanceLogic();
            int[][] seats = PLogic.GetSeatsById(6);
            int[][] result = new int[][] {
                new int[] { 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9 }
            };
            int rows = result.Length;
            int maxelem = result[0].Length;
            for (int x = 0; x < rows; x += 1) {
                for (int y = 0; y < maxelem; y += 1) {
                    if (result[x][y] != seats[x][y]){
                        test = false;
                    }
                }
            }

            Assert.IsTrue(test);
        }
        [TestMethod]
        public void TestPrintSeatsFalse()
        {
            bool test = true;
            PerformanceLogic PLogic = new PerformanceLogic();
            int[][] seats = PLogic.GetSeatsById(6);
            int[][] result = new int[][] {
                new int[] { 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9 },
                new int[] { 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9 }
            };
            int rows = result.Length;
            int maxelem = result[0].Length;
            for (int x = 0; x < rows; x += 1) {
                for (int y = 0; y < maxelem; y += 1) {
                    if (result[x][y] != seats[x][y]){
                        test = false;
                    }
                }
            }

            Assert.IsFalse(test);
        }
    }
}