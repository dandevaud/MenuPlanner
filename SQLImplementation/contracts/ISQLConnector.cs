// <copyright file="ISQLConnector.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

namespace SQLImplementation.contracts
{
    public interface ISqlConnector
    {

        public ISqlQueryBuilder GetQueryBuilder();

        public void SetConnection(ISqlConnection sqlConnection);


    }
}
