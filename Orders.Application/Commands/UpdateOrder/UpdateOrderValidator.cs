using FluentValidation;
using Orders.Application.Commands.UpdateOrder;
using Orders.Application.Common;

namespace Orders.Application.Commands.UpdateOrder
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Order ID must not be null.")
                .NotEqual(Guid.Empty).WithMessage("Order ID must not be empty.");
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name must not be empty.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status must be a valid OrderStatus enum value.");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order items must not be empty.");
            RuleForEach(x => x.Items)
                            .NotEmpty().WithMessage("Order item must not be empty.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());

        }
    }
}
