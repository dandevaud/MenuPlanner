// <copyright file="ISqlQuerySelect.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    /// <summary>
    /// Class used to build both the Select as well as the Select Distinct query.
    /// </summary>
    public interface ISqlQuerySelect
    {
        /// <summary>
        /// Adds a From clause to the Statement. Please use the correct the correct table name
        /// </summary>
        /// <param name="tableName">String, table name to take from including the schema</param>
        /// <returns><see cref="ISqlQueryFrom"/>, allows to add additional information to the query</returns>
        public ISqlQueryFrom From(String tableName);

        /// <summary>
        /// Sets the Select Query Distinct and returns the <see cref="ISqlQuerySelect"/> to continue with the query
        /// </summary>
        /// <returns><see cref="ISqlQuerySelect"/>, allows to continue with the query</returns>
        public ISqlQuerySelect Distinct();
    }
}
