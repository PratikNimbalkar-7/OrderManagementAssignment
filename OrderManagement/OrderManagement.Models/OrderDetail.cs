namespace OrderManagement.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }

        public int DiscountId { get; set; }

        public string? DiscountType { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal NetAmount { get; set; }
    }
}
