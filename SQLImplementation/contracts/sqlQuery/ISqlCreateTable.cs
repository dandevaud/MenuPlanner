// <copyright file="ISqlCreateTable.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Class used to build a query which creates a new Table
    /// </summary>
    public interface ISqlCreateTable
    {
        /// <summary>
        /// Defines the columns to be added to the table to create.
        /// </summary>
        /// <param name="columnList"><see cref="List"/> containing implementations of <see cref="ISqlColumn"/></param>
        /// <returns><see cref="ISqlCreateTable"/>, in order to add further information on the table to add</returns>
        public ISqlCreateTable ColumnList(List<ISqlColumn> columnList);

        /// <summary>
        /// Creates new table with an AS Select statement
        /// </summary>
        /// <param name="columnList"><see cref="List"/> of <see cref="ISqlColumn"/> consisting of all columns used from other table</param>
        /// <returns><see cref="ISqlQuerySelect"/>, in order to add additional information</returns>
        public ISqlQuerySelect AsSelect(List<ISqlColumn> columnList);

    }
}
