using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Models.Validator
{
	public class ResetPasswordTokenValidator : AbstractValidator<ResetPasswordTokenModel>
	{
		public ResetPasswordTokenValidator()
		{
			RuleFor(x => x.Email).EmailAddress().WithMessage("Email no formato errado.").NotEmpty().WithMessage("Não pode ser vazio");
			RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Senha não pode ser vazia").Equal(x => x.Password).WithMessage("Senhas não conferem");
			RuleFor(x => x.Password).MinimumLength(8).NotEmpty().NotNull().Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$").WithMessage("A senha deve conter 8 caracteres sendo 1 maíusculo, 1 numérico e 1 caractere especial");
			RuleFor(x => x.Token).NotEmpty().WithMessage("Token não pode ser nullo");
		}
	}
}
