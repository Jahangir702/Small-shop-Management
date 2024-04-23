using Microsoft.Data.SqlClient;
using System.Data;

namespace MVC.Data
{
    public class ReportDbContext
    {
        private readonly string _connectionString;

        public ReportDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);
    }
}
