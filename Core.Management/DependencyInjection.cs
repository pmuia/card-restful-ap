using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

using IdGen;
using Dapper;

using Core.Management.Interfaces;
using Core.Management.Repositories;
using Core.Management.Interfaces.AdminModule;
using Core.Management.Repositories.AdminModule;
using Core.Management.Infrastructure.Seedwork;
using Core.Management.Interfaces.CardModule;
using Core.Management.Repositories.CardModule;

namespace Core.Management
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IIdGenerator<long>>(_ => new IdGenerator(0, new IdGeneratorOptions(idStructure: new IdStructure(45, 2, 16), timeSource: new DefaultTimeSource(new DateTime(2021, 4, 23, 11, 0, 0, DateTimeKind.Utc)))));

            SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);

            //Repository Initializations
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IDataServiceFactory<>), typeof(DataServiceFactory<>));
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();

            services.AddScoped<ICardRepository, CardRepository>();

            services.AddScoped<ISeed, Seed>();
            return services;
        }
    }
}
