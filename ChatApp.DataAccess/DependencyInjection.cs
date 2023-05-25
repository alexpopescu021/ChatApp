using Microsoft.Extensions.DependencyInjection;

using ChatApp.DataAccess.Repositories;
using ChatApp.Domain.Interfaces;

namespace ChatApp.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>(_ => new UserRepository(connectionString));
        services.AddScoped<IMessageRepository, MessageRepository>(_ => new MessageRepository(connectionString));

        return services;
    }
}
