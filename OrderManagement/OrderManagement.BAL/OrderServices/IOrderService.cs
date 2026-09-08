using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BAL.OrderServices
{
    public interface IOrderService
    {
        List<Product> GetProducts();

        Product GetProductById(int productId);

        List<Discount> GetDiscounts();

        Discount GetDiscountById(int discountId);

        OrderDetail CalculateOrderDetail(
            int productId,
            int quantity,
            int discountId);

        int SaveOrder(Order order);
        Order GetOrderByCode(string orderCode);
    }
}
