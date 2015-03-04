namespace DaceasyMigration.Helpers
{
    public class TermsHelper : BaseQbHelper
    {
        public void DoAddTerms()
        {
            Initialize();

            BuildQuery();

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

            //LogRequestAndResponse();
        }

        private void BuildQuery()
        {
            RequestMsgSet.AppendTermsQueryRq();
        }
    }
}