using Application.Dishes.Model;
using Domain.Entities;
using System;

namespace Application.Dishes.Interface
{
    public interface IDishQuery
    {
        Task<Dish> GetDishById(Guid id);
        Task<List<getDishResponse>> GetAllDishes(DishFilter? filter = null);
        Task<List<string>> GetCategories();
    }
}