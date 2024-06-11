using Billing.Data.Dto;
using FluentValidation;

namespace API.Validators
{
    public class OrderInputValidator : AbstractValidator<OrderInputDto>
    {
        public OrderInputValidator()
        {
            RuleFor(order => order).NotNull();
            RuleFor(order => order.UserId).GreaterThan(0);
            RuleFor(order => order.OrderNumber).GreaterThan(0);
            RuleFor(order => order.GatewayType).IsInEnum();
            RuleFor(order => order.PaymentAmount).GreaterThan(0);
        }
    }
}
