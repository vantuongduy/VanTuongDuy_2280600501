using System.ComponentModel.DataAnnotations;

namespace VanTuongDuy_2280600501.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public required string Name { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
