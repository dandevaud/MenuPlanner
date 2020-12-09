// <copyright file="SqlDataTypeEnum.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

namespace SQLImplementation.contracts.sqlStructure
{
    
    /// <summary>
    /// Basic SQL DataTypes 
    /// </summary>
    public enum SqlDataTypeEnum
    {
        //From https://www.journaldev.com/16774/sql-data-types
        Bit, 
        Tinyint, 
        Smallint, 
        Int,
        Bigint,
		Decimal,
		Numeric,
		Float,
		Real,
		Date,
		Time,
		Datetime,
		Timestamp,
		Year,
		Char,
		Varchar,
		Text,
		Nchar,
		Nvarchar,
		Ntext,
		Binary,
		Varbinary,
		Image,
		Clob,
		Blob,
		Xml,
		Json
    }
}
