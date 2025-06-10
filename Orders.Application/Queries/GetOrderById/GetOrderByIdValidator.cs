using FluentValidation;

namespace Orders.Application.Queries.GetOrderById
{
    internal class GetOrderByIdValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID must not be empty.")
                .NotEqual(Guid.Empty).WithMessage("Order ID must be a valid GUID.");
        }
    }
}
