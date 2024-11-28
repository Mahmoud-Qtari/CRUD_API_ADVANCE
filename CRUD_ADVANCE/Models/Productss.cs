using System.ComponentModel.DataAnnotations;

namespace CRUD_ADVANCE.Models
{
    public class Productss
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "name is requried")]
        [MinLength(5, ErrorMessage = "min is 5")]
        [MaxLength(30, ErrorMessage = "max is 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "description is requried")]
        [MinLength(5, ErrorMessage = "min is 10")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is requried")]
        [Range(20,3000,ErrorMessage ="range between 20 and 3000")]
        public double Price { get; set; }
    }
}
