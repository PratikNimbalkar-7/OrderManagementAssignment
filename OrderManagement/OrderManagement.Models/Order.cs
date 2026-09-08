namespace OrderManagement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal GrandTotal { get; set; }

        public string Remark { get; set; }

        public string BillingAddress { get; set; }

        public string ShippingAddress { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
