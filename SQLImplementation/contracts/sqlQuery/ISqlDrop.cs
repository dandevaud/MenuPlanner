// <copyright file="ISqlDrop.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using SQLImplementation.contracts.sqlQuery.queryparts;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Class use to create a query droping something (Table or Index)
    /// </summary>
    public interface ISqlDrop
    {
        /// <summary>
        /// Drops the table with the name provided
        /// </summary>
        /// <param name="tableName"><see cref="String"/></param>
        /// <returns><see cref="ISqlStatementEnd"/> no further statements can be added</returns>
        public ISqlStatementEnd Table(String tableName);

        /// <summary>
        /// Drops the index with the name provided in the table provided
        /// </summary>
        /// <param name="tableName"><see cref="String"/></param>
        /// <param name="indexName"><see cref="String"/></param>
        /// <returns><see cref="ISqlStatementEnd"/> no further statements can be added</returns>
        public ISqlStatementEnd Index(String tableName, String indexName);
    }
}
