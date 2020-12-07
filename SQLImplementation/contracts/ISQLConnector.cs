namespace SQLImplementation.contracts
{
    public interface ISQLConnector
    {

        public ISQLQueryBuilder GetQueryBuilder();

        public void SetConnection(ISQLConnection sqlConnection);

        
    }
}