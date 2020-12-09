// <copyright file="ISqlUpdate.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    /// <summary>
    /// Used to create a query which updates information 
    /// </summary>
    public interface ISqlUpdate
    {
        /// <summary>
        /// Sets the information according to the column value mapping
        /// </summary>
        /// <param name="columnObjectMapping"><see cref="Dictionary{ISqlColumn,Object}"/></param>
        /// <returns><see cref="ISqlQuerySet"/> in order to add further statements</returns>
        public ISqlQuerySet Set(Dictionary<ISqlColumn, Object> columnObjectMapping);
    }
}
