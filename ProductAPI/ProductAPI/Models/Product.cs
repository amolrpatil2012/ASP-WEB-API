using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1000, 100000)]
        public int Price { get; set; }

    }
}
