using System;
using System.Collections.Generic;
using SQLImplementation.contracts.sqlStructure;

namespace SQLImplementation.contracts.sqlQuery
{
    public interface ISqlCreateTable
    {
        public ISqlCreateTable Name(String name);
        public ISqlCreateTable ColumnList(List<ISqlColumn> columnList);

        public ISqlSelect AsSelect(List<ISqlColumn> columnList);

    }
}
