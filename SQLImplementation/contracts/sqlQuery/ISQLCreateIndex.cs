using System;
using System.Collections.Generic;
using System.Text;

namespace SQLImplementation
{
    public interface ISQLCreateIndex
    {
        public String On(String tableName, params String[] columns);
    }
}