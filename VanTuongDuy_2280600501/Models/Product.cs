using System.ComponentModel.DataAnnotations;
namespace VanTuongDuy_2280600501.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string Name { get; set; }
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
       

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
