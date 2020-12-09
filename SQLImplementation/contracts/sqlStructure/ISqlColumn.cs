// <copyright file="ISqlColumn.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlStructure
{

    /// <summary>
    /// Defines a SqlColumn (Name, TableName and DataType)
    /// </summary>
    public interface ISqlColumn
    {
        public String Name { get; set; }
        public String TableName { get; set; }
        /// <summary>
        /// <see cref="ISqlDataType"/> DataType of the column
        /// </summary>
        public ISqlDataType DataType { get; set; }
    }
}
