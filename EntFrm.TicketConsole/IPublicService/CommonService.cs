using EntFrm.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.TicketConsole
{
    public class CommonService
    {
        private volatile static CommonService _instance = null;
        private static readonly object lockHelper = new object();

        public static CommonService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new CommonService();
                }
            }
            return _instance;
        }

        private CommonService() { }
        
        public string GetServiceCount(string serviceNo)
        {
            try
            {
                return IUserContext.OnExecuteCommand_Xp("getVTicketCountByServiceNo", new string[] { serviceNo, DateTime.Now.ToString("yyyy-MM-dd"), IPublicConsts.PROCSTATE_WAITING + "", IPublicConsts.PROCSTATE_CALLING + "" });
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public string GetTicketStyle_FormatStr(string sStyleNo)
        {
            string sResult = "";
            string s = IUserContext.OnExecuteCommand_Xp("getTicketStyle", new string[] { sStyleNo });
            TicketStyle info = JsonConvert.DeserializeObject<TicketStyle>(s);

            if (info != null)
            {
                //bResult = System.Text.Encoding.Default.GetBytes(info.sTicketFormat);
                sResult = info.sTicketFormat;
            }

            return sResult;
        }

        public bool IsActiveService(string sServiceNo)
        {
            bool bResult = true;
            string sCount = "0";
            string sCondition = "";

            ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(sServiceNo));

            if (info != null)
            {
                DateTime dtCurrent = DateTime.Now;
                DateTime dtMiddle = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 13:00:00");
                DateTime dAMStartTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dAMStartTime.ToString("HH:mm:ss"));
                DateTime dAMEndTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dAMEndTime.ToString("HH:mm:ss"));
                DateTime dPMStartTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dPMStartTime.ToString("HH:mm:ss"));
                DateTime dPMEndTime = DateTime.Parse(dtCurrent.ToString("yyyy-MM-dd") + " " + info.dPMEndTime.ToString("HH:mm:ss"));


                if (info.iWeekLimit == 1)
                {
                    string weekDay = ((int)dtCurrent.DayOfWeek).ToString();
                    bResult = info.sWeekDays.IndexOf(weekDay) >= 0 ? true : false;
                }

                if (bResult)
                {
                    if (info.iAMLimit == 1 && dtCurrent < dtMiddle)
                    {
                        if (dtCurrent >= dAMStartTime && dtCurrent <= dAMEndTime)
                        {
                            bResult = true;

                            sCondition = "ServiceNo= '" + sServiceNo + "' And   EnqueueTime Between '" + info.dAMStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + info.dAMEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                            sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sCondition });
                            if (info.iAMTotal < int.Parse(sCount))
                            {
                                bResult = false;
                            }
                        }
                        else
                        {
                            bResult = false;
                        }
                    }

                    if (info.iPMLimit == 1 && dtCurrent > dtMiddle)
                    {
                        if (dtCurrent >= dPMStartTime && dtCurrent <= dPMEndTime)
                        {
                            bResult = true;

                            sCondition = "ServiceNo= '" + sServiceNo + "' And   EnqueueTime Between '" + info.dPMStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' And '" + info.dPMEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                            sCount = IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sCondition });
                            if (info.iPMTotal < int.Parse(sCount))
                            {
                                bResult = false;
                            }
                        }
                        else
                        {
                            bResult = false;
                        }
                    }
                }

            }

            return bResult;
        }

        public bool IsActiveCardNo(string sServiceNo, string sIdCardNo, ref string sMessage)
        {
            bool bResult = true;
            string sWhere = "";
            int count = 0;

            ServiceInfo info = IPublicHelper.serviceList.Find(p => p.sServiceNo.Equals(sServiceNo));

            if (info != null && (!string.IsNullOrEmpty(info.sComments)))
            {
                string[] stemp = info.sComments.Split(';');
                int nums = int.Parse(stemp[0]);

                sWhere = "CardNo='" + sIdCardNo + "' And QueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";
                count = int.Parse(IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sWhere }));
                if (count + 1 > nums)
                {
                    sMessage = "您已经超过了当天刷卡取票限制的次数（限" + nums + "次）!";
                    return false;
                }

                if (stemp[1].Equals("1"))
                {
                    sWhere = " ProcessStatus=0 And CardNo='" + sIdCardNo + "' And QueueTime Between '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' And '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' ";

                    count = int.Parse(IUserContext.OnExecuteCommand_Xp("getVTicketCountByCondition", new string[] { sWhere }));
                    if (count > 0)
                    {
                        sMessage = "同一个身份证同时只能限取一张票号！";
                        return false;
                    }
                }

                ViewTicketFlows vTicketFlow = JsonConvert.DeserializeObject<ViewTicketFlows>(IUserContext.OnExecuteCommand_Xp("getVTicketFlowByCardNo", new string[] { sIdCardNo }));
                if (vTicketFlow != null)
                {
                    bool bresult = DateTime.Now > vTicketFlow.dEnqueueTime.AddMinutes(int.Parse(stemp[2]));

                    if (!bresult)
                    {
                        sMessage = "您上次取票时间为：" + vTicketFlow.dEnqueueTime.ToString("HH:mm") + ",请间隔" + stemp[2] + "分钟后才能再次取票！";
                        return false;
                    }
                }

            }

            return bResult;
        }
    }
}
