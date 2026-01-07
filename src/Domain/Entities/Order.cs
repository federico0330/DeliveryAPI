namespace Domain.Entities
{
    public class Order
    {
        public long OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();

        public int DeliveryTypeId { get; set; }
        public DeliveryType? DeliveryType { get; set; }

        public string DeliveryTo { get; set; } = null!;

        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public string? Notes { get; set; }
        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}