using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Tadpole.Client.Shared.Api;
using Tadpole.Client.Shared.Auth;
using Tadpole.Client.Shared.RealTime;

namespace Tadpole.Client.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTadpoleClient(
        this IServiceCollection services,
        Action<TadpoleClientOptions> configure)
    {
        services.Configure(configure);
        services.AddScoped<AuthSession>();
        services.AddScoped<AuthenticationStateProvider, TadpoleAuthStateProvider>();
        services.AddAuthorizationCore();
        services.AddScoped<TadpoleApiClient>();
        services.AddScoped<MessageHubClient>();
        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<TadpoleClientOptions>>().Value;
            return new HttpClient { BaseAddress = new Uri(options.ApiBaseUrl.TrimEnd('/') + "/") };
        });

        return services;
    }
}
