using Application.Dishes.Interface;
using Application.Dishes.Model;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure.Query
{
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _context;

        public DishQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<getDishResponse>> GetAllDishes(DishFilter? filter = null)
        {
            var query = _context.Dishes.AsQueryable();

            if (filter is not null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    query = query.Where(d => d.Name.Contains(filter.Name));
                }

                if (filter.CategoryId.HasValue)
                {
                    query = query.Where(d => d.CategoryId == filter.CategoryId.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.SortOrder))
                {
                    if (string.Equals(filter.SortOrder, "asc", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderBy(d => d.Price);
                    }
                    else if (string.Equals(filter.SortOrder, "desc", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.OrderByDescending(d => d.Price);
                    }
                }
            }

            var dishes = query.Select(d => new getDishResponse
            {
                DishId = d.DishId,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Available = d.Available,
                CategoryId = d.CategoryId,
                Category = d.Category,
                ImageUrl = d.ImageUrl
            }).ToListAsync();

            return await dishes;
        }

        public async Task<Dish?> GetDishById(System.Guid id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.DishId == id);
        }

        public async Task<List<string>> GetCategories()
        {
            return await _context.Dishes
                .Where(d => d.Category != null)
                .Select(d => d.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
    }
}
