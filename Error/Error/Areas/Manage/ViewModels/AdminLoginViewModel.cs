//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Error.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
   
        [StringLength(maximumLength:20)]
        public string UserName { get; set; }

      
        [StringLength(maximumLength: 20,MinimumLength =8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
