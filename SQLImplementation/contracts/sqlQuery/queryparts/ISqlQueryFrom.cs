// <copyright file="ISqlQueryFrom.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    public interface ISqlQueryFrom : ISqlStatementEnd
    {
        /// <summary>
        /// Adds a Where clause to the Statement. Please use the correct Syntax for the Expression
        /// </summary>
        /// <param name="expression">String, expression of the where clause (without 'where')</param>
        /// <returns><see cref="ISqlQueryWhere"/>, allows to add additional information to the query</returns>
        public ISqlQueryWhere Where(String expression);
    }
}
