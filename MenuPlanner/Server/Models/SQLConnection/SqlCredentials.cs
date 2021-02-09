using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Server.Models.SQLConnection
{
    public class SqlCredentials
    {
        public SqlServerType Type { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string ServerVersion { get; set; }
        

        public string User { get; set; }
        public string Password { get; set; }
    }
}
