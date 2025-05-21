
using FluentValidation;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Notifications.Validators;

namespace OnboardingSIGDB1.Domain.Entity;

public class Entidade
{
    public int Id { get; private set; }
    public bool Valid { get; private set; }
    public bool Invalid => !Valid;
    public ValidationResult ValidationResult { get; private set; }

    public bool Validar<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return Valid = ValidationResult.IsValid;
    }
}