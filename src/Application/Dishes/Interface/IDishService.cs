using Application.Dishes.Model;
using Domain.Entities;
using System;

namespace Application.Dishes.Interface
{
    public interface IDishService
    {
        Task createDish(createDishRequest request);
        Task<List<getDishResponse>> GetAllDishes(DishFilter? filter = null);
        Task updateDish(Guid id, updateDishRequest request);
        Task<List<string>> GetCategories();
    }
}