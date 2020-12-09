// <copyright file="ISqlDelete.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using SQLImplementation.contracts.sqlQuery.queryparts;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Class used to create a query deleting information from the database
    /// </summary>
    public interface ISqlDelete
    {
        /// <summary>
        /// Adds a From statement to the query
        /// </summary>
        /// <param name="tableName"><see cref="String"/></param>
        /// <returns><see cref="ISqlQueryFrom"/>, in order to add further statements to the query</returns>
        public ISqlQueryFrom From(String tableName);
    }
}
