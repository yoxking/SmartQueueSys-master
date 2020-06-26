using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.DataAdapter.Business;
using EntFrm.DataAdapter.Entities;
using EntFrm.Framework.Utility;
using FluentScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EntFrm.DataAdapter.Services
{
    public class ScheduleService
    {
        private volatile static ScheduleService _instance = null;
        private static readonly object lockHelper = new object(); 

        public static ScheduleService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ScheduleService();
                }
            }
            return _instance;
        }

        private ScheduleService() { }


        /// <summary>
        /// 启动定时任务
        /// </summary>
        public void StartSchedule()
        {
            try
            {
                JobManager.Initialize(new ScheduleFactory());
            }
            catch(Exception ex) { }
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public void StopSchedule()
        {
            try
            {
                JobManager.Stop();
            }
            catch(Exception ex)
            {

            }
        }
         
        internal class ScheduleFactory : Registry
        {
            public ScheduleFactory()
            {
                Schedule<WipeSerivceJob>().ToRunNow().AndEvery(30).Minutes(); //立即执行每30分钟一次的计划任务 

                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetAllRecords();

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] timeAt = quartz.sCornExp.Split(':');

                        int hours = int.Parse(timeAt[0]);
                        int minutes = int.Parse(timeAt[1]);
                        //int seconds = int.Parse(timeAt[2]);

                        switch (quartz.sJobTask)
                        {
                            case "Shutdown":
                                Schedule<ShutdownJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            case "Reboot":
                                Schedule<RebootJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            case "PowerOnOff":
                                Schedule<PowerOnOffJob>().ToRunEvery(1).Days().At(hours, minutes);
                                break;
                            default: break;
                        }
                    }
                }

                //Schedule<WipeSerivceJob>().ToRunEvery(1).Days().At(13, 00); //在每天的下午 1:00 分执行
                //Schedule<BranchInfoJob>().ToRunEvery(1).Days().At(13, 00); //在每天的下午 1:00 分执行
                //Schedule<ServiceInfoJob>().ToRunEvery(1).Days().At(13, 10); //在每天的下午 1:10 分执行
                //Schedule<CounterInfoJob>().ToRunEvery(1).Days().At(13, 20); //在每天的下午 1:10 分执行 
                //Schedule<StaffInfoJob>().ToRunEvery(1).Days().At(13, 20); //在每天的下午 1:10 分执行 
                //Schedule<StaffRotaJob>().ToRunEvery(1).Days().At(13, 30); //在每天的下午 1:10 分执行 
                //Schedule<ServiceRotaJob>().ToRunEvery(1).Days().At(13, 40); //在每天的下午 1:10 分执行 
                //Schedule<RegisteUserJob>().ToRunNow().AndEvery(20).Seconds(); //立即执行每两秒一次的计划任务

                MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "任务调度服务启动完成...");
            }
        }

        internal class WipeSerivceJob : IJob
        {
            void IJob.Execute()
            {
                IAdapterBusiness adapterBoss=AdapterFactory.Create();
                if (adapterBoss!=null&&adapterBoss.wipeHrtbeatFlows())
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新完成...");
                }
                else
                {
                    MainFrame.PrintMessage(DateTime.Now.ToString("[MM-dd HH:mm:ss] ") + "清除心跳信息更新失败...");
                }
            }
        }

        internal class ShutdownJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " JobTask='Shutdown' And  CornExp Between '"+ dtStart + "' And '"+ dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        command = new CmmdData();
                        command.cmmdName = "doShutdown"; 
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { "" };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes) {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }

        internal class RebootJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " JobTask='Reboot' And  CornExp Between '" + dtStart + "' And '" + dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        command = new CmmdData();
                        command.cmmdName = "doReboot";
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { "" };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes)
                        {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }
        internal class PowerOnOffJob : IJob
        {
            void IJob.Execute()
            {
                int count = 0;
                CmmdData command = null;
                string s = "";
                int weekDay = CalendarHelper.GetDayOfWeek(DateTime.Now);
                string dtStart = DateTime.Now.AddMinutes(-1).ToString("HH:mm");
                string dtEndit = DateTime.Now.AddMinutes(1).ToString("HH:mm");
                DsQuartzInfoBLL quartzBoss = new DsQuartzInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例 
                DsQuartzInfoCollections quartzColl = quartzBoss.GetRecordsByPaging(ref count, 1, 10, " WeekDay=" + weekDay + " And  JobTask='PowerOnOff' And  CornExp Between '" + dtStart + "' And '" + dtEndit + "' ");

                if (quartzColl != null && quartzColl.Count > 0)
                {
                    foreach (DsQuartzInfo quartz in quartzColl)
                    {
                        string[] playerCodes = quartz.sPlayerNos.Split(';');
                        string[] powerTime = quartz.sComments.Split(':');
                        command = new CmmdData();
                        command.cmmdName = "doPowerOnOff";
                        command.cmmdType = "MAdapter";
                        command.cmmdArgs = new string[] { DateTime.Now.AddDays(1).ToString("yyyy,MM,dd,") + powerTime[0], DateTime.Now.ToString("yyyy,MM,dd,") + powerTime[1] };

                        s = JsonConvert.SerializeObject(command);

                        foreach (string playerCode in playerCodes)
                        {
                            NettyHostService.CreateInstance().SendCommandData(playerCode, s);
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }
    }
}
