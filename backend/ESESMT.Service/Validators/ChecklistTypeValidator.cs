using ESESMT.Domain.Entities;
using FluentValidation;

namespace ESESMT.Service.Validators
{
    public class ChecklistTypeValidator : AbstractValidator<ChecklistType>
    {
        public ChecklistTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name");
        }
    }
}
