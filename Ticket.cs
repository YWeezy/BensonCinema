public class Ticket
{
    public User name { set; get; }
    public static int ticketID { set; get; }
    public double price { set; get; }
    public int seat { set; get; }

    public Ticket(User user, double price, int seat)
    {
        this.name = user;
        this.price = price;
        this.seat = seat;
        ticketID++;
    }
}