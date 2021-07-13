using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Bag
{
    public class LetterBag : Bag
    {
        [Required]
        [Range(1, int.MaxValue)]
        [DisplayName("Count of letters")]
        public int LetterCount { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, (double) decimal.MaxValue)]
        public decimal Price { get; set; }
    }
}