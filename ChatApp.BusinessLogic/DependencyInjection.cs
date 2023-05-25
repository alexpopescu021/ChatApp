using Microsoft.Extensions.DependencyInjection;
using ChatApp.BusinessLogic.Services;

namespace ChatApp.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<MessageService>();

        return services;
    }
}
