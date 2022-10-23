using System.ComponentModel.DataAnnotations;

namespace BlazorSPABookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        public string Description { get; set; }

        public double Value { get; set; }

        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "The field Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The field Category is required")]
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
    }
}