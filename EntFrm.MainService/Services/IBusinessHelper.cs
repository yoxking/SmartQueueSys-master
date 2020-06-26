using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EntFrm.MainService.Services
{
    public class IBusinessHelper
    {
        public static List<T> ToViewList<T>(CollectionBase infoColl)
        {
            if (infoColl != null && infoColl.Count > 0)
            {
                List<T> infoList = new List<T>();
                foreach (T info in infoColl)
                {
                    infoList.Add(info);
                }

                return infoList;
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, int> SortDictionary_Desc(Dictionary<string, int> dic)
        {
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(dic);
            myList.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                return s2.Value.CompareTo(s1.Value);
            });
            dic.Clear();
            foreach (KeyValuePair<string, int> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        public static Dictionary<string, int> SortDictionary_Asc(Dictionary<string, int> dic)
        {
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>(dic);
            myList.Sort(delegate (KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                return s1.Value.CompareTo(s2.Value);
            });
            dic.Clear();
            foreach (KeyValuePair<string, int> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        public static string getCounterNosByServiceNo(string sServiceNo, string sBranchNo)
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

        public static string getNextWorkFlowServiceNo(string sWFlowNo, int WFlowIndex)
        {
            try
            {
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfo info = infoBoss.GetRecordByNo(sWFlowNo);

                string sResult = "";

                if (info != null)
                {
                    string[] serviceList = info.sWorkflowValue.Split(';');
                    if (serviceList.Length > WFlowIndex)
                    {
                        sResult = serviceList[WFlowIndex];
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string EnqueueRegister(RegistFlows regFlow)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "";
                DateTime workDate = DateTime.Now.AddMinutes(30);

                ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                RegistFlowsBLL rflowBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                if (regFlow != null)
                {
                    sTicketNo = GenerateTicketNo(regFlow.sServiceNo, regFlow.sBranchNo);

                    sTFlowNo = InsertTicketFlow(sTicketNo, regFlow.sRUserNo, regFlow.sBranchNo);
                    if (!string.IsNullOrEmpty(sTFlowNo))
                    {
                        /////插入下一个流程 
                        int WFlowsIndex = 0;
                        string sNextServiceNo = IBusinessHelper.getNextWorkFlowServiceNo(regFlow.sServiceNo, WFlowsIndex);
                        if (string.IsNullOrEmpty(sNextServiceNo))
                        {
                            sNextServiceNo = regFlow.sServiceNo;
                        }
                        string sCounterNos = IBusinessHelper.getCounterNosByServiceNo(sNextServiceNo, IUserContext.GetBranchNo());

                        sPFlowNo = InsertProcessFlow(sTFlowNo, sNextServiceNo, sCounterNos, regFlow.sServiceNo, WFlowsIndex, IUserContext.GetBranchNo());
                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            regFlow.iRegistState = 1;
                            regFlow.dModDate = DateTime.Now;

                            if (rflowBLL.UpdateRecord(regFlow))
                            {
                                return sPFlowNo;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public static string EnqueueCalling(string sCounterNo, bool enqueueFlag = true)
        {
            try
            {
                string tempPFlow = "";
                string serviceList = "";
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);

                if (counter != null)
                {
                    string[] serviceGroups = counter.sServiceGroupValue.Split(';');
                    Dictionary<string, int> serviceDict = new Dictionary<string, int>();
                    string[] serviceArray = null;
                    foreach (string service in serviceGroups)
                    {
                        serviceArray = service.Split(':');
                        serviceDict.Add(serviceArray[0], int.Parse(serviceArray[1]));
                    }

                    serviceDict = SortDictionary_Desc(serviceDict);

                    foreach (var item in serviceDict)
                    {
                        serviceList = "";
                        foreach (var temp in serviceDict)
                        {
                            if (item.Value == temp.Value)
                            {
                                serviceList += " '" + temp.Key + "',";
                            }
                        }
                        serviceList = serviceList.Trim(',');
                        tempPFlow = EnqueueNextTicket(serviceList, sCounterNo, enqueueFlag);

                        if (!string.IsNullOrEmpty(tempPFlow))
                        {
                            break;
                        }
                    }
                }

                return tempPFlow;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static string EnqueueNextTicket(string sServiceList, string sCounterNo, bool enqueueFlag)
        {

            try
            {
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;
                string sWhere = " DataFlag=0 And BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessState = " + IPublicConsts.PROCSTATE_WAITING + "  And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "' And  ServiceNo IN (" + sServiceList + ") ";

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ProcessFlows";

                processFlows = processBoss.GetRecordsByPaging(s_model);

                if (processFlows != null && processFlows.Count > 0)
                {
                    processFlow = processFlows.GetFirstOne();

                    if (enqueueFlag)
                    {
                        processFlow.iProcessState = IPublicConsts.PROCSTATE_CALLING;
                        processFlow.sProcessedCounterNo = sCounterNo;
                        processFlow.sProcessedStafferNo = "";
                        processFlow.dProcessedTime = DateTime.Now;
                        processFlow.dModDate = DateTime.Now;

                        if (processBoss.UpdateRecord(processFlow))
                        {
                            return processFlow.sPFlowNo;
                        }
                    }
                    else
                    {
                        return processFlow.sPFlowNo;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertTicketFlow(string sTicketNo, string sRUserNo, string sBranchNo)
        {
            try
            {
                TicketFlowsBLL flowBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                string sTFlowNo = CommonHelper.Get_New12ByteGuid();

                TicketFlows flowInfo = new TicketFlows();

                flowInfo.sTFlowNo = sTFlowNo;
                flowInfo.iDataFlag = 0;
                flowInfo.sTicketNo = sTicketNo;
                flowInfo.sRUserNo = sRUserNo;

                flowInfo.sBranchNo = sBranchNo;
                flowInfo.sComments = "";
                flowInfo.sAddOptor = "00000000";
                flowInfo.dAddDate = DateTime.Now;
                flowInfo.sModOptor = "00000000";
                flowInfo.dModDate = DateTime.Now;
                flowInfo.iValidityState = 1;
                flowInfo.sAppCode = IUserContext.GetAppCode() + ";";

                if (flowBoss.AddNewRecord(flowInfo))
                {
                    return sTFlowNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertProcessFlow(string sTFlowNo, string sServiceNo, string sCounterNos, string sWFlowNo, int iWFlowIndex, string sBranchNo)
        {
            try
            {
                ProcessFlowsBLL flowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ProcessFlows flowInfo = new ProcessFlows();

                string sPFlowNo = CommonHelper.Get_New12ByteGuid();

                flowInfo.sPFlowNo = sPFlowNo;
                flowInfo.sTFlowNo = sTFlowNo;
                flowInfo.iDataFlag = 0;
                flowInfo.sServiceNo = sServiceNo;
                flowInfo.sCounterNos = sCounterNos;
                flowInfo.sWFlowsNo = sWFlowNo;
                flowInfo.iWFlowsIndex = iWFlowIndex;
                flowInfo.dEnqueueTime = DateTime.Now;
                flowInfo.dBeginTime = DateTime.Now;
                flowInfo.dFinishTime = DateTime.Now;
                flowInfo.iProcessState = IPublicConsts.PROCSTATE_WAITING;
                flowInfo.sProcessFormat = "";
                flowInfo.iProcessIndex = 0;
                flowInfo.iPriorityType = IPublicConsts.PRIORITY_TYPE0;
                flowInfo.iOrderWeight = 0;
                flowInfo.iPauseState = 0;
                flowInfo.iDelayType = 0;
                flowInfo.iDelayTimeValue = 0;
                flowInfo.iDelayStepValue = 0;
                flowInfo.dProcessedTime = DateTime.Now;
                flowInfo.sProcessedCounterNo = "";
                flowInfo.sProcessedStafferNo = "";

                flowInfo.sComments = "";
                flowInfo.sBranchNo = sBranchNo;
                flowInfo.sAddOptor = "00000000";
                flowInfo.dAddDate = DateTime.Now;
                flowInfo.sModOptor = "00000000";
                flowInfo.dModDate = DateTime.Now;
                flowInfo.iValidityState = 1;
                flowInfo.sAppCode = IUserContext.GetAppCode() + ";";

                if (flowBoss.AddNewRecord(flowInfo))
                {
                    return sPFlowNo;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string InsertRUserInfo(string sUName, string sIdCardNo, string sTelphone, string PatRiNo = "")
        {
            try
            {
                int count = 0;

                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RUsersInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + sIdCardNo + "' ");
                RUsersInfo info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    info.sCnName = sUName;
                    info.sTelphone = sTelphone;

                    info.dModDate = DateTime.Now;

                    if (infoBoss.UpdateRecord(info))
                    {
                        return info.sRUserNo;
                    }
                }
                else
                {
                    string sRUserNo = CommonHelper.Get_New12ByteGuid();
                    info = new RUsersInfo();

                    info.sRUserNo = sRUserNo;
                    info.sCnName = sUName;
                    info.sEnName = "";
                    info.iAge = 0;
                    info.iSex = 1;
                    info.sNation = "汉";
                    info.iCardType = 1;
                    info.sIdCardNo = sIdCardNo;
                    info.sRiCardNo = PatRiNo;
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = sTelphone;
                    info.sHeadPhoto = "";
                    info.sSummary = "";

                    info.sBranchNo = IUserContext.GetBranchNo();
                    info.sComments = "";
                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    if (infoBoss.AddNewRecord(info))
                    {
                        return sRUserNo;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string InsertRUserInfo(string sRUserData)
        {
            try
            {
                int count = 0;

                RUserData ruserInfo = JsonConvert.DeserializeObject<RUserData>(sRUserData);

                if (ruserInfo != null && (!string.IsNullOrEmpty(ruserInfo.UserName)))
                {

                    RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RUsersInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + ruserInfo.IdcardNo + "' ");
                    RUsersInfo info = null;

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();

                        info.sCnName = ruserInfo.UserName;
                        info.sTelphone = ruserInfo.Telephone;

                        info.dModDate = DateTime.Now;

                        if (infoBoss.UpdateRecord(info))
                        {
                            return info.sRUserNo;
                        }
                    }
                    else
                    {
                        string sRUserNo = CommonHelper.Get_New12ByteGuid();
                        info = new RUsersInfo();

                        info.sRUserNo = sRUserNo;
                        info.sCnName = ruserInfo.UserName;
                        info.sEnName = "";
                        info.iAge = 0;
                        info.iSex = 1;
                        info.sNation = "汉";
                        info.iCardType = 1;
                        info.sIdCardNo = ruserInfo.IdcardNo;
                        info.sRiCardNo = ruserInfo.RicardNo;
                        info.sAddress = "";
                        info.sPostCode = "";
                        info.sTelphone = ruserInfo.Telephone;
                        info.sHeadPhoto = "";
                        info.sSummary = "";

                        info.sBranchNo = IUserContext.GetBranchNo();
                        info.sComments = "";
                        info.sAddOptor = "00000000";
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = "00000000";
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sAppCode = IUserContext.GetAppCode() + ";";

                        if (infoBoss.AddNewRecord(info))
                        {
                            return sRUserNo;
                        }
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GenerateTicketNo(string sServiceNo, string sBranchNo)
        {
            int count = 0;
            int serviceNum = 0;
            string sTicketNo = "001";
            string sKeyType = "Ticket";
            DateTime workDate = DateTime.Now;
            ServiceInfo serviceInfo = null;
            SysParams info = null;
            SysParamsCollections infoColl = null;

            try
            {
                SysParamsBLL infoBLL = new SysParamsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ViewTicketFlowsBLL vflowBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                serviceInfo = IPublicHelper.GetServiceByNo(sServiceNo);
                if (serviceInfo != null)
                {
                    serviceNum = vflowBLL.GetCountByCondition(" BranchNo='" + sBranchNo + "' And  ServiceNo='" + sServiceNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ") + 1;
                    infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sServiceNo + "' And KeyType='" + sKeyType + "' ");

                    if (serviceInfo != null && infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();
                        DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                        if (info.dModDate < dt)
                        {
                            info.sKeyValue = 0 + "";
                        }

                        count = int.Parse(info.sKeyValue);
                        count++;
                        if (serviceNum > count)
                        {
                            count = serviceNum;
                        }

                        info.sKeyValue = count.ToString();
                        info.dModDate = DateTime.Now;
                        infoBLL.UpdateRecord(info);

                        sTicketNo = serviceInfo.sServiceType + String.Format("{0:D" + serviceInfo.iDigitLength + "}", count);
                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sServiceNo;
                        info.sKeyValue = serviceNum.ToString();
                        info.sKeyType = sKeyType;
                        info.sBranchNo = sBranchNo;

                        info.sAddOptor = "";
                        info.dAddDate = DateTime.Now;
                        info.sModOptor = "";
                        info.dModDate = DateTime.Now;
                        info.iValidityState = 1;
                        info.sComments = "";
                        info.sAppCode = IUserContext.GetAppCode() + ";";

                        infoBLL.AddNewRecord(info);

                        if (serviceInfo != null)
                        {
                            sTicketNo = serviceInfo.sServiceType + String.Format("{0:D" + serviceInfo.iDigitLength + "}", serviceNum);
                        }
                        else
                        {
                            sTicketNo = String.Format("{0:D" + serviceInfo.iDigitLength + "}", serviceNum);
                        }
                    }
                }

                else
                {
                    //默认生成票号
                    ViewTicketFlowsBLL vticketBLL = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                    string sWhere = " BranchNo='" + sBranchNo + "' And EnqueueTime  Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    int countNum = vticketBLL.GetCountByCondition(sWhere) + 1;
                    sTicketNo = String.Format("{0:D3}", countNum);
                }
            }
            catch (Exception ex)
            {
            }
            return sTicketNo;
        }

        public static ViewTicketFlows getVTicketFlowBy(string sCounterNo, string sWorkDate, string sProcessState, string sOrderType = "Asc")
        {
            try
            {
                DateTime workDate = DateTime.Parse(sWorkDate);
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                string sWhere = " ProcessedCounterNo='" + sCounterNo + "' And ProcessState In (" + sProcessState + ") And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = "ID ";
                s_model.sOrderType = sOrderType;
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                if (infoColl != null)
                {
                    return infoColl.GetFirstOne();
                }
            }
            catch (Exception ex) { }
            return null;

        }

        public static string InsertRUserInfo(string sRUserNo, string sRUserName, string sRUserSex, string sTelephone, string sIdNo, string sRemark)
        {
            try
            {
                int count = 0;

                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RUsersInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, " IdCardNo='" + sIdNo + "' ");
                RUsersInfo info = null;

                if (infoColl != null && infoColl.Count > 0)
                {
                    info = infoColl.GetFirstOne();

                    info.sCnName = sRUserName;
                    info.iSex = sRUserSex.Equals("男") ? 1 : 0;
                    info.sTelphone = sTelephone;
                    info.sSummary = sRemark;

                    info.dModDate = DateTime.Now;

                    if (infoBoss.UpdateRecord(info))
                    {
                        return info.sRUserNo;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(sRUserNo))
                    {
                        sRUserNo = CommonHelper.Get_New12ByteGuid();
                    }
                    info = new RUsersInfo();

                    info.sRUserNo = sRUserNo;
                    info.sCnName = sRUserName;
                    info.sEnName = "";
                    info.iAge = 0;
                    info.iSex = sRUserSex.Equals("男") ? 1 : 0;
                    info.sNation = "汉";
                    info.iCardType = 1;
                    info.sIdCardNo = sIdNo;
                    info.sRiCardNo = "";
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = sTelephone;
                    info.sHeadPhoto = "";
                    info.sSummary = sRemark;

                    info.sBranchNo = "";
                    info.sComments = "";
                    info.sAddOptor = "00000000";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "00000000";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sAppCode =IUserContext.GetAppCode() + ";";

                    if (infoBoss.AddNewRecord(info))
                    {
                        return sRUserNo;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}