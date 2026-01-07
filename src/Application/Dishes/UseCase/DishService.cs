using Application.Dishes.Interface;
using Application.Dishes.Model;
using Domain.Entities;

namespace Application.Dishes.UseCase
{
    public class DishService : IDishService
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        public DishService(IDishCommand dishCommand, IDishQuery dishQuery)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
        }

        public async Task createDish(createDishRequest request)
        {
            var dish = new Dish
            {
                DishId = request.DishId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Available = request.Available,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                CreateDate = request.CreateDate
            };
            await _dishCommand.InsertDish(dish);
        }

        public Task<List<getDishResponse>> GetAllDishes(DishFilter? filter = null)
        {
            return _dishQuery.GetAllDishes(filter);
        }

        public async Task updateDish(Guid id, updateDishRequest request)
        {
            var dish = await _dishQuery.GetDishById(id);
            if (dish == null)
            {
                throw new Exception("Dish not found");
            }
            await _dishCommand.UpdateDish(dish, request);
        }
    }
}