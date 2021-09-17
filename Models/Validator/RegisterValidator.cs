using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantafeApi.Models;

namespace SantafeApi.Models.Validator
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Primeiro nome é obrigatório")
                .MaximumLength(255);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Último nome é obrigatório")
                .MaximumLength(255);
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email inválido")
                .NotEmpty()
                .NotNull()
                .WithMessage("Email é obrigatório");
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("A senha deve conter no mínimo 8 caracteres")
                .NotEmpty()
                .NotNull()
                .WithMessage("A senha é obrigatória")
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")
                .WithMessage("A senha deve conter 8 caracteres sendo 1 maiúsculo, 1 numérico e 1 caractere especial");
        }
    }
}
