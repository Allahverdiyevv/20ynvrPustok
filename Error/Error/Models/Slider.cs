using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Error.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:25)]
        public string Title1 { get; set; }
        [StringLength(maximumLength:25)]
        public string Title2 { get; set; }
        [StringLength(maximumLength:250)]
        public string Desc { get; set; }
        public string RedirectUrl1 { get; set; }
        [StringLength(maximumLength: 300)]
        public string RedirectUrlText { get; set; }
        [StringLength(maximumLength:100)]
        public string? Image { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
