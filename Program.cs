using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a test ticket
       
        Ticket testTicket = new Ticket("jhonn",50.0,101);
        Ticket.Reserve(testTicket); // Reserve the test ticket

        // Create a test account
        AccountModel testAccount = new AccountModel(1, "test@example.com", "password", "Test User", 1);

        // Print details of the ticket associated with the account's ticket ID
        Console.WriteLine("Printing ticket details:");
        testAccount.PrintTicketDetails(AccountModel.TicketID);

        Console.ReadLine(); // Keep the console open
    }
}
