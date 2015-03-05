using System;
using System.Collections.Generic;

namespace DaceasyMigration.Models
{
    public class InvoiceAddModel
    {
        public string PoNumber {get; set; }
        public string CustomerListId {get; set;}        
        public string AccountListId {get; set;}        
        public string TermsListId {get; set;}        
        public string ClassClassId {get; set;}        
        public string TemplateClassId {get; set;}        
        public string CustomerMsgId {get; set;}        
        public string SalesTaxId {get; set;}
        public IList<InvoiceItemModel> InvoiceItems { get; set; }
        public string Memo { get; set; }
        public DateTime TransactionDate { get; set; }

        public InvoiceAddModel()
        {
            InvoiceItems = new List<InvoiceItemModel>();
        }
    }

    public class InvoiceItemModel
    {
        public string ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}