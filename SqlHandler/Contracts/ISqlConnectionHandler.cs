using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SqlHandler.Contracts
{
    public interface ISqlConnectionHandler
    {
        public IConfiguration Configuration { get; set; }
        
        public IServiceCollection HandleSQLServers<T>(IServiceCollection services,SqlCredentials credentials) where T:DbContext;
       public SqlCredentials GetCredentialsFromConfiguration(string fork);
    }
}
