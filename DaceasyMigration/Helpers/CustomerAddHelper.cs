using QBFC13Lib;

namespace DaceasyMigration
{
    public class CustomerAddHelper : BaseQbHelper
    {
        public void DoCustomerAdd(DaceasyCustomer customer = null)
        {
            Initialize();
          
            BuildQuery(customer);

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        private void BuildQuery(DaceasyCustomer customer)
        {
            ICustomerAdd customerAddRq = RequestMsgSet.AppendCustomerAddRq();
            customerAddRq.Name.SetValue(customer.Name);
            if (!string.IsNullOrWhiteSpace(customer.Contact))
            {
                var contact = customer.Contact;
                var names = contact.Split(' ');
                var length = names.Length;
                if (length > 0)
                {
                    customerAddRq.FirstName.SetValue(names[0]);
                }
                if (length > 1)
                {
                    customerAddRq.LastName.SetValue(names[1]);
                }
                
            }
        
            customerAddRq.IsActive.SetValue(true);
            customerAddRq.CompanyName.SetValue(customer.Name);
            customerAddRq.Contact.SetValue(customer.Contact);
            customerAddRq.BillAddress.Addr1.SetValue(customer.Add1);
            customerAddRq.BillAddress.Addr2.SetValue(customer.Add2);
            customerAddRq.BillAddress.Addr3.SetValue(customer.Add3);
            customerAddRq.BillAddress.City.SetValue(customer.City);
            customerAddRq.BillAddress.State.SetValue(customer.State);
            customerAddRq.BillAddress.PostalCode.SetValue(customer.Zip);
            customerAddRq.BillAddress.Note.SetValue(customer.NoteFile);
            customerAddRq.Phone.SetValue(customer.Phone1);
            customerAddRq.AltPhone.SetValue(customer.Phone2);
            customerAddRq.Fax.SetValue(customer.Fax);
            customerAddRq.Email.SetValue(customer.EMail);
                    
        }
    
    }
}