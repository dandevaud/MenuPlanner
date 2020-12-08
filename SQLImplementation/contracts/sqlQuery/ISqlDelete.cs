using System;
using SQLImplementation.contracts.sqlQuery.queryparts;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlDelete
    {
        public ISqlQueryFrom From(String tableName);
    }
}
