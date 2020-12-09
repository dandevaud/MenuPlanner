// <copyright file="ISqlConnection.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Data.Common;

namespace SQLImplementation.contracts
{
    /// <summary>
    /// Class used to initialize connection with SQL Database
    /// </summary>
    public interface ISqlConnection
    {
        /// <summary>
        /// Will initalize the Connection but not yet connect to it.
        /// </summary>
        /// <returns> <see cref="ISQLConnection"/> (instance currently working on)</returns>
        public ISqlConnection InitializeConnection();

        /// <summary>
        /// Will connect to the Database setUp, if not yet initialized it will be initialized.
        /// </summary>
        /// <returns><see cref="ISQLConnection"/> (instance currently working on)</returns>
        public ISqlConnection Connect();

        /// <summary>
        /// Returns the DBConnection initialized.
        /// </summary>
        /// <returns><see cref="DbConnection"/></returns>
        public DbConnection GetConnection();
    }
}
