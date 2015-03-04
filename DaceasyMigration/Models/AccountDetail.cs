using QBFC13Lib;

namespace DaceasyMigration
{
    public class AccountDetail
    {
        public string Name { get; set; }
        public string ListId { get; set; }
        public string FullName { get; set; }
        public string ParentListId { get; set; }
        public string ParentName { get; set; }
        public string Description { get; set; }
        public ENAccountType AccountType { get; set; }
        public string AccountTypeString { get; set; }
        public int SubLevel { get; set; }
    }
}