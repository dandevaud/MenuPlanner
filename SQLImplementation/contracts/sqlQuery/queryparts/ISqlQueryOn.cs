// <copyright file="ISqlQueryOn.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts 
{
    public interface ISqlQueryOn : ISqlStatementEnd
    {
        public ISqlStatementEnd where(String expression);
    }
}
