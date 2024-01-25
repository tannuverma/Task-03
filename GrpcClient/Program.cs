using System;
using System.Buffers.Text;
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

            while (true)
            {
                try
                {
                    // Call the GetProductList method async
                    var productListRequest = new Empty();
                    var productListResponse = await client.GetProductListAsync(productListRequest);
                    foreach (var product in productListResponse.Products)
                    {
                        Console.WriteLine($"Product ID: {product.ProductId}");
                        Console.WriteLine($"Product Name: {product.ProductName}");
                        Console.WriteLine($"Description: {product.Description}");
                        Console.WriteLine($"Price: {product.Price}");
                        Console.WriteLine($"Manufacturer: {product.Manufacturer}");
                        Console.WriteLine($"Available Quantity: {product.Quantity}");
                        Console.WriteLine();
                    }

                    Console.WriteLine("Select an option:");
                    Console.WriteLine();
                    Console.WriteLine("1. Want to buy some electronic items? ");
                    Console.WriteLine("2. Not interested! ");
                    Console.WriteLine();

                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                var productSelections = new List<ProductSelection>();
                                var productUpdates = new List<ProductUpdate>();

                                while (true)
                                {
                                    Console.Write("Enter the Id and Quantity of the item(comma-separated): ");
                                    string input = Console.ReadLine();

                                    if (string.IsNullOrWhiteSpace(input))
                                        break;

                                    // Splitting of id and quantity
                                    string[] parts = input.Split(',');
                                    if (parts.Length == 2 && int.TryParse(parts[0], out int itemId) && int.TryParse(parts[1], out int quantity))
                                    {
                                        Product selectedProduct = productListResponse.Products.FirstOrDefault(p => p.ProductId == itemId);

                                        if (selectedProduct != null && selectedProduct.Quantity >= quantity)
                                        {
                                            productSelections.Add(new ProductSelection { ProductId = itemId, Quantity = quantity });
                                            productUpdates.Add(new ProductUpdate { ProductId = itemId, Quantity = quantity });
                                        }
                                        else
                                        {
                                            Console.WriteLine(selectedProduct != null ? "Insufficient Stock!" : "Product doesn't exist!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input.");
                                    }

                                    // Check if the user wants to stop adding products
                                    Console.Write("Do you want to add any other product? ");
                                    string moreInputs = Console.ReadLine();

                                    if (!(moreInputs.Trim().ToLower() == "y" || moreInputs.Trim().ToLower() == "yes"))
                                    {
                                        Console.Write("Do you want to place an order? ");
                                        string reply = Console.ReadLine();
                                        if (!(reply.Trim().ToLower() == "y" || reply.Trim().ToLower() == "yes"))
                                        {
                                            Console.Write("Do you want to add more products? ");
                                            string moreInputss = Console.ReadLine();
                                        }
                                        else
                                            break;
                                    }
                                }
                                Console.WriteLine();

                                // Call the PlaceOrder method async with dynamic product selections
                                var placeorderrequest = new MultiProductOrderRequest
                                {
                                    ProductSelections = { productSelections }
                                };

                                var placeorderresponse = await client.PlaceOrderAsync(placeorderrequest);
                                Console.WriteLine($"placeorder response: {placeorderresponse.Message}");

                                // Call the UpdateOrder method async with dynamic product selections
                                var updateOrderRequest = new MultiProductUpdateRequest
                                {
                                    ProductUpdates = { productUpdates }
                                };

                                var updateOrderResponse = await client.UpdateOrderAsync(updateOrderRequest);
                                Console.WriteLine($"UpdateOrder Response: {updateOrderResponse.Message}");


                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Id and quantity must be an integer");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Exit");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;

                    }




                    //// Call the PlaceOrder method async
                    //var placeOrderRequest = new MultiProductOrderRequest
                    //{
                    //    ProductSelections =
                    //{
                    //    new ProductSelection { ProductId = 1, Quantity = 2 },
                    //    new ProductSelection { ProductId = 2, Quantity = 3 },

                    //}
                    //};

                    //var placeOrderResponse = await client.PlaceOrderAsync(placeOrderRequest);
                    //Console.WriteLine($"PlaceOrder Response: {placeOrderResponse.Message}");



                    //// Call the UpdateOrder method async
                    //var updateOrderRequest = new MultiProductUpdateRequest
                    //{
                    //    ProductUpdates =
                    //{
                    //    new ProductUpdate { ProductId = 1, Quantity = 1 },
                    //    new ProductUpdate { ProductId = 2, Quantity = 2 },
                    //}
                    //};

                    //var updateOrderResponse = await client.UpdateOrderAsync(updateOrderRequest);
                    //Console.WriteLine($"UpdateOrder Response: {updateOrderResponse.Message}");


                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be either 1 or 2.");
                }
                catch (RpcException ex)
                {
                    Console.WriteLine($"Error: {ex.Status}");
                }
                Console.ReadKey();
            }
        }
    }
}

