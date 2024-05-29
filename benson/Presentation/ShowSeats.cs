using Microsoft.VisualBasic;

public class ShowSeats{
    public int[][] Seats;
    public PerformanceLogic PLogic;
    public HallLogic HLogic;
    private int PerId;
    public ShowSeats(int perId){
        PLogic =  new PerformanceLogic();
        HLogic = new HallLogic();
        PerId = perId;
        Seats = PLogic.GetSeatsById(perId);
        
        
    }

    public void PrintSeats(int rowSelected = -1, int seatSelected = -1){ 
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
    }

    public List<(int, int)> SelectSeats(){
        List<(int, int)> SelectedSeats = new();
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
                Console.WriteLine("Seat is already selected or unavailable. Try again.");
                
            }else{
                Console.WriteLine("Are you sure you want to select this seat? (y/n)");
                if (Console.ReadLine().ToLower() == "y")
                {
                    SelectedSeats.Add((rowselect, seatselect));
                    Seats[rowselect][seatselect] = 2;
                    Console.WriteLine("Do you want to add another seat? (y/n)");
                    if (Console.ReadLine().ToLower() != "y")
                    {
                        done = true;
                    }
                }
                

                Console.Clear();
            }
            
        }
        

        return SelectedSeats;
    }

    public void SaveSeats(){
        for (int i = 0; i < Seats.Length; i++)
        {
            for (int j = 0; j < Seats[0].Length; j++){
                if (Seats[i][j] == 2){
                    Seats[i][j] = 1;
                }
            }
        }
        PerformanceModel perf = PLogic.GetPerfById(PerId);
        perf.ticketsAvailable[0]["seats"] = Seats;
        PLogic.UpdateList(perf);



    }
        
    
}