using System;
using System.Linq;
using System.Text.Json;
using IdentityServer4.Extensions;
using MenuPlanner.Server.Contracts.Sql;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models.SQLConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MenuPlanner.Server.Data
{
    public class SqlConnectionHandler : ISqlConnectionHandler
    {

        public IConfiguration Configuration { get; set;}

        public SqlCredentials CredentialsData { get; set; }
        public SqlCredentials CredentialsAuth { get; set; }

      
        public SqlCredentials GetCredentialsFromConfiguration(string fork)
        {
            var sqlCredentials = Configuration.GetSection(fork).Get<SqlCredentials>();
           
           
            return sqlCredentials;
        }


        public IServiceCollection HandleSQLServers(IServiceCollection services)
        {
            SetDbConnection<ApplicationDbContext>(services,CredentialsAuth);
            SetDbConnection<MenuPlannerContext>(services, CredentialsData);

           
            return services;
        }

        private void SetDbConnection<T>(IServiceCollection services,SqlCredentials credentials) where T:DbContext
        {
            switch (credentials.Type)
            {
                case SqlServerType.MySql:
                    break;
                case SqlServerType.SqLite:
                    HandleSqLite<T>(services, credentials.Server);
                    break;
                case SqlServerType.MariaDb: HandleMariaDb<T>(services,credentials);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleSqLite<T>(IServiceCollection services, string dataSource) where T:DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseSqlite($"Data Source={dataSource}"));
        }

        private void HandleMariaDb<T>(IServiceCollection services, SqlCredentials credentials) where T : DbContext
        {
           
            var version = credentials.ServerVersion.Split(".");
            services.AddDbContext<T>(options =>
                options.UseMySql(
                    $"server={credentials.Server}; port={credentials.Port}; database={credentials.Database}; user={credentials.User}; password={credentials.Password}",
                    new MySqlServerVersion(new Version(Convert.ToInt32(version[0]), Convert.ToInt32(version[0]), Convert.ToInt32(version[0])))));
        }
    }
}
