using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("product name is mandatory")
                .Length(3, 20).WithMessage("product name must contain between 3 and 20 characters.");
            //.Must(MustUniqueName).WithMessage("product name must be unique.");

            //price validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("price must be greater than 0.");

            //stock validation
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("stock must be between 1 and 100.");
        }

        ////1.way: synchronous validation
        //private bool MustUniqueName(string name)
        //{
        //        var existingProduct = _productRepository.Where(x => x.Name == name).Any();
        //        return !existingProduct;

        //    // false => there is an error
        //    // true => there is no error
        //}
    }
}
