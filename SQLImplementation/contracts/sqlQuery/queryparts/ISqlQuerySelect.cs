using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    public interface ISqlQuerySelect
    {
        public ISqlQueryFrom From(String tableName);
        public ISqlQuerySelect Distinct();
    }
}
