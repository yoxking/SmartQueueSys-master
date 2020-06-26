using EntFrm.Framework.Utility;
using EntFrm.MainService.Business;
using EntFrm.MainService.Services;
using System;

namespace EntFrm.MainService.Services
{
    public class QueueService:IQueueService
    { 
        public string OnExecuteCommand(string methodName, string paramList)
        {
            try
            {

                //添加过滤器
                //FilterChain filterChain = new FilterChain();
                //filterChain.addFilter(new QueueFilter());
                //filterChain.filter(new string[] { methodName ,paramList});

                string[] sParameters = null ;
                string sResult = "";
                //LoggerHelper.CreateInstance().Info(typeof(MainFrame), "命令：" + methodName + "；参数：" + paramList + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null);
                if (!string.IsNullOrEmpty(paramList))
                {
                    sParameters = paramList.Split('$');
                }
                else
                {
                    sParameters = new string[] { ""};
                }

                switch (methodName)
                {
                    case "doHelloQueue":
                        sResult = CommonBusiness.CreateInstance().HelloQueue();
                        break;
                    case "getParamValue":
                        sResult = IUserContext.GetParamValue(sParameters[0], sParameters[1]);
                        break;
                    case "doPrintMessage":
                        sResult = CommonBusiness.CreateInstance().doPrintMessage(sParameters[0]);
                        break;
                    case "getImageFrom":
                        sResult = CommonBusiness.CreateInstance().getImageFrom(sParameters[0]);
                        break;
                    case "getHtmlSource":
                        sResult = CommonBusiness.CreateInstance().getHtmlSource(sParameters[0]);
                        break;
                    case "doClearQueueData":
                        DbaseService.CreateInstance().doClearQueueData();
                        break;
                    case "doClearAllData":
                        DbaseService.CreateInstance().doClearAllData();
                        break;

                    case "getDefaultTStyle":
                        sResult = TicketStyleBusiness.CreateInstance().getDefaultTicketStyle();
                        break;
                    case "getNextTicketNo":
                        sResult = ITicketService.CreateInstance().getNextTicketNo(sParameters[0]);
                        break;
                    case "getTransferList":
                        sResult = ITicketService.CreateInstance().getTransferList();
                        break;

                    case "getAllServices":
                        sResult = ServiceInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getServicesByPaging":
                        sResult = ServiceInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getService":
                        sResult = ServiceInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postService":
                        sResult = ServiceInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllCounters":
                        sResult = CounterInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getCountersByPaging":
                        sResult = CounterInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getCounter":
                        sResult = CounterInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postCounter":
                        sResult = CounterInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllStaffers":
                        sResult = StafferInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getStaffersByPaging":
                        sResult = StafferInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getStaffer":
                        sResult = StafferInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postStaffer":
                        sResult = StafferInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllRusers":
                        sResult = RUsersInfoBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getRusersByPaging":
                        sResult = RUsersInfoBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRuser":
                        sResult = RUsersInfoBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRuser":
                        sResult = RUsersInfoBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break; 


                    case "getAllTicketFlows":
                        sResult = TicketFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getTicketFlowsByPaging":
                        sResult = TicketFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getTicketFlow":
                        sResult = TicketFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postTicketFlow":
                        sResult = TicketFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllProcFlows":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getProcFlowsByPaging":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getProcFlow":
                        sResult = ProcessFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postProcFlow":
                        sResult = ProcessFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllEvalFlows":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getEvalFlowsByPaging":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getEvalFlow":
                        sResult = EvaluateFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postEvalFlow":
                        sResult = EvaluateFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllRegistFlows":
                        sResult = RegistFlowsBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getRegistFlowsByPaging":
                        sResult = RegistFlowsBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getRegistFlow":
                        sResult = RegistFlowsBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postRegistFlow":
                        sResult = RegistFlowsBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "getAllTicketStyles":
                        sResult = TicketStyleBusiness.CreateInstance().GetAllRecords();
                        break;
                    case "getTicketStylesByPaging":
                        sResult = TicketStyleBusiness.CreateInstance().GetRecordsByPaging(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "getTicketStyle":
                        sResult = TicketStyleBusiness.CreateInstance().GetRecordByNo(sParameters[0]);
                        break;
                    case "postTicketStyle":
                        sResult = TicketStyleBusiness.CreateInstance().PostRecord(sParameters[0]);
                        break;

                    case "doSignIn":
                        sResult = ITicketService.CreateInstance().doSignIn(sParameters[0], sParameters[1], sParameters[2]);
                        break;
                    case "doSignoutAll":
                        ITicketService.CreateInstance().doSignoutAll();
                        break;
                    case "doSignOut":
                        sResult = ITicketService.CreateInstance().doSignOut(sParameters[0]);
                        break;
                    case "doSeekHelp":
                        sResult = ITicketService.CreateInstance().doSeekHelp(sParameters[0]);
                        break;
                    case "doOutService":
                        sResult = ITicketService.CreateInstance().doOutService(sParameters[0]);
                        break;
                    case "doInService":
                        sResult = ITicketService.CreateInstance().doInService(sParameters[0]);
                        break;
                    case "doCallNextTicket":
                        sResult = ITicketService.CreateInstance().doCallNextTicket(sParameters[0]);
                        break;
                    case "doSpecialTicket":
                        sResult = ITicketService.CreateInstance().doSpecialTicket(sParameters[0], sParameters[1]);
                        break;
                    case "doTransferTicket":
                        sResult = ITicketService.CreateInstance().doTransferTicket(sParameters[0], sParameters[1]);
                        break;
                    case "doRecallTicket":
                        sResult = ITicketService.CreateInstance().doRecallTicket(sParameters[0]);
                        break;
                    case "doStartTicket":
                        sResult = ITicketService.CreateInstance().doStartTicket(sParameters[0]);
                        break;
                    case "doFinishTicket":
                        sResult = ITicketService.CreateInstance().doFinishTicket(sParameters[0]);
                        break; 
                    case "doNotcomeTicket":
                        sResult = ITicketService.CreateInstance().doNotcomeTicket(sParameters[0]);
                        break;
                    case "doDelayTicket":
                        sResult = ITicketService.CreateInstance().doDelayTicket(sParameters[0]);
                        break;
                    case "doAbortTicket":
                        sResult = ITicketService.CreateInstance().doAbortTicket(sParameters[0]);
                        break;
                    case "doAddNewTicket1":
                        sResult = ITicketService.CreateInstance().doAddNewTicket1(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "doAddNewTicket2":
                        sResult = ITicketService.CreateInstance().doAddNewTicket2(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;

                    case "doRegistEvaluator":
                        sResult = EvaluateService.CreateInstance().doRegistEvaluator(sParameters[0], sParameters[1], sParameters[2]);
                        break; 
                    case "doFinishEvalutator":
                        sResult = EvaluateService.CreateInstance().doFinishEvaluator(sParameters[0], sParameters[1], sParameters[2]);
                        break; 

                    case "doEnqueueRegUser":
                        sResult = IRegistService.CreateInstance().doEnqueueRegUser(sParameters[0]);
                        break;


                    case "getCallingCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingCountByCounterNo(sParameters[0]);
                        break;
                    case "getWaitingCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingCountByCounterNo(sParameters[0]);
                        break;
                    case "getCallingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getWaitingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getQueuingListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getQueuingListByCounterNo(sParameters[0], sParameters[1]);
                        break;
                    case "getNonarrivalListByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getNonarrivalListByCounterNo(sParameters[0], sParameters[1]);
                        break;


                    case "getCallingCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingCountByServiceNo(sParameters[0]);
                        break;
                    case "getWaitingCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingCountByServiceNo(sParameters[0]);
                        break;
                    case "getCallingListByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getCallingListByServiceNo(sParameters[0], sParameters[1]);
                        break;
                    case "getWaitingListByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getWaitingListByServiceNo(sParameters[0], sParameters[1]);
                        break;

                    case "getTicketFlowByTFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getTicketFlowByTFlowNo(sParameters[0]);
                        break; 
                    case "getVTicketFlowByPFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByPFlowNo(sParameters[0]);
                        break;
                    case "getVTicketFlowsByClassNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowsByClassNo(sParameters[0]);
                        break;
                    case "getVTicketFlowsByPaging":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowsByPaging(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketFlowByTicketNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByTicketNo(sParameters[0]);
                        break;
                    case "getVTicketFlowByCardNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByCardNo(sParameters[0]);
                        break;
                    case "getVTicketFlowByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByCounterNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketFlowByStafferNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketFlowByStafferNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3], sParameters[4]);
                        break;
                    case "getVTicketCountByCondition":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByCondition(sParameters[0]);
                        break;
                    case "getVTicketCountByCounterNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByCounterNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "getVTicketCountByServiceNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByServiceNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break;
                    case "getVTicketCountByWFlowNo":
                        sResult = TicketStatisticsBusiness.CreateInstance().getVTicketCountByWFlowNo(sParameters[0], sParameters[1], sParameters[2], sParameters[3]);
                        break; 
                    default:
                        break;
                }
                return sResult;
            }
            catch(Exception ex)
            {
                LoggerHelper.CreateInstance().Error(typeof(MainFrame), ex.Message + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex);
                return "";
            }
        }
    }
}
