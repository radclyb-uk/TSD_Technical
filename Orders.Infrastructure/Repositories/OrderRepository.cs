using Microsoft.EntityFrameworkCore;
using Orders.Contracts.Infrastructure;
using Orders.Domain.Entities;
using Orders.Infrastructure.Contexts;

namespace Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            var newOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return newOrder.Entity.Id;
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            var oldIds = order.Items.Select(i => i.Id).ToList();
            var deleteItems = _context.OrderItems
                .Where(oi => oi.OrderId == order.Id && !oldIds.Contains(oi.Id));

            _context.OrderItems.RemoveRange(deleteItems);
            _context.Orders.Update(order);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
