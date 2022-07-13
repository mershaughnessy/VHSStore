using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace VHSStore.Utility.Sql
{
    public class DapperWrap
    {
        private readonly string _connectionString;

        public DapperWrap(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, [Optional] object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(query, parameters);
                return result;
            }
        }

        public async Task<T> QuerySingleAsync<T>(string query, [Optional] object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
                return result;
            }
        }

        public async Task<int> ExecuteAsync(string query, [Optional] object parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(query, parameters);
                return result;
            }
        }
    }
}
