using OrderManagement.BAL.OrderServices;
using OrderManagement.DAL.Repository;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.BAL.OrderServiceImplementaion
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public List<Product> GetProducts()
        {
            return _orderRepository.GetProducts();
        }


        public Product GetProductById(int productId)
        {
            if (productId <= 0)
            {
                throw new Exception("Invalid Product.");
            }

            return _orderRepository.GetProductById(productId);
        }


        public List<Discount> GetDiscounts()
        {
            return _orderRepository.GetDiscounts();
        }


        public Discount GetDiscountById(int discountId)
        {
            if (discountId <= 0)
            {
                throw new Exception("Invalid Discount.");
            }

            return _orderRepository.GetDiscountById(discountId);
        }


        public OrderDetail CalculateOrderDetail(
            int productId,
            int quantity,
            int discountId)
        {

            if (productId <= 0)
            {
                throw new Exception("Please select product.");
            }

            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }

            if (discountId <= 0)
            {
                throw new Exception("Please select discount.");
            }


            Product product =
                _orderRepository.GetProductById(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }


            Discount discount =
                _orderRepository.GetDiscountById(discountId);

            if (discount == null)
            {
                throw new Exception("Discount not found.");
            }

            decimal rate = product.Rate;

            decimal amount = rate * quantity;

            decimal discountAmount = 0;


            if (discount.DiscountType.Equals(
                "Percentage",
                StringComparison.OrdinalIgnoreCase))
            {

                discountAmount =
                    Math.Round(
                        amount * discount.Value / 100,
                        2);
            }
            else if (discount.DiscountType.Equals(
                "Fixed",
                StringComparison.OrdinalIgnoreCase))
            {

                discountAmount =
                    Math.Round(
                        discount.Value * quantity,
                        2);
            }
            else
            {
                throw new Exception(
                    "Invalid discount type.");
            }


            decimal netAmount =
                amount - discountAmount;


            if (netAmount < 0)
            {
                throw new Exception(
                    "Discount cannot be greater than amount.");
            }


            OrderDetail detail = new OrderDetail
            {
                ProductId = product.Id,
                ProductName = product.ProductName,

                Quantity = quantity,

                Rate = rate,

                Amount = amount,

                DiscountId = discount.Id,
                DiscountType = discount.DiscountType,

                DiscountAmount = discountAmount,

                NetAmount = netAmount
            };


            return detail;
        }


        public int SaveOrder(Order order)
        {

            if (order == null)
            {
                throw new Exception("Order cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(order.OrderCode))
            {
                throw new Exception("Order Code is required.");
            }

            if (order.OrderDate == DateTime.MinValue)
            {
                throw new Exception("Order Date is required.");
            }

            if (string.IsNullOrWhiteSpace(order.BillingAddress))
            {
                throw new Exception(
                    "Billing Address is required.");
            }

            if (string.IsNullOrWhiteSpace(order.ShippingAddress))
            {
                throw new Exception(
                    "Shipping Address is required.");
            }

            if (order.OrderDetails == null ||
                order.OrderDetails.Count == 0)
            {
                throw new Exception(
                    "Please add at least one product.");
            }


            order.SubTotal =
                order.OrderDetails.Sum(x => x.Amount);

            order.TotalDiscount =
                order.OrderDetails.Sum(x => x.DiscountAmount);

            order.GrandTotal =
                order.OrderDetails.Sum(x => x.NetAmount);


            if (order.SubTotal < 0)
            {
                throw new Exception(
                    "Subtotal cannot be negative.");
            }

            if (order.TotalDiscount < 0)
            {
                throw new Exception(
                    "Total discount cannot be negative.");
            }

            if (order.GrandTotal < 0)
            {
                throw new Exception(
                    "Grand total cannot be negative.");
            }

            int orderId =
                _orderRepository.SaveOrder(order);


            return orderId;
        }

        public Order GetOrderByCode(string orderCode)
        {
            if (string.IsNullOrWhiteSpace(orderCode))
            {
                throw new Exception(
                    "Order Code is required.");
            }

            Order order =
                _orderRepository.GetOrderByCode(orderCode);

            if (order == null)
            {
                throw new Exception(
                    "Order not found.");
            }

            return order;
        }
    }
}
