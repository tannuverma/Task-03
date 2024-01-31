// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/notification.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcService {
  public static partial class NotificationService
  {
    static readonly string __ServiceName = "notification.NotificationService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.OrderNotificationRequest> __Marshaller_notification_OrderNotificationRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.OrderNotificationRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.OrderNotificationResponse> __Marshaller_notification_OrderNotificationResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.OrderNotificationResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse> __Method_NotifyOrderCreated = new grpc::Method<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "NotifyOrderCreated",
        __Marshaller_notification_OrderNotificationRequest,
        __Marshaller_notification_OrderNotificationResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse> __Method_NotifyOrderUpdated = new grpc::Method<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "NotifyOrderUpdated",
        __Marshaller_notification_OrderNotificationRequest,
        __Marshaller_notification_OrderNotificationResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcService.NotificationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of NotificationService</summary>
    [grpc::BindServiceMethod(typeof(NotificationService), "BindService")]
    public abstract partial class NotificationServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.OrderNotificationResponse> NotifyOrderCreated(global::GrpcService.OrderNotificationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.OrderNotificationResponse> NotifyOrderUpdated(global::GrpcService.OrderNotificationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(NotificationServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_NotifyOrderCreated, serviceImpl.NotifyOrderCreated)
          .AddMethod(__Method_NotifyOrderUpdated, serviceImpl.NotifyOrderUpdated).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, NotificationServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_NotifyOrderCreated, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse>(serviceImpl.NotifyOrderCreated));
      serviceBinder.AddMethod(__Method_NotifyOrderUpdated, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcService.OrderNotificationRequest, global::GrpcService.OrderNotificationResponse>(serviceImpl.NotifyOrderUpdated));
    }

  }
}
#endregion
