using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.BAL.OrderServices;
using OrderManagement.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _orderService.GetProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching products.",
                    error = ex.Message
                });
            }
        }


        [HttpGet("products/{productId}")]
        public IActionResult GetProductById(int productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid ProductId."
                    });
                }

                var product = _orderService.GetProductById(productId);

                if (product == null)
                {
                    return NotFound(new
                    {
                        message = "Product not found."
                    });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching product.",
                    error = ex.Message
                });
            }
        }


        [HttpGet("discounts")]
        public IActionResult GetDiscounts()
        {
            try
            {
                var discounts = _orderService.GetDiscounts();

                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching discounts.",
                    error = ex.Message
                });
            }
        }


        [HttpGet("discounts/{discountId}")]
        public IActionResult GetDiscountById(int discountId)
        {
            try
            {
                if (discountId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid DiscountId."
                    });
                }

                var discount = _orderService.GetDiscountById(discountId);

                if (discount == null)
                {
                    return NotFound(new
                    {
                        message = "Discount not found."
                    });
                }

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching discount.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("calculate-detail")]
        public IActionResult CalculateDetail([FromBody] OrderDetailRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new
                    {
                        message = "Request is required."
                    });
                }

                if (request.ProductId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid ProductId."
                    });
                }

                if (request.Quantity <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Quantity must be greater than zero."
                    });
                }

                if (request.DiscountId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid DiscountId."
                    });
                }

                var detail = _orderService.CalculateOrderDetail(
                    request.ProductId,
                    request.Quantity,
                    request.DiscountId
                );

                return Ok(detail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while calculating order detail.",
                    error = ex.Message
                });
            }
        }


        [HttpPost]
        public IActionResult SaveOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest(new
                    {
                        message = "Order data is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(order.OrderCode))
                {
                    return BadRequest(new
                    {
                        message = "Order Code is required."
                    });
                }

                if (order.OrderDetails == null ||
                    order.OrderDetails.Count == 0)
                {
                    return BadRequest(new
                    {
                        message = "At least one order detail is required."
                    });
                }

                int orderId = _orderService.SaveOrder(order);

                return Ok(new
                {
                    message = "Order created successfully.",
                    orderId = orderId,
                    orderCode = order.OrderCode,
                    subTotal = order.SubTotal,
                    totalDiscount = order.TotalDiscount,
                    grandTotal = order.GrandTotal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while saving order.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{orderCode}")]
        public IActionResult GetOrderByCode(string orderCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(orderCode))
                {
                    return BadRequest(new
                    {
                        message = "Order Code is required."
                    });
                }

                var order = _orderService.GetOrderByCode(orderCode);

                if (order == null)
                {
                    return NotFound(new
                    {
                        message = "Order not found."
                    });
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching order.",
                    error = ex.Message
                });
            }
        }
    }


    public class OrderDetailRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int DiscountId { get; set; }
    }
}
