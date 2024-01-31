using Grpc.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GrpcService.Services
{
    public class NotificationServiceImp : NotificationService.NotificationServiceBase
    {
        private const string RabbitMQConnectionString = "amqp://demouser:demo123@localhost:5672/DemoApp";
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _rabbitMqConnection;
        private IModel _channel;

        public NotificationServiceImp()
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri(RabbitMQConnectionString) };
            _connectionFactory.AutomaticRecoveryEnabled = true;
            _connectionFactory.DispatchConsumersAsync = true;
            _rabbitMqConnection = _connectionFactory.CreateConnection("DemoAppClient");
            _channel = _rabbitMqConnection.CreateModel();
        }

        public override Task<OrderNotificationResponse> NotifyOrderCreated(OrderNotificationRequest request, ServerCallContext context)
        {
            try
            {
                var message = $"{request.Message}. Total Price: {request.Total}";

                _channel.ExchangeDeclare("CustomerNotification", ExchangeType.Direct, true, false);
                _channel.QueueDeclare("Queue1", true, false, false);
                _channel.QueueBind("Queue1", "CustomerNotification", "queue1");

                var properties = _channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                _channel.BasicPublish("CustomerNotification", "queue1", properties, Encoding.UTF8.GetBytes(message));

                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = "";
                return Task.FromResult(response);
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = "Broker Unreachable";
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = $"Error: {ex.Message}";
                return Task.FromResult(response);
            }
        }

        public override Task<OrderNotificationResponse> NotifyOrderUpdated(OrderNotificationRequest request, ServerCallContext context)
        {
            try
            {
                var message = $"{request.Message}";

                _channel.QueueDeclare("Queue2", true, false, false);
                _channel.QueueBind("Queue2", "CustomerNotification", "queue2");

                var properties = _channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                _channel.BasicPublish("CustomerNotification", "queue2", properties, Encoding.UTF8.GetBytes(message));

                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = "";
                return Task.FromResult(response);
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = "Broker Unreachable";
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                OrderNotificationResponse response = new OrderNotificationResponse();
                response.Status = $"Error: {ex.Message}";
                return Task.FromResult(response);
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMqConnection?.Dispose();
        }

    }
}
