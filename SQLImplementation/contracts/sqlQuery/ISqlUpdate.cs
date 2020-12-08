using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlQuery.queryparts;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlUpdate
    {
        public ISqlQuerySet Set(Dictionary<ISqlColumn, Object> columnObjectMapping);
    }
}
