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

namespace FloridaLM
{
    public class ServiceAccess : Utilities
    {
       
        public bool SiteAvailable()
        {
            bool connection = false;
            Ping myPing = new Ping();
            String host = "www.floridalottery.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            connection = (reply.Status == IPStatus.Success);
            return connection;
        }

        
        public bool ParseHistoryResults()
        {

            string Game = "2";
            
            bool bReturn = true;

            int HistoryDays = 0;

            string uriPath = string.Empty;

            int numcntresult = 0;

            uriPath = System.Configuration.ConfigurationManager.AppSettings["LuckyMoneyHistoryURI"];
            HistoryDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LuckyMoneyHistoryDays"]);
            numcntresult = 5;


                DataAccess da = new DataAccess();

                using (WebClient webClient = new WebClient())
                {
                    using (Stream stream = webClient.OpenRead(uriPath))
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

                            da.CUDHistory(1, 0, 0, 0, 0, 0, DateTime.Now);
                            List<LMNumbers> llmt = new List<LMNumbers>();

                            foreach (string tk in winnum)
                            {
                                if (tk.Length > 0)
                                {
                                    LMNumbers lmt = new LMNumbers();
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

                            var lmticks = llmt.OrderByDescending(x => x.WinDate).ToList().Take(HistoryDays);

                            foreach (LMNumbers flmt in lmticks)
                            {
                                da.CUDHistory(3, flmt.Num1, flmt.Num2, flmt.Num3, flmt.Num4, flmt.LB, flmt.WinDate);
                            }
 
                        }
                    }
                }

            return bReturn;
        }

    }
}