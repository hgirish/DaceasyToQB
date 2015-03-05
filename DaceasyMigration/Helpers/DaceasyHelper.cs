using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DaceasyMigration.Helpers
{
    public class DaceasyHelper
    {
        public const string ConnectionString = "Data Source=.\\SqlExpress;Initial Catalog=Daceasy;Integrated Security=True";

        public async Task<IList<string>> GetSalesRepsAync()
        {
            return await Task.Run(() => GetSalesReps());
        }

        private IList<string> GetSalesReps()
        {
            using (var connection = GetOpenConnection())
            {
                var daceasyAccounts = connection.Query<string>("Select SPNo from SalesPerson");
                return daceasyAccounts.ToList();
            }
        }


        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}