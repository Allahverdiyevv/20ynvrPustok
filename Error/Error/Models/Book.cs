using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Error.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }

        [StringLength(maximumLength:20,ErrorMessage ="Max 20 olar")]
        public string Name { get; set; }
      
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        [StringLength(maximumLength: 10, ErrorMessage = "Max 10 olar")]
        public string Code { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "Max 250 olar")]
        public string Desc { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public Genre? Genre { get; set; }
        public Author? Author { get; set; }

        public List<BookImage>? bookImages { get; set; }

        [NotMapped]
        public IFormFile? PosterImageFile { get; set; }
        [NotMapped]

        public IFormFile? HoverImageFile { get; set; }

        [NotMapped]
        public List<IFormFile>? ImageFile { get; set; }
        [NotMapped]
        public List<int> BookImageIds { get; set; }

        public List<BasketItem> basketItems { get; set; }


        public List<OrderItem> orderItems { get; set; }
        
                
    }
}
