using System.ComponentModel.DataAnnotations;

namespace Error.ViewModels.Member
{
	public class MemberLoginViewModel
	{
		[StringLength(maximumLength:40)]
		public string UserName { get; set; }
		[Required]
		[StringLength(maximumLength:30 ,MinimumLength =8),DataType(DataType.Password)]
		public string Pasword { get; set; }
	}
}
