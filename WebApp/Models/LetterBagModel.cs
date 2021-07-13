using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LetterBagModel : BagModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int LetterCount { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }
    }
}