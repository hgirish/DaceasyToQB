using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using NLog;
using QBFC13Lib;

namespace DaceasyMigration
{
    public class BaseQbHelper
    {
        protected   Logger Log { get; private set; }

        public BaseQbHelper()
        {
            Log = LogManager.GetLogger(GetType().FullName);
        }
       protected  bool SessionBegun;
       protected  bool ConnectionOpen;
       protected  QBSessionManager SessionManager;
        protected  IMsgSetRequest RequestMsgSet;
        protected IMsgSetResponse ResponseMsgSet = null;
        public IMsgSetRequest GetLatestMsgSetRequest(QBSessionManager sessionManager)
        {
            // Find and adapt to supported version of QuickBooks
            double supportedVersion = QbfcLatestVersion(sessionManager);

            short qbXmlMajorVer;
            short qbXmlMinorVer;

            if (supportedVersion >= 6.0)
            {
                qbXmlMajorVer = 6;
                qbXmlMinorVer = 0;
            }
            else if (supportedVersion >= 5.0)
            {
                qbXmlMajorVer = 5;
                qbXmlMinorVer = 0;
            }
            else if (supportedVersion >= 4.0)
            {
                qbXmlMajorVer = 4;
                qbXmlMinorVer = 0;
            }
            else if (supportedVersion >= 3.0)
            {
                qbXmlMajorVer = 3;
                qbXmlMinorVer = 0;
            }
            else if (supportedVersion >= 2.0)
            {
                qbXmlMajorVer = 2;
                qbXmlMinorVer = 0;
            }
            else if (supportedVersion >= 1.1)
            {
                qbXmlMajorVer = 1;
                qbXmlMinorVer = 1;
            }
            else
            {
                qbXmlMajorVer = 1;
                qbXmlMinorVer = 0;
                Console.WriteLine("It seems that you are running QuickBooks 2002 Release 1. We strongly recommend that you use QuickBooks' online update feature to obtain the latest fixes and enhancements");
            }

            // Create the message set request object
            IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", qbXmlMajorVer, qbXmlMinorVer);
            return requestMsgSet;
        }

        protected  async Task SaveXml(string xmlstring)
        {
              using (StreamWriter writer = File.AppendText("xmlfile.txt"))
                {
                   await writer.WriteLineAsync(xmlstring);
                   
                }
        }

        private double QbfcLatestVersion(QBSessionManager sessionManager)
        {
            // Use oldest version to ensure that this application work with any QuickBooks (US)
            IMsgSetRequest msgset = sessionManager.CreateMsgSetRequest("US", 1, 0);
            msgset.AppendHostQueryRq();
            IMsgSetResponse queryResponse = sessionManager.DoRequests(msgset);
            //Console.WriteLine("Host query = " + msgset.ToXMLString());
            //SaveXML(msgset.ToXMLString());


            // The response list contains only one response,
            // which corresponds to our single HostQuery request
            IResponse response = queryResponse.ResponseList.GetAt(0);

            // Please refer to QBFC Developers Guide for details on why 
            // "as" clause was used to link this derrived class to its base class
            IHostRet hostResponse = response.Detail as IHostRet;
            Debug.Assert(hostResponse != null, "hostResponse != null");
            IBSTRList supportedVersions = hostResponse.SupportedQBXMLVersionList;

            int i;
            double lastVers = 0;

            for (i = 0; i <= supportedVersions.Count - 1; i++)
            {
                var svers = supportedVersions.GetAt(i);
                var vers = Convert.ToDouble(svers);
                if (vers > lastVers)
                {
                    lastVers = vers;
                }
            }
            return lastVers;
        }

        protected async void LogRequestAndResponse()
        {
            var requestString = RequestMsgSet.ToXMLString();
            Console.WriteLine(requestString);
            
             Log.Info(requestString);
          //  await SaveXml(requestString);
            var responseString = ResponseMsgSet.ToXMLString();
            Console.WriteLine(responseString);
            Log.Info(responseString);
           //await  SaveXml(responseString);   
        }
        protected  void Initialize()
        {
            SessionManager = new QBSessionManager();
            //Connect to QuickBooks and begin a session
            SessionManager.OpenConnection("", "Sample Code from OSR");
            ConnectionOpen = true;
            SessionManager.BeginSession("", ENOpenMode.omDontCare);
            SessionBegun = true;

            RequestMsgSet = SessionManager.CreateMsgSetRequest("US", 13, 0);
            RequestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }

        public void CleanUp()
        {
            if (SessionManager == null)
            {
                SessionManager = new QBSessionManager();
            }
            SessionManager.EndSession();
            SessionBegun = false;
            SessionManager.CloseConnection();
            ConnectionOpen = false;
        }
    }
}