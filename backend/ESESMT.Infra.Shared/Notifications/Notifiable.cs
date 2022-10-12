using FluentValidation;
using FluentValidation.Results;

namespace ESESMT.Infra.Shared.Notifications
{
    public abstract class Notifiable
    {
        public bool Valid { get; private set; }

        public bool Invalid => !Valid;

        public virtual ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
