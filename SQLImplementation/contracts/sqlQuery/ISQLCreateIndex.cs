// <copyright file="ISQLCreateIndex.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlCreateIndex
    {
        public String On(String tableName, params String[] columns);
    }
}
