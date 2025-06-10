namespace Orders.Models.Dto.Common
{
    public class OrderItemDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } = 0.0m;
        public Guid OrderId {  get; set; } = Guid.Empty;
    }
}