using System;
using System.Data.SqlClient;
using MenuPlanner.Server.SqlImplementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuPlanner.Server.IoC
{
    public class IoCContainer
    {
        private IConfiguration configuration;
        public IoCContainer(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public void BindIoC(IServiceCollection services)
        {
            
        }

    }
}
