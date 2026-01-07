namespace Domain.Entities
{
    public class Category
    {
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Order { get; set; }

    public List<Dish> Dishes { get; set; } = new();
    }
}
