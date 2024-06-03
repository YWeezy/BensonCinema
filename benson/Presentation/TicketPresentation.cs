// using System;
// using System.Linq;

// public class TicketPresentation
// {
//     private PerformanceLogic performanceLogic = new PerformanceLogic();
//     private TicketLogic ticketLogic = new TicketLogic();

//     public void ShowAvailablePerformances()
//     {
//         performanceLogic.DisplayTable();
//     }

//     public void ReserveTicket()
//     {
//         int selectedPerformanceIndex = 0;
//         int totalPerformances = performanceLogic.GetTotalPerformances();

//         while (true)
//         {
//             DisplayPerformances(selectedPerformanceIndex);

//             var key = Console.ReadKey(true).Key;

//             switch (key)
//             {
//                 case ConsoleKey.UpArrow:
//                     selectedPerformanceIndex = selectedPerformanceIndex == 0 ? totalPerformances - 1 : selectedPerformanceIndex - 1;
//                     break;
//                 case ConsoleKey.DownArrow:
//                     selectedPerformanceIndex = selectedPerformanceIndex == totalPerformances - 1 ? 0 : selectedPerformanceIndex + 1;
//                     break;
//                 case ConsoleKey.Enter:
//                     ConfirmPerformanceSelection(selectedPerformanceIndex);
//                     return;
//                 case ConsoleKey.Escape:
//                     return;
//                 default:
//                     break;
//             }
//         }
//     }

//     private void DisplayPerformances(int selectedPerformanceIndex)
//     {
//         Console.Clear();
//         Console.WriteLine($"{Color.Yellow}Select a performance to reserve a ticket for:{Color.Blue}\n");

//         var performances = performanceLogic.GetPerformances();

//         Console.WriteLine("{0,-6}{1,-22}{2,-26}{3,-26}{4,-20}{5,-10}", "ID", "Name", "Start", "End", "Hall", "Active");
//         Console.WriteLine($"{Color.Reset}  -------------------------------------------------------------------------------------------------------------");

//         int index = 0;
//         foreach (var performance in performances)
//         {
//             Console.Write(index == selectedPerformanceIndex ? ">> " : "   ");
//             Console.WriteLine("{0,-6}{1,-22}{2,-26}{3,-26}{4,-20}{5,-10}", performance.id, performance.name, performance.startDate, performance.endDate, performance.hallId, performance.active ? "Active" : "Inactive");
//             index++;
//         }
//     }

//     private void ConfirmPerformanceSelection(int selectedPerformanceIndex)
//     {
//         var performance = performanceLogic.GetPerformances()[selectedPerformanceIndex];

//         Console.Clear();
//         Console.WriteLine($"Selected Performance: {performance.name}");
//         Console.WriteLine("Enter seat:");

//         string seat = Console.ReadLine();

//         ticketLogic.GenerateTicket(performance.id, seat);

//         Console.Clear();
//         Console.WriteLine("Ticket reserved successfully!");
//     }
// }
