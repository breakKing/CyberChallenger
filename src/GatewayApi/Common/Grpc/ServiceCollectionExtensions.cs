using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GatewayApi.Common.Grpc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClientWithHeader<TClient>(this IServiceCollection services, string address,
        Action<GrpcChannelOptions>? configureChannel = null)
        where TClient : class
    {
        services.TryAddTransient<AuthGrpcInterceptor>();
        
        var clientBuilder = services.AddGrpcClient<TClient>(opt =>
        {
            opt.Address = new Uri(address);
        }).AddInterceptor<AuthGrpcInterceptor>();
        
        if (configureChannel is not null)
        {
            clientBuilder.ConfigureChannel(configureChannel);
        }

        return services;
    }
}