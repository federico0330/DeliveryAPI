namespace Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}