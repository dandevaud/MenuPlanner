using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    public interface ISqlQueryWhere : ISqlStatementEnd
    {
        public ISqlStatementEnd GroupBy(String expression);
    }
}
