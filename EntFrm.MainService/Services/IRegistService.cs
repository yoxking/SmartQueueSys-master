using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class IRegistService
    {
        private volatile static IRegistService _instance = null;
        private static readonly object lockHelper = new object();

        private bool isQuitFlag = false;

        public static IRegistService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new IRegistService();
                }
            }
            return _instance;
        }

        private IRegistService() { }

        public void StartRegistFlows()
        {
            string registeMode = IUserContext.GetParamValue(IPublicConsts.DEF_REGISTEMODE, "Other");
            if (!registeMode.Equals("AutoRegiste"))
            {
                return;
            }

            SqlModel s_model = null;
            RegistFlowsBLL registBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            RegistFlowsCollections registColl = null; 

            MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "注册服务启动完成...");

            while (true)
            {
                if (isQuitFlag)
                {
                    break;
                }

                s_model = new SqlModel();

                s_model.iPageNo = 1;
                s_model.iPageSize = 1;
                s_model.sFields = "*";
                s_model.sCondition = " RegistState=0 And RegistDate Between '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                s_model.sOrderField = " RegistDate Asc,ID ";
                s_model.sOrderType = "Asc";
                s_model.sTableName = "RegistFlows";


                registColl = registBLL.GetRecordsByPaging(s_model);

                if (registColl != null && registColl.Count > 0)
                {
                    foreach (RegistFlows registFlow in registColl)
                    {
                        enqueueRegisteUser(registFlow);
                    }
                }

                Thread.Sleep(30000);
            }
        } 

        public void StopRegistFlows()
        {
            isQuitFlag = true;
        }

        private void enqueueRegisteUser(RegistFlows regFlow)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "";

                if (regFlow != null)
                { 
                    sTicketNo = IBusinessHelper.GenerateTicketNo(regFlow.sServiceNo, IUserContext.GetBranchNo()); 

                    sTFlowNo = IBusinessHelper.InsertTicketFlow(sTicketNo, regFlow.sRUserNo, IUserContext.GetBranchNo());
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

                        sPFlowNo=IBusinessHelper.InsertProcessFlow(sTFlowNo, sNextServiceNo, sCounterNos, regFlow.sServiceNo, WFlowsIndex, IUserContext.GetBranchNo());

                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            doUpdateRegFlow(regFlow.sRFlowNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            } 
        }

        private void doUpdateRegFlow(string rflowNo)
        {
            RegistFlowsBLL registBLL = new RegistFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
            RegistFlows registFlow = registBLL.GetRecordByNo(rflowNo);

            if (registFlow != null)
            {
                registFlow.iDataFlag = 1;
                registFlow.iRegistState = 1;
                registFlow.dModDate = DateTime.Now;

                registBLL.UpdateRecord(registFlow);
            }
        }
         
        public string doEnqueueRegUser(string idNo)
        {
            try
            {
                string sTicketNo = "";
                string sTFlowNo = "";
                string sPFlowNo = "0";
                string sServiceNo = "0";
                RegistFlows regFlow = IPublicHelper.GetRegFlowByRuserIdNo(idNo);

                if (regFlow != null)
                {
                    sTicketNo = IBusinessHelper.GenerateTicketNo(regFlow.sServiceNo, IUserContext.GetBranchNo());
                    sServiceNo = regFlow.sServiceNo;

                    sTFlowNo = IBusinessHelper.InsertTicketFlow(sTicketNo, regFlow.sRUserNo, IUserContext.GetBranchNo());
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

                        sPFlowNo = IBusinessHelper.InsertProcessFlow(sTFlowNo, sNextServiceNo, sCounterNos, regFlow.sServiceNo, WFlowsIndex, IUserContext.GetBranchNo());

                        if (!string.IsNullOrEmpty(sPFlowNo))
                        {
                            doUpdateRegFlow(regFlow.sRFlowNo);
                        }
                    }
                }

                return sPFlowNo + ";" + sServiceNo;
            }
            catch (Exception ex)
            {
                return "0;0";
            }
        }
    }
}