using QBFC13Lib;

namespace DaceasyMigration
{
    public class CustomerMessagesHelper : BaseQbHelper
    {
        public void DoCustomerMessageAdd(CustomerMessageAddModel model)
        {
            Initialize();

            BuildQuery(model);
            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        private void BuildQuery(CustomerMessageAddModel model)
        {
            ICustomerMsgAdd customerMsgAddRq = 
                RequestMsgSet.AppendCustomerMsgAddRq();
            customerMsgAddRq.Name.SetValue(model.Name);
            customerMsgAddRq.IsActive.SetValue(model.IsActive);

        }

        
    }
}