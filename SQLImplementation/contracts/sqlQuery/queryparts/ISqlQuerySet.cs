using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    public interface ISqlQuerySet
    {
        ISqlStatementEnd Where(String expression);
    }
}
