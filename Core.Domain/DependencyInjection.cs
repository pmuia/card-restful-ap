using Core.Domain.Enums;
using Core.Domain.Infrastructure.Database;
using Core.Domain.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CardContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(CardContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.MigrationsHistoryTable("__ERPMigrationsHistory", nameof(Schemas.card));
                }));

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddScoped<ICardContext>(provider => provider.GetService<CardContext>());
            services.AddScoped<IConnection>(_ => new Connection(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
