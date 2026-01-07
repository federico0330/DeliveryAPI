using Application.Dishes.Interface;
using Application.Dishes.Model;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class DishCommand : IDishCommand
    {
        private readonly AppDbContext _context;
        public DishCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public Task UpdateDish(Dish dish, updateDishRequest request)
        {
            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Price = request.Price;
            dish.Available = request.Available;
            dish.CategoryId = request.CategoryId;
            dish.ImageUrl = request.ImageUrl;
            dish.UpdateDate = DateTime.UtcNow;
            _context.Dishes.Update(dish);
            return _context.SaveChangesAsync();
        }
    }
}