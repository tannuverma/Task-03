using System;
using System.Buffers.Text;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string RabbitMQConnectionString = "amqp://demouser:demo123@localhost:5672/DemoApp";
            var factory = new ConnectionFactory { Uri = new Uri(RabbitMQConnectionString) };

            using var channel = GrpcChannel.ForAddress("https://localhost:7041");
            var client = new OrderService.OrderServiceClient(channel);

            while (true)
            {
                try
                {
                    var productListResponse = await client.GetProductListAsync(new Empty());
                    DisplayProductList(productListResponse.Products);

                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Want to buy some electronic items?");
                    Console.WriteLine("2. Not interested!");
                    Console.Write("Enter your choice: ");
                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            await ProcessOrder(client, productListResponse.Products, factory);
                            break;
                        case 2:
                            Console.WriteLine("Exit");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;

                    }

                }
                catch (RpcException ex)
                {
                    Console.WriteLine($"Error: {ex.Status}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                //Console.ReadKey();
            }
        }

        static void DisplayProductList(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductId}");
                Console.WriteLine($"Product Name: {product.ProductName}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Manufacturer: {product.Manufacturer}");
                Console.WriteLine($"Available Quantity: {product.Quantity}");
                Console.WriteLine();
            }
        }

        static async Task ProcessOrder(OrderService.OrderServiceClient client, IEnumerable<Product> products, ConnectionFactory factory)
        {
            var productSelections = new List<ProductSelection>();
            var productUpdates = new List<ProductUpdate>();

            while (true)
            {
                Console.Write("Enter the ID and Quantity of the item (comma-separated): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    break;

                string[] parts = input.Split(',');
                if (parts.Length != 2 || !int.TryParse(parts[0], out int itemId) || !int.TryParse(parts[1], out int quantity))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                if (productSelections.Any(p => p.ProductId == itemId))
                {
                    Console.WriteLine("Item already added. Please enter a different itemId.");
                    continue;  // Restart the loop
                }

                var selectedProduct = products.FirstOrDefault(p => p.ProductId == itemId);
                if (selectedProduct == null)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }

                if (selectedProduct.Quantity < quantity)
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

                productSelections.Add(new ProductSelection { ProductId = itemId, Quantity = quantity });
                productUpdates.Add(new ProductUpdate { ProductId = itemId, Quantity = quantity });

                Console.Write("Do you want to add any other product? (Y/N): ");
                if (!Console.ReadLine().Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    break;
            }

            var placeOrderRequest = new MultiProductOrderRequest { ProductSelections = { productSelections } };
            var placeOrderResponse = await client.PlaceOrderAsync(placeOrderRequest);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Place order response: {placeOrderResponse.Message}");

            var updateOrderRequest = new MultiProductUpdateRequest { ProductUpdates = { productUpdates } };
            var updateOrderResponse = await client.UpdateOrderAsync(updateOrderRequest);
            Console.WriteLine($"Update order response: {updateOrderResponse.Message}");

            Console.WriteLine();
            Console.WriteLine();
            ListenToQueue(factory, "Queue1");
            ListenToQueue(factory, "Queue1");
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Do you want to see the list of product bought till date? (Y/N): ");
            if (Console.ReadLine().Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                ListenToQueue2(factory, "Queue2");
            }

        }

        static void ListenToQueue(ConnectionFactory factory, string queueName)
        {
            using var connection = factory.CreateConnection();
            using var channelRabbitMQ = connection.CreateModel();
            channelRabbitMQ.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channelRabbitMQ);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channelRabbitMQ.BasicConsume(queueName, true, consumer);
        }

        static void ListenToQueue2(ConnectionFactory factory, string queueName)
        {
            using var connection = factory.CreateConnection();
            using var channelRabbitMQ = connection.CreateModel();
            channelRabbitMQ.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channelRabbitMQ);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                // Acknowledge the message
                channelRabbitMQ.BasicAck(ea.DeliveryTag, false);
            };

            channelRabbitMQ.BasicConsume(queueName, false, consumer);

            while (true)
            {
                Thread.Sleep(1000);

                var messageCount = GetMessageCount(channelRabbitMQ, queueName);

                if (messageCount == 0)
                    break;
            }
        }

        static uint GetMessageCount(IModel channel, string queueName)
        {
            var declareOk = channel.QueueDeclarePassive(queueName);
            return declareOk.MessageCount;
        }
    }
}
