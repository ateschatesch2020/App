using App.Application.Contracts.Persistence;
using FluentValidation;


namespace App.Application.Features.Categories.Create
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public readonly ICategoryRepository _categoryRepository;

        public CreateCategoryRequestValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("category name is mandatory")
                .Length(3, 20).WithMessage("category name must contain between 3 and 20 characters.");
            //.Must(MustUniqueName).WithMessage("category name must be unique.");

        }
    }
}
