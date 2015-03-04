using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class AccountAddHelper : BaseQbHelper
    {
        public void DoAdd(AccountDetail detail)
        {
            Initialize();

            BuildQuery(detail);

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        void BuildQuery(AccountDetail detail)
        {
            if (detail == null)
            {
                return;
            }
            IAccountAdd accountAddRq = RequestMsgSet.AppendAccountAddRq();
            if (!string.IsNullOrWhiteSpace(detail.Name))
            {
                accountAddRq.Name.SetValue(detail.Name);
            }
            
            accountAddRq.IsActive.SetValue(true);
            accountAddRq.AccountType.SetValue(detail.AccountType);
            accountAddRq.IsActive.SetValue(true);
            if (!string.IsNullOrWhiteSpace(detail.ParentListId))
            {
                accountAddRq.ParentRef.ListID.SetValue(detail.ParentListId);
            }
            if (!string.IsNullOrWhiteSpace(detail.ParentName))
            {
                accountAddRq.ParentRef.FullName.SetValue(detail.ParentName);
            }
            
            
            accountAddRq.Desc.SetValue(detail.Description);
          
        }
    }
}