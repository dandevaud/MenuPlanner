using System;
using SQLImplementation.contracts.sqlQuery;

namespace SQLImplementation.contracts
{
    public interface ISQLQueryBuilder
    {
        /**
         * https://sqlite.org/lang.html
         * Implement only the Statements needed
         */

        public ISQLCreateIndex CreateIndex(Boolean isUnique);


    }
}