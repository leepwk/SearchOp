using Microsoft.Data.SqlClient;
using SearchEngine.Common.Model;
using SearchEngine.Common.Model.Response;
using SearchEngine.Repository.Interface;
using System.Data;

namespace SearchEngine.Repository
{
    /// <summary>
    /// Database repository access methods using common Sql classes from Microsoft
    /// </summary>
    public class SearchRepository : BaseLogger<SearchRepository>, ISearchRepository
    {
        private readonly IDataConnection _dataConnection;

        public SearchRepository(ILogger<SearchRepository> logger, IDataConnection dataConnection) : base(logger)
        {
            _dataConnection = dataConnection;
        } 

        public async Task<SearchEngineTypeResponse> GetSearchEngineType()
        {
            var results = new List<SearchEngineType>();
            var msg = string.Empty;
            var cmd = "SELECT Id, EngineId, EngineDescription FROM SearchEngine";

            await using SqlConnection connection = _dataConnection.GetConnection();
            await using SqlCommand command = new SqlCommand(cmd, connection);
            try
            {
                connection.Open();

                await using SqlDataReader reader = command.ExecuteReader();

                // Loop through the rows
                while (reader.Read())
                {
                    results.Add(new SearchEngineType
                    {
                        Id = reader.GetInt32(0),
                        EngineId = reader.GetInt32(1), 
                        EngineDescription = reader.GetString(2) 
                    });
                }
            }
            catch (SqlException ex)
            {
                msg = ex.Message;
                Logger.LogError("Database error: " + msg);
            }

            return new SearchEngineTypeResponse { Data = results.OrderBy(r => r.Id).ToList(), Message = msg };
        }

        public async void InsertSearchEngineType(int engineId, string engineDesc)
        {
            // command to run
            var cmd = "INSERT INTO SearchEngine (EngineId, EngineDescription) VALUES (@EngineId, @EngineDescription)";

            await using SqlConnection connection = _dataConnection.GetConnection();
            await using SqlCommand command = new SqlCommand(cmd, connection);
            try
            {
                connection.Open();

                // parameterise the inputs safely
                command.Parameters.AddWithValue("@EngineId", engineId);
                command.Parameters.AddWithValue("@EngineDescription", engineDesc);

                int rowsAffected = command.ExecuteNonQuery();
                Logger.LogInformation($"Inserted {rowsAffected} row");
            }
            catch (SqlException ex)
            {
                Logger.LogError("Database error: " + ex.Message);
            }
        }

        public async Task<IEnumerable<SearchEngineResult>> GetSearchEngineResults(int engineId)
        {
            var results = new List<SearchEngineResult>();
            var cmd = "SELECT Id, EngineId, EntryDate, Rank, Url, SearchTerm FROM SearchResults WHERE EngineId = @EngineId";

            await using SqlConnection connection = _dataConnection.GetConnection();
            await using SqlCommand command = new SqlCommand(cmd, connection);
            try
            {
                connection.Open();

                // parameterise the inputs safely
                command.Parameters.AddWithValue("@EngineId", engineId);

                await using SqlDataReader reader = command.ExecuteReader();

                // Loop through the rows
                while (reader.Read())
                {
                    results.Add(new SearchEngineResult
                    {
                        Id = reader.GetInt32(0),
                        EngineId = reader.GetInt32(1),
                        EntryDate = reader.GetDateTime(2),
                        Rank = reader.GetInt32(3),
                        Url = reader.GetString(4),
                        SearchTerm = reader.GetString(5)
                    });
                }
            }
            catch (SqlException ex)
            {
                Logger.LogError("Database error: " + ex.Message);
            }

            return results;
        }

        /// <summary>
        /// Insert new result records for the current day, will overwrite the existing day
        /// </summary>
        /// <param name="engineId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="toInsert"></param>
        /// <returns></returns>
        public async Task<bool> InsertSearchEngineResults(int engineId, string searchTerm, List<SearchEngineResultBase> toInsert)
        {
            var result = false;
            var recordDate = DateTime.Today;
            var deleteCmd = "DELETE FROM SearchResults WHERE EngineId = @EngineId AND EntryDate = @EntryDate";
            var insertCmd = "INSERT INTO SearchResults (EngineId, EntryDate, Rank, Url, SearchTerm) VALUES (@EngineId, @EntryDate, @Rank, @Url, @SearchTerm)";

            await using SqlConnection connection = _dataConnection.GetConnection();
            SqlTransaction transaction = null;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // delete first, then insert for the current day
                await using SqlCommand deleteCommand = new SqlCommand(deleteCmd, connection, transaction);
                // parameterise the inputs safely
                deleteCommand.Parameters.AddWithValue("@EngineId", engineId);
                deleteCommand.Parameters.AddWithValue("@EntryDate", recordDate);
                deleteCommand.ExecuteNonQuery();

                await using SqlCommand command = new SqlCommand(insertCmd, connection, transaction);
                // parameterise the inputs safely
                command.Parameters.AddWithValue("@EngineId", engineId);
                command.Parameters.AddWithValue("@EntryDate", recordDate);
                command.Parameters.AddWithValue("@SearchTerm", searchTerm);
                var rankParam = command.Parameters.Add("@Rank", SqlDbType.Int);
                var urlParam = command.Parameters.Add("@Url", SqlDbType.NVarChar, 200);

                foreach (var item in toInsert)
                {
                    rankParam.Value = item.Rank;
                    urlParam.Value = item.Url;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
                Logger.LogInformation($"Inserted {toInsert.Count()} rows");
                result = true;
            }
            catch (SqlException ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                Logger.LogError("Database error: " + ex.Message);
            }
            
            return result;
        }
    }
}
