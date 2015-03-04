using System;
using QBFC13Lib;

namespace DaceasyMigration.Models
{
  
    public class DaceasyAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string GenAcct { get; set; }
        public string Description { get; set; }
        public string GroupDesc { get; set; }
        public string Level { get; set; }
        public Int16 CashFlow { get; set; }
        public Int16 GroupName { get; set; }
        public decimal BegBal { get; set; }
        public decimal CurrBal { get; set; }
      
        public ENAccountType AccountType { get; set; }
      
        public DaceasyAccount ParentAccount { get; set; }

    }
}