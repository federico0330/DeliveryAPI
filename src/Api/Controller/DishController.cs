using Application.Dishes.Interface;
using Application.Dishes.Model;
using Application.Dishes.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;
        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromBody] createDishRequest request)
        {
            await dishService.createDish(request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDishes([FromQuery] Application.Dishes.Model.DishFilter? filter = null)
        {
            var result = await dishService.GetAllDishes(filter);
            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(Guid id, [FromBody] updateDishRequest request)
        {
            await dishService.updateDish(id, request);
            return Ok();
        }
    }
}