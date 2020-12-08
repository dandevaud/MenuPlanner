using System;

namespace SQLImplementation.contracts.sqlStructure
{
    public interface ISqlColumn
    {
        public String Name { get; set; }

        public String TableName { get; set; }
        public ISqlDataType dataType { get; set; }
    }
}
