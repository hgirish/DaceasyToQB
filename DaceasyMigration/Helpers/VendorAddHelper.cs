using DaceasyMigration.Models;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class VendorAddHelper : BaseQbHelper
    {
        public void DoVendorAdd(DaceasyVendor vendor = null)
        {
            Initialize();

            if (vendor == null)
            {
                 BuildQuery();
            }
            else
            {
                BuildQuery(vendor);
            }
           

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        private void BuildQuery(DaceasyVendor vendor)
        {
            IVendorAdd vendorAddRq = RequestMsgSet.AppendVendorAddRq();
            vendorAddRq.Name.SetValue(vendor.Name);
            if (!string.IsNullOrWhiteSpace(vendor.Contact))
            {
                var contact = vendor.Contact;
                var names = contact.Split(' ');
                var length = names.Length;
                if (length > 0)
                {
                    vendorAddRq.FirstName.SetValue(names[0]);
                }
                if (length > 1)
                {
                    vendorAddRq.LastName.SetValue(names[1]);
                }
                
            }
        
            vendorAddRq.IsActive.SetValue(true);
            vendorAddRq.CompanyName.SetValue(vendor.Name);
            vendorAddRq.Contact.SetValue(vendor.Contact);
            vendorAddRq.VendorAddress.Addr1.SetValue(vendor.Add1);
            vendorAddRq.VendorAddress.Addr2.SetValue(vendor.Add2);
            vendorAddRq.VendorAddress.Addr3.SetValue(vendor.Add3);
            vendorAddRq.VendorAddress.City.SetValue(vendor.City);
            vendorAddRq.VendorAddress.State.SetValue(vendor.State);
            vendorAddRq.VendorAddress.PostalCode.SetValue(vendor.Zip);
            vendorAddRq.VendorAddress.Note.SetValue(vendor.NoteFile);
            vendorAddRq.Phone.SetValue(vendor.Phone1);
            vendorAddRq.AltPhone.SetValue(vendor.Phone2);
            vendorAddRq.Fax.SetValue(vendor.Fax);
            vendorAddRq.Email.SetValue(vendor.eMAIL);
            var taxId = vendor.TaxId;
            //if (!string.IsNullOrWhiteSpace(taxId))
            //{
            //    taxId = taxId.Replace("-", "");
            //}
            vendorAddRq.VendorTaxIdent.SetValue(taxId);
            //VendorAddRq.TermsRef.FullName.SetValue(vendor.TermNo);
           
        }
        private void BuildQuery()
        {
            IVendorAdd vendorAddRq = RequestMsgSet.AppendVendorAddRq();
            //Set field value for Name
            vendorAddRq.Name.SetValue("kl");
            vendorAddRq.TermsRef.FullName.SetValue("NO");
            //Set field value for IsActive
            vendorAddRq.IsActive.SetValue(true);
            //Set field value for ListID
           // VendorAddRq.ClassRef.ListID.SetValue("200000-1011023419");
            //Set field value for FullName
            //VendorAddRq.ClassRef.FullName.SetValue("kl");
            //Set field value for CompanyName
            vendorAddRq.CompanyName.SetValue("kl");
            //Set field value for Salutation
          //  VendorAddRq.Salutation.SetValue("kl");
            //Set field value for FirstName
          //  VendorAddRq.FirstName.SetValue("kl");
            //Set field value for MiddleName
            //VendorAddRq.MiddleName.SetValue("kl");
            //Set field value for LastName
           // VendorAddRq.LastName.SetValue("kl");
            //Set field value for JobTitle
         //   VendorAddRq.JobTitle.SetValue("kl");
            //Set field value for Addr1
            vendorAddRq.VendorAddress.Addr1.SetValue("kl");
            //Set field value for Addr2
            vendorAddRq.VendorAddress.Addr2.SetValue("kl");
            //Set field value for Addr3
            vendorAddRq.VendorAddress.Addr3.SetValue("kl");
            //Set field value for Addr4
            vendorAddRq.VendorAddress.Addr4.SetValue("kl");
            //Set field value for Addr5
           // VendorAddRq.VendorAddress.Addr5.SetValue("kl");
            vendorAddRq.VendorAddress.City.SetValue("kl");
            //Set field value for State
            vendorAddRq.VendorAddress.State.SetValue("kl");
            //Set field value for PostalCode
            vendorAddRq.VendorAddress.PostalCode.SetValue("kl");
            //Set field value for Country
            vendorAddRq.VendorAddress.Country.SetValue("kl");
            //Set field value for Note
            vendorAddRq.VendorAddress.Note.SetValue("kl");
            //Set field value for Addr1
            vendorAddRq.ShipAddress.Addr1.SetValue("kl");
            //Set field value for Addr2
            vendorAddRq.ShipAddress.Addr2.SetValue("kl");
            //Set field value for Addr3
            //VendorAddRq.ShipAddress.Addr3.SetValue("kl");
            ////Set field value for Addr4
            //VendorAddRq.ShipAddress.Addr4.SetValue("kl");
            ////Set field value for Addr5
            //VendorAddRq.ShipAddress.Addr5.SetValue("kl");
            ////Set field value for City
            //VendorAddRq.ShipAddress.City.SetValue("kl");
            ////Set field value for State
            //VendorAddRq.ShipAddress.State.SetValue("kl");
            ////Set field value for PostalCode
            //VendorAddRq.ShipAddress.PostalCode.SetValue("kl");
            ////Set field value for Country
            //VendorAddRq.ShipAddress.Country.SetValue("kl");
            ////Set field value for Note
            //VendorAddRq.ShipAddress.Note.SetValue("kl");
            //Set field value for Phone
            //VendorAddRq.Phone.SetValue("kl");
            ////Set field value for AltPhone
            //VendorAddRq.AltPhone.SetValue("kl");
            ////Set field value for Fax
            //VendorAddRq.Fax.SetValue("kl");
            ////Set field value for Email
            //VendorAddRq.Email.SetValue("kl");
            ////Set field value for Cc
            //VendorAddRq.Cc.SetValue("kl");
           vendorAddRq.Contact.SetValue("kl");
          //  VendorAddRq.AltContact.SetValue("kl");
          //  IQBBaseRef AdditionalContactRef1 = VendorAddRq.AdditionalContactRefList.Append();
          //  //Set field value for ContactName
           
          //  AdditionalContactRef1.FullName.SetValue("kl");
          //  //Set field value for ContactValue
          ////  AdditionalContactRef1.ContactValue.SetValue("kl");
          //  IContacts Contacts2 = VendorAddRq.ContactsList.Append();
          //  //Set field value for Salutation
          //  Contacts2.Salutation.SetValue("kl");
          //  //Set field value for FirstName
          //  Contacts2.FirstName.SetValue("kl");
          //  //Set field value for MiddleName
          //  Contacts2.MiddleName.SetValue("kl");
          //  //Set field value for LastName
          //  Contacts2.LastName.SetValue("kl");
          //  //Set field value for JobTitle
          //  Contacts2.JobTitle.SetValue("kl");
          //  IQBBaseRef AdditionalContactRef3 = Contacts2.AdditionalContactRefList.Append();
          //  //Set field value for ContactName
          //  AdditionalContactRef3.FullName.SetValue("kl");
          //  //Set field value for ContactValue
          ////  AdditionalContactRef3.ContactValue.SetValue("kl");
          //  //Set field value for NameOnCheck
          //  VendorAddRq.NameOnCheck.SetValue("kl");
          //  //Set field value for AccountNumber
          //  VendorAddRq.AccountNumber.SetValue("kl");
          //  //Set field value for Notes
          //  VendorAddRq.Notes.SetValue("kl");
          //  IAdditionalNotes AdditionalNotes4 = VendorAddRq.AdditionalNotesList.Append();
          //  //Set field value for Note
          //  AdditionalNotes4.Note.SetValue("kl");
          //  //Set field value for ListID
          //  VendorAddRq.VendorTypeRef.ListID.SetValue("200000-1011023419");
          //  //Set field value for FullName
          //  VendorAddRq.VendorTypeRef.FullName.SetValue("kl");
          //  //Set field value for ListID
          //  VendorAddRq.TermsRef.ListID.SetValue("200000-1011023419");
          //  //Set field value for FullName
          //  VendorAddRq.TermsRef.FullName.SetValue("kl");
          //  //Set field value for CreditLimit
          //  VendorAddRq.CreditLimit.SetValue(10.01);
          //  //Set field value for VendorTaxIdent
          //  VendorAddRq.VendorTaxIdent.SetValue("kl");
          //  //Set field value for IsVendorEligibleFor1099
          //  VendorAddRq.IsVendorEligibleFor1099.SetValue(true);
          //  //Set field value for OpenBalance
          //  VendorAddRq.OpenBalance.SetValue(10.01);
          //  //Set field value for OpenBalanceDate
          //  VendorAddRq.OpenBalanceDate.SetValue(DateTime.Parse("12/15/2007"));
          //  //Set field value for ListID
          //  VendorAddRq.BillingRateRef.ListID.SetValue("200000-1011023419");
          //  //Set field value for FullName
          //  VendorAddRq.BillingRateRef.FullName.SetValue("kl");
          //  //Set field value for ExternalGUID
          //  VendorAddRq.ExternalGUID.SetValue(Guid.NewGuid().ToString("B"));
          //  IQBBaseRef PrefillAccountRef5 = VendorAddRq.PrefillAccountRefList.Append();
          //  //Set field value for ListID
          //  PrefillAccountRef5.ListID.SetValue("200000-1011023419");
          //  //Set field value for FullName
          //  PrefillAccountRef5.FullName.SetValue("kl");
          //  //Set field value for ListID
          //  VendorAddRq.CurrencyRef.ListID.SetValue("200000-1011023419");
          //  //Set field value for FullName
          //  VendorAddRq.CurrencyRef.FullName.SetValue("kl");
          //  //Set field value for IncludeRetElementList
          //  //May create more than one of these if needed
          //  VendorAddRq.IncludeRetElementList.Add("kl");
        }
    }
}