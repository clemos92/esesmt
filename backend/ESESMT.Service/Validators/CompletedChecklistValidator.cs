using ESESMT.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Service.Validators
{
    public class CompletedChecklistValidator : AbstractValidator<CompletedChecklist>
    {
        public CompletedChecklistValidator()
        {
            RuleFor(x => x.CreationDate).Must(BeAValidDate).WithMessage("CreationDate is required");
            RuleFor(x => x.CreationDate).Must(CannotBeAPastDate).WithMessage("CreationDate cannot be a past date");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool CannotBeAPastDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}