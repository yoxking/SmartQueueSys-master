using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.TicketConsole
{
    public class IPublicHelper
    {
        public static bool IsLoadFinished = false;
        public static int BgAutoStretch = 1;
        public static string BackgroundImage =System.Windows.Forms.Application.StartupPath + "\\AppImages\\BackgroundImage.jpg";
        public static List<ServiceInfo> serviceList = new List<ServiceInfo>();
        public static string RegisteModel = "AutoRegiste";


        public static void Set_ConfigValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        }
        public static void DoAutoUpdate()
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                string updateFile = AppDomain.CurrentDomain.BaseDirectory + "\\AutoUpdate\\EntFrm.AutoUpdate.exe";
                //判断文件的存在
                if (File.Exists(updateFile))
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = updateFile;
                    startInfo.Arguments = "";
                    System.Diagnostics.Process.Start(startInfo);
                }

            }))).Start();
        }

        public static string Get_ServerIp()
        {
            return ConfigurationManager.AppSettings["ServerIp"].ToString();
        }

        public static string Get_ServerPort()
        {
            return ConfigurationManager.AppSettings["ServerPort"].ToString();
        }

        public static string Get_ShutAtMinute()
        {
            return ConfigurationManager.AppSettings["ShutAtMinute"].ToString();
        }

        public static string Get_ShutAtHour()
        {
            return ConfigurationManager.AppSettings["ShutAtHour"].ToString();
        }

        public static string Get_ScreenMode()
        {
            return ConfigurationManager.AppSettings["ScreenMode"].ToString();
        }

        public static string Get_QrcodeMode()
        {
            return ConfigurationManager.AppSettings["PrintQrcode"].ToString();
        }

        public static string Get_QrcodeText()
        {
            return ConfigurationManager.AppSettings["QrcodeText"].ToString();
        }

        public static string Get_WindowWidth()
        {
            return ConfigurationManager.AppSettings["WindowWidth"].ToString();
        }

        public static string Get_WindowHeight()
        {
            return ConfigurationManager.AppSettings["WindowHeight"].ToString();
        }

        public static string Get_VerifCode()
        {
            return ConfigurationManager.AppSettings["VerifCode"].ToString();
        }

        public static string Get_TerminalCode()
        {
            return ConfigurationManager.AppSettings["TerminalCode"].ToString();
        }

        public static string Get_PrinterName()
        {
            return ConfigurationManager.AppSettings["PrinterName"].ToString();
        }

        public static CounterInfo GetCounterByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getCounter", new string[] { sNo });
            CounterInfo info = JsonConvert.DeserializeObject<CounterInfo>(s);

            return info;
        }

        public static string GetServiceNameByNo(string sNo)
        {

            string s = IUserContext.OnExecuteCommand_Xp("getService", new string[] { sNo });
            ServiceInfo info = JsonConvert.DeserializeObject<ServiceInfo>(s);

            if(info!=null)
            {
                return info.sServiceName;
            }

            return "";
        }

        public static string ReplaceVariables(string sFormatStr,string sPFlowNo)
        {
            try
            {
                string nextTicketNo = "";
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");

                ViewTicketFlows vTicketFlow = JsonConvert.DeserializeObject<ViewTicketFlows>(IUserContext.OnExecuteCommand_Xp("getVTicketFlowByPFlowNo", new string[] { sPFlowNo }));
                if (vTicketFlow != null)
                {
                    CounterInfo counter = GetCounterByNo(vTicketFlow.sProcessedCounterNo);
                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                        nextTicketNo = IUserContext.OnExecuteCommand_Xp("getNextTicketNo", new string[] { counter.sCounterNo });
                    } 
                     
                    string waiterNum = IUserContext.OnExecuteCommand_Xp("getVTicketCountByServiceNo", new string[] { vTicketFlow.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), IPublicConsts.PROCSTATE_WAITING + "", IPublicConsts.PROCSTATE_CALLING + "" });
                    string where = "ProcessState="+ IPublicConsts.PROCSTATE_WAITING + " And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                    string allWaiterNum = IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { where });

                    sFormatStr = sFormatStr.Replace("ServiceName", GetServiceNameByNo(vTicketFlow.sServiceNo));
                    sFormatStr = sFormatStr.Replace("TicketNo", vTicketFlow.sTicketNo);
                    sFormatStr = sFormatStr.Replace("ServiceWaiterNumber", waiterNum);
                    sFormatStr = sFormatStr.Replace("AllWaitingNumber", allWaiterNum);
                    sFormatStr = sFormatStr.Replace("NextNo", nextTicketNo); 
                    sFormatStr = sFormatStr.Replace("FullName", vTicketFlow.sCnName);
                    sFormatStr = sFormatStr.Replace("UserSex", (vTicketFlow.iSex==1)?"先生":"女士");
                    sFormatStr = sFormatStr.Replace("IdNumber", vTicketFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("CardNumber", vTicketFlow.sRiCardNo);
                    sFormatStr = sFormatStr.Replace("Telephone", vTicketFlow.sTelphone);
                    sFormatStr = sFormatStr.Replace("Parameters", vTicketFlow.sSummary);
                    sFormatStr = sFormatStr.Replace("yyyy-MM-dd-HH:mm:ss", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("yyyy/MM/dd-HH:mm:ss", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("HH:mm:ss", DateTime.Now.ToString("HH:mm:ss"));
                    sFormatStr = sFormatStr.Replace("hh:mm:ss", DateTime.Now.ToString("hh:mm:ss"));

                }
                return sFormatStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
