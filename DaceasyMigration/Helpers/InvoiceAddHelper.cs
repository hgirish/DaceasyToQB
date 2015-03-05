using DaceasyMigration.Models;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class InvoiceAddHelper : BaseQbHelper
    {
       

        public void DoInvoiceAdd(InvoiceAddModel invoiceAddModel)
        {
            Initialize();

            BuildQuery();
            BuildInvoiceAddRq(invoiceAddModel);
            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        

        private void BuildQuery()
        {
            
        }

        void BuildInvoiceAddRq(InvoiceAddModel invoiceAddModel)
        {
            IInvoiceAdd invoiceAddRq = RequestMsgSet.AppendInvoiceAddRq();
           
            invoiceAddRq.CustomerRef.ListID.SetValue(invoiceAddModel.CustomerListId);
         
            invoiceAddRq.ClassRef.ListID.SetValue(invoiceAddModel.ClassClassId);
         
            invoiceAddRq.ARAccountRef.ListID.SetValue(invoiceAddModel.AccountListId);
          
            invoiceAddRq.TemplateRef.ListID.SetValue(invoiceAddModel.TemplateClassId);

            invoiceAddRq.TxnDate.SetValue(invoiceAddModel.TransactionDate);

            invoiceAddRq.PONumber.SetValue(invoiceAddModel.PoNumber);

            invoiceAddRq.TermsRef.ListID.SetValue(invoiceAddModel.TermsListId);
            
            invoiceAddRq.ItemSalesTaxRef.ListID.SetValue(invoiceAddModel.SalesTaxId);
           
            invoiceAddRq.Memo.SetValue(invoiceAddModel.Memo);
         
            invoiceAddRq.CustomerMsgRef.ListID.SetValue(invoiceAddModel.CustomerMsgId);

            foreach (var item in invoiceAddModel.InvoiceItems)
            {
                var invoiceLineElement = invoiceAddRq.ORInvoiceLineAddList.Append();

                invoiceLineElement.InvoiceLineAdd.ItemRef.ListID.SetValue(item.ListId);

                invoiceLineElement.InvoiceLineAdd.Desc.SetValue(item.Description);

                invoiceLineElement.InvoiceLineAdd.Quantity.SetValue(item.Quantity);
            }

        }



    }
}