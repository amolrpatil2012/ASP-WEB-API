using System.ComponentModel.DataAnnotations;

namespace DemoRestApi.Models.Data
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
