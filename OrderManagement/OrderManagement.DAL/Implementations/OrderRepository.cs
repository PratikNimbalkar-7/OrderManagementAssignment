using Microsoft.Extensions.Configuration;
using OrderManagement.DAL.Repository;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DAL.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString =
                _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetProducts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ProductName = reader["ProductName"].ToString(),
                                Rate = Convert.ToDecimal(reader["Rate"])
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
        public Product GetProductById(int productId)
        {
            Product product = null;

            using (SqlConnection con =
                   new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd =
                       new SqlCommand("sp_GetProductById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@ProductId",
                        SqlDbType.Int
                    ).Value = productId;

                    con.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),

                                ProductName =
                                    reader["ProductName"].ToString(),

                                Rate =
                                    Convert.ToDecimal(reader["Rate"])
                            };
                        }
                    }
                }
            }

            return product;
        }


        public List<Discount> GetDiscounts()
        {
            List<Discount> discounts = new List<Discount>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDiscounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Discount discount = new Discount
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                DiscountType = reader["DiscountType"].ToString(),
                                Value = Convert.ToDecimal(reader["Value"])
                            };

                            discounts.Add(discount);
                        }
                    }
                }
            }

            return discounts;
        }


        public Discount GetDiscountById(int discountId)
        {
            Discount discount = null;

            using (SqlConnection con =
                   new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd =
                       new SqlCommand("sp_GetDiscountById", con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.Add(
                        "@DiscountId",
                        SqlDbType.Int
                    ).Value = discountId;

                    con.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            discount = new Discount
                            {
                                Id = Convert.ToInt32(
                                    reader["Id"]),

                                DiscountType =
                                    reader["DiscountType"].ToString(),

                                Value =
                                    Convert.ToDecimal(
                                        reader["Value"])
                            };
                        }
                    }
                }
            }

            return discount;
        }

        public int SaveOrder(Order order)
        {
            int orderId = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SaveOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@OrderCode", SqlDbType.VarChar, 50)
                        .Value = order.OrderCode;

                    cmd.Parameters.Add("@OrderDate", SqlDbType.Date)
                        .Value = order.OrderDate;

                    SqlParameter subTotalParameter =
                        cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal);

                    subTotalParameter.Precision = 18;
                    subTotalParameter.Scale = 2;
                    subTotalParameter.Value = order.SubTotal;


                    SqlParameter totalDiscountParameter =
                        cmd.Parameters.Add("@TotalDiscount", SqlDbType.Decimal);

                    totalDiscountParameter.Precision = 18;
                    totalDiscountParameter.Scale = 2;
                    totalDiscountParameter.Value = order.TotalDiscount;


                    SqlParameter grandTotalParameter =
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.Decimal);

                    grandTotalParameter.Precision = 18;
                    grandTotalParameter.Scale = 2;
                    grandTotalParameter.Value = order.GrandTotal;


                    cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 500)
                        .Value = string.IsNullOrEmpty(order.Remark)
                            ? DBNull.Value
                            : order.Remark;


                    cmd.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 500)
                        .Value = string.IsNullOrEmpty(order.BillingAddress)
                            ? DBNull.Value
                            : order.BillingAddress;


                    cmd.Parameters.Add("@ShippingAddress", SqlDbType.VarChar, 500)
                        .Value = string.IsNullOrEmpty(order.ShippingAddress)
                            ? DBNull.Value
                            : order.ShippingAddress;


                  

                    DataTable orderDetailsTable = new DataTable();

                    orderDetailsTable.Columns.Add(
                        "ProductId",
                        typeof(int));

                    orderDetailsTable.Columns.Add(
                        "Quantity",
                        typeof(int));

                    orderDetailsTable.Columns.Add(
                        "Rate",
                        typeof(decimal));

                    orderDetailsTable.Columns.Add(
                        "Amount",
                        typeof(decimal));

                    orderDetailsTable.Columns.Add(
                        "DiscountId",
                        typeof(int));

                    orderDetailsTable.Columns.Add(
                        "DiscountAmount",
                        typeof(decimal));

                    orderDetailsTable.Columns.Add(
                        "NetAmount",
                        typeof(decimal));



                    if (order.OrderDetails != null)
                    {
                        foreach (var detail in order.OrderDetails)
                        {
                            orderDetailsTable.Rows.Add(
                                detail.ProductId,
                                detail.Quantity,
                                detail.Rate,
                                detail.Amount,
                                detail.DiscountId,
                                detail.DiscountAmount,
                                detail.NetAmount
                            );
                        }
                    }



                    SqlParameter orderDetailsParameter =
                        cmd.Parameters.Add(
                            "@OrderDetails",
                            SqlDbType.Structured);

                    orderDetailsParameter.TypeName =
                        "dbo.OrderDetailType";

                    orderDetailsParameter.Value =
                        orderDetailsTable;



                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orderId =
                                Convert.ToInt32(reader["OrderId"]);
                        }
                    }
                }
            }

            return orderId;
        }

        public Order GetOrderByCode(string orderCode)
        {
            Order order = null;

            using (SqlConnection con =
                   new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd =
                       new SqlCommand("sp_GetOrderByCode", con))
                {
                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    // Order Code parameter
                    cmd.Parameters.Add(
                        "@OrderCode",
                        SqlDbType.VarChar,
                        50
                    ).Value = orderCode;

                    con.Open();

                    using (SqlDataReader reader =
                           cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            order = new Order
                            {
                                Id = Convert.ToInt32(
                                    reader["Id"]),

                                OrderCode =
                                    reader["OrderCode"].ToString(),

                                OrderDate =
                                    Convert.ToDateTime(
                                        reader["OrderDate"]),

                                SubTotal =
                                    Convert.ToDecimal(
                                        reader["SubTotal"]),

                                TotalDiscount =
                                    Convert.ToDecimal(
                                        reader["TotalDiscount"]),

                                GrandTotal =
                                    Convert.ToDecimal(
                                        reader["GrandTotal"]),

                                Remark =
                                    reader["Remark"] == DBNull.Value
                                        ? null
                                        : reader["Remark"].ToString(),

                                BillingAddress =
                                    reader["BillingAddress"] == DBNull.Value
                                        ? null
                                        : reader["BillingAddress"].ToString(),

                                ShippingAddress =
                                    reader["ShippingAddress"] == DBNull.Value
                                        ? null
                                        : reader["ShippingAddress"].ToString(),

                                OrderDetails =
                                    new List<OrderDetail>()
                            };
                        }


                        if (order != null &&
                            reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                OrderDetail detail =
                                    new OrderDetail
                                    {
                                        Id =
                                            Convert.ToInt32(
                                                reader["Id"]),

                                        OrderId =
                                            Convert.ToInt32(
                                                reader["OrderId"]),

                                        ProductId =
                                            Convert.ToInt32(
                                                reader["ProductId"]),

                                        ProductName =
                                            reader["ProductName"].ToString(),

                                        Quantity =
                                            Convert.ToInt32(
                                                reader["Quantity"]),

                                        Rate =
                                            Convert.ToDecimal(
                                                reader["Rate"]),

                                        Amount =
                                            Convert.ToDecimal(
                                                reader["Amount"]),

                                        DiscountId =
                                            Convert.ToInt32(
                                                reader["DiscountId"]),

                                        DiscountType =
                                            reader["DiscountType"].ToString(),

                                        DiscountAmount =
                                            Convert.ToDecimal(
                                                reader["DiscountAmount"]),

                                        NetAmount =
                                            Convert.ToDecimal(
                                                reader["NetAmount"])
                                    };

                                order.OrderDetails.Add(detail);
                            }
                        }
                    }
                }
            }

            return order;
        }
    }
}
