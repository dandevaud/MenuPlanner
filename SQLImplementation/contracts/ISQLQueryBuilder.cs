// <copyright file="ISQLQueryBuilder.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts
{
    /// <summary>
    /// QueryBuilder initializes the building of SQL Queries 
    /// </summary>
    public interface ISqlQueryBuilder
    {
        

        /// <summary>
        /// Initializes the query to create an index
        /// </summary>
        /// <param name="isUnique"><see cref="Boolean"/> provide the information if the Index is unique</param>
        /// <returns><see cref="ISqlCreateIndex"/> allows to add further statements</returns>
        public ISqlCreateIndex CreateIndex(Boolean isUnique);

        /// <summary>
        /// Initializes the query to create a new table
        /// </summary>
        /// <param name="tableName"><see cref="String"/></param>
        /// <returns><see cref="ISqlCreateTable"/> allows to add further statements</returns>
        public ISqlCreateTable CreateTable(String tableName);

        /// <summary>
        /// Initializes the query to delete a table or an index
        /// </summary>
        /// <returns><see cref="ISqlDelete"/> allows to add further statements</returns>
        public ISqlDelete Delete();

        /// <summary>
        /// Initializes the query to drop a table or an index
        /// </summary>
        /// <returns><see cref="ISqlDrop"/>  allows to add further statements</returns>
        public ISqlDrop Drop();

        /// <summary>
        /// Initializes the query to insert a data into a table
        /// </summary>
        /// <returns><see cref="ISqlInsert"/>  allows to add further statements</returns>
        public ISqlInsert Insert();


        /// <summary>
        /// Initializes the query to update existing information in a table
        /// </summary>
        /// <param name="tableName"><see cref="String"/> location of the data to be changed</param>
        /// <returns><see cref="ISqlUpdate"/>  allows to add further statements</returns>
        public ISqlUpdate Update(String tableName);


        /// <summary>
        /// Initializes the query to select information from the database
        /// </summary>
        /// <param name="columns"><see cref="List{ISqlColumn}"/>, List of all columns to get from database</param>
        /// <returns><see cref="ISqlQuerySelect"/> allows to add further statements</returns>
        public ISqlQuerySelect Select(List<ISqlColumn> columns);

    }
}
