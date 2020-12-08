using System;
using SQLImplementation.contracts.sqlQuery.queryparts;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlDrop : ISqlStatementEnd
    {
        public ISqlStatementEnd Table(String tableName);
        public ISqlStatementEnd Index(String tableName, String indexName);
    }
}
