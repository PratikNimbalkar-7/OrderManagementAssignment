using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DAL.Repository
{
    public interface IOrderRepository
    {
        List<Product> GetProducts();

        List<Discount> GetDiscounts();

        Product GetProductById(int productId);

        Discount GetDiscountById(int discountId);

        int SaveOrder(Order order);
        Order GetOrderByCode(string orderCode);
    }
}
