// <copyright file="ISQLQueryBuilder.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using SQLImplementation.contracts.sqlQuery;

namespace SQLImplementation.contracts
{
    public interface ISqlQueryBuilder
    {
        /**
         * https://sqlite.org/lang.html
         * Implement only the Statements needed
         */

        public ISqlCreateIndex CreateIndex(Boolean isUnique);


    }
}
