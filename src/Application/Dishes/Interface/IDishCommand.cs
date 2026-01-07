using Application.Dishes.Model;
using Domain.Entities;
namespace Application.Dishes.Interface
{
    public interface IDishCommand
    {
        Task InsertDish(Dish dish);
        Task UpdateDish(Dish dish, updateDishRequest request);
    }
}