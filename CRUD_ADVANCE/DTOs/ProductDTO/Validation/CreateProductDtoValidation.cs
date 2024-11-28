using FluentValidation;

namespace CRUD_ADVANCE.DTOs.ProductDTO.Validation
{
    public class CreateProductDtoValidation: AbstractValidator<CreateProductDTO>
    {
        public CreateProductDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("name is required!!");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("min is 5!!");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("max is 30!!");
            RuleFor(x=> x.Description).NotEmpty().WithMessage("description is required!!");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("min is 10!!");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("price is required!!");
            RuleFor(x => x.Price).ExclusiveBetween(20, 3000).WithMessage("between 20 and 3000");


        }
    }
}
