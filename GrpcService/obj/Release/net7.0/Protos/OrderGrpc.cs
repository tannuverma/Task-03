// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/order.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcService {
  public static partial class OrderService
  {
    static readonly string __ServiceName = "order.OrderService";

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
    static readonly grpc::Marshaller<global::GrpcService.MultiProductOrderRequest> __Marshaller_order_MultiProductOrderRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.MultiProductOrderRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.OrderResponse> __Marshaller_order_OrderResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.OrderResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.MultiProductUpdateRequest> __Marshaller_order_MultiProductUpdateRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.MultiProductUpdateRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.Empty> __Marshaller_order_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcService.ProductListResponse> __Marshaller_order_ProductListResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcService.ProductListResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcService.MultiProductOrderRequest, global::GrpcService.OrderResponse> __Method_PlaceOrder = new grpc::Method<global::GrpcService.MultiProductOrderRequest, global::GrpcService.OrderResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PlaceOrder",
        __Marshaller_order_MultiProductOrderRequest,
        __Marshaller_order_OrderResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcService.MultiProductUpdateRequest, global::GrpcService.OrderResponse> __Method_UpdateOrder = new grpc::Method<global::GrpcService.MultiProductUpdateRequest, global::GrpcService.OrderResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateOrder",
        __Marshaller_order_MultiProductUpdateRequest,
        __Marshaller_order_OrderResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcService.Empty, global::GrpcService.ProductListResponse> __Method_GetProductList = new grpc::Method<global::GrpcService.Empty, global::GrpcService.ProductListResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetProductList",
        __Marshaller_order_Empty,
        __Marshaller_order_ProductListResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcService.OrderReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of OrderService</summary>
    [grpc::BindServiceMethod(typeof(OrderService), "BindService")]
    public abstract partial class OrderServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.OrderResponse> PlaceOrder(global::GrpcService.MultiProductOrderRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.OrderResponse> UpdateOrder(global::GrpcService.MultiProductUpdateRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.ProductListResponse> GetProductList(global::GrpcService.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(OrderServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_PlaceOrder, serviceImpl.PlaceOrder)
          .AddMethod(__Method_UpdateOrder, serviceImpl.UpdateOrder)
          .AddMethod(__Method_GetProductList, serviceImpl.GetProductList).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, OrderServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_PlaceOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcService.MultiProductOrderRequest, global::GrpcService.OrderResponse>(serviceImpl.PlaceOrder));
      serviceBinder.AddMethod(__Method_UpdateOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcService.MultiProductUpdateRequest, global::GrpcService.OrderResponse>(serviceImpl.UpdateOrder));
      serviceBinder.AddMethod(__Method_GetProductList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcService.Empty, global::GrpcService.ProductListResponse>(serviceImpl.GetProductList));
    }

  }
}
#endregion