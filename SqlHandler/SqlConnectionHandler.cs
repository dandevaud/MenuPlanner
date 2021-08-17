using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlHandler.Contracts;
using SqlHandler.Models;

namespace SqlHandler
{
    public class SqlConnectionHandler : ISqlConnectionHandler
    {

        public IConfiguration Configuration { get; set;}

       
      
        public SqlCredentials GetCredentialsFromConfiguration(string fork)
        {
            var sqlCredentials = Configuration.GetSection(fork).Get<SqlCredentials>();

            return sqlCredentials;
        }


        public IServiceCollection HandleSQLServers<T>(IServiceCollection services, SqlCredentials credentials) where T:DbContext
        {
           SetDbConnection<T>(services,credentials);
          
           
            return services;
        }

        private IServiceCollection SetDbConnection<T>(IServiceCollection services,SqlCredentials credentials) where T:DbContext
        {
            switch (credentials.Type)
            {
                case SqlServerType.MySql:
                    break;
                case SqlServerType.SqLite:
                    return HandleSqLite<T>(services, credentials.Server);
                 case SqlServerType.MariaDb: return HandleMariaDb<T>(services,credentials);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return services;
        }

        private IServiceCollection HandleSqLite<T>(IServiceCollection services, string dataSource) where T:DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseSqlite($"Data Source={dataSource}").
                    EnableSensitiveDataLogging(Configuration));
            return services;
        }

        private IServiceCollection HandleMariaDb<T>(IServiceCollection services, SqlCredentials credentials) where T : DbContext
        {
           
            var version = credentials.ServerVersion.Split(".");
             services.AddDbContext<T>(options =>
                
                    options.UseMySql(
                        $"server={credentials.Server}; port={credentials.Port}; database={credentials.Database}; user={credentials.User}; password={credentials.Password}",
                        new MySqlServerVersion(new Version(Convert.ToInt32(version[0]), Convert.ToInt32(version[0]),
                            Convert.ToInt32(version[0])))).
                        EnableSensitiveDataLogging(Configuration)

            );
            return services;
        }

       
    }
}
