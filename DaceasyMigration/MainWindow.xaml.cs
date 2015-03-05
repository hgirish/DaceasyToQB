using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DaceasyMigration.Helpers;
using DaceasyMigration.Models;
using Dapper;

namespace DaceasyMigration
{

    public partial class MainWindow
    {
        public static IList<AccountDetail> AccountDetails { get; set; }

        public const string ConnectionString = "Data Source=.\\SqlExpress;Initial Catalog=Daceasy;Integrated Security=True";
       // private readonly BackgroundWorker _worker;
       // private IEnumerable<DaceasyAccount> accounts = new List<DaceasyAccount>();
        readonly CustomerMessagesHelper _customerMessagesHelper =
            new CustomerMessagesHelper();

        readonly CustomerAddHelper _customerAddHelper = new CustomerAddHelper();
        readonly  AccountAddHelper _accountAddHelper = new AccountAddHelper();
        private readonly VendorAddHelper _vendorAddHelper = new VendorAddHelper();
        private readonly VendorsQueryHelper _vendorsQueryHelper = new VendorsQueryHelper();
        private DaceasyHelper _daceasyHelper = new DaceasyHelper();
        private readonly SalesRepAddHelper _salesRepAddHelper = new SalesRepAddHelper();
        public MainWindow()
        {
            InitializeComponent();
            AccountDetails = new List<AccountDetail>();
           // _worker = FindResource("BackgroundWorkerComponentModel") as BackgroundWorker;
        }

        

        //private void AddCustomerMessagesButton_OnClick(object sender, RoutedEventArgs e)
        //{
        //    if ((string)AddCustomerMessagesButton.Content == "Cancel")
        //    {
        //        _worker.CancelAsync();
        //        AddCustomerMessagesButton.Content = "Add Customer Messages";
        //        return;
        //    }

        //    if (!_worker.IsBusy)
        //    {
        //        Cursor = Cursors.Wait;
        //        _worker.RunWorkerAsync();
        //        AddCustomerMessagesButton.Content = "Cancel";
        //    }
        //    else
        //    {
        //        _worker.CancelAsync();
        //    }

           
        //}

        //private void AddCustomerMessages(DoWorkEventArgs doWorkEventArgs)
        //{
        //    var messages = GetDaceasyMessages();
        //    AddCustomerMessagesToQb(messages, doWorkEventArgs);
        //}

        //private void AddCustomerMessagesToQb(List<DaceasyMessage> messages, DoWorkEventArgs doWorkEventArgs)
        //{
           
        //    int totalMessages = messages.Count;
        //    int index = 0;
        //    var helper = new CustomerMessagesHelper();
        //    foreach (var message in messages)
        //    {
        //        if (_worker.CancellationPending)
        //        {
        //            helper.CleanUp();
        //            doWorkEventArgs.Cancel = true;
        //            break;
        //        }
        //        _statusMessage = message.Message;
        //        index++;

        //        var model = new CustomerMessageAddModel {IsActive = true, Name = message.Message};
        //        helper.DoCustomerMessageAdd(model);
        //        _worker.ReportProgress(index*100/totalMessages);
        //    }
        //}

        private List<DaceasyMessage> GetDaceasyMessages()
        {
            using (var conneciton = GetOpenConnection())
            {
                var messages = conneciton.Query<DaceasyMessage>("Select * from Messages");
                return messages.ToList();
            }
        }

        private Task<List<DaceasyMessage>> GetDaceasyMessagesAsync()
        {
            return Task.Run(() => GetDaceasyMessages());
        }
        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        //private void BackgroundWorker_OnDoWork(object sender, DoWorkEventArgs e)
        //{
        //    AddCustomerMessages(e);
        //}

        //private void BackgroundWorker_OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Cursor = Cursors.Arrow;
        //    if (e.Error != null) Console.WriteLine(e.Error.Message);
        //    MessageBox.Show(e.Cancelled ? "Cancelled" : "Operation successful!");
        //    AddCustomerMessagesButton.Content = "Add Customer Messages";
        //}

        //private void BackgroundWorker_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    ProgressBar.Value = e.ProgressPercentage;
        //    ResultBox.Items.Add(_statusMessage);
        //    ScrollToBottom();
        //}

        public void ScrollToBottom()
        {
            ResultBox.ScrollIntoView(ResultBox.Items[ResultBox.Items.Count - 1]);
        }

        private void CloseApplicationButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void AddCustomerMessagesParallelButtonClick(object sender, RoutedEventArgs e)
        {
            AddCustomerMessagesParallelButton.IsEnabled = false;
            var messages = await GetDaceasyMessagesAsync();
            ResultBox.Items.Add(string.Format("Got {0} Daceasy Messages", messages.Count));
            int index = 0;
            foreach (var daceasyMessage in messages)
            {
                index++;
                var message = daceasyMessage.Message;
                var model = new CustomerMessageAddModel { IsActive = true, Name = message };
                await AddMessageToQbAsync(model);
                ResultBox.Items.Add(string.Format("Message: {0}: {1}", index, message));
            }
            MessageBox.Show("Finished adding");


            
        }

        private Task AddMessageToQbAsync(CustomerMessageAddModel model)
        {
            return Task.Run(() => _customerMessagesHelper.DoCustomerMessageAdd(model));
        }

        //private void AddCustomerMessagesToQb(List<DaceasyMessage> messages)
        //{
        //    int totalMessages = messages.Count;
        //    int index = 0;
        //    var helper = new CustomerMessagesHelper();

        //    Task.Factory.StartNew((() =>
        //    {
        //        Parallel.ForEach(messages, message =>
        //        {
        //            _statusMessage = message.Message;
        //            index++;
        //            string messageString = _statusMessage;
        //            var model = new CustomerMessageAddModel { IsActive = true, Name = messageString };
        //            helper.DoCustomerMessageAdd(model);
        //            int index1 = index;
        //            Dispatcher.BeginInvoke(new Action(() =>
        //            {
        //                _worker.ReportProgress(index1 * 100 / totalMessages);
        //                ResultBox.Items.Add(messageString);
        //            }));
        //        });

        //    }));
            
        //}

        private async void AddCustomerButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddCustomerButton.IsEnabled = false;
            //ResultBox.Items.Clear();
            var customers = await GetDaceasyCustomersAsync();
            var daceasyCustomers = customers as IList<DaceasyCustomer> ?? customers.ToList();
            ResultBox.Items.Add(string.Format("Got {0} Daceasy Customers", daceasyCustomers.Count()));
            await AddCustomersToQbAsync(daceasyCustomers);
            MessageBox.Show("Customer adding completed succesfully");
        }

        private async Task AddCustomersToQbAsync(IList<DaceasyCustomer> daceasyCustomers)
        {
            int index = 0;
            foreach (var customer in daceasyCustomers)
            {
                index ++;
                await AddCustomerToQbAsync(customer);
                ResultBox.Items.Add(string.Format("Customer: {0}: {1}", index,customer.Name));
                ScrollToBottom();
            }
        }

        private Task AddCustomerToQbAsync(DaceasyCustomer customer)
        {
           return  Task.Run(() =>
            {
                _customerAddHelper.DoCustomerAdd(customer);
            });
        }

        private Task<IEnumerable<DaceasyCustomer>> GetDaceasyCustomersAsync()
        {
            return Task.Run(() => GetDaceasyCustomeres());
        }
        private  IEnumerable<DaceasyCustomer> GetDaceasyCustomeres()
        {
            using (var connection = GetOpenConnection())
            {
                var customers = connection.Query<DaceasyCustomer>("Select * from Customers");
                return customers.ToList();
            }
        }

        private async void AddAccountsButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddAccountsButton.IsEnabled = false;
             var accounts = await  GetDaceasyAccountsAsync();
           
            var daceasyAccounts = accounts as IList<DaceasyAccount> ?? accounts.ToList();
           
            ResultBox.Items.Add(string.Format("{0} Daceasy Accounts", daceasyAccounts.Count()));
           
            await GetAccountListAsync();
            ResultBox.Items.Add(string.Format("{0} QB Accounts", AccountDetails.Count()));
         
            await AddAccountsToQbAsync(daceasyAccounts);
            MessageBox.Show("Accounts adding completed succesfully");
        }

        private Task GetAccountListAsync()
        {
          return   Task.Run(() => GetAccountList());
        }

        private async Task AddAccountsToQbAsync(IEnumerable<DaceasyAccount> accounts)
        {
            int index = 0;
            foreach (var account in accounts)
            {
                index++;
                await AddAccountToQbAsync(account);
                ResultBox.Items.Add(string.Format("Account: {0}: {1}", index, account.Description));
                ScrollToBottom();
            }
        }

        private Task AddAccountToQbAsync(DaceasyAccount d)
        {
            return Task.Run(() =>
            {
                AccountDetail account = new AccountDetail();
                account.Name = d.Description;
                account.Description = d.Description;
                account.AccountType = d.AccountType;

                if (d.ParentAccount != null)
                {
                    var parentAccount = GetAccountDetail(d.ParentAccount.Description);
                    if (parentAccount != null)
                    {
                        account.ParentListId = parentAccount.ListId;
                        account.ParentName = parentAccount.Name;
                    }
                }
                _accountAddHelper.DoAdd(account);
            });
        }
        private  AccountDetail GetAccountDetail(string name)
        {
            var account = AccountDetails.FirstOrDefault(x => x.Name == name);
            return account;
        }

        private Task<IEnumerable<DaceasyAccount>>  GetDaceasyAccountsAsync()
        {
            return Task.Run(() => GetDaceasyAccounts());
        }

        private IEnumerable<DaceasyAccount> GetDaceasyAccounts()
        {
            using (var connection = GetOpenConnection())
            {
                var daceasyAccounts = connection.Query<DaceasyAccount>("Select * from Accounts");
                return daceasyAccounts.ToList();
            }
        }

        private  void GetAccountList()
        {
            AccountListHelper helper = new AccountListHelper();
           AccountDetails = helper.GetList();
            
        }

        private async void AddVendores_OnClick(object sender, RoutedEventArgs e)
        {
            AddVendores.IsEnabled = false;
            var vendors = await GetDacesyVendorsAsync();
            var daceasyVendors = vendors as IList<DaceasyVendor> ?? vendors.ToList();

            ResultBox.Items.Add(string.Format("Got {0} Daceasy Vendors", daceasyVendors.Count()));
            var qbVendors = await GetQbVendorsAsync();
            var qbVendorsList = qbVendors as IList<VendorQueryModel> ?? qbVendors.ToList();
            ResultBox.Items.Add(string.Format("{0} QB Vendors", qbVendorsList.Count()));
            var q = daceasyVendors.Where(v => qbVendorsList.All(p => p.Name != v.Name)).ToList();
            ResultBox.Items.Add("Not in existing vendor list: " + q.Count.ToString());
            foreach (var daceasyVendor in q)
            {
                ResultBox.Items.Add(string.Format("New Vendor: {0}", daceasyVendor.Name));
            }
            await AddVendorsToQbAsync(q);
            MessageBox.Show("Vendor adding completed succesfully");
            AddVendores.IsEnabled = true;
        }

        private async Task<IEnumerable<VendorQueryModel>>  GetQbVendorsAsync()
        {
            return await _vendorsQueryHelper.GetQbVendors();
        }

        private async Task AddVendorsToQbAsync(IList<DaceasyVendor> daceasyVendors)
        {
            int index = 0;
            int total = daceasyVendors.Count;
            foreach (var vendor in daceasyVendors)
            {
                index++;
                await AddVendorToQbAsync(vendor);
                ResultBox.Items.Add(string.Format("Vendor: {0} of {1}: {2}", index,total, vendor.Name));
                ScrollToBottom();
            }
        }

        private Task AddVendorToQbAsync(DaceasyVendor vendor)
        {
            return Task.Run(() =>
            {
                _vendorAddHelper.DoVendorAdd(vendor);
            });
        }

        private Task<IEnumerable<DaceasyVendor>> GetDacesyVendorsAsync()
        {
            return Task.Run(() => GetDaceasyVendores());
        }

        public  IEnumerable<DaceasyVendor> GetDaceasyVendores()
        {
            using (var conneciton = GetOpenConnection())
            {
                var vendores = conneciton.Query<DaceasyVendor>("Select * from Vendors");
                return vendores.ToList();
            }
        }

        private  async void AddSalesRepButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddSalesRepButton.IsEnabled = false;
            var daceasySalesReps = await _daceasyHelper.GetSalesRepsAync();
            NotifyUi(string.Format("Daceasy Sales Reps: {0}", daceasySalesReps.Count));
            await AddSalesRepsToQb(daceasySalesReps);
            MessageBox.Show("Add Sales Rep completed");
        }

        private async Task AddSalesRepsToQb(IEnumerable<string> daceasySalesReps)
        {
            int index = 0;
            foreach (var rep in daceasySalesReps)
            {
                index++;
                await AddSalesRepToQb(rep);
                NotifyUi(string.Format("Sales Rep {0}: {1}",index, rep));
            }
        }

        private Task AddSalesRepToQb(string rep)
        {
            return Task.Run(() =>
            {
                SalesRepAddModel model = new SalesRepAddModel
                {
                    Initial = rep,
                    FullName = rep,
                    IsActive = true
                };
                _salesRepAddHelper.DoAdd(model);
            });
        }

        private void NotifyUi(string message)
        {
            ResultBox.Items.Add(message);
            ScrollToBottom();
        }
    }
}
