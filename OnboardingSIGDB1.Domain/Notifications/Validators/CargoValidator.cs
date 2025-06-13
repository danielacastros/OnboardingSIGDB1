using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Notifications.Validators;

public class CargoValidator : AbstractValidator<Cargo>
{
    public CargoValidator()
    {
        RuleFor(c => c.Descricao)
            .NotEmpty().WithMessage(Resource.DescricaoObrigatoria)
            .MaximumLength(250).WithMessage(Resource.QuantidadeDeCaracteresInvalida);
    }
}