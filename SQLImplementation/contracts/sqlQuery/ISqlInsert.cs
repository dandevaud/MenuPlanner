// <copyright file="ISqlInsert.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Class to create query to insert data into a table
    /// </summary>
    public interface ISqlInsert
    {
        /// <summary>
        /// Inserts the information provided into the table. The information is structured as Dictionary
        /// </summary>
        /// <param name="tableName"><see cref="String"/></param>
        /// <param name="columnValueMapping"><see cref="Dictionary{ISqlColumn,Object}"/>, column value mapping</param>
        /// <returns><see cref="ISqlStatementEnd"/> no further statements are allowed</returns>
        public ISqlStatementEnd Into(String tableName, Dictionary<ISqlColumn, Object> columnValueMapping);
    }
}
