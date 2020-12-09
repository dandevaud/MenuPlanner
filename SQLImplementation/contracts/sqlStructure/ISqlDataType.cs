// <copyright file="ISqlDataType.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlStructure
{
    /// <summary>
    /// Used to define a DataType for SQL Columns
    /// </summary>
    public interface ISqlDataType
    {
        /// <summary>
        /// Underlying dataType
        /// </summary>
        public SqlDataTypeEnum Type { get; set; }

        /// <summary>
        /// Max as nullable Int16, used for instance for NVARCHAR(max)
        /// </summary>
        public Int16 Max { get; set; }
    }
}
