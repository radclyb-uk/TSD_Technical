using FluentValidation;
using Orders.Models.Dto.Common;

namespace Orders.Application.Common
{
    public class OrderItemValidator : AbstractValidator<OrderItemRequestDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name must not be empty.")
                .MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.UnitPrice)
                .GreaterThan(0.0m).WithMessage("Unit price must be greater than zero.");
        }
    }
}
