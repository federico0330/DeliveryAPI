using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dishes.Model
{
    public class DishFilter
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive integer.")]
        public int? CategoryId { get; set; }

        [RegularExpression("^(?i)(asc|desc)$", ErrorMessage = "SortOrder must be 'asc' or 'desc'.")]
        public string? SortOrder { get; set; }
    }
}
