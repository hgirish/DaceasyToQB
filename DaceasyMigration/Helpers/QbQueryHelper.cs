using System.Collections.Generic;
using System.Threading.Tasks;
using DaceasyMigration.Models;

namespace DaceasyMigration.Helpers
{
    public class QbQueryHelper
    {
        public  IList<AccountDetail> AccountDetails { get; set; }
        public IList<QbGenericQueryModel> QbVendors { get; set; }
        public IList<QbGenericQueryModel> QbCustomers { get; set; } 
        public QbQueryHelper()
        {
            AccountDetails = new List<AccountDetail>();
            QbVendors = new List<QbGenericQueryModel>();
            QbCustomers = new List<QbGenericQueryModel>();
        }

        public async void PopulateLists()
        {
           await  GetAccountListAsync();
            await GetVendorsListAsync();
            await GetCustomersListAsync();
        }

        private async Task GetCustomersListAsync()
        {
            QbCustomers = await  new CustomerQueryHelper().GetCustomersAsync();
        }

        private async Task GetVendorsListAsync()
        {
            QbVendors = await  new VendorsQueryHelper().GetQbVendorsAsync();
        }

        public async Task GetAccountListAsync()
        {
            AccountDetails = await new AccountListHelper().GetListAsync();
        }


    }
}