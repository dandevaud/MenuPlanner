// <copyright file="ISqlConnection.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Data.Common;

namespace SQLImplementation.contracts
{
    public interface ISqlConnection
    {
        /// <summary>
        /// Will initalize the Connection but not yet connect to it.
        /// </summary>
        /// <returns> ISQLConnection (instance currently working on)</returns>
        public ISqlConnection InitializeConnection();

        /// <summary>
        /// Will connect to the Database setUp, if not yet initialized it will be initialized.
        /// </summary>
        /// <returns>ISQLConnection (instance currently working on)</returns>
        public ISqlConnection Connect();

        /// <summary>
        /// Returns the DBConnection initialized.
        /// </summary>
        /// <returns>DBConnection</returns>
        public DbConnection GetConnection();
    }
}
