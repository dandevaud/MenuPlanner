using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    public interface ISqlQueryFrom : ISqlStatementEnd
    {
        public ISqlQueryWhere Where(String Expression);
    }
}
