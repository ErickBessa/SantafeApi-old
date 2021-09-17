using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Models.Validator
{
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
	{
		public ResetPasswordValidator()
		{
			RuleFor(x => x.Email).EmailAddress().WithMessage("Formato inválido");
			RuleFor(x => x.Email).NotNull().WithMessage("Campo Requirido");
		}
	}
}
