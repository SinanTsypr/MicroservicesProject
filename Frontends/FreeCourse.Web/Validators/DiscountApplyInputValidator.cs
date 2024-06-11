﻿using FluentValidation;
using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Lütfen indirim kuponu giriniz");
        }
    }
}
