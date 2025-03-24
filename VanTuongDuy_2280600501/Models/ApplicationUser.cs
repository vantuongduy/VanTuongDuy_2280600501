using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VanTuongDuy_2280600501.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}
