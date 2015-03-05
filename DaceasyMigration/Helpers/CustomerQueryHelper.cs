using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DaceasyMigration.Models;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class CustomerQueryHelper : BaseQbHelper
    {
        public CustomerQueryHelper()
        {
            List = new List<QbGenericQueryModel>();
        }

        public Task<IList<QbGenericQueryModel>> GetCustomersAsync()
        {
            return Task.Run(() => GetCustomers());
        }
        public IList<QbGenericQueryModel> GetCustomers()
        {
            Initialize();

            BuildQuery();

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();
            ExtractCustomerList();
            return List;
        }

       

        private void BuildQuery()
        {
            RequestMsgSet.AppendCustomerQueryRq();
        } 
        private void ExtractCustomerList()
        {
            if (ResponseMsgSet == null) return;

            IResponseList responseList = ResponseMsgSet.ResponseList;
            if (responseList == null) return;

            var count = responseList.Count;
            Console.WriteLine(count);
            for (int i = 0; i < count; i++)
            {

                IResponse response = responseList.GetAt(i);
                if (response.StatusCode >= 0)
                {
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtCustomerQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            var retList = (ICustomerRetList)response.Detail;
                            for (int j = 0; j < retList.Count; j++)
                            {
                                WalkRet(retList.GetAt(j));
                            }
                        }
                    }
                }
            }
        }

        private void WalkRet(ICustomerRet customerRet)
        {
            if (customerRet == null)
            {
                return;
            }

            var model = new QbGenericQueryModel
            {
                ListId = customerRet.ListID.GetValue(),
                Name = customerRet.Name.GetValue()
            };
            List.Add(model);
        }

        public IList<QbGenericQueryModel> List { get; set; }
    }
}