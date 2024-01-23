using Grpc.Core;
using Newtonsoft.Json;

namespace GrpcService.Services
{
    public class OrderServiceImp : OrderService.OrderServiceBase
    {
        private List<Product> products;
        public OrderServiceImp()
        {
            // Read products from JSON file
            string json = File.ReadAllText("products.json");
            products = JsonConvert.DeserializeObject<List<Product>>(json);
        }
        public override Task<OrderResponse> PlaceOrder(MultiProductOrderRequest request, ServerCallContext context)
        {
            foreach (var productSelection in request.ProductSelections)
            {
                int productId = productSelection.ProductId;
                int quantity = productSelection.Quantity;

                Product orderedProduct = products.FirstOrDefault(p => p.ProductId == productId);

                if (orderedProduct != null && orderedProduct.Quantity > 0)
                {
                    decimal totalPrice = (decimal)(orderedProduct.Price * quantity);
                }

            }

            return Task.FromResult(new OrderResponse { Message = "Order placed successfully" });
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

            return Task.FromResult(new OrderResponse { Message = "Orders updated successfully" });
        }

        private void SaveProductsToFile()
        {
            // Serialize the list of products back to JSON and save it to the file
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText("products.json", json);
        }
    }
}
