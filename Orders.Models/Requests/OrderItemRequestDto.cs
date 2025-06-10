namespace Orders.Models.Dto.Common
{
    public class OrderItemRequestDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } = 0.0m;
    }
}