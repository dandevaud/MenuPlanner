// <copyright file="ISqlCreateIndex.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Class used in order to create an Index on a table.
    /// </summary>
    public interface ISqlCreateIndex
    {
        /// <summary>
        /// Adds a on statement to the Query. Please use correct table- and columnnames.
        /// </summary>
        /// <param name="tableName"><see cref="String"/>, reflecting the table name</param>
        /// <param name="columns"><see cref="Array"/> of <see cref="ISqlColumn"/> with all columns</param>
        /// <returns><see cref="ISqlQueryOn"/>, allows to add further statements to the query</returns>
        public ISqlQueryOn On(String tableName, params ISqlColumn[] columns);
    }
}
