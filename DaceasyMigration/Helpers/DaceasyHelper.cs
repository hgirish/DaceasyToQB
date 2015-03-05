using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DaceasyMigration.Models;
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

        public async Task<IList<DaceasyCustomer>> GetCustomersAsync()
        {
            return await Task.Run(() => GetCustomers());
        }

        private IList<DaceasyCustomer> GetCustomers()
        {
            using (var connection = GetOpenConnection())
            {
                var customers = connection.Query<DaceasyCustomer>("Select * from Customers");
                return customers.ToList();
            }
        }

        public async Task<IEnumerable<DaceasyVendor>> GetVendorsAsync()
        {
            return await  Task.Run(() => GetVendors());
        }

        public IEnumerable<DaceasyVendor> GetVendors()
        {
            using (var conneciton = GetOpenConnection())
            {
                var vendores = conneciton.Query<DaceasyVendor>("Select * from Vendors");
                return vendores.ToList();
            }
        }
        public Task<List<DaceasyMessage>> GetDaceasyMessagesAsync()
        {
            return Task.Run(() => GetDaceasyMessages());
        }
        public List<DaceasyMessage> GetDaceasyMessages()
        {
            using (var conneciton = GetOpenConnection())
            {
                var messages = conneciton.Query<DaceasyMessage>("Select * from Messages");
                return messages.ToList();
            }
        }

        public async Task<IEnumerable<DaceasyAccount>> GetDaceasyAccountsAsync()
        {
            return await  Task.Run(() => GetDaceasyAccounts());
        }

        public IEnumerable<DaceasyAccount> GetDaceasyAccounts()
        {
            using (var connection = GetOpenConnection())
            {
                var daceasyAccounts = connection.Query<DaceasyAccount>("Select * from Accounts");
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