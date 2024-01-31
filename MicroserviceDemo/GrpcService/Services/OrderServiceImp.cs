using Grpc.Core;
using Newtonsoft.Json;

namespace GrpcService.Services
{
    public class OrderServiceImp : OrderService.OrderServiceBase
    {
        private List<Product> products;
        string filePath;
        private readonly NotificationServiceImp _notificationService;
        public OrderServiceImp(NotificationServiceImp notificationService)
        {
            // Read products from JSON file
            filePath = @"C:\Users\swatantratiwari\OneDrive - Nagarro\Desktop\Task-3(MicroserviceDemo)\Task-03\MicroserviceDemo\GrpcService\Data\products.json";
            string json = File.ReadAllText(filePath);
            products = JsonConvert.DeserializeObject<List<Product>>(json);
            _notificationService = notificationService;
        }
        public override Task<OrderResponse> PlaceOrder(MultiProductOrderRequest request, ServerCallContext context)
        {
            double totalPrice = 0;
            foreach (var productSelection in request.ProductSelections)
            {
                int productId = productSelection.ProductId;
                int quantity = productSelection.Quantity;

                Product orderedProduct = products.FirstOrDefault(p => p.ProductId == productId);

                if (orderedProduct != null && orderedProduct.Quantity > 0)
                {
                    totalPrice += (double)(orderedProduct.Price * quantity);
                }

            }

            //Send Notification to RabbitMQ 
            var notificationRequest = new OrderNotificationRequest
            {
                Message = "Order Placed Successfully",
                Total = totalPrice,
            };
            var notificationResponse = _notificationService.NotifyOrderCreated(notificationRequest, context);

            OrderResponse response = new OrderResponse();
            response.TotalPrice = totalPrice;
            response.Message = "Order Placed Successfully";

            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> GetProductList(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new ProductListResponse { Products = { products } });
        }

        public override Task<OrderResponse> UpdateOrder(MultiProductUpdateRequest request, ServerCallContext context)
        {
            foreach (var productUpdate in request.ProductUpdates)
            {
                int productId = productUpdate.ProductId;
                int quantity = productUpdate.Quantity;

                Product productToUpdate = products.FirstOrDefault(p => p.ProductId == productId);

                if (productToUpdate != null)
                {
                    productToUpdate.Quantity -= quantity;

                }
            }
            SaveProductsToFile();

            //Send Notification to RabbitMQ 
            var notificationRequest = new OrderNotificationRequest
            {
                Message = "Order Updated Successfully",
            };
            var notificationResponse = _notificationService.NotifyOrderUpdated(notificationRequest, context);

            return Task.FromResult(new OrderResponse { Message = "Orders updated successfully" });
        }

        private void SaveProductsToFile()
        {
            // Serialize the list of products back to JSON and save it to the file
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
