﻿using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EntFrm.MainService.Business
{
    public class TicketStatisticsBusiness
    {
        private volatile static TicketStatisticsBusiness _instance = null;
        private static readonly object lockHelper = new object();
        public static TicketStatisticsBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new TicketStatisticsBusiness();
                }
            }
            return _instance;
        }


        public string getCallingCountByCounterNo(string sCounterNo)
        {
            try
            { 
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere  = " BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState = " + IPublicConsts.PROCSTATE_CALLING + " And    EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                  
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                 

                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getWaitingCountByCounterNo(string sCounterNo)
        {
            try
            { 
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere =  " DataFlag=0 And  BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState =" + IPublicConsts.PROCSTATE_WAITING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                 

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                  
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        
        public string getCallingListByCounterNo(string sCounterNo, string iSize)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere =   " BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState = " + IPublicConsts.PROCSTATE_CALLING + "And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                 

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = int.Parse(iSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getWaitingListByCounterNo(string sCounterNo, string iSize)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere =   " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState = " + IPublicConsts.PROCSTATE_WAITING + "   And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                 

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = int.Parse(iSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getQueuingListByCounterNo(string sCounterNo, string iSize)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState Between " + IPublicConsts.PROCSTATE_WAITING + " And " + IPublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
 


                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = int.Parse(iSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        //item.Name = info.sTicketNo + " " + info.sCnName + "(" + (info.iSex == 0 ? "女" : "男") + "," + info.iWFlowsIndex + ")";
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getNonarrivalListByCounterNo(string sCounterNo, string iSize)
        {
            try
            {
                string sResult = "";
                int count = 0;
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo ='" + sCounterNo + "' And ProcessState = " + IPublicConsts.PROCSTATE_NOTCOME + " And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, int.Parse(iSize), sWhere);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getCallingCountByServiceNo(string sServiceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere  = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo='" + sServiceNo + "' And ProcessState = " + IPublicConsts.PROCSTATE_CALLING + "   And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
 
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getWaitingCountByServiceNo(string sServiceNo)
        {
            try
            {
                DateTime workDate = DateTime.Now.AddMinutes(30);
                string sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo='" + sServiceNo + "' And ProcessState = " + IPublicConsts.PROCSTATE_WAITING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                return infoBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        } 
        
        public string getCallingListByServiceNo(string sServiceNo, string iSize)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now.AddMinutes(30); 
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo='" + sServiceNo + "' And ProcessState = " + IPublicConsts.PROCSTATE_CALLING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                
                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = int.Parse(iSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getWaitingListByServiceNo(string sServiceNo, string iSize)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now.AddMinutes(30);
                string sWhere = " DataFlag=0 And   BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo='" + sServiceNo + "' And ProcessState = " + IPublicConsts.PROCSTATE_WAITING + "  And   EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = int.Parse(iSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = " ProcessState Desc,OrderWeight Desc,ProcessedTime Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                List<ItemObject> itemList = new List<ItemObject>();
                ItemObject item = null;
                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ViewTicketFlows info in infoColl)
                    {
                        item = new ItemObject();
                        item.Name = info.sTicketNo + " " + info.sCnName;
                        item.Value = info.sPFlowNo;

                        itemList.Add(item);
                    }
                }
                sResult = JsonConvert.SerializeObject(itemList);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
          
        public string getTicketFlowByTFlowNo(string sTFlowNo)
        {
            try
            {
                string sResult = "";
                TicketFlowsBLL infoBoss = new TicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                TicketFlows info = infoBoss.GetRecordByNo(sTFlowNo);

                sResult = JsonConvert.SerializeObject(info);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getVTicketFlowByPFlowNo(string sPFlowNo)
        {
            try
            {
                string sResult = "";
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlows info = infoBoss.GetRecordByNo(sPFlowNo);

                sResult = JsonConvert.SerializeObject(info);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getVTicketFlowsByClassNo(string sTFlowNo)
        {
            try
            {
                string sResult = "";
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByClassNo(sTFlowNo);

                if (infoColl != null && infoColl.Count > 0)
                {
                    sResult = JsonConvert.SerializeObject(infoColl);
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
          
        public string getVTicketFlowsByPaging(string sPageIndex, string sPageSize, string sCondition, string sOrderField = "ID", string sOrderType = "Asc")
        {
            try
            {
                string sResult = "";
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(sCondition))
                {
                    sWhere += " And " + sCondition;
                }
                SqlModel s_model = new SqlModel();

                s_model.iPageNo = int.Parse(sPageIndex);
                s_model.iPageSize = int.Parse(sPageSize);
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = sOrderField;
                s_model.sOrderType = sOrderType;
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                if (infoColl != null && infoColl.Count > 0)
                {
                    sResult = JsonConvert.SerializeObject(infoColl);
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getVTicketFlowByTicketNo(string sTicketNo)
        {
            try
            {
                string sResult = "";
                ViewTicketFlows Info = getVTicketFlowByTicketNo(sTicketNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (Info != null)
                {
                    sResult = JsonConvert.SerializeObject(Info);
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public ViewTicketFlows getVTicketFlowByTicketNo(string sTicketNo, string sWorkDate)
        {
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Parse(sWorkDate);
                string where = " BranchNo = '" + IUserContext.GetBranchNo() + "' And TicketNo='" + sTicketNo + "'  And EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                ViewTicketFlowsBLL vTicketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ViewTicketFlowsCollections vTicketFlowColl = vTicketBoss.GetRecordsByPaging(ref count, 1, 1, where);

                if (vTicketFlowColl != null && vTicketFlowColl.Count > 0)
                {
                    return vTicketFlowColl.GetFirstOne();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public string getVTicketFlowByCardNo(string sCardNo)
        {
            try
            {
                string sResult = "";
                int count = 1;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And IdCardNo='" + sCardNo + "' And EnqueueTime Between '" + DateTime.Now.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);
                if (infoColl != null && infoColl.Count > 0)
                {
                    sResult = JsonConvert.SerializeObject(infoColl.GetFirstOne());
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        public string getVTicketFlowByCounterNo(string sCounterNo, string sWorkDate, string sProcessState, string sOrderField="ID", string sOrderType = "Asc")
        {
            try
            { 
                string sResult = "";
                DateTime workDate = DateTime.Parse(sWorkDate);

                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState=" + sProcessState + " And EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                SqlModel s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = sWhere;
                s_model.sOrderField = sOrderField;
                s_model.sOrderType = sOrderType;
                s_model.sTableName = "ViewTicketFlows";

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

                if (infoColl != null&&infoColl.Count>0)
                {
                    sResult = JsonConvert.SerializeObject(infoColl.GetFirstOne(), Formatting.None);
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getVTicketFlowByStafferNo(string sStafferNo, string sWorkDate, string sProcessState,  string sOrderField = "ID", string sOrderType = "Asc")
        {
            string sResult = "";
            DateTime workDate = DateTime.Parse(sWorkDate);
            ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

            string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And StafferNo='" + sStafferNo + "' And ProcessState=" + sProcessState + " And EnqueueTime Between '" + workDate.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + workDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

            SqlModel s_model = new SqlModel();

            s_model.iPageNo = 1;
            s_model.iPageSize = 1;
            s_model.sFields = "*";
            s_model.sCondition = sWhere;
            s_model.sOrderField = "ID";
            s_model.sOrderType = sOrderType;
            s_model.sTableName = "ViewTicketFlows";
            ViewTicketFlowsCollections infoColl = infoBoss.GetRecordsByPaging(s_model);

            if (infoColl != null && infoColl.Count > 0)
            {
                sResult = JsonConvert.SerializeObject(infoColl.GetFirstOne(), Formatting.None);
            }
            return sResult; 
        }
         

        public string getVTicketCountByCondition(string sCondition)
        {
            try
            {
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' ";
                if (!string.IsNullOrEmpty(sCondition))
                {
                    sWhere += " And " + sCondition;
                }
                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                return  infoBoss.GetCountByCondition(sWhere).ToString(); 
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
 
        public string getVTicketCountByCounterNo(string sCounterNo, string sWorkDate, string sPStateStart,string sPStateEnd)
        {
            try
            { 
                DateTime workDate = DateTime.Parse(sWorkDate);
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And CounterNos Like '%" + sCounterNo + "%' And ProcessState Between " + sPStateStart + " And " + sPStateEnd + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
               


                return ticketBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        
        public string getVTicketCountByServiceNo(string sServiceNo, string sWorkDate, string sPStateStart, string sPStateEnd)
        {
            try
            { 
                DateTime workDate = DateTime.Parse(sWorkDate);
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ServiceNo= '" + sServiceNo + "' And ProcessState  Between " + sPStateStart + " And " + sPStateEnd + " And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                
                return ticketBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
                  
        public string getVTicketCountByWFlowNo(string sWFlowNo, string sWorkDate, string sPStateStart, string sPStateEnd)
        {
            try
            {
                DateTime workDate = DateTime.Parse(sWorkDate);
                 string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And WorkFlowsNo= '" + sWFlowNo + "' And ProcessState Between " + sPStateStart + " And " + sPStateEnd + "  And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                     
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例

                return ticketBoss.GetCountByCondition(sWhere).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

    }
}
