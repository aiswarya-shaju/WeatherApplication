using Dapper;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WeatherApplication.DataAccess.Abstractions;
using WeatherApplication.Models.Models;
using Microsoft.Data.SqlClient;

namespace WeatherApplication.DataAccess.Implementations
{
    public class UserHistory : IUserHistory
    {
        public IConfiguration Configuration { get; }
        public UserHistory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IEnumerable<T>> GetUserHistoryAsync<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            
            return await db.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            /*{
                string sql = "SELECT * FROM UserHistory WHERE UserId = @UserId";
                return await db.QueryAsync<WeatherHistory>(sql, new { UserId = userId });
            }*/
        }
        public async Task<bool> AddUserHistoryAsync<T>(string storedProcedure, T parameters)
        {
            try
            {
                using IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                var result = await db.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
                
            }

            return false;
        }
    }
}
