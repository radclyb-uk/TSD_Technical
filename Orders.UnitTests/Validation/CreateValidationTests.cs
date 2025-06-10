using FluentValidation.TestHelper;
using Orders.Application.Commands.CreateOrder;
using Orders.Models.Dto.Common;

namespace Orders.UnitTests.Validation
{
    public class CreateValidationTests
    {
        private CreateOrderValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateOrderValidator();
        }

        [Test]
        public void CustomerNameTooLong_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                CustomerName = new string('a', 101)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameEmpty_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                CustomerName = new string(' ', 10)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameMissing_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                CustomerName = null
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameCorrect_ReturnsSuccess()
        {
            var model = new CreateOrderCommand()
            {
                CustomerName = "Test Name"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void OrderWithNoItems_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                Items = new List<OrderItemRequestDto>()
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Items);
        }

        [Test]
        public void OrderItemEmpty_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                Items = new List<OrderItemRequestDto>() { null }
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Items);
        }

        [Test]
        public void OrderHasItems_ReturnsSuccess()
        {
            var model = new CreateOrderCommand()
            {
                Items = new List<OrderItemRequestDto>() {  
                    new OrderItemRequestDto {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Items);
        }

        
        [Test]
        public void FutureOrder_ReturnsError()
        {
            var model = new CreateOrderCommand()
            {
                OrderDate = DateTime.Now.AddDays(1)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.OrderDate);
        }

        [Test]
        public void PastOrder_ReturnsSuccess()
        {
            var model = new CreateOrderCommand()
            {
                OrderDate = DateTime.Now.AddDays(-1)
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.OrderDate);
        }
    }
}
