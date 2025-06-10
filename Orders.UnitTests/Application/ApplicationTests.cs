using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Commands.UpdateOrder;
using Orders.Application.Configuration;
using Orders.Application.Queries.GetOrderById;
using Orders.Contracts.Infrastructure;
using Orders.Domain.Entities;
using Orders.Infrastructure.Repositories;
using Orders.Models.Dto.Common;
using Orders.UnitTests.Mocks;

namespace Orders.UnitTests.Application
{
    public class Tests
    {
        public ServiceProvider ServiceProvider { get; private set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddApplicationInjection();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderRepository, MockOrderRepository>();
            ServiceProvider = services.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceProvider?.Dispose();
        }

        [Test]
        public async Task Order_CanBeRetrieved()
        {
            var guid = Guid.NewGuid();

            var mediator = ServiceProvider.GetRequiredService<IMediator>();

            var query = new GetOrderByIdQuery(guid);

            // Act
            var response = await mediator.Send(query);

            Assert.That(response.Success, Is.True, "Should successfully retrieve an order");
            Assert.That(response.Data.Id, Is.EqualTo(guid), "Guid of returned object should match the query");
        }

        [Test]
        public async Task OrderStatus_CanBeUpdated()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var model = new Order
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Status = Domain.Entities.OrderStatus.Pending,
                TotalAmount = 0,
                Items = new List<OrderItem>() {
                    new OrderItem {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            // Setup
            model.Id = await repository.AddAsync(model);

            var command = mapper.Map<UpdateOrderCommand>(model);
            command.Status = Models.Dto.Common.OrderStatus.Processing;

            // Act
            var response = await mediator.Send(command);

            // Validate
            Assert.That(response.Success, Is.True, "Should successfully retrieve an order");
            Assert.That(response.Data.Status, Is.EqualTo(Models.Dto.Common.OrderStatus.Processing));
        }

        [Test]
        public async Task DelevieredOrder_CanNotBeModified()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var model = new Order
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Status = Domain.Entities.OrderStatus.Delivered,
                TotalAmount = 0,
                Items = new List<OrderItem>() {
                    new OrderItem {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            // Setup
            model.Id = await repository.AddAsync(model);

            var command = mapper.Map<UpdateOrderCommand>(model);
            command.Status = Models.Dto.Common.OrderStatus.Processing;

            // Act
            var response = await mediator.Send(command);

            // Validate
            Assert.That(response.Success, Is.False, "Should fail to update a delivered order");
        }
        
        [Test]
        public async Task  CancelledOrder_CanNotBeModified()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var model = new Order
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Status = Domain.Entities.OrderStatus.Cancelled,
                TotalAmount = 0,
                Items = new List<OrderItem>() {
                    new OrderItem {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            // Setup
            model.Id = await repository.AddAsync(model);

            var command = mapper.Map<UpdateOrderCommand>(model);
            command.Status = Models.Dto.Common.OrderStatus.Processing;

            // Act
            var response = await mediator.Send(command);

            // Validate
            Assert.That(response.Success, Is.False, "Should fail to update a delivered order");
        }

        [Test]
        public async Task OrderWithItems_CanBeCreated()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var command = new CreateOrderCommand()
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Items = new List<OrderItemRequestDto>() {
                    new OrderItemRequestDto {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            var response = await mediator.Send(command);
            Assert.That(response.Success, Is.True, "Should successfully create an order with items");
            Assert.That(response.Data.Status, Is.EqualTo(Models.Dto.Common.OrderStatus.Pending), "New order should have status Pending");
        }


        [Test]
        public async Task OrderTotal_IsCalculatedOnCreate()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var command = new CreateOrderCommand()
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Items = new List<OrderItemRequestDto>() {
                    new OrderItemRequestDto {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    },
                    new OrderItemRequestDto {
                        ProductName = "Test Product 2",
                        Quantity = 2,
                        UnitPrice = 20.0m
                    }
                }
            };

            var response = await mediator.Send(command);
            Assert.That(response.Success, Is.True, "Should successfully create an order with items");
            Assert.That(response.Data.TotalAmount, Is.EqualTo(50.0m), "Total amount should be calculated correctly based on order items");
        }

        [Test]
        public async Task OrderTotalIsCalculatedOnUpdate()
        {
            var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            var model = new Domain.Entities.Order
            {
                CustomerName = "Test Customer",
                OrderDate = DateTime.Today,
                Status = Domain.Entities.OrderStatus.Pending,
                TotalAmount = 0,
                Items = new List<OrderItem>() {
                    new OrderItem {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 10.0m
                    }
                }
            };

            var command = new UpdateOrderCommand()
            {
                CustomerName = "Test Customer",
                Status = Models.Dto.Common.OrderStatus.Pending,
                Items = new List<OrderItemRequestDto>() {
                    new OrderItemRequestDto {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = 30.0m
                    },
                    new OrderItemRequestDto {
                        ProductName = "Test Product 2",
                        Quantity = 2,
                        UnitPrice = 20.0m
                    }
                }
            };

            // Setup
            command.Id = await repository.AddAsync(model);

            var response = await mediator.Send(command);
            Assert.That(response.Success, Is.True, "Should successfully create an order with items");
            Assert.That(response.Data.TotalAmount, Is.EqualTo(70.0m), "Total amount should be calculated correctly based on order items");
        }
    }
}
