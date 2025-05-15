using Microsoft.Data.SqlClient;

namespace SearchEngine.Repository.Interface;

public interface IDataConnection
{
    SqlConnection GetConnection();
}