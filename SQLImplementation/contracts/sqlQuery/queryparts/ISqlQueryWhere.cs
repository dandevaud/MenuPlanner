// <copyright file="ISqlQueryWhere.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    /// <summary>
    /// Adds a Where clause to the SQL Query
    /// </summary>
    public interface ISqlQueryWhere : ISqlStatementEnd
    {
        /// <summary>
        /// Allows you to add a GroupBy to the SQL Statement. Please use the correct syntax for the expression. 
        /// </summary>
        /// <param name="expression">String, GroupBy expression</param>
        /// <returns><see cref="ISqlStatementEnd"/>, as there are not further statements to be added after the GroupBy.</returns>
        public ISqlStatementEnd GroupBy(String expression);
    }
}
