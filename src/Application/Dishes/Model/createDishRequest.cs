namespace Application.Dishes.Model
{
    public class createDishRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}