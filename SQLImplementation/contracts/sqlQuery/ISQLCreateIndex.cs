// <copyright file="ISQLCreateIndex.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using SQLImplementation.contracts.sqlQuery.queryparts;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlCreateIndex
    {
        public ISqlQueryOn On(String tableName, params String[] columns);
    }
}
