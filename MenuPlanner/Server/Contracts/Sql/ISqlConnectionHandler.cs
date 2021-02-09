using MenuPlanner.Server.Models.SQLConnection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuPlanner.Server.Contracts.Sql
{
    public interface ISqlConnectionHandler
    {
        public IConfiguration Configuration { get; set; }

        public SqlCredentials CredentialsData { get; set; }
        public SqlCredentials CredentialsAuth { get; set; }
        public IServiceCollection HandleSQLServers(IServiceCollection services);
       public SqlCredentials GetCredentialsFromConfiguration(string fork);
    }
}
