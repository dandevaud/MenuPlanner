// <copyright file="ISqlQuerySet.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    /// <summary>
    /// Adds a set to the update statement of the SQL Query
    /// </summary>
    public interface ISqlQuerySet
    {
        /// <summary>
        /// Adds a Where clause to the Statement. Please use the correct Syntax for the Expression
        /// </summary>
        /// <param name="expression">String, expression of the where clause (without 'where')</param>
        /// <returns><see cref="ISqlQueryWhere"/>, allows to add additional information to the query</returns>
        ISqlStatementEnd Where(String expression);
    }
}
