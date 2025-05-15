using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SearchEngine.Common.Model;
using SearchEngine.Repository.Interface;

namespace SearchEngine.Repository.Helpers
{
    public class DataConnection : IDataConnection
    {
        private readonly AppSettings _appSettings;
        
        public DataConnection(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_appSettings.ConnectionString);
        }
    }
}
