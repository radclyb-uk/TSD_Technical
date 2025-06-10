using FluentValidation;
using Orders.Application.Common;

namespace Orders.Application.Commands.CreateOrder
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name must not be empty.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");
            RuleFor(x => x.OrderDate)
                .NotNull().WithMessage("Order date must not be null.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order items must not be empty.");
            RuleForEach(x => x.Items)
                .NotEmpty().WithMessage("Order item must not be empty.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
                
                
        }
    }
}
