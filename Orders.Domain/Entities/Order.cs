using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Orders.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem>? Items { get; set; }

    }
}
