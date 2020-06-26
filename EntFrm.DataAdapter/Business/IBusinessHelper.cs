﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EntFrm.DataAdapter.Business
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
        public static string getServiceNoByCounterNo(string counterNo)
        {
            try
            {
                string result = "";

                CounterInfoBLL infoBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo info = infoBoss.GetRecordByNo(counterNo);

                if (info != null && !string.IsNullOrEmpty(info.sServiceGroupValue))
                {
                    string serValue = info.sServiceGroupValue.Split(',')[0];
                    result = serValue.Split(':')[0];
                }

                return result;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getServiceNoByServiceName(string serviceName)
        {
            try
            {
                string result = "";

                int count = 0;
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "ServiceAlias = '" + serviceName + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    return infoColl.GetFirstOne().sServiceNo;
                }
                return result;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool findRUserInfo(string PathId)
        {

            RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            int count = infoBoss.GetCountByCondition("RUserNo='" + PathId + "'");
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        public static string addRUserInfo(string PatId, string PatName, string PatAge, string PatSex, string PatIdNo, string PatPhone, string PatRiNo = "")
        {
            try
            {
                RUsersInfoBLL infoBoss = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                RUsersInfo info = infoBoss.GetRecordByNo(PatId);

                if (info == null)
                {
                    info = new RUsersInfo();

                    info.sRUserNo = PatId;
                    info.sCnName = PatName;
                    info.sEnName = PinYinHelper.GetPinYinCode(PatName).ToLower();
                    info.iAge = IPublicHelper.getChnAge(PatAge);
                    info.iSex = PatSex.Equals("男") ? 1 : 0;
                    info.sNation = "汉";
                    info.iCardType = 1;
                    info.sIdCardNo = PatIdNo;
                    info.sRiCardNo = PatRiNo;
                    info.sAddress = "";
                    info.sPostCode = "";
                    info.sTelphone = PatPhone;
                    info.sHeadPhoto = "";
                    info.sSummary = "";
                    info.sBranchNo = "";
                    info.sAddOptor = "";
                    info.dAddDate = DateTime.Now;
                    info.sModOptor = "";
                    info.dModDate = DateTime.Now;
                    info.iValidityState = 1;
                    info.sComments = "";
                    info.sAppCode = IUserContext.GetAppCode() + ";";

                    infoBoss.AddNewRecord(info);
                }
                else
                {
                    info.sEnName = PinYinHelper.GetPinYinCode(PatName).ToLower();
                    info.sIdCardNo = PatIdNo;
                    info.sRiCardNo = PatRiNo;
                    info.sTelphone = PatPhone;
                    info.dModDate = DateTime.Now;

                    infoBoss.UpdateRecord(info);
                }

                return PatId;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage("更新失败." + ex.Message);
                return "";
            }
        }

        public static bool hasRegistFlowsByRUserNo(string serviceNo, string StafferNo, string ruserNo, string branchNo)
        {
            try
            {
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                //int count = infoBoss.GetCountByCondition(" ( ServiceNo='"+ serviceNo + "' Or StafferNo='"+ StafferNo + "') And  RUserNo='" + ruserNo + "' And BranchNo='" + branchNo + "' And RegistDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                int count = infoBoss.GetCountByCondition(" ServiceNo='" + serviceNo + "' And  StafferNo='" + StafferNo + "' And RUserNo='" + ruserNo + "' And BranchNo='" + branchNo + "' And AddDate Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ");

                return (count > 0 ? true : false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         


        public static string getDoctorNoByDoctorName(string doctorName)
        {
            try
            {
                string result = "";

                int count = 0;
                StafferInfoBLL infoBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, "StafferName = '" + doctorName + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    result = infoColl.GetFirstOne().sStafferNo;

                }
                return result;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
         
        //更新数据显示状态
        public static void UpdateShowState(string branchNo,string serviceNo, string StafferNo, string svalue = "1111111111")
        {
            if (!string.IsNullOrEmpty(serviceNo))
            {
                IUserContext.SetStateValue(branchNo, serviceNo, svalue);
            }
            if (!string.IsNullOrEmpty(StafferNo))
            {
                IUserContext.SetStateValue(branchNo, StafferNo, svalue);
            }
        }
        private static string InsertTicketFlow(string sTicketNo, string sRUserNo, string sBranchNo)
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
         
        private static string getCounterNosByServiceNo(string sServiceNo, string sBranchNo)
        {
            try
            {
                int count = 0;
                string sResult = "";

                if (!string.IsNullOrEmpty(sServiceNo))
                {
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
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static string doGenerateTicketNo( string sServiceNo, string sStafferNo,string sBranchNo)
        {
            int count = 0;
            int serviceNum = 0;
            int staffNum = 0;
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

                if (!string.IsNullOrEmpty(sServiceNo))
                {
                    serviceInfo = IPublicHelper.GetServiceByNo(sServiceNo);
                    serviceNum = vflowBLL.GetCountByCondition(" BranchNo='" + sBranchNo + "' And  ServiceNo='" + sServiceNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ")+1;
                    infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sServiceNo + "' And KeyType='"+ sKeyType + "' ");

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
                else if (!string.IsNullOrEmpty(sStafferNo))
                {
                    staffNum = vflowBLL.GetCountByCondition(" BranchNo='" + sBranchNo + "' And  StafferNo='" + sStafferNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ") + 1;
                    infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " BranchNo='" + sBranchNo + "' And  KeyName='" + sStafferNo + "' And KeyType='" + sKeyType + "' ");

                    if (infoColl != null && infoColl.Count > 0)
                    {
                        info = infoColl.GetFirstOne();
                        DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                        if (info.dModDate < dt)
                        {
                            info.sKeyValue = 0 + "";
                        }

                        count = int.Parse(info.sKeyValue);
                        count++;
                        if (staffNum > count)
                        {
                            count = staffNum;
                        }

                        info.sKeyValue = count.ToString();
                        info.dModDate = DateTime.Now;
                        infoBLL.UpdateRecord(info);

                        sTicketNo = String.Format("{0:D3}", count);

                    }
                    else
                    {
                        info = new SysParams();

                        info.sParamNo = CommonHelper.Get_New12ByteGuid();
                        info.sKeyName = sStafferNo;
                        info.sKeyValue = staffNum.ToString();
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

                        sTicketNo = String.Format("{0:D3}", staffNum);
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


    }
}
