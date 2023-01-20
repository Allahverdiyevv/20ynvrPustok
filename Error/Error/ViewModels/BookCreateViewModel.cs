using System.ComponentModel.DataAnnotations;

namespace Error.ViewModels
{
    public class BookCreateViewModel
    {
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        [StringLength(maximumLength:20 , ErrorMessage ="Max 20 olar")]
        public string Name { get; set; }

        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        [StringLength(maximumLength: 10, ErrorMessage = "Max 10 olar")]
        public string Code { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "Max 250 olar")]
        public string Desc { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; }
     
        public bool IsNew { get; set; }

    }
}
