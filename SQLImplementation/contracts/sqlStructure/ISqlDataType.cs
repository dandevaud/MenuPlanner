namespace SQLImplementation.contracts.sqlStructure
{
    public interface ISqlDataType
    {
        public SqlDataTypeEnum Type { get; set; }
        public int Max { get; set; }
    }
}
