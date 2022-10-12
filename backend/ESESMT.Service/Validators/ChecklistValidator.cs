using ESESMT.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Service.Validators
{
    public class ChecklistValidator : AbstractValidator<Checklist>
    {
        public ChecklistValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please specify a description");
        }
    }
}
