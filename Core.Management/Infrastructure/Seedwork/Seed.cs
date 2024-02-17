using Core.Domain.Entities;
using Core.Domain.Infrastructure.Database;
using Core.Domain.Utils;
using Core.Management.Interfaces.AdminModule;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Core.Management.Infrastructure.Seedwork
{
    public class Seed : ISeed
    {
        private readonly ILogger<Seed> logger;
        private readonly CardContext context;
        private readonly IConnection connection;
        private readonly ISecurityRepository securityRepository;

        public Seed(CardContext context, IConnection connection, ILogger<Seed> logger, ISecurityRepository securityRepository)
        {
            this.logger = logger;
            this.context = context;
            this.connection = connection;
            this.securityRepository = securityRepository;
        }

        public async Task SeedDefaults()
        {
            // Create Defaults
            if (!context.Clients.Any(x => x.Role == Roles.Root))
            {
                Client client = await securityRepository.CreateClient("Nito POS", "info@nitopos.co.ke", "Nito POS Client").ConfigureAwait(false);
                client = await securityRepository.AssignClientRole(client.ClientId, Roles.Root).ConfigureAwait(false);
            }

            //UpdateHiLoSequences();
        }

        public void UpdateHiLoSequences()
        {
            using SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString);

            int max = sqlConnection.ExecuteScalar<int?>("SELECT MAX(SettingId) FROM erp.Settings") ?? 0;
            sqlConnection.Execute($"ALTER SEQUENCE [erp].[Setting_HiLo] RESTART WITH {max += 1} INCREMENT BY 1");
        }
    }
}
