using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() 
        { 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("product name is mandotary")
                .Length(3, 10).WithMessage("product name must contain at least 3 character.");
        }
    }
}
