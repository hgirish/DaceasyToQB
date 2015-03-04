using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DaceasyMigration.Models;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class VendorsQueryHelper : BaseQbHelper
    {
        private IList<VendorQueryModel>  list = new List<VendorQueryModel>();
        public Task<IList<VendorQueryModel>> GetQbVendors()
        {
           return  Task.Run(() =>
            {
                Initialize();

                BuildQuery();

                ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

                CleanUp();

               // LogRequestAndResponse();
                ExtractVendorList();
                return list;
            });

        }

        private void ExtractVendorList()
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
                        if (responseType == ENResponseType.rtVendorQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            var retList = (IVendorRetList)response.Detail;
                            for (int j = 0; j < retList.Count; j++)
                            {
                                WalkRet(retList.GetAt(j));
                            }
                        }
                    }
                }
            }
        }

        private void WalkRet(IVendorRet vendorRet)
        {
            if (vendorRet == null)
            {
                return;
            }
            
            var model = new VendorQueryModel
            {
                ListId = vendorRet.ListID.GetValue(), 
                Name = vendorRet.Name.GetValue()
            };
            list.Add(model);
        }

        private void BuildQuery()
        {
            RequestMsgSet.AppendVendorQueryRq();
        }


    }
}