using FluentValidation.TestHelper;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Common;
using Orders.Models.Dto.Common;

namespace Orders.UnitTests.Validation
{
    public class OrderItemValidationTests
    {
        private OrderItemValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new OrderItemValidator();
        }


        [Test]
        public void ProductNameEmpty_ReturnsError()
        {
            var model = new OrderItemRequestDto {
                ProductName = string.Empty    
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ProductName);
        }

        [Test]
        public void ProductNameMissing_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                ProductName = null
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ProductName);
        }

        [Test]
        public void ProductNameTooLong_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                ProductName = new string('a', 51)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ProductName);
        }

        [Test]
        public void ProductNameCorrect_ReturnsSuccess()
        {
            var model = new OrderItemRequestDto
            {
                ProductName = "Test Product"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.ProductName);
        }

        [Test]
        public void QuantityZero_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                Quantity = 0
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Quantity);
        }

        [Test]
        public void QuantityNegative_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                Quantity = -3
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Quantity);
        }

        [Test]
        public void QuantityPositive_ReturnsSuccess()
        {
            var model = new OrderItemRequestDto
            {
                Quantity = 1
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Quantity);
        }

        [Test]
        public void PriceZero_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                UnitPrice = 0
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UnitPrice);
        }

        [Test]
        public void PriceNegative_ReturnsError()
        {
            var model = new OrderItemRequestDto
            {
                UnitPrice = -5.3m
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UnitPrice);
        }

        [Test]
        public void PricePositive_ReturnsSuccess()
        {
            var model = new OrderItemRequestDto
            {
                UnitPrice = 4.6m
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.UnitPrice);
        }
    }
}
