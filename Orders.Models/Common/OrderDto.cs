namespace Orders.Models.Dto.Common
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalAmount { get; set; } = 0.0m;

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

    }
}
