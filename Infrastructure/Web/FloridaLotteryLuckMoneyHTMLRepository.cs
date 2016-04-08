//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        FloridaLotteryLuckMoneyHTMLRepository
//Description:  This class serves as the main repository for retrieving Florida Lottery Luck Money
//              history. The class inherits from the base class "Checks" and its interface IFloridaLotteryLuckMoneyHTMLRepository.
//              This class utilizes chaching by retrieving the data in HTML, parsing it, and storing it in cache.

#region Assemblies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Data;
using System.Net.NetworkInformation;
using Business;
using App;
using System.Runtime.Caching;
#endregion

namespace Infrastructure.Web
{
    public class FloridaLotteryLuckMoneyHTMLRepository : Checks, IFloridaLotteryLuckMoneyHTMLRepository
    {
        #region Properties
        private string _uriPath {get; set;}
        private int _HistoryDays { get; set; }
        private string _Host { get; set; }
        public ObjectCache _cache { get; set; }
        #endregion

        #region Methods
        public FloridaLotteryLuckMoneyHTMLRepository(string uriPath, int HistoryDays, string Host)
        {
            _uriPath = uriPath;
            _HistoryDays = HistoryDays;
            _Host = Host;
            _cache = MemoryCache.Default;
        }
        public bool SiteAvailable()
        {
            bool connection = false;
            Ping myPing = new Ping();
            String host = _Host;
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            connection = (reply.Status == IPStatus.Success);
            return connection;
        }
        public List<LuckyMoneyNumbers> GetParsedHistoryResults()
        {
            List<LuckyMoneyNumbers> llmt = new List<LuckyMoneyNumbers>();

            if (_cache.Contains("LMResultsFromFLWebSite", null))
            {
                llmt = (List<LuckyMoneyNumbers>)_cache.Get("LMResultsFromFLWebSite");
            }
            else
            { 
                string Game = "2";
            
                bool bReturn = true;

                int numcntresult = 0;

                numcntresult = 5;

                using (WebClient webClient = new WebClient())
                {
                    using (Stream stream = webClient.OpenRead(_uriPath))
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {

                            StringBuilder sb = new StringBuilder();
                            int iLineCount = 0;
                            string line;
                            int numcnt = 0;

                            while ((line = sr.ReadLine()) != null)
                            {

                                int Start, End;

                                string strStart = "<font size=2 face=\"helvetica\">";
                                string strEnd = "</font>";


                                if (line.Contains(strStart) && line.Contains(strEnd))
                                {
                                    Start = line.IndexOf(strStart, 0) + strStart.Length;
                                    End = line.IndexOf(strEnd, Start);
                                    string retVal = line.Substring(Start, End - Start);

                                    retVal = retVal.Replace("<b>", "").Replace("</b>", "");

                                    if (CheckDate(retVal))
                                    {
                                        sb.Append(retVal + "-");
                                    }
                                    else if (CheckInt(retVal))
                                    {
                                        numcnt += 1;

                                        if (numcnt == numcntresult)
                                        {
                                            sb.Append(retVal + "|");
                                            numcnt = 0;
                                            iLineCount += 1;
                                        }
                                        else
                                        {
                                            sb.Append(retVal + "-");
                                        }
                                    }

                                }
                            }

                            sr.Close();

                            string winningnums = sb.ToString();
                            string[] winnum = winningnums.Split('|');

                            foreach (string tk in winnum)
                            {
                                if (tk.Length > 0)
                                {
                                    LuckyMoneyNumbers lmt = new LuckyMoneyNumbers();
                                    string[] pk = tk.Split('-');
                                    string NumValue = pk[1] + pk[2] + pk[3] + pk[4] + pk[5];

                                    lmt.WinDate = DateTime.Parse(pk[0]);
                                    lmt.Num1 = int.Parse(pk[1]);
                                    lmt.Num2 = int.Parse(pk[2]);
                                    lmt.Num3 = int.Parse(pk[3]);
                                    lmt.Num4 = int.Parse(pk[4]);
                                    lmt.LB = int.Parse(pk[5]);
                                    llmt.Add(lmt);
                                }
                            }

                            SetCache(llmt);

                        }
                    }
                }
            }
                
                return llmt;
        }
        private void SetCache(List<LuckyMoneyNumbers> llmt)
        {

            CacheItemPolicy policy = new CacheItemPolicy();

            _cache.Set("LMResultsFromFLWebSite", llmt, policy);

        }
        #endregion
    }
}