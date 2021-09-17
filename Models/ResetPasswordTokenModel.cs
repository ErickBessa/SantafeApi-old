using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Models
{
	public class ResetPasswordTokenModel : ResetPasswordModel
	{
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Token { get; set; }

	}
}
