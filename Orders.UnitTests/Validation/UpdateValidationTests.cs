using FluentValidation.TestHelper;
using Orders.Application.Commands.UpdateOrder;
using Orders.Models.Dto.Common;

namespace Orders.UnitTests.Validation
{
    public class UpdateValidationTests
    {
        private UpdateOrderValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateOrderValidator();
        }

        [Test]
        public void CustomerNameTooLong_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                CustomerName = new string('a', 101)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameEmpty_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                CustomerName = new string(' ', 10)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameMissing_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                CustomerName = null
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void CustomerNameCorrect_ReturnsSuccess()
        {
            var model = new UpdateOrderCommand()
            {
                CustomerName = "Test Name"
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.CustomerName);
        }

        [Test]
        public void OrderWithNoItems_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                Items = new List<OrderItemRequestDto>()
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Items);
        }

        [Test]
        public void OrderItemEmpty_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                Items = new List<OrderItemRequestDto>() { null }
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Items);
        }

        [Test]
        public void OrderHasItems_ReturnsSuccess()
        {
            var model = new UpdateOrderCommand()
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
        public void OrderIdEmpty_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                Id = Guid.Empty
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Id);
        }

        [Test]
        public void OrderIdMissing_ReturnsError()
        {
            var model = new UpdateOrderCommand()
            {
                Id = null
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Id);
        }
    }
}
