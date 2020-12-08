using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlInsert
    {
        public ISqlStatementEnd Into(String tableName, Dictionary<ISqlColumn, Object> columnValueMapping);
        public ISqlQuerySelect Into(String tableName);
    }
}
