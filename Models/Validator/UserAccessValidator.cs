using FluentValidation;
namespace SantafeApi.Models.Validator
{
    public class UserAccessValidator : AbstractValidator<UserAccessModel>
    {
        public UserAccessValidator()
        {
            RuleFor(x => x.CodCliente).NotNull().WithMessage("O código do cliente precisa ser específicado");
            RuleFor(x => x.UserId).NotNull().WithMessage("O código do usuário precisa ser específicado");
            RuleFor(x => x.HasAccess).NotNull().WithMessage("O tipo de acesso precisa ser específicado");
        }
    }
}
