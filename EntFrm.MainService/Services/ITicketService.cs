using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class ITicketService
    { 
        private volatile static ITicketService _instance = null;
        private static readonly object lockHelper = new object();
         
        public static ITicketService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ITicketService();
                }
            }
            return _instance;
        }

        //清理队列
        private void doClearQueue(string sCounterNo)
        {
            try
            {
                int count = 0;
                string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And ProcessedCounterNo='" + sCounterNo + "' And EnqueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'";
                ProcessFlowsBLL pflowBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ProcessFlowsCollections pflowColl = pflowBoss.GetRecordsByPaging(ref count, 1, 100,sWhere);

                if (pflowColl != null && pflowColl.Count > 0)
                {
                    foreach (ProcessFlows pflow in pflowColl)
                    {
                        pflow.iDataFlag = 1;
                        pflow.dFinishTime = DateTime.Now;
                        pflow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                        pflow.dProcessedTime = DateTime.Now;
                        pflow.dModDate = DateTime.Now;
                        if (pflowBoss.UpdateRecord(pflow))
                        {
                            /////插入下一个流程               
                            int WFlowsIndex = pflow.iWFlowsIndex + 1;
                            string sNextServiceNo = IBusinessHelper.getNextWorkFlowServiceNo(pflow.sWFlowsNo, WFlowsIndex); 
                            string sCounterNos = IBusinessHelper.getCounterNosByServiceNo(sNextServiceNo, IUserContext.GetBranchNo());
                            if (!string.IsNullOrEmpty(sNextServiceNo))
                            {
                                IBusinessHelper.InsertProcessFlow(pflow.sTFlowNo, sNextServiceNo, sCounterNos, pflow.sWFlowsNo, WFlowsIndex, IUserContext.GetBranchNo());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
          
        //登录窗口
        public string doSignIn(string sCounterNo, string sLoginId, string sPsword)
        {
            try
            {
                int count = 0;
                string sResult = "";
                StafferInfoBLL staffBoss = new StafferInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                StafferInfoCollections staffColl = staffBoss.GetRecordsByPaging(ref count, 1, 1, " BranchNo = '" + IUserContext.GetBranchNo() + "' And LoginId='" + sLoginId + "'");

                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = counterBoss.GetRecordByNo(sCounterNo);

                if (staffColl != null && staffColl.Count > 0 && counter != null)
                {
                    StafferInfo staffer = staffColl.GetFirstOne();
                    if (staffer.sPassword.Equals(sPsword))
                    {
                        counter.iLogonState = 1;
                        counter.sLogonStafferNo = staffer.sStafferNo;
                        counter.dModDate = DateTime.Now;

                        counterBoss.UpdateRecord(counter);
                        sResult = staffer.sStafferNo;

                        //评价器登录
                        EvaluateService.CreateInstance().doSigninEvaluator(counter.sCounterNo, staffer.sStafferNo);

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "医生(" + sLoginId + ")登录窗口(" + counter.sCounterName + ")呼叫器");
                    }
                }

                ////刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        //登出所有窗口
        public void doSignoutAll()
        {
            try
            {
                int count = 0;
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfoCollections counterList = counterBoss.GetRecordsByPaging(ref count, 1, 100, "  BranchNo = '" + IUserContext.GetBranchNo() + "' ");

                if (counterList != null && counterList.Count > 0)
                {
                    foreach (CounterInfo counter in counterList)
                    {
                        counter.iLogonState = 0;
                        counter.sLogonStafferNo = "";
                        counter.dModDate = DateTime.Now;

                        counterBoss.UpdateRecord(counter).ToString();

                        //评价器退出
                        EvaluateService.CreateInstance().doSignoutEvaluator(counter.sCounterNo);
                    }

                    //刷新呼叫器  
                    Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                    t2.Start();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //登出窗口
        public string doSignOut(string sCounterNo)
        {
            try
            {
                string sResult = "false";

                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = counterBoss.GetRecordByNo(sCounterNo);

                if (counter != null)
                {
                    counter.iLogonState = 0;
                    counter.sLogonStafferNo = "";
                    counter.dModDate = DateTime.Now;

                    counterBoss.UpdateRecord(counter);

                    //评价器退出
                    EvaluateService.CreateInstance().doSignoutEvaluator(counter.sCounterNo);

                    //刷新呼叫器  
                    Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                    t2.Start();

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + counter.sCounterName + ")登出呼叫器");
                    sResult = "true";
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //窗口求助：5
        public string doSeekHelp(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                if (counter != null)
                {
                    string sCallVoiceEnable = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEENABLE, "Others");
                    string sCallVoiceFormat = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEFORMAT, "Others");
                    string sCallVoiceStyle = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICESTYLE, "Others");
                    string sCallVoiceVolume = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICEVOLUME, "Others");
                    string sCallVoiceRate = IUserContext.GetParamValue(IPublicConsts.DEF_CALLVOICERATE, "Others");

                    if (!string.IsNullOrEmpty(sCallVoiceEnable) && sCallVoiceEnable.Equals("1"))
                    {
                        sCallVoiceFormat = sCallVoiceFormat.Replace("[窗口名称]", counter.sCounterName);
                        sCallVoiceFormat = sCallVoiceFormat.Replace("[窗口别名]", counter.sCounterAlias);

                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, sCallVoiceStyle, int.Parse(sCallVoiceRate), int.Parse(sCallVoiceVolume), sCallVoiceFormat);

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + counter.sCounterName + ")呼叫求助");
                        sResult = "true";
                    }
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //窗口暂停服务：6
        public string doOutService(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                LEDDisplay ledinfo = null;

                if (counter != null)
                {
                    counter.iPauseState = 1;
                    counter.dModDate = DateTime.Now;
                    counterBoss.UpdateRecord(counter);

                    //led屏
                    ledinfo = IPublicHelper.GetLEDDisplayByNo(counter.sLedDisplayNo);
                    if (ledinfo != null)
                    {
                        DisplayService.CreateInstance().doDisplayText(ledinfo, ledinfo.sOnPauseTip);
                    }
                     
                    //评价器暂停服务
                    EvaluateService.CreateInstance().doPauseEvaluator(counter.sCounterNo);

                    ////刷新呼叫器  
                    Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                    t2.Start();

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + counter.sCounterName + ")暂停服务");
                    sResult = "true";
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //窗口开始服务
        public string doInService(string sCounterNo)
        {
            try
            {
                string sResult = "false";
                CounterInfoBLL counterBoss = new CounterInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counter = IPublicHelper.GetCounterByNo(sCounterNo);
                LEDDisplay ledinfo = null;

                if (counter != null)
                {
                    counter.iPauseState = 0;
                    counter.dModDate = DateTime.Now;
                    counterBoss.UpdateRecord(counter);

                    //led屏
                    ledinfo = IPublicHelper.GetLEDDisplayByNo(counter.sLedDisplayNo);
                    if (ledinfo != null)
                    {
                        DisplayService.CreateInstance().doDisplayText(ledinfo, ledinfo.sPowerOnTip);
                    }

                    //评价器恢复服务
                    EvaluateService.CreateInstance().doResumeEvaluator(counter.sCounterNo);

                    //刷新呼叫器  
                    Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                    t2.Start();

                    MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + counter.sCounterName + ")开始服务");
                    sResult = "true";
                }
                return sResult;
            }
            catch (Exception ex)
            {
                return "false";
            }
        }

        //呼叫下一位：8，9   
        public string doCallNextTicket(string sCounterNo)
        {
            try
            { 
                string sResult = ""; 

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    doClearQueue(sCounterNo);
                    string pflowno = IBusinessHelper.EnqueueCalling(sCounterNo);

                    if (!string.IsNullOrEmpty(pflowno))
                    {
                        //sResult = IPublicHelper.GetVTicketFlowByNo(pflowno).sTicketNo;

                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, pflowno);

                        //led屏
                        DisplayService.CreateInstance().doDisplayText(sCounterNo,pflowno);

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")呼叫下一位");
                        sResult = pflowno;
                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start(); 

                //推送消息 
                Thread t3 = new Thread(new ParameterizedThreadStart(IMessageService.CreateInstance().SendWaitingMessage));
                t3.Start(sCounterNo);

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //呼叫下一位：8，9   
        public string getNextTicketNo(string sCounterNo)
        {
            try
            {
                string sResult = "";

                if (!string.IsNullOrEmpty(sCounterNo))
                { 
                    string pflowno = IBusinessHelper.EnqueueCalling(sCounterNo,false);

                    if (!string.IsNullOrEmpty(pflowno))
                    {
                        sResult = IPublicHelper.GetVTicketFlowByNo(pflowno).sTicketNo; 
                    }
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //重呼：10
        public string doRecallTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;   

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    int count = 0;
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "' ";
                      
                    ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                    ProcessFlowsCollections processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {

                        //播放语音
                        SpeechService.CreateInstance().doPlayVoice(sCounterNo, processFlows.GetFirstOne().sPFlowNo);

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")进行重呼");
                        sResult = processFlows.GetFirstOne().sPFlowNo; 
                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //指定叫号
        public string doSpecialTicket(string sCounterNo, string sPFlowNo)
        {
            try
            {
                string sResult = "";

                if (!string.IsNullOrEmpty(sCounterNo))
                {

                    doClearQueue(sCounterNo);

                    ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                    ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);

                    if (processFlow != null)
                    {
                        processFlow.iProcessState = IPublicConsts.PROCSTATE_CALLING;
                        processFlow.sProcessedCounterNo = sCounterNo;
                        processFlow.sProcessedStafferNo = "";
                        processFlow.dProcessedTime = DateTime.Now;
                        processFlow.dModDate = DateTime.Now;

                        if (processBoss.UpdateRecord(processFlow))
                        {

                            //播放语音
                            SpeechService.CreateInstance().doPlayVoice(sCounterNo, processFlow.sPFlowNo);

                            //led屏
                            DisplayService.CreateInstance().doDisplayText(sCounterNo, processFlow.sPFlowNo);

                            MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")指定呼叫");
                            sResult = processFlow.sPFlowNo;
                        }
                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        //获取所有业务列表
        public string getTransferList()
        {
            try
            {
                int count = 0;
                string sResult = "";
                string sWhere = "";
                List<ItemObject> itemList = new List<ItemObject>();

                sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And HaveChild=0 ";
                ServiceInfoBLL infoBoss = new ServiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode());
                ServiceInfoCollections infoColl = infoBoss.GetRecordsByPaging(ref count, 1, 100, sWhere);

                if (infoColl != null && infoColl.Count > 0)
                {
                    foreach (ServiceInfo info in infoColl)
                    {
                        itemList.Add(new ItemObject(info.sServiceNo, info.sServiceName));
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

        //转移
        public string doTransferTicket(string sServiceNo, string sPFlowNo)
        {
            try
            {
                string sResult = "";

                if (!string.IsNullOrEmpty(sPFlowNo))
                {
                    string counterNos = IPublicHelper.GetCounterNosByServiceNo(sServiceNo, IUserContext.GetBranchNo());

                    ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例  
                    ProcessFlows processFlow = processBoss.GetRecordByNo(sPFlowNo);

                    if (!string.IsNullOrEmpty(counterNos) && processFlow != null)
                    {

                        processFlow.sServiceNo = sServiceNo;
                        processFlow.sCounterNos = counterNos;

                        processFlow.iOrderWeight = 0;
                        processFlow.iProcessState = IPublicConsts.PROCSTATE_WAITING;
                        processFlow.iPriorityType = 0;
                        processFlow.iDelayTimeValue = 0;
                        processFlow.dProcessedTime = DateTime.Now;
                        processFlow.sProcessedCounterNo = "";
                        processFlow.sProcessedStafferNo = "";
                        processFlow.dModDate = DateTime.Now;

                        if (processBoss.UpdateRecord(processFlow))
                        {

                            MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "病人转诊完成");
                            sResult = processFlow.sPFlowNo;
                        }

                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //转移（确定）：17  
        public string doTransferTicketByAlias(string sServiceAlias, string sPFlowNo)
        {
            try
            {
                string sServiceNo = IPublicHelper.GetServiceNoByAlias(sServiceAlias);
                return doTransferTicket(sServiceNo,sPFlowNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //呼叫（确定） 开始办理业务：11
        public string doStartTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState=" + IPublicConsts.PROCSTATE_CALLING + " And   EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "' ";

                    int count = 0;
                    processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {
                        processFlow = processFlows.GetFirstOne();
                        if (processFlow != null)
                        {
                            processFlow.dBeginTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_PROCESSING;
                            processFlow.sProcessedCounterNo = sCounterNo;
                            processFlow.sProcessedStafferNo = "";
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                //评价器欢迎光临
                                EvaluateService.CreateInstance().doWelcomeEvaluator(sCounterNo); 

                                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")开始操作");
                                sResult = processFlow.sPFlowNo;
                            }
                        }
                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //呼叫（完成）：12
        public string doFinishTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState In(" + IPublicConsts.PROCSTATE_CALLING + "," + IPublicConsts.PROCSTATE_PROCESSING + ") And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "'";

                    int count = 0;
                    processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {
                        processFlow = processFlows.GetFirstOne();
                        if (processFlow != null)
                        {
                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_FINISHED;
                            processFlow.sProcessedCounterNo = sCounterNo;
                            processFlow.sProcessedStafferNo = "";
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {

                                //评价器开始评价
                                EvaluateService.CreateInstance().doStartEvaluator(sCounterNo);

                                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")完成操作");
                                sResult = processFlow.sPFlowNo;
                            }
                        }
                    }
                }

                //刷新呼叫器  
                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                t2.Start();

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //终止：14
        public string doAbortTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState In(" + IPublicConsts.PROCSTATE_CALLING + "," + IPublicConsts.PROCSTATE_PROCESSING + ") And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "'";

                    int count = 0;
                    processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {
                        processFlow = processFlows.GetFirstOne();
                        if (processFlow != null)
                        {
                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_ABORT;
                            processFlow.sProcessedCounterNo = sCounterNo;
                            processFlow.sProcessedStafferNo = "";
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")中止操作");
                                sResult = processFlow.sPFlowNo;
                            }
                        }
                    }
                } 

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //未到：15
        public string doNotcomeTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState In(" + IPublicConsts.PROCSTATE_CALLING + "," + IPublicConsts.PROCSTATE_PROCESSING + ") And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "'";

                    int count = 0;
                    processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {
                        processFlow = processFlows.GetFirstOne();
                        if (processFlow != null)
                        {
                            processFlow.iDataFlag = 1;
                            processFlow.dFinishTime = DateTime.Now;
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_NOTCOME;
                            processFlow.sProcessedCounterNo = sCounterNo;
                            processFlow.sProcessedStafferNo = "";
                            processFlow.dProcessedTime = DateTime.Now;
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                //刷新呼叫器  
                                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                                t2.Start();

                                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")过号操作");
                                sResult = processFlow.sPFlowNo;
                            }
                        }
                    }
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        //滞后10分钟：16
        public string doDelayTicket(string sCounterNo)
        {
            try
            {
                string sResult = "";
                DateTime workDate = DateTime.Now;

                ProcessFlowsBLL processBoss = new ProcessFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例    
                ProcessFlowsCollections processFlows = null;
                ProcessFlows processFlow = null;

                if (!string.IsNullOrEmpty(sCounterNo))
                {
                    string sWhere = " BranchNo = '" + IUserContext.GetBranchNo() + "' And ProcessedCounterNo='" + sCounterNo + "' And ProcessState In(" + IPublicConsts.PROCSTATE_CALLING + "," + IPublicConsts.PROCSTATE_PROCESSING + ") And  EnqueueTime Between '" + workDate.ToString("yyyy-MM-dd 00:00:00") + "' And '" + workDate.AddDays(1).ToString("yyyy-MM-dd 00:00:0") + "'";

                    int count = 0;
                    processFlows = processBoss.GetRecordsByPaging(ref count, 1, 1, sWhere);

                    if (processFlows != null && processFlows.Count > 0)
                    {
                        processFlow = processFlows.GetFirstOne();
                        if (processFlow != null)
                        {
                            processFlow.iDataFlag = 0; 
                            processFlow.iProcessState = IPublicConsts.PROCSTATE_WAITING;
                            processFlow.sProcessedCounterNo = "";
                            processFlow.sProcessedStafferNo = ""; 
                            processFlow.dProcessedTime = DateTime.Now.AddMinutes(30);
                            processFlow.dModDate = DateTime.Now;

                            if (processBoss.UpdateRecord(processFlow))
                            {
                                //刷新呼叫器  
                                Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                                t2.Start();

                                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "窗口(" + IPublicHelper.GetCounterNameByNo(sCounterNo) + ")滞后操作");
                                sResult = processFlow.sPFlowNo;
                            }
                        }
                    }
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
         
        //插入票号
        public string doAddNewTicket1(string sNewTicketNo, string sServiceNo,string sRUserData, string sWorkDate)
        {
            try
            {
                string sRUserNo = IBusinessHelper.InsertRUserInfo(sRUserData);
                return doAddNewTicket2(sNewTicketNo, sServiceNo, sRUserNo, sWorkDate);
                
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string doAddNewTicket2(string sNewTicketNo, string sServiceNo, string sRUserNo, string sWorkDate)
        {
            try
            {
                string sTFlowNo = "";
                string sPFlowNo = ""; 
                string sTicketNo = sNewTicketNo;
                DateTime workDate = DateTime.Parse(sWorkDate);

                ViewTicketFlowsBLL infoBoss = new ViewTicketFlowsBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                ServiceInfo serviceInfo = IPublicHelper.GetServiceByNo(sServiceNo);

                if (serviceInfo != null)
                {
                    if (string.IsNullOrEmpty(sTicketNo))
                    {
                        sTicketNo = IBusinessHelper.GenerateTicketNo(sServiceNo, IUserContext.GetBranchNo());
                    }

                    sTFlowNo = IBusinessHelper.InsertTicketFlow(sTicketNo, sRUserNo, IUserContext.GetBranchNo());
                    if (!string.IsNullOrEmpty(sTFlowNo))
                    {
                        /////插入下一个流程 
                        int WFlowsIndex = 0;
                        string sNextServiceNo = IBusinessHelper.getNextWorkFlowServiceNo(serviceInfo.sServiceNo, WFlowsIndex);
                        if (string.IsNullOrEmpty(sNextServiceNo))
                        {
                            sNextServiceNo = serviceInfo.sServiceNo;
                        }
                        string sCounterNos = IBusinessHelper.getCounterNosByServiceNo(sNextServiceNo, IUserContext.GetBranchNo());

                        sPFlowNo = IBusinessHelper.InsertProcessFlow(sTFlowNo, sNextServiceNo, sCounterNos, serviceInfo.sServiceNo, WFlowsIndex, IUserContext.GetBranchNo());

                        //刷新呼叫器  
                        Thread t2 = new Thread(HdcallerService.CreateInstance().doRefreshCaller);
                        t2.Start();

                        MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "取票(" + sTicketNo + ")成功!");
                        return sPFlowNo;
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
