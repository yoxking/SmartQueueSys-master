﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;

namespace EntFrm.MainService.Business
{
    public class RegistFlowsBusiness
    {

        private volatile static RegistFlowsBusiness _instance = null;
        private static readonly object lockHelper = new object();
        public static RegistFlowsBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new RegistFlowsBusiness();
                }
            }
            return _instance;
        }
        public string GetAllRecords()
        {
            try
            {
                string sResult = "";
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);
                if (infoColl != null)
                {
                    sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetRecordByNo(string sNo)
        {
            try
            {
                string sResult = "";
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlows info = infoBoss.GetRecordByNo(sNo);
                if (info != null)
                {
                    sResult = JsonConvert.SerializeObject(info, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetRecordsByPaging(string pageIndex, string pageSize, string condition)
        {
            try
            {
                string sResult = "";
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(condition))
                {
                    sWhere += " And " + condition;
                }
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), sWhere);
                if (infoColl != null)
                {
                    sResult = JsonConvert.SerializeObject(infoColl, Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetCountByCondition(string condition)
        {
            try
            {
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(condition))
                {
                    sWhere += " And " + condition;
                }
                RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public string PostRecord(string sInfo)
        {
            try
            {
                string sResult = "";
                string sSuNo = "00000000";
                RegistFlows info1 = JsonConvert.DeserializeObject<RegistFlows>(sInfo);
                if (info1 != null)
                {
                    RegistFlowsBLL infoBoss = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                    RegistFlows info2 = infoBoss.GetRecordByNo(info1.sRFlowNo);
                    if (info2 == null)
                    {
                        info2 = info1;
                        info2.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                        info2.sBranchNo = IUserContext.GetBranchNo();
                        info2.sAddOptor = sSuNo;
                        info2.dAddDate = DateTime.Now;
                        info2.sModOptor = sSuNo;
                        info2.dModDate = DateTime.Now;
                        info2.iValidityState = 1;
                        info2.sAppCode = IUserContext.GetAppCode() + "; ";
                        sResult = infoBoss.AddNewRecord(info2).ToString();
                    }
                    else
                    {
                        info2 = info1;
                        info2.sBranchNo = IUserContext.GetBranchNo();
                        info2.sModOptor = sSuNo;
                        info2.dModDate = DateTime.Now;
                        sResult = infoBoss.UpdateRecord(info2).ToString();
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}

