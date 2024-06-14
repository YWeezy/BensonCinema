using System.Text.Json.Serialization;



public class ShowSeats{
    public int[][] Seats;
    List<(int, int)> SelectedSeats = new();
    List<TicketType> SelectedTickets = new();
    public PerformanceLogic PLogic;
    public HallLogic HLogic;
    private int PerId;
    public List<TicketType> TicketTypes; 
    public ShowSeats(int perId){
        PLogic =  new PerformanceLogic();
        HLogic = new HallLogic();
        PerId = perId;
        Seats = PLogic.GetSeatsById(perId);
        TicketTypes = PLogic.GetTicketTypesById(perId);
        
        
    }

    public void PrintSeats(int rowSelected = -1, int seatSelected = -1){ 
        Console.Clear();
        Console.WriteLine($"{Color.Yellow}Please select a seat. You can select more seats afterwards if you want to.\n");
        Console.WriteLine($"{Color.Green} Use arrow keys to move. Press enter to select.");
        Console.WriteLine($"_ = Empty\nX = Occupied\nO = Selected{Color.Reset}");
        int rows = Seats.Length;
        int cols = Seats[0].Length;
        
        Console.WriteLine("  ");
        for (int i = 0; i < rows; i++) 
        {   
            if (rowSelected != -1 && rowSelected == i && seatSelected == -1)
            {
                Console.Write(Color.Blue);
            }
            
            int spacesCountLeft = 0;
            int spacecount = 0;
            for (int j = 0; j < cols; j++)
            {   
                  
                if (Seats[i][j] == 9){
                    spacecount += 1;

                }
            }
            

            if (spacecount % 2 != 0) // Check if the number is odd
                {
                    spacesCountLeft = (spacecount + 1) / 2;
                }
                else // If the number is even
                {
                    spacesCountLeft = spacecount / 2;
                }
                // System.Console.WriteLine(spacesCountLeft);
                for (int l = 0; l < spacesCountLeft; l++)
                {
                    Console.Write(" ");
                }
                    
                
                

            
            for (int j = 0; j < cols; j++)
            {   
                if (rowSelected != -1 && seatSelected == j && seatSelected != -1 && rowSelected == i)
                {
                    Console.Write(Color.Blue);
                }
                // _ = Empty
                // X = Occupied
                // O = Selected
                if (Seats[i][j] == 0){
                    Console.Write("_");
                }
                else if (Seats[i][j] == 1){
                    Console.Write("X");
                }
                else if (Seats[i][j] == 2){
                    Console.Write("O");
                }
                
                if (rowSelected != -1 && seatSelected == j && seatSelected != -1)
                {
                    Console.Write(Color.Reset);
                }

            }
            Console.Write(Color.Reset);
            Console.WriteLine();
        }
        // Print the screen 
        Console.WriteLine();
        Console.WriteLine(new string('=', Seats[0].Length));
        Console.WriteLine("     Podium     \n");
    }

    public List<(int, int)> SelectSeats(){

        bool done = false;
        while (!done){
            int rowselect = 0;
            int seatselect = 0;
            int maxrows = Seats.Length;
            PrintSeats(0);
            ConsoleKeyInfo selR;
            do
            {
                
                selR = Console.ReadKey();
                switch (selR.Key)
                {
                    case ConsoleKey.DownArrow:
                        
                        if (rowselect >= maxrows-1){
                            
                        }else{
                            Console.Clear();
                            rowselect++;
                            PrintSeats(rowselect);
                        }
                        
                        break;
                    case ConsoleKey.UpArrow:
                        if (rowselect < 1){
                            
                        }else{
                            Console.Clear();
                            rowselect -= 1;
                            PrintSeats(rowselect);
                        }
                        
                        break;
                }
                
            } while (selR.Key != ConsoleKey.Enter);
            Console.Clear();
            
            int[,] hall = HLogic.GetSeatsOfHall(PLogic.GetPerfById(PerId).hallId);
            int[][]hallJagged = ManagePerformance.ConvertInt2DArrayToIntJArray(hall);

            int countNoSeats = hallJagged[rowselect].Count(s => s == 9);
            PrintSeats(rowselect, 0);
            ConsoleKeyInfo selS;
            do
            {
                
                selS = Console.ReadKey();
                switch (selS.Key)
                {
                    case ConsoleKey.RightArrow:
                        
                        if (seatselect >= hallJagged[rowselect].Count()-countNoSeats-1){
                            
                        }else{
                            Console.Clear();
                            seatselect++;
                            PrintSeats(rowselect, seatselect);
                        }
                        
                        break;
                    case ConsoleKey.LeftArrow:
                        if (seatselect < 1){
                            
                        }else{
                            Console.Clear();
                            seatselect -= 1;
                            PrintSeats(rowselect, seatselect);
                        }
                        
                        break;
                }
                
            } while (selS.Key != ConsoleKey.Enter);
            if (Seats[rowselect][seatselect] == 2 || Seats[rowselect][seatselect] == 1)
            {
                Console.Clear();
                Console.WriteLine($"{Color.Red}❌ Seat is already selected or unavailable. Try again.{Color.Reset}");
                Console.ReadLine();
                
            }else{
                Console.WriteLine($"{Color.Yellow}Are you sure you want to select this seat? (y/n){Color.Reset}");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.Clear();
                    Console.WriteLine($"{Color.Yellow}Please choose a ticket for this seat:{Color.Reset}");
                    int id = 1;
                    foreach (var ticket in TicketTypes)
                    {
                        string formattedPrice = ticket.Price.ToString("F2");
                        Console.WriteLine($"{Color.Cyan}{id}: €{Convert.ToDouble(formattedPrice) / 100} - {ticket.Name}{Color.Reset}");
                        id++;
                    }

                    int ticketId = 0;
                    bool isValidInput = false;

                    while (!isValidInput)
                    {
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out int parsedInput))
                        {
                            ticketId = parsedInput - 1;
                            isValidInput = true;
                        }
                        else
                        {
                            Console.WriteLine($"{Color.Red}❌ Invalid input. Please enter a valid ticket ID.{Color.Reset}");
                        }
                    }

                    if (ticketId >= 0 && ticketId < TicketTypes.Count)
                    {
                        TicketType selectedTicket = TicketTypes[ticketId];
                        SelectedTickets.Add(selectedTicket);
                        SelectedSeats.Add((rowselect, seatselect));
                        Seats[rowselect][seatselect] = 2;
                        Console.WriteLine($"{Color.Green}✅ The seat & ticket type is selected.{Color.Reset}");
                    } else {
                        Console.WriteLine($"{Color.Red}❌ Invalid ticket selection. Please try again.{Color.Reset}");
                    }
                    Console.WriteLine($"{Color.Yellow}Do you want to add another seat? (y/n){Color.Reset}");
                    if (Console.ReadLine().ToLower() != "y")
                    {
                        Console.Clear();
                        Console.WriteLine($"{Color.Yellow}Selected seats:{Color.Reset}");

                        for (int i = 0; i < SelectedSeats.Count; i++)
                        {
                            RowLetter rowLetter = (RowLetter)SelectedSeats[i].Item1;
                            string row = rowLetter.ToString();
                            string seat = (SelectedSeats[i].Item2 + 1).ToString();
                            Console.WriteLine($"{Color.Cyan}Row: {row}, Seat: {seat} - {SelectedTickets[i].Name}");
                        }

                        int totalPrice = 0;

                        foreach (var ticket in SelectedTickets)
                        {
                            totalPrice += ticket.Price;
                        }

                        string formattedTotalPrice = totalPrice.ToString("F2");
                        
                        Console.WriteLine($"\n{Color.Yellow}Total price: {Color.Cyan}€{Convert.ToDouble(formattedTotalPrice) / 100}");

                        Console.WriteLine($"\n{Color.Yellow}Press Enter to make the payment.{Color.Reset}");
                        Console.ReadLine();
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment.");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment..");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment...");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment..");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment.");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment..");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment...");
                        Thread.Sleep(333);
                        
                        Console.Clear();
                        Console.WriteLine($"{Color.Cyan}Processing payment..");
                        Thread.Sleep(333);

                        Console.Clear();
                        Console.WriteLine($"{Color.Green}✅ Payment succesful!{Color.Reset}\n\n{Color.Yellow}Press Enter to continue{Color.Reset}");
                        Console.ReadLine();

                        done = true;
                    }
                }
                

                Console.Clear();
            }
            
        }
        

        return SelectedSeats;
    }

    public void SaveSeats(int Perfid){

        TicketLogic tinkiter = new TicketLogic();
        for (int i = 0; i < Seats.Length; i++)
        {
            for (int j = 0; j < Seats[0].Length; j++){
                if (Seats[i][j] == 2){
                    Seats[i][j] = 1;
                }
            }
        }
        PerformancesModel perf = PLogic.GetPerfById(PerId);
        perf.ticketsAvailable[0]["seats"] = Seats;
        PLogic.UpdateList(perf);

        for (int i = 0; i < SelectedSeats.Count; i++)
        {
            RowLetter rowLetter = (RowLetter)SelectedSeats[i].Item1;
            string row = rowLetter.ToString();
            string seat = (SelectedSeats[i].Item2 + 1).ToString();
            int price = SelectedTickets[i].Price; // Added price
            tinkiter.GenerateTicket(Perfid, seat, row, price);
        }
        }

        public enum RowLetter
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4,
            F = 5,
            G = 6,
        }

            
    
}