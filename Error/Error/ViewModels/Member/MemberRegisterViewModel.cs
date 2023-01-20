using System.ComponentModel.DataAnnotations;

namespace Error.ViewModels.Member
{
	public class MemberRegisterViewModel
	{
		[Required]
		[StringLength(maximumLength:25)]
		public string Fullname { get; set; }

		[Required]
		[StringLength(maximumLength: 25)]
		public string Username { get; set; }

		[Required]
		[StringLength(maximumLength: 50)]
	
		public string Email { get; set; }

		[Required]
		[StringLength(maximumLength: 20, MinimumLength =8)]
		[DataType(DataType.Password)]
		public string Pasword { get; set; }


		[Required]
		[StringLength(maximumLength: 25 , MinimumLength =8)]
		[Compare(nameof(Pasword))]
		[DataType(DataType.Password)]
		public string ConfirePasword { get; set; }

	}
}
