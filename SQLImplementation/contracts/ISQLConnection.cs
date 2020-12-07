using System.Data.Common;

namespace SQLImplementation.contracts
{
    public interface ISQLConnection
    {
        /// <summary>
        /// Will initalize the Connection but not yet connect to it.
        /// </summary>
        /// <returns> ISQLConnection (instance currently working on)</returns>
        public ISQLConnection InitializeConnection();

        /// <summary>
        /// Will connect to the Database setUp, if not yet initialized it will be initialized.
        /// </summary>
        /// <returns>ISQLConnection (instance currently working on)</returns>
        public ISQLConnection Connect();

        /// <summary>
        /// Returns the DBConnection initialized.
        /// </summary>
        /// <returns>DBConnection</returns>
        public DbConnection GetConnection();
    }
}