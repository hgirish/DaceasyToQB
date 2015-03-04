using System;
using System.Collections.Generic;
using QBFC13Lib;

namespace DaceasyMigration.Helpers
{
    public class AccountListHelper : BaseQbHelper
    {
        public IList<AccountDetail> AccountDetails = new List<AccountDetail>();
        public IList<AccountDetail> GetList()
        {
            Initialize();

            BuildQuery();

            ResponseMsgSet = SessionManager.DoRequests(RequestMsgSet);

            CleanUp();

           // LogRequestAndResponse();
            WalkAccountQueryRs();
            return AccountDetails;
        }

        private void BuildQuery()
        {
            RequestMsgSet.AppendAccountQueryRq();
        }
        void WalkAccountQueryRs()
        {
            if (ResponseMsgSet == null) return;

            IResponseList responseList = ResponseMsgSet.ResponseList;
            if (responseList == null) return;

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
                        if (responseType == ENResponseType.rtAccountQueryRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            IAccountRetList AccountRet = (IAccountRetList)response.Detail;
                            WalkAccountRet(AccountRet);
                        }
                    }
                }
            }
        }



        void WalkAccountRet(IAccountRetList AccountRetList)
        {
            if (AccountRetList == null) return;
            AccountDetail detail = new AccountDetail();
            var AccountRet = AccountRetList.GetAt(0);
            //Go through all the elements of IAccountRetList
            //Get value of ListID
            string ListID31 = (string)AccountRet.ListID.GetValue();
            detail.ListId = ListID31;

            //Get value of TimeCreated
            DateTime TimeCreated32 = (DateTime)AccountRet.TimeCreated.GetValue();
            //Get value of TimeModified
            DateTime TimeModified33 = (DateTime)AccountRet.TimeModified.GetValue();
            //Get value of EditSequence
            string EditSequence34 = (string)AccountRet.EditSequence.GetValue();
            //Get value of Name
            string Name35 = (string)AccountRet.Name.GetValue();
            detail.Name = Name35;
            //Get value of FullName
            string FullName36 = (string)AccountRet.FullName.GetValue();
            detail.FullName = FullName36;
            //Get value of IsActive
            if (AccountRet.IsActive != null)
            {
                bool IsActive37 = (bool)AccountRet.IsActive.GetValue();
            }
            if (AccountRet.ParentRef != null)
            {
                //Get value of ListID
                if (AccountRet.ParentRef.ListID != null)
                {
                    string ListID38 = (string)AccountRet.ParentRef.ListID.GetValue();
                    detail.ParentListId = ListID38;
                }
                //Get value of FullName
                if (AccountRet.ParentRef.FullName != null)
                {
                    string FullName39 = (string)AccountRet.ParentRef.FullName.GetValue();
                    detail.ParentName = FullName39;
                }
            }
            //Get value of Sublevel
            int Sublevel40 = (int)AccountRet.Sublevel.GetValue();
            detail.SubLevel = Sublevel40;
            //Get value of AccountType
            ENAccountType AccountType41 = (ENAccountType)AccountRet.AccountType.GetValue();
            //Get value of SpecialAccountType
            if (AccountRet.SpecialAccountType != null)
            {
                ENSpecialAccountType SpecialAccountType42 = (ENSpecialAccountType)AccountRet.SpecialAccountType.GetValue();
            }
            //Get value of AccountNumber
            if (AccountRet.AccountNumber != null)
            {
                string AccountNumber43 = (string)AccountRet.AccountNumber.GetValue();
            }
            //Get value of BankNumber
            if (AccountRet.BankNumber != null)
            {
                string BankNumber44 = (string)AccountRet.BankNumber.GetValue();
            }
            //Get value of Desc
            if (AccountRet.Desc != null)
            {
                string Desc45 = (string)AccountRet.Desc.GetValue();
            }
            //Get value of Balance
            if (AccountRet.Balance != null)
            {
                double Balance46 = (double)AccountRet.Balance.GetValue();
            }
            //Get value of TotalBalance
            if (AccountRet.TotalBalance != null)
            {
                double TotalBalance47 = (double)AccountRet.TotalBalance.GetValue();
            }
            if (AccountRet.TaxLineInfoRet != null)
            {
                //Get value of TaxLineID
                int TaxLineID48 = (int)AccountRet.TaxLineInfoRet.TaxLineID.GetValue();
                //Get value of TaxLineName
                if (AccountRet.TaxLineInfoRet.TaxLineName != null)
                {
                    string TaxLineName49 = (string)AccountRet.TaxLineInfoRet.TaxLineName.GetValue();
                }
            }
            //Get value of CashFlowClassification
            if (AccountRet.CashFlowClassification != null)
            {
                ENCashFlowClassification CashFlowClassification50 = (ENCashFlowClassification)AccountRet.CashFlowClassification.GetValue();
            }
            if (AccountRet.CurrencyRef != null)
            {
                //Get value of ListID
                if (AccountRet.CurrencyRef.ListID != null)
                {
                    string ListID51 = (string)AccountRet.CurrencyRef.ListID.GetValue();
                }
                //Get value of FullName
                if (AccountRet.CurrencyRef.FullName != null)
                {
                    string FullName52 = (string)AccountRet.CurrencyRef.FullName.GetValue();
                }
            }
            if (AccountRet.DataExtRetList != null)
            {
                for (int i53 = 0; i53 < AccountRet.DataExtRetList.Count; i53++)
                {
                    IDataExtRet DataExtRet = AccountRet.DataExtRetList.GetAt(i53);
                    //Get value of OwnerID
                    if (DataExtRet.OwnerID != null)
                    {
                        string OwnerID54 = (string)DataExtRet.OwnerID.GetValue();
                    }
                    //Get value of DataExtName
                    string DataExtName55 = (string)DataExtRet.DataExtName.GetValue();
                    //Get value of DataExtType
                    ENDataExtType DataExtType56 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                    //Get value of DataExtValue
                    string DataExtValue57 = (string)DataExtRet.DataExtValue.GetValue();
                }
            }
            AccountDetails.Add(detail);
        }
    }
}