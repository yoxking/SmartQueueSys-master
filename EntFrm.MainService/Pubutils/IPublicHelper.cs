using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EntFrm.MainService
{
    public class IPublicHelper
    { 
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

        public static string GetServiceNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
            return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static ServiceInfo GetServiceByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static string GetServiceNoByAlias(string sAlias)
        {
            if (sAlias != null && sAlias.Length > 0)
            {
                int count = 0;
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceAlias='" + sAlias + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sServiceNo;
                }
            }
            return "";
        }

        public static string GetCounterNameByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }

        public static CounterInfo GetCounterByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static VoiceInfo GetVoiceInfoByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                VoiceInfoBLL infoBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CounterInfo GetCounterByStafferNo(string sStafferNo)
        {
            if (sStafferNo != null && sStafferNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl[0];
                }
            }
            return null;
        }

        public static string GetCounterNosByServiceNo(string sServiceNo, string sBranchNo)
        {
            try
            {
                int count = 0;
                string sResult = "";
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 10, " BranchNo='" + sBranchNo + "' And  ServiceGroupValue Like '%" + sServiceNo + "%' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sResult += info.sCounterNo + ";";
                    }

                    sResult.Trim(';');
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static CounterInfo GetCounterByCallerAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count=0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1,1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if(callerColl!=null&&callerColl.Count>0)
                {
                    CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And CallerNo='" + callerColl[0].sCallerNo+"'");
                
                    if(counterColl!=null&&counterColl.Count>0)
                    {
                        return counterColl[0];
                    }
                }

            }
            return null;
        }

        public static CounterInfo GetCounterByCallerNo(string sCallerNo)
        {
            if (!string.IsNullOrEmpty(sCallerNo))
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And CallerNo='" + sCallerNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl.GetFirstOne();
                }
            }
            return null;
        }

        public static CounterInfo GetCounterByDisplayNo(string sLedNo)
        {
            if (sLedNo != null && sLedNo.Length > 0)
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterColl = counterBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And LedDisplayNo='" + sLedNo + "'");

                if (counterColl != null && counterColl.Count > 0)
                {
                    return counterColl[0];
                }
            }
            return null;
        }

        public static string getCounterGroupByServiceNo(string sServiceNo)
        {
            StringBuilder sb = new StringBuilder();
            if (sServiceNo != null && sServiceNo.Length > 0)
            {
                int count = 0;

                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1000, " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceGroupValue Like '%" + sServiceNo + ":%'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (CounterInfo info in infoColl)
                    {
                        sb.Append(info.sCounterNo + ";");
                    }
                }
            }
            return sb.ToString();
        }


        public static string GetStafferNameById(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            { 
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return   infoBoss.GetRecordNameByNo(sNo); 
            }
            return "";
        }


        public static string GetRUserNameById(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordNameByNo(sNo);
            }
            return "";
        }


        public static StafferInfo GetStaffByLoginId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "  BranchNo = '" + IUserContext.GetBranchNo() + "' And LoginId='" + sNo + "' ");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne();
                }
            }
            return null;
        }

        public static CallerInfo GetCallerByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CallerInfoBLL infoBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static CallerInfo GetCallerByPhyAddr(string sSerialPort, string sPhyAddr)
        {
            if (sSerialPort != null && sSerialPort.Length > 0)
            {
                int count = 0;
                CallerInfoBLL callerBoss = new CallerInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CallerInfoCollections callerColl = callerBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And SerialPort='" + sSerialPort + "' And PhyAddr=" + sPhyAddr);

                if (callerColl != null && callerColl.Count > 0)
                {
                    return callerColl.GetFirstOne();
                }

            }
            return null;
        }

        public static EvaluatorInfo GetEvaluatorByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                EvaluatorInfoBLL infoBoss = new EvaluatorInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static LEDDisplay GetLEDDisplayByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                LEDDisplayBLL infoBoss = new LEDDisplayBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static ProcessFlows GetProcessFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ProcessFlowsBLL infoBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }


        public static TicketFlows GetTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                TicketFlowsBLL infoBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        public static ViewTicketFlows GetVTicketFlowByNo(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetRecordByNo(sNo);
            }
            return null;
        }

        private static string GetStaffIdByCounterId(string sNo)
        {
            if (sNo != null && sNo.Length > 0)
            {
                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo info = infoBoss.GetRecordByNo(sNo);

                if (info != null)
                {
                    return info.sLogonStafferNo;
                }
            }
            return "";
        }

        public  static RegistFlows GetRegFlowByRuserIdNo(string sIdNo)
        {
            RegistFlows regFlow = null;
            if (sIdNo != null && sIdNo.Length > 0)
            {
                int count = 0;
                string where= "DataFlag=0 And RegistState=0 And  IdCardNo='"+ sIdNo + "' And RegistDate Between '"+DateTime.Now.ToString("yyyy-MM-hh 00:00:00")+"' And '"+ DateTime.Now.AddDays(1).ToString("yyyy-MM-hh 00:00:00") + "' ";
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count,1,1, where);

                if (infoColl != null&& infoColl.Count>0)
                {
                    regFlow = new RegistFlows();

                    regFlow.iID = infoColl.GetFirstOne().iID;
                    regFlow.sRFlowNo = infoColl.GetFirstOne().sRFlowNo;
                    regFlow.iDataFlag = infoColl.GetFirstOne().iDataFlag;
                    regFlow.sRUserNo = infoColl.GetFirstOne().sRUserNo;
                    regFlow.iRegistType = infoColl.GetFirstOne().iRegistType;
                    regFlow.sDataFrom = infoColl.GetFirstOne().sDataFrom;
                    regFlow.dRegistDate = infoColl.GetFirstOne().dRegistDate;
                    regFlow.sServiceNo = infoColl.GetFirstOne().sServiceNo;
                    regFlow.sCounterNo = infoColl.GetFirstOne().sCounterNo;
                    regFlow.iWorkTime = infoColl.GetFirstOne().iWorkTime;
                    regFlow.dStartDate = infoColl.GetFirstOne().dStartDate;
                    regFlow.dEnditDate = infoColl.GetFirstOne().dEnditDate;
                    regFlow.iRegistState = infoColl.GetFirstOne().iRegistState;
                    regFlow.sBranchNo = infoColl.GetFirstOne().sBranchNo;
                    regFlow.sAddOptor = infoColl.GetFirstOne().sAddOptor;
                    regFlow.dAddDate = infoColl.GetFirstOne().dAddDate;
                    regFlow.sModOptor = infoColl.GetFirstOne().sModOptor;
                    regFlow.dModDate = infoColl.GetFirstOne().dModDate;
                    regFlow.iValidityState = infoColl.GetFirstOne().iValidityState;
                    regFlow.sComments = infoColl.GetFirstOne().sComments;
                    regFlow.sAppCode = infoColl.GetFirstOne().sAppCode;
                    regFlow.sVersion = infoColl.GetFirstOne().sVersion;
                }
            }
            return regFlow;
        }


        public static int getProcessingCountByServiceNo(string branchNo, string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int getProcessingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            {
                string StafferNo = GetStaffIdByCounterId(counterNo);
                return getProcessingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int getProcessingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByServiceNo(string branchNo, string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            {
                string StafferNo = GetStaffIdByCounterId(counterNo);
                return getWaitingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getWaitingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByServiceNo(string branchNo, string serviceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And ServiceNo='" + serviceNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_WAITING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByCounterNo(string branchNo, string counterNo)
        {
            try
            {
                string StafferNo = GetStaffIdByCounterId(counterNo);
                return getQueuingCountByStafferNo(branchNo, StafferNo);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getQueuingCountByStafferNo(string branchNo, string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " BranchNo = '" + branchNo + "' And StafferNo='" + StafferNo + "' And ProcessState Between " + IPublicConsts.PROCSTATE_OUTQUEUE + " And " + IPublicConsts.PROCSTATE_WAITING + " And  EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int getAllCountByStafferNo(string StafferNo)
        {
            try
            {
                DateTime workDate = DateTime.Now;
                string sWhere = " StafferNo='" + StafferNo + "' And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 

                return infoBoss.GetCountByCondition(sWhere);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static string ReplaceVariables(string sFormatStr, string sPFlowNo)
        {
            try
            {
                sFormatStr = sFormatStr.Replace("[", "");
                sFormatStr = sFormatStr.Replace("]", "");

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ViewTicketFlows vTicketFlow = infoBoss.GetRecordByNo(sPFlowNo);

                if(vTicketFlow!=null)
                {
                    CounterInfo counter = GetCounterByNo(vTicketFlow.sProcessedCounterNo);

                    if (counter != null)
                    {
                        sFormatStr = sFormatStr.Replace("CounterName", counter.sCounterName);
                        sFormatStr = sFormatStr.Replace("CounterAlias", counter.sCounterAlias);
                    }

                    //string waiterNum = OpenTicketBLL.getVTicketCountByServiceNo(vTicketFlow.sServiceNo, DateTime.Now.ToString("yyyy-MM-dd"), "0");
                    //string where = "ProcessStatus=0 And ComeTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                    //string allWaiterNum = OpenTicketBLL.getVTicketCountByCondition(where);
                    string waiterNum = "";
                    string where = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessStatus=0 And ComeTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                    string allWaiterNum ="";


                    sFormatStr = sFormatStr.Replace("ServiceName", GetServiceNameByNo(vTicketFlow.sServiceNo));
                    sFormatStr = sFormatStr.Replace("TicketNo", vTicketFlow.sTicketNo);
                    sFormatStr = sFormatStr.Replace("ServiceWaiterNumber", waiterNum);
                    sFormatStr = sFormatStr.Replace("AllWaitingNumber", allWaiterNum);
                    sFormatStr = sFormatStr.Replace("FullName", vTicketFlow.sCnName);
                    sFormatStr = sFormatStr.Replace("IdNumber", vTicketFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("CardNumber", vTicketFlow.sIdCardNo);
                    sFormatStr = sFormatStr.Replace("Telephone", vTicketFlow.sTelphone);
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
