public class TicketModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Seat { get; set; }
    public decimal Price { get; set; }
    public string RelationId { get; set; }

    public TicketModel(string seat, string title, int id, decimal price)
    {
        Seat = seat;
        Title = title;
        Id = id;
        Price = price;
        RelationId = Utils.LoggedInUser.Id;
    }
}