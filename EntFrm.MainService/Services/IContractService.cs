using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using EntFrm.MainService.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EntFrm.MainService.Services
{
    public class IContractService
    {
        private volatile static IContractService _instance = null;
        private static readonly object lockHelper = new object();

        public static IContractService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IContractService();
                }
            }
            return _instance;
        }

        private IContractService() { }

        public string getServTime()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return JsonConvert.SerializeObject(codeData);
        }
        public string getContent(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                ContentInfoBLL infoBLL = new ContentInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ContentInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    ContentData model = new ContentData();
                    model.Title = info.sTitle;
                    model.Author = info.sAuthor;
                    model.PubDate = info.dPublicDate.ToString("yyyy-MM-dd HH:mm:ss");
                    model.Content = info.sNContent;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getAllBranchs()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                List<BranchData> modelList = new List<BranchData>();
                BranchInfoBLL infoBLL = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                BranchInfoCollections infoColl = infoBLL.GetAllRecords();

                if (infoColl != null && infoColl.Count > 0)
                {
                    BranchData model = null;
                    foreach (BranchInfo info in infoColl)
                    {
                        model = new BranchData();
                        model.BranchNo = info.sBranchNo;
                        model.BranchName = info.sBranchName;

                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getAllServices(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<ServiceData> modelList = new List<ServiceData>();
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ServiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    ServiceData model = null;
                    foreach (ServiceInfo info in infoColl)
                    {
                        if (info.iHaveChild == 0)
                        {
                            model = new ServiceData();
                            model.ServiceNo = info.sServiceNo;
                            model.ServiceName = info.sServiceName;
                            model.ServiceAlias = info.sServiceAlias;
                            model.ServiceType = info.sServiceType;
                            model.AmLimit = info.iAMLimit == 1 ? "true" : "false";
                            model.AmStartTime = info.dAMStartTime.ToString("HH:mm:ss");
                            model.AmEndTime = info.dAMEndTime.ToString("HH:mm:ss");
                            model.PmLimit = info.iPMLimit == 1 ? "true" : "false";
                            model.PmStartTime = info.dPMStartTime.ToString("HH:mm:ss");
                            model.PmEndTime = info.dPMEndTime.ToString("HH:mm:ss");
                            model.WeekLimit = info.iWeekLimit == 1 ? "true" : "false";
                            model.WeekDays = info.sWeekDays;

                            modelList.Add(model);
                        }
                    }
                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getService(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ServiceInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    ServiceData model = new ServiceData();
                    model.ServiceNo = info.sServiceNo;
                    model.ServiceName = info.sServiceName;
                    model.ServiceAlias = info.sServiceAlias;
                    model.ServiceType = info.sServiceType;
                    model.AmLimit = info.iAMLimit == 1 ? "true" : "false";
                    model.AmStartTime = info.dAMStartTime.ToString("HH:mm:ss");
                    model.AmEndTime = info.dAMEndTime.ToString("HH:mm:ss");
                    model.PmLimit = info.iPMLimit == 1 ? "true" : "false";
                    model.PmStartTime = info.dPMStartTime.ToString("HH:mm:ss");
                    model.PmEndTime = info.dPMEndTime.ToString("HH:mm:ss");
                    model.WeekLimit = info.iWeekLimit == 1 ? "true" : "false";
                    model.WeekDays = info.sWeekDays;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getAllCounters(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<CounterData> modelList = new List<CounterData>();
                CounterInfoBLL infoBLL = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                CounterInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    CounterData model = null;
                    foreach (CounterInfo info in infoColl)
                    {
                        model = new CounterData();
                        model.CounterNo = info.sCounterNo;
                        model.CounterName = info.sCounterName;
                        model.CounterAlias = info.sCounterAlias;
                        model.LogonState = info.iLogonState;
                        model.PauseState = info.iPauseState;
                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getCounter(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                CounterInfoBLL infoBLL = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                CounterInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    CounterData model = new CounterData();
                    model.CounterNo = info.sCounterNo;
                    model.CounterName = info.sCounterName;
                    model.CounterAlias = info.sCounterAlias;
                    model.LogonState = info.iLogonState;
                    model.PauseState = info.iPauseState;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getAllStaffers(string branchNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                string sWhere = " BranchNo='" + branchNo + "' ";
                List<StafferData> modelList = new List<StafferData>();
                StafferInfoBLL infoBLL = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                StafferInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    StafferData model = null;
                    foreach (StafferInfo info in infoColl)
                    {
                        model = new StafferData();
                        model.StafferNo = info.sStafferNo;
                        model.LoginId = info.sLoginId;
                        model.StafferName = info.sStafferName;
                        model.OrganizName = info.sOrganizName;
                        model.StarLevel = info.sStarLevel;
                        model.HeadPhoto = info.sHeadPhoto;
                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getStaffer(string id)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                StafferInfoBLL infoBLL = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                StafferInfo info = infoBLL.GetRecordByNo(id);

                if (info != null)
                {
                    StafferData model = new StafferData();
                    model.StafferNo = info.sStafferNo;
                    model.LoginId = info.sLoginId;
                    model.StafferName = info.sStafferName;
                    model.OrganizName = info.sOrganizName;
                    model.StarLevel = info.sStarLevel;
                    model.HeadPhoto = info.sHeadPhoto;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getTicket(string branchNo, string ticketNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                DateTime workDate = DateTime.Now;
                ViewTicketFlowsBLL ticketBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());

                string where = " BranchNo='" + branchNo + "' And TicketNo='" + ticketNo + "'  And EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                ViewTicketFlowsCollections vTicketFlowColl = ticketBoss.GetRecordsByPaging(ref count, 1, 1, where);

                if (vTicketFlowColl != null && vTicketFlowColl.Count > 0)
                {
                    ViewTicketFlows info = vTicketFlowColl.GetFirstOne();
                    if (info != null)
                    {
                        TicketViewData model = new TicketViewData();
                        model.PFlowNo = info.sPFlowNo;
                        model.TicketNo = info.sTicketNo;

                        codeData.data = JsonConvert.SerializeObject(model);
                    }
                }
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByServiceId(string branchNo, string serviceId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = IPublicHelper.getProcessingCountByServiceNo(branchNo, serviceId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByCounterId(string branchNo, string counterId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = IPublicHelper.getProcessingCountByCounterNo(branchNo, counterId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getWaitingNumByStafferId(string branchNo, string stafferId)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                codeData.data = IPublicHelper.getProcessingCountByStafferNo(branchNo, stafferId).ToString();
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string getRegistFlowsByBranchId(string branchNo, string pageIndex, string pageSize, string strWhere)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                ViewRegistFlowsBLL infoBoss = new ViewRegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                ViewRegistFlowsCollections infoColl = infoBoss.GetRecordsByPaging(ref count, int.Parse(pageIndex), int.Parse(pageSize), strWhere);

                codeData.data = JsonConvert.SerializeObject(infoColl);
                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";
                codeData.code = "400";
                return JsonConvert.SerializeObject(codeData);
            }
        }

        #region 微信预约接口

        public string getRegbranchs()
        {

            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int i = 0;
                List<RegBranchData> modelList = new List<RegBranchData>();
                BranchInfoBLL infoBLL = new BranchInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                BranchInfoCollections infoColl = infoBLL.GetAllRecords();

                if (infoColl != null && infoColl.Count > 0)
                {
                    RegBranchData model = null;
                    foreach (BranchInfo info in infoColl)
                    {
                        model = new RegBranchData();
                        model.BranchNo = info.sBranchNo;
                        model.BranchName = info.sBranchName;
                        model.ShowFlag = (i == 0);

                        modelList.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(modelList);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getAdvertises(string pageSize)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                List<AdvertiseData> list = new List<AdvertiseData>();
                AdvertiseData model = null;
                ContentInfoBLL infoBLL = new ContentInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ContentInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, int.Parse(pageSize), " ClassNo='786317232768'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ContentInfo info in infoColl)
                    {
                        model = new AdvertiseData();
                        model.title = info.sTitle;
                        model.url = info.sNContent;
                        model.img = info.sPostPicture;

                        list.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(list);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getDeptProfile()
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                ContentInfoBLL infoBLL = new ContentInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ContentInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 1, " ClassNo='778684891872'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    DeptProfileData profile = new DeptProfileData();
                    profile.Title = infoColl.GetFirstOne().sTitle;
                    profile.PubDate = infoColl.GetFirstOne().dPublicDate.ToString("yyyy-MM-dd HH:mm:ss");
                    profile.Url = infoColl.GetFirstOne().sComments;
                    profile.Content = infoColl.GetFirstOne().sNContent;

                    codeData.data = JsonConvert.SerializeObject(profile);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getUserProfile(string userNo)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                RUsersInfoBLL infoBLL = new RUsersInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RUsersInfo info = infoBLL.GetRecordByNo(userNo);

                if (info != null)
                {
                    UserProfileData model = new UserProfileData();
                    model.UserNo = info.sRUserNo;
                    model.UserName = info.sCnName;
                    model.HeadPhoto = info.sHeadPhoto;
                    model.Telephone = info.sTelphone;
                    model.Resume = info.sSummary;

                    codeData.data = JsonConvert.SerializeObject(model);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getRegServices(string branchNo, string registDate)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                List<RegServiceData> list = new List<RegServiceData>();
                RegServiceData model = null;
                ServiceInfoBLL infoBLL = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ServiceInfoCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, 100, " HaveChild=0");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo info in infoColl)
                    {
                        model = new RegServiceData();
                        model.ServiceNo = info.sServiceNo;
                        model.ServiceName = info.sServiceName;
                        model.RegistDate = registDate;
                        model.Registable = true;
                        model.RegistCount = 1 + "";
                        model.Remark = info.sComments;

                        list.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(list);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        public string getRegHistories(string userNo, string pageSize)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                int count = 0;
                List<RegHistoryData> list = new List<RegHistoryData>();
                RegHistoryData model = null;
                RegistFlowsBLL infoBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlowsCollections infoColl = infoBLL.GetRecordsByPaging(ref count, 1, int.Parse(pageSize), " RUserNo='" + userNo + "'");

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (RegistFlows info in infoColl)
                    {
                        model = new RegHistoryData();
                        model.ServiceNo = info.sServiceNo;
                        model.ServiceName = info.sServiceNo;
                        model.RegistDate = info.dRegistDate.ToString("yyyy-MM-dd HH:mm:ss");
                        model.RegistState = info.iRegistState + "";
                        model.ProcessState = info.iDataFlag + "";
                        model.Remark = info.sComments;

                        list.Add(model);
                    }

                    codeData.data = JsonConvert.SerializeObject(list);
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }

        public string postRegistInfo(string branchNo, string serviceNo, string regDate, string regType, string workTime, string userNo, string userName, string sex, string telephone, string idNo, string remark)
        {
            CodeData codeData = new CodeData();
            codeData.msg = "success";
            codeData.code = "200";
            codeData.data = "";
            try
            {
                DateTime dtTime = DateTime.Parse(regDate + " " + workTime.Substring(0, 5) + ":00");

                RegistFlowsBLL infoBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                RegistFlows info = new RegistFlows();

                info.sRFlowNo = CommonHelper.Get_New12ByteGuid();
                info.iDataFlag = 0;
                info.sRUserNo = IBusinessHelper.InsertRUserInfo(userNo, userName, sex, telephone, idNo, remark);
                info.iRegistType = IPublicConsts.REGISTETYPE2;
                info.sDataFrom = "在线预约";
                info.dRegistDate = dtTime;
                info.sServiceNo = serviceNo;
                info.sCounterNo = "";
                info.iWorkTime = 1;
                info.dStartDate = dtTime;
                info.dEnditDate = dtTime.AddMinutes(30);
                info.iRegistState = 0;
                info.sBranchNo = branchNo;

                info.sAddOptor = "";
                info.dAddDate = DateTime.Now;
                info.sModOptor = "";
                info.dModDate = DateTime.Now;
                info.iValidityState = 1;
                info.sAppCode = IUserContext.GetAppCode() + ";";

                if (infoBLL.AddNewRecord(info))
                {
                    codeData.data = "{\"result\":1}";
                }
                else
                {
                    codeData.msg = "error";
                    codeData.data = "{\"result\":0}";
                }

                return JsonConvert.SerializeObject(codeData);
            }
            catch (Exception ex)
            {
                codeData.msg = "error";

                return JsonConvert.SerializeObject(codeData);
            }
        }
        #endregion

    }
}
