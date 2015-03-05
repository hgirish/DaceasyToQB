using System;
using DaceasyMigration.Models;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class SalesRepAddHelper : BaseQbHelper
    {
        public void DoAdd(SalesRepAddModel model)
        {
            var listId = GetListId(model.FullName);
            if (string.IsNullOrWhiteSpace(listId))
            {
                throw new Exception("Could not add other name : " + model.FullName);
            }
            model.ListId = listId;
            Initialize();

            BuildQuery(model);

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();
            LogRequestAndResponse();

        }

        private string GetListId(string fullName)
        {
            Initialize();

            BuildOtherNameAddQuery(fullName);

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);
           var listId = WalkOtherNameAddRs();
            return listId;
        }

        private string WalkOtherNameAddRs()
        {
            if (ResponseMsgSet == null) return null;

            IResponseList responseList = ResponseMsgSet.ResponseList;
            if (responseList == null) return null;

            //if we sent only one request, there is only one response, we'll walk the list for this sample
            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                //check the status code of the response, 0=ok, >0 is warning
                if (response.StatusCode >= 0)
                {
                    //the request-specific response is in the details, make sure we have some
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtOtherNameAddRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            IOtherNameRet otherNameRet = (IOtherNameRet)response.Detail;
                          var listId =  WalkOtherNameRet(otherNameRet);
                            return listId;
                        }

                    }

                }

            }
            return null;


        }

        private string WalkOtherNameRet(IOtherNameRet otherNameRet)
        {
            if (otherNameRet == null) return null;

            //Go through all the elements of IOtherNameRet
            //Get value of ListID
            string listId = (string)otherNameRet.ListID.GetValue();
            return listId;

        }

        private void BuildOtherNameAddQuery(string fullName)
        {
            IOtherNameAdd otherNameAddRq = RequestMsgSet.AppendOtherNameAddRq();

            otherNameAddRq.Name.SetValue(fullName);
        }

        private void BuildQuery(SalesRepAddModel model)
        {
            

            ISalesRepAdd salesRepAdd =
                RequestMsgSet.AppendSalesRepAddRq();
            salesRepAdd.Initial.SetValue(model.Initial);
            salesRepAdd.IsActive.SetValue(model.IsActive);
           
           salesRepAdd.SalesRepEntityRef.ListID.SetValue(model.ListId);
        }
    }
}