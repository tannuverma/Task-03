using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7041");

            var client = new OrderService.OrderServiceClient(channel);

            try
            {
                // Call the PlaceOrder method async
                var placeOrderRequest = new MultiProductOrderRequest
                {
                    ProductSelections =
                    {
                        new ProductSelection { ProductId = 1, Quantity = 2 },
                        new ProductSelection { ProductId = 2, Quantity = 3 },
                        
                    }
                };

                var placeOrderResponse = await client.PlaceOrderAsync(placeOrderRequest);
                Console.WriteLine($"PlaceOrder Response: {placeOrderResponse.Message}");

                

                // Call the UpdateOrder method async
                var updateOrderRequest = new MultiProductUpdateRequest
                {
                    ProductUpdates =
                    {
                        new ProductUpdate { ProductId = 1, Quantity = 1 },
                        new ProductUpdate { ProductId = 2, Quantity = 2 },
                       
                    }
                };

                var updateOrderResponse = await client.UpdateOrderAsync(updateOrderRequest);
                Console.WriteLine($"UpdateOrder Response: {updateOrderResponse.Message}");

                // Call the GetProductList method async
                var productListRequest = new Empty();
                var productListResponse = await client.GetProductListAsync(productListRequest);
                foreach (var product in productListResponse.Products)
                {
                    Console.WriteLine($"Product : {product.ProductName}, Price : {product.Price}, Quantity: {product.Quantity}");
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Error: {ex.Status}");
            }

            Console.ReadKey();
        }
    }
}
